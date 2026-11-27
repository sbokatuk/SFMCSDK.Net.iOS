using SFMCSDK;

namespace SFMCSDK.Net.iOS.Example;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnInitializeClicked(object? sender, EventArgs e)
    {
        InitializeButton.IsEnabled = false;
        AppendLog("Configuring…");

        // Everything below is the raw binding, in the same shapes Salesforce's Swift quick-start
        // uses. With no module configs attached the core initializes standalone.
        SFMCSdk.SetLogger(SFMCSdkLogLevel.Debug, null);

        var config = new SFMCSdkConfigBuilder().Build();

        // The completion block reports one status per configured module - none here - and
        // arrives on a background thread a few milliseconds after the call. It is the only
        // signal that the SDK came up: if it never arrives, the SDK did not initialize, which on
        // a simulator means the app has no keychain access (see Troubleshooting in the README).
        SFMCSdk.InitializeSdk(config, statuses =>
            MainThread.BeginInvokeOnMainThread(() =>
            {
                AppendLog($"Module initialization reported {statuses.Length} status(es).");
                SetStatus($"SDK {SFMCSdk.SdkVersion} initialized.");
                ProfileIdEntry.IsEnabled = true;
                ProfileIdButton.IsEnabled = true;
                TrackButton.IsEnabled = true;
            }));

        SetStatus($"SDK {SFMCSdk.SdkVersion} initializing…");
        AppendLog($"Native SFMCSDK {SFMCSdk.SdkVersion} is up.");
    }

    private void OnSetProfileIdClicked(object? sender, EventArgs e)
    {
        var profileId = ProfileIdEntry.Text?.Trim();
        if (string.IsNullOrEmpty(profileId))
        {
            AppendLog("Enter a profile id first.");
            return;
        }

        // v4 identity edits go through the modifier block; the returned modifier is committed.
        SFMCSdk.Identity.Edit(modifier =>
        {
            modifier.ProfileId = profileId;
            return modifier;
        });
        AppendLog($"Profile id set to '{profileId}'.");
    }

    private void OnTrackClicked(object? sender, EventArgs e)
    {
        var custom = new SFMCSdkCustomEvent(
            "sample_button_tapped",
            new Foundation.NSDictionary<Foundation.NSString, Foundation.NSObject>(
                new Foundation.NSString("source"), new Foundation.NSString("SFMCSDK.Net.iOS.Example")));

        SFMCSdk.Track(custom);
        AppendLog("Tracked 'sample_button_tapped'.");
    }

    private void SetStatus(string status)
    {
        StatusLabel.Text = status;
        Console.WriteLine($"[SFMCSDK.Net.iOS.Example] status: {status}");
    }

    private void AppendLog(string line)
    {
        LogLabel.Text = $"{DateTime.Now:HH:mm:ss}  {line}\n{LogLabel.Text}";

        // Also to stdout, so a run started with `xcrun simctl launch --console-pty` leaves the
        // same lines in a terminal or a log file.
        Console.WriteLine($"[SFMCSDK.Net.iOS.Example] {line}");
    }
}
