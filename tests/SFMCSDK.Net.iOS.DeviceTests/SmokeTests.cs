using System.Diagnostics;
using Foundation;
using ObjCRuntime;
using Security;
using SFMCSDK;

namespace SFMCSDK.Net.iOS.DeviceTests;

/// <summary>One check: a name and something that throws when the SDK misbehaves.</summary>
public sealed record SmokeTest(string Name, Func<Task> Execute);

/// <summary>
/// Drives the real, packed binding on a real iOS runtime. No credentials: the configuration
/// below carries no module config, so nothing registers anywhere - what is being proven is that
/// the native framework loads, the ObjC surface works, and the block bridges fire.
/// </summary>
public static class SmokeTests
{
    /// <summary>Where progress lines go; the AppDelegate points this at stdout.</summary>
    public static Action<string> Reporter { get; set; } = _ => { };

    /// <summary>
    /// How long initialization may take before it counts as not having happened. Measured on a
    /// simulator: the completion block typically arrives 4-13 ms after the call on 5.0.0 (net8,
    /// net9 and net10) and 6-7 ms on 4.0.1 (net9 and net10; net8 was not measured there). The
    /// budget is for a loaded CI runner, not for the SDK.
    /// </summary>
    private static readonly TimeSpan InitializationBudget = TimeSpan.FromSeconds(30);

