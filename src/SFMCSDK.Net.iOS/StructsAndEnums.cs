//
// StructsAndEnums.cs
//
// Hand-maintained binding for SFMCSDK (sfmc-sdk-ios) 4.0.1.
// Updated by diffing SFMCSDK-Swift.h between releases (1.1.3 -> 4.0.1).
// Do NOT regenerate with Objective Sharpie; edit by hand against the header diff.
//

using ObjCRuntime;

namespace SFMCSDK
{
	[Native]
	public enum SFKeychainItemExceptionErrorCode : ulong
	{
		SFKeychainItemExceptionKeychainInaccessible = 1
	}

	[Native]
	public enum SFMCInAppMessagingAnalyticsEventType : long
	{
		DisplayCompleted = 0,
		Downloaded = 1,
		Triggered = 2,
		ValidationError = 3,
		Presented = 4
	}

	[Native]
	public enum SFMCPNContextDestination : long
	{
		None = 0,
		Mce = 1,
		Mam = 2,
		DataCloud = 4
	}

	[Native]
	public enum SFMCPNContextSource : long
	{
		None = 0,
		Mce = 1,
		Mam = 2
	}

	[Native]
	public enum SFMCPushFeatureEventType : long
	{
		PushToken = 0,
		PushClick = 1,
		PushEnabledInSdk = 2,
		PushEnabledInGlobalSettings = 3,
		QuietPushEnabled = 4,
		PushUrlHandling = 5,
		SilentPush = 6
	}

	[Native]
	public enum SFMCSdkBehaviorType : long
	{
		AppBecameActive = 0,
		AppForegrounded = 1,
		AppBackgrounded = 2,
		AppVersionChanged = 3,
		ScreenEntry = 4,
		AppTerminated = 5
	}

	[Native]
	public enum SFMCSdkConsent : long
	{
		OptIn = 0,
		OptOut = 1,
		Pending = 2,
		None = 3
	}

	[Native]
	public enum SFMCSdkCustomEventPriority : long
	{
		Low = 0,
		High = 1
	}

	[Native]
	public enum SFMCSDKDataMergePolicy : long
	{
		None = 0,
		Auto = 1,
		Manual = 2
	}

	[Native]
	public enum SFMCSdkEventCategory : long
	{
		Engagement = 0,
		Identity = 1,
		System = 2,
		PushFeature = 3,
		MobileAppMessaging = 4,
		InappMessaging = 5,
		FlushEvents = 6
	}

	[Native]
	public enum SFMCSdkInAppMessageAlignment : long
	{
		Unknown = -1,
		Beginning = 0,
		Center = 1,
		End = 2
	}

	[Native]
	public enum SFMCSdkInAppMessageAnimationType : long
	{
		Unknown = -1,
		Fade = 0,
		SlideTop = 1,
		SlideBottom = 2,
		SlideLeft = 3,
		SlideRight = 4,
		None = 5
	}

	[Native]
	public enum SFMCSdkInAppMessageButtonActionType : long
	{
		Unknown = -1,
		Close = 0,
		Url = 1,
		PushSettings = 2,
		LocationSettings = 3,
		PushPermission = 4
	}

	[Native]
	public enum SFMCSdkInAppMessageButtonConfiguration : long
	{
		Unknown = -1,
		TwoUp = 0,
		Stacked = 1
	}

	[Native]
	public enum SFMCSdkInAppMessageCloseButtonAlignment : long
	{
		Unknown = -1,
		End = 0
	}

	[Native]
	public enum SFMCSdkInAppMessageDismissReason : long
	{
		Auto = 1,
		Button = 2,
		Closed = 3
	}

	[Native]
	public enum SFMCSdkInAppMessageImageContentMode : long
	{
		Unknown = -1,
		Fit = 0,
		Fill = 1
	}

	[Native]
	public enum SFMCSdkInAppMessageImageSize : long
	{
		Unknown = -1,
		E2e = 0,
		Inset = 1
	}

	[Native]
	public enum SFMCSdkInAppMessageLayoutOrder : long
	{
		Unknown = -1,
		TitleImageBody = 0,
		ImageTitleBody = 1
	}

	[Native]
	public enum SFMCSdkInAppMessageSize : long
	{
		Unknown = -1,
		XSmall = 0,
		Small = 1,
		Medium = 2,
		Large = 3,
		XLarge = 4,
		XxLarge = 5
	}

	[Native]
	public enum SFMCSdkInAppMessageType : long
	{
		Unknown = -1,
		BannerTop = 0,
		BannerBottom = 1,
		FullImageFill = 2,
		Full = 3,
		Modal = 4,
		PushPrimer = 5
	}

	[Native]
	public enum SFMCSdkLogLevel : long
	{
		Debug = 0,
		Warn = 1,
		Error = 2,
		Fault = 3,
		None = 4
	}

	[Native]
	public enum SFMCSdkLoggerCategory : long
	{
		Auth = 0,
		Behavior = 1,
		Encryption = 2,
		EventBus = 3,
		Network = 4,
		Module = 5,
		Sdk = 6,
		Storage = 7,
		Consent = 8,
		Coredata = 9,
		Database = 10,
		Event = 11,
		Identity = 12,
		Interface = 13,
		Location = 14,
		Session = 15,
		Util = 16
	}

	[Native]
	public enum SFMCSdkMessageProducer : long
	{
		Cdp = 0,
		Engagement = 1,
		Sdk = 2,
		Personalization = 3,
		PushFeature = 4,
		MobileAppMessaging = 5,
		InappMessagingFeature = 6
	}

	[Native]
	public enum SFMCSdkModuleName : long
	{
		Cdp = 0,
		Engagement = 1,
		Personalization = 2,
		PushFeature = 3,
		MobileAppMessaging = 4,
		InappMessagingFeature = 5
	}

	[Native]
	public enum SFMCSdkModuleStatus : long
	{
		Cancelled = 0,
		Failed = 1,
		Inactive = 2,
		Initializing = 3,
		Operational = 4
	}

	[Native]
	public enum SFMCSdkNetworkManagerError : long
	{
		TokenFetchFailed = 0,
		TokenRefreshFailed = 1,
		TooManyRequests = 2,
		RequestAlreadyInFlight = 3
	}

	[Native]
	public enum SFMCSdkOperationResult : long
	{
		Cancelled = 0,
		Error = 1,
		Success = 2,
		Timeout = 3
	}

	[Native]
	public enum SFMCSdkTraceEventType : long
	{
		Debug = 0
	}
}
