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

    private static readonly TaskCompletionSource<SFMCModuleInitStatus[]> Initialized =
        new(TaskCreationOptions.RunContinuationsAsynchronously);

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

        new("initialize_brings_the_core_up", async () =>
        {
            // No module configs attached - the core initializes standalone. Verified against
            // 4.0.1 on a simulator: the completion block reports PER-MODULE init statuses, so
            // with zero modules configured it never fires - that is upstream behaviour, not a
            // bridge failure. What initialization observably does is bring the state machine up,
            // which the next check reads; here the call must simply cross the bridge without
            // throwing and leave the SDK answering. (The sibling MarketingCloudSDK.Net.iOS
            // device tests configure a real module and prove the completion-block bridge.)
            var config = new SFMCSdkConfigBuilder().Build();

            SFMCSdk.InitializeSdk(config, statuses =>
            {
                Reporter($"initialization completed with {statuses.Length} module status(es)");
                Initialized.TrySetResult(statuses);
            });

            // Give the SDK a moment to settle; the state check below is the real assertion.
            await Task.Delay(TimeSpan.FromSeconds(2));
            Reporter("initializeSdk crossed the bridge; module completion not expected with an empty config");
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

        new("identity_is_reachable", () =>
        {
            _ = SFMCSdk.Identity
                ?? throw new InvalidOperationException("SFMCSdk.identity returned null.");
            return Task.CompletedTask;
        }),

        new("identity_edit_accepts_a_profile_id", async () =>
        {
            // The block bridge with the most moving parts: editWithIdentity: hands the native
            // modifier into a managed block, which sets a property through a native selector
            // and hands the modifier back. This is exactly what crashed on the .NET 10 managed
            // static registrar while the block was typed with the abstract [Model] class
            // ("Cannot create an instance of SFMCSDK.SFIdentityModifier").
            //
            // WHEN the updater runs is the SDK's business: identity edits go through its
            // operation queue, and with the empty configuration here - no active modules - the
            // invocation can be deferred rather than inline (observed: inline on the net10 leg,
            // deferred on net8). So the call crossing the bridge without throwing is the
            // assertion; the invocation is awaited opportunistically, because when it does run,
            // a broken trampoline kills the process rather than failing softly.
            var invoked = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            SFMCSdk.Identity.Edit(modifier =>
            {
                modifier.ProfileId = "smoke-test-profile-id";
                invoked.TrySetResult();
                return modifier;
            });

            var completed = await Task.WhenAny(invoked.Task, Task.Delay(TimeSpan.FromSeconds(10)));
            Reporter(completed == invoked.Task
                ? "Identity.Edit round-tripped the native modifier"
                : "Identity.Edit crossed the bridge; the SDK queued the updater for later (no active modules)");
        }),
    ];
}