    public static readonly SmokeTest[] All =
    [
        new("sdk_version_reads", () =>
        {
            // The cheapest possible native round-trip: a static Swift property crossing the ObjC
            // bridge. A mis-linked framework dies here with a dyld or selector error.
            var version = SFMCSdk.SdkVersion;
            Reporter($"native SFMCSDK version: {version}");
            if (string.IsNullOrWhiteSpace(version))
            {
                throw new InvalidOperationException("SFMCSdk.sdkVersion returned nothing.");
            }

            return Task.CompletedTask;
        }),

        new("keychain_is_usable", () =>
        {
            // A precondition rather than a property of the binding, checked first because every
            // later result depends on it. The SDK stores its keys in the keychain; without one it
            // does not initialize, and says nothing: no completion, no identity edits, a state
            // read that still answers. An app with no entitlements gets errSecMissingEntitlement
            // here on the simulator - see the project file for how this app gets its entitlement.
            // Failing with the status code turns a later, baffling timeout into a one-line cause.
            var probe = new SecRecord(SecKind.GenericPassword)
            {
                Service = "com.sbokatuk.sfmcsdknet.devicetests.probe",
                Account = "probe",
            };

            SecKeyChain.Remove(probe);
            var status = SecKeyChain.Add(new SecRecord(SecKind.GenericPassword)
            {
                Service = probe.Service,
                Account = probe.Account,
                ValueData = NSData.FromString("probe"),
            });
            SecKeyChain.Remove(probe);

            Reporter($"keychain: SecItemAdd -> {status} ({(int)status})");
            if (status != SecStatusCode.Success)
            {
                throw new InvalidOperationException(
                    $"The keychain is not usable (SecItemAdd returned {status}, {(int)status}), so the SDK " +
                    "cannot initialize. On a simulator this means the app was built without entitlements.");
            }

            return Task.CompletedTask;
        }),

        new("initialize_completes_with_no_module_statuses", async () =>
        {
            // No module configs attached - the core initializes standalone, and the completion
            // block reports one status per configured module, so here it reports none. It does
            // fire: once per call, a few milliseconds after initializeSdk returns, on a
            // background thread, with an empty array. Measured on 5.0.0 and, with the same code,
            // on the published 4.0.1.2 package.
            //
            // This check used to say the opposite - "with zero modules the completion never
            // fires" - and awaited nothing. That was the missing keychain above, seen on the net8
            // and net9 legs and mistaken for upstream behaviour.
            var initialized = new TaskCompletionSource<SFMCModuleInitStatus[]>(
                TaskCreationOptions.RunContinuationsAsynchronously);
            var clock = Stopwatch.StartNew();

            var config = new SFMCSdkConfigBuilder().Build();
            SFMCSdk.InitializeSdk(config, statuses =>
            {
                Reporter($"initialization completed after {clock.ElapsedMilliseconds} ms with " +
                         $"{statuses.Length} module status(es), main thread: {NSThread.IsMain}");
                initialized.TrySetResult(statuses);
            });

            var first = await Task.WhenAny(initialized.Task, Task.Delay(InitializationBudget));
            if (first != initialized.Task)
            {
                throw new TimeoutException(
                    $"initializeSdk's completion block did not fire within {InitializationBudget.TotalSeconds:0} s.");
            }

            var reported = await initialized.Task;
            if (reported.Length != 0)
            {
                throw new InvalidOperationException(
                    $"An empty configuration reported {reported.Length} module status(es); expected none.");
            }
        }),

        new("state_is_reported", () =>
        {
            var state = SFMCSdk.State;
            Reporter($"SDK state: {state}");
            if (string.IsNullOrWhiteSpace(state))
            {
                throw new InvalidOperationException("SFMCSdk.state returned nothing.");
            }

            return Task.CompletedTask;
        }),

        new("module_status_is_reported", () =>
        {
            // getStatusWithModule: is new in 5.0.0. Asked about every module the enum knows -
            // InboxFeature, new in 5.0.0, included - so both the selector and the enum's raw
            // values cross the bridge. Nothing is configured, so every module is inactive.
            foreach (var module in Enum.GetValues<SFMCSdkModuleName>())
            {
                var status = SFMCSdk.GetStatus(module);
                if (status != SFMCSdkModuleStatus.Inactive)
                {
                    throw new InvalidOperationException(
                        $"{module} reports {status} although no module was configured.");
                }
            }

            Reporter($"every module reports {SFMCSdkModuleStatus.Inactive}");
            return Task.CompletedTask;
        }),

        new("identity_is_reachable", () =>
        {
            _ = SFMCSdk.Identity
                ?? throw new InvalidOperationException("SFMCSdk.identity returned null.");
            return Task.CompletedTask;
        }),

        new("identity_edit_runs_the_updater", async () =>
        {
            // The block bridge with the most moving parts: editWithIdentity: hands the native
            // modifier into a managed block, which sets a property through a native selector
            // and hands the modifier back. This is exactly what crashed on the .NET 10 managed
            // static registrar while the block was typed with the abstract [Model] class
            // ("Cannot create an instance of SFMCSDK.SFIdentityModifier").
            //
            // On an initialized SDK the updater runs inline, before Edit returns (measured on
            // net9 and net10). It used to be awaited "opportunistically" because the net8 leg
            // never ran it - the uninitialized SDK again, not a scheduling difference.
            var invoked = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            SFMCSdk.Identity.Edit(modifier =>
            {
                modifier.ProfileId = "smoke-test-profile-id";
                invoked.TrySetResult();
                return modifier;
            });

            var inline = invoked.Task.IsCompleted;
            var completed = await Task.WhenAny(invoked.Task, Task.Delay(TimeSpan.FromSeconds(10)));
            if (completed != invoked.Task)
            {
                throw new TimeoutException("Identity.Edit never ran the updater.");
            }

            Reporter($"Identity.Edit round-tripped the native modifier ({(inline ? "inline" : "deferred")})");
        }),

        new("event_categories_match_the_native_values", () =>
        {
            // 5.0.0 inserted InboxFeature at 6 and moved FlushEvents to 7. A binding still on the
            // 4.x numbering would read a FlushEvent's category as InboxFeature, so ask the
            // native objects what they are.
            var flush = new SFMCSdkFlushEvent();
            Expect(flush.Category == SFMCSdkEventCategory.FlushEvents,
                $"FlushEvent.category is {flush.Category} ({(long)flush.Category})");

            var request = new SFMCSdkInboxStatusUpdateRequestEvent();
            Expect(request.Category == SFMCSdkEventCategory.InboxFeature,
                $"InboxStatusUpdateRequestEvent.category is {request.Category} ({(long)request.Category})");

            Reporter($"FlushEvents = {(long)flush.Category}, InboxFeature = {(long)request.Category}");
            return Task.CompletedTask;
        }),

        new("inbox_surface_round_trips", () =>
        {
            // The Inbox surface is new in 5.0.0 and hand-ported from the header. Nothing here
            // talks to a server: each object is built through its initializer and read back, so a
            // mistyped selector, argument order or enum value fails on the spot.
            var button = new SFMCSdkInboxMessageInteraction(SFMCSdkInboxInteractionType.Button, "button-1");
            Expect(button.Type == SFMCSdkInboxInteractionType.Button && button.Id == "button-1",
                "InboxMessageInteraction did not round-trip a button");
            var cta = new SFMCSdkInboxMessageInteraction(SFMCSdkInboxInteractionType.Cta, null);
            Expect(cta.Type == SFMCSdkInboxInteractionType.Cta && cta.Id is null,
                "InboxMessageInteraction did not round-trip a call to action");

            var analytics = new SFMCSdkInboxAnalyticsEvent("message-1", "request-1", SFMCInboxAnalyticsType.Open);
            Expect(analytics.MessageId == "message-1" && analytics.RequestId == "request-1" &&
                   analytics.AnalyticsType == SFMCInboxAnalyticsType.Open &&
                   analytics.Category == SFMCSdkEventCategory.InboxFeature &&
                   !string.IsNullOrEmpty(analytics.Id) && !string.IsNullOrEmpty(analytics.Name),
                "InboxAnalyticsEvent did not round-trip");

            var clicked = new SFMCSdkInboxInteractionAnalyticsEvent("message-1", null, "button", "button-1", "Open");
            Expect(clicked.ClickedObjectType == "button" && clicked.ClickedObjectId == "button-1" &&
                   clicked.ClickedObjectText == "Open" && clicked.RequestId is null &&
                   clicked.AnalyticsType == SFMCInboxAnalyticsType.Interacted,
                "InboxInteractionAnalyticsEvent did not round-trip");

            var message = new NSDictionary<NSString, NSObject>(new NSString("id"), new NSString("message-1"));
            var data = new SFMCSdkInboxDataEvent([message], "watermark", "user", SFMCSdkModuleName.Engagement);
            Expect(data.Messages?.Length == 1 && data.Watermark == "watermark" && data.UserId == "user" &&
                   data.Source == SFMCSdkModuleName.Engagement,
                "InboxDataEvent did not round-trip");
            var empty = new SFMCSdkInboxDataEvent(null, null, null, SFMCSdkModuleName.InboxFeature);
            Expect(empty.Messages is null && empty.Watermark is null && empty.UserId is null &&
                   empty.Source == SFMCSdkModuleName.InboxFeature,
                "InboxDataEvent did not round-trip its nullable arguments");

            var failed = new SFMCSdkInboxDataFailedEvent(
                SFMCSdkInboxDataFailedReason.ThrottleBlock, SFMCSdkModuleName.MobileAppMessaging);
            Expect(failed.Reason == SFMCSdkInboxDataFailedReason.ThrottleBlock &&
                   failed.Source == SFMCSdkModuleName.MobileAppMessaging,
                "InboxDataFailedEvent did not round-trip");

            var processed = new SFMCSdkInboxDataProcessedEvent("watermark", "user", SFMCSdkModuleName.InboxFeature);
            Expect(processed.Watermark == "watermark" && processed.UserId == "user" &&
                   processed.Source == SFMCSdkModuleName.InboxFeature,
                "InboxDataProcessedEvent did not round-trip");

            var requested = new SFMCSdkInboxDataRequestEvent(SFMCSdkInboxDataRequestTrigger.UserRefresh);
            Expect(requested.Trigger == SFMCSdkInboxDataRequestTrigger.UserRefresh,
                "InboxDataRequestEvent did not round-trip");

            var migrated = new SFMCSdkInboxMigrationCompleted(SFMCSdkModuleName.InboxFeature);
            Expect(migrated.Source == SFMCSdkModuleName.InboxFeature, "InboxMigrationCompleted did not round-trip");
            var migration = new SFMCSdkInboxMigrationDataEvent([message], SFMCSdkModuleName.Engagement);
            Expect(migration.Messages?.Length == 1 && migration.Source == SFMCSdkModuleName.Engagement,
                "InboxMigrationDataEvent did not round-trip");

            var updates = new SFMCSdkInboxStatusUpdateEvent([message], SFMCSdkModuleName.InboxFeature);
            Expect(updates.StatusUpdates.Length == 1 && updates.Source == SFMCSdkModuleName.InboxFeature,
                "InboxStatusUpdateEvent did not round-trip");
            var actions = new NSDictionary<NSString, NSString>(new NSString("message-1"), new NSString("read"));
            var acknowledged = new SFMCSdkInboxStatusUpdateSuccessEvent(actions, SFMCSdkModuleName.Engagement);
            Expect(acknowledged.MessageActions.Count == 1 && acknowledged.Source == SFMCSdkModuleName.Engagement,
                "InboxStatusUpdateSuccessEvent did not round-trip");

            // The privacy events: the flags became readable and writable ObjC properties in
            // 5.0.0, and both subclasses now expose their source.
            var privacy = new SFMCSdkInboxPrivacyStatusChangeEvent(
                "privacy", SFMCSdkModuleName.Engagement, true, false, null);
            Expect(privacy.IsBlocked && !privacy.ShouldDeleteData && privacy.Source == SFMCSdkModuleName.Engagement,
                "InboxPrivacyStatusChangeEvent did not round-trip");
            privacy.IsBlocked = false;
            privacy.ShouldDeleteData = true;
            Expect(!privacy.IsBlocked && privacy.ShouldDeleteData,
                "PrivacyStatusChangeEvent's flags are not writable");
            var inAppPrivacy = new SFMCSdkInAppPrivacyStatusChangeEvent(
                "privacy", SFMCSdkModuleName.MobileAppMessaging, false, true, null);
            Expect(inAppPrivacy.Source == SFMCSdkModuleName.MobileAppMessaging,
                "InAppPrivacyStatusChangeEvent.source did not round-trip");

            // InAppMessageCloseAction had no ObjC members before 5.0.0.
            var close = new SFMCSdkInAppMessageCloseAction(SFMCSdkInAppMessageDismissReason.Button, "close-1");
            Expect(close.DismissReason == SFMCSdkInAppMessageDismissReason.Button && close.Id == "close-1",
                "InAppMessageCloseAction did not round-trip");

            Reporter("the Inbox, privacy and close-action objects round-tripped");
            return Task.CompletedTask;
        }),

        new("string_constants_read", () =>
        {
            // The constant holders are classes with static string properties. Reading every one
            // proves each class is registered and each getter selector exists.
            string[] constants =
            [
                SFMCSdkInboxActionType.None, SFMCSdkInboxActionType.WebUrl, SFMCSdkInboxActionType.AppUrl,
                SFMCSdkInboxActionType.LandingPage, SFMCSdkInboxActionType.Clipboard, SFMCSdkInboxActionType.Other,
                SFMCSdkInboxAudienceType.Device, SFMCSdkInboxAudienceType.User,
                SFMCSdkInboxMediaType.Image, SFMCSdkInboxMediaType.Gif, SFMCSdkInboxMediaType.Video,
                SFMCSdkInboxMediaType.Audio, SFMCSdkInboxMediaType.Other,
                SFMCSdkURLType.CloudPage, SFMCSdkURLType.OpenDirect, SFMCSdkURLType.Action, SFMCSdkURLType.Other,
            ];

            Expect(constants.All(constant => !string.IsNullOrEmpty(constant)), "a string constant read as empty");
            Expect(SFMCSdkBadgeAggregator.Shared is not null, "BadgeAggregator.shared returned null");
            Expect(Class.GetHandle("SFMCSdkAppWillResignActive") != IntPtr.Zero,
                "the AppWillResignActive behaviour class is not in the framework");

            Reporter($"{constants.Length} constants read: {string.Join(" ", constants)}");
            return Task.CompletedTask;
        }),
    ];

    private static void Expect(bool condition, string failure)
    {
        if (!condition)
        {
            throw new InvalidOperationException(failure);
        }
    }
}
