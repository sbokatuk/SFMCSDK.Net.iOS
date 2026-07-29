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
        // uses. With no module configs attached the core initializes standalone; the completion
        // block reports per-module statuses, so it stays silent here - the state read below is
        // what shows the SDK came up.
        SFMCSdk.SetLogger(SFMCSdkLogLevel.Debug, null);

        var config = new SFMCSdkConfigBuilder().Build();
        SFMCSdk.InitializeSdk(config, statuses =>
            MainThread.BeginInvokeOnMainThread(() =>
                AppendLog($"Module initialization reported {statuses.Length} status(es).")));

        MainThread.BeginInvokeOnMainThread(() =>
        {
            StatusLabel.Text = $"SDK {SFMCSdk.SdkVersion} initialized.";
            AppendLog($"Native SFMCSDK {SFMCSdk.SdkVersion} is up.");
            ProfileIdEntry.IsEnabled = true;
            ProfileIdButton.IsEnabled = true;
            TrackButton.IsEnabled = true;
        });
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

    private void AppendLog(string line)
        => LogLabel.Text = $"{DateTime.Now:HH:mm:ss}  {line}\n{LogLabel.Text}";
}
