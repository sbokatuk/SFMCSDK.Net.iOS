//
// ApiDefinitions.cs
//
// Hand-maintained binding for SFMCSDK (sfmc-sdk-ios) 4.0.1.
// Updated by diffing SFMCSDK-Swift.h between releases (1.1.3 -> 4.0.1).
// Do NOT regenerate with Objective Sharpie; edit by hand against the header diff.
//

using System;
using Foundation;
using ObjCRuntime;
using UIKit;
using UserNotifications;

namespace SFMCSDK
{
// @interface SFMCEncryptionKey : NSObject <NSCoding, NSCopying>
[BaseType (typeof(NSObject))]
interface SFMCEncryptionKey : INSCoding, INSCopying
{
	// -(id _Nonnull)initWithData:(NSData * _Nonnull)keyData initializationVector:(NSData * _Nullable)iv;
	[Export ("initWithData:initializationVector:")]
	NativeHandle Constructor (NSData keyData, [NullAllowed] NSData iv);

	// +(SFMCEncryptionKey * _Nonnull)createKey;
	[Static]
	[Export ("createKey")]
	SFMCEncryptionKey CreateKey ();

	// -(NSData * _Nullable)encryptData:(NSData * _Nonnull)dataToEncrypt;
	[Export ("encryptData:")]
	[return: NullAllowed]
	NSData EncryptData (NSData dataToEncrypt);

	// -(NSData * _Nullable)decryptData:(NSData * _Nonnull)dataToDecrypt;
	[Export ("decryptData:")]
	[return: NullAllowed]
	NSData DecryptData (NSData dataToDecrypt);

	// @property (copy, nonatomic) NSData * _Nullable key;
	[NullAllowed, Export ("key", ArgumentSemantic.Copy)]
	NSData Key { get; set; }

	// @property (copy, nonatomic) NSData * _Nonnull initializationVector;
	[Export ("initializationVector", ArgumentSemantic.Copy)]
	NSData InitializationVector { get; set; }

	// @property (readonly, nonatomic) NSString * _Nullable keyAsString;
	[NullAllowed, Export ("keyAsString")]
	string KeyAsString { get; }

	// @property (readonly, nonatomic) NSString * _Nullable initializationVectorAsString;
	[NullAllowed, Export ("initializationVectorAsString")]
	string InitializationVectorAsString { get; }
}

// @interface SFMCKeyStoreManager : NSObject
[BaseType (typeof(NSObject))]
interface SFMCKeyStoreManager
{
	// +(instancetype _Nonnull)sharedInstance;
	[Static]
	[Export ("sharedInstance")]
	SFMCKeyStoreManager SharedInstance { get; }

	// -(SFMCEncryptionKey * _Nonnull)retrieveKeyWithLabel:(NSString * _Nonnull)keyLabel autoCreate:(BOOL)create;
	[Export ("retrieveKeyWithLabel:autoCreate:")]
	SFMCEncryptionKey RetrieveKey (string keyLabel, bool create);

	// -(void)storeKey:(SFMCEncryptionKey * _Nonnull)key withLabel:(NSString * _Nonnull)keyLabel;
	[Export ("storeKey:withLabel:")]
	void StoreKey (SFMCEncryptionKey key, string keyLabel);

	// -(void)removeKeyWithLabel:(NSString * _Nonnull)keyLabel;
	[Export ("removeKeyWithLabel:")]
	void RemoveKey (string keyLabel);

	// -(BOOL)keyWithLabelExists:(NSString * _Nonnull)keyLabel;
	[Export ("keyWithLabelExists:")]
	bool KeyExists (string keyLabel);

	// -(BOOL)keyExists:(NSString * _Nonnull)keyLabel keychain:(NSString * _Nonnull)keychain;
	[Export ("keyExists:keychain:")]
	bool KeyExists (string keyLabel, string keychain);

	// -(OSStatus)storeData:(NSData * _Nonnull)data forKey:(NSString * _Nonnull)key keychainID:(NSString * _Nonnull)keychain;
	[Export ("storeData:forKey:keychainID:")]
	int StoreData (NSData data, string key, string keychain);

	// -(NSData * _Nullable)dataForKey:(NSString * _Nonnull)key keychainID:(NSString * _Nonnull)keychain;
	[Export ("dataForKey:keychainID:")]
	[return: NullAllowed]
	NSData DataForKey (string key, string keychain);

	// -(BOOL)removeKeyChainWithID:(NSString * _Nonnull)keychain;
	[Export ("removeKeyChainWithID:")]
	bool RemoveKeyChain (string keychain);
}

// @interface SFMCKeychainItemWrapper : NSObject
[BaseType (typeof(NSObject))]
interface SFMCKeychainItemWrapper
{
	// +(SFMCKeychainItemWrapper * _Nullable)itemWithIdentifier:(NSString * _Nullable)identifier account:(NSString * _Nullable)account;
	[Static]
	[Export ("itemWithIdentifier:account:")]
	[return: NullAllowed]
	SFMCKeychainItemWrapper Item ([NullAllowed] string identifier, [NullAllowed] string account);

	// -(BOOL)resetKeychainItem;
	[Export ("resetKeychainItem")]
	bool ResetKeychainItem ();

	// -(OSStatus)setValueData:(NSData * _Nullable)data;
	[Export ("setValueData:")]
	int SetValueData ([NullAllowed] NSData data);

	// -(NSString * _Nullable)valueString;
	[NullAllowed, Export ("valueString")]
	string ValueString { get; }

	// -(OSStatus)setValueString:(NSString * _Nullable)string;
	[Export ("setValueString:")]
	int SetValueString ([NullAllowed] string value);

	// -(NSData * _Nullable)valueData;
	[NullAllowed, Export ("valueData")]
	NSData ValueData { get; }

	// +(NSString * _Nullable)keychainErrorCodeString:(OSStatus)errorCode;
	[Static]
	[Export ("keychainErrorCodeString:")]
	[return: NullAllowed]
	string KeychainErrorCodeString (int errorCode);

	// +(BOOL)updateKeychainAccessibleAttribute:(CFTypeRef _Nonnull)accessibleAttributeToUpdate;
	[Static]
	[Export ("updateKeychainAccessibleAttribute:")]
	unsafe bool UpdateKeychainAccessibleAttribute (IntPtr accessibleAttributeToUpdate);
}

// @interface  (UIViewController)
[Category]
[BaseType (typeof(UIViewController))]
interface UIViewController_
{
	// +(void)swizzleViewDidAppearForScreenTracking;
	[Static]
	[Export ("swizzleViewDidAppearForScreenTracking")]
	void SwizzleViewDidAppearForScreenTracking ();
}

[Static]
interface Constants
{
	// extern NSString *const _Nullable kSFKeychainItemExceptionType;
	[Field ("kSFKeychainItemExceptionType", "__Internal")]
	[NullAllowed]
	NSString kSFKeychainItemExceptionType { get; }

	// extern NSString *const _Nullable kSFKeychainItemExceptionErrorCodeKey;
	[Field ("kSFKeychainItemExceptionErrorCodeKey", "__Internal")]
	[NullAllowed]
	NSString kSFKeychainItemExceptionErrorCodeKey { get; }

}

interface ISFMCSdkEvent
{
}

// SWIFT_PROTOCOL_NAMED("Event")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkEvent
{
	// @required @property (readonly, copy, nonatomic) NSString * _Nonnull id;
	[Abstract]
	[Export ("id")]
	string Id { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nonnull name;
	[Abstract]
	[Export ("name")]
	string Name { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkEventCategory category;
	[Abstract]
	[Export ("category")]
	SFMCSdkEventCategory Category { get; }

	// @required @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nullable attributes;
	[Abstract]
	[NullAllowed, Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Attributes { get; }
}

// @interface SFMCSdkEngagementEvent : NSObject <SFMCSdkEvent>
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkEngagementEvent : SFMCSdkEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
	[Export ("id")]
	string Id { get; }

	// @property (readonly, nonatomic) enum SFMCSdkEventCategory category;
	[Export ("category")]
	SFMCSdkEventCategory Category { get; }

	// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nullable attributes;
	[NullAllowed, Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Attributes { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
	[Export ("name")]
	string Name { get; }
}

// @interface SFMCSdkCartEvent : SFMCSdkEngagementEvent
[BaseType (typeof(SFMCSdkEngagementEvent))]
interface SFMCSdkCartEvent
{
	// @property (readonly, copy, nonatomic) NSArray<SFMCSdkLineItem *> * _Nonnull lineItems;
	[Export ("lineItems", ArgumentSemantic.Copy)]
	SFMCSdkLineItem[] LineItems { get; }
}

// @interface SFMCSdkAddToCartEvent : SFMCSdkCartEvent
[BaseType (typeof(SFMCSdkCartEvent))]
interface SFMCSdkAddToCartEvent
{
	// -(instancetype _Nonnull)initWithLineItem:(SFMCSdkLineItem * _Nonnull)lineItem __attribute__((objc_designated_initializer));
	[Export ("initWithLineItem:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkLineItem lineItem);
}

// @interface SFMCSdkBehavior : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkBehavior
{
}

// @interface SFMCSdkAppBackgrounded : SFMCSdkBehavior
[BaseType (typeof(SFMCSdkBehavior))]
interface SFMCSdkAppBackgrounded
{
}

// @interface SFMCSdkAppBecameActive : SFMCSdkBehavior
[BaseType (typeof(SFMCSdkBehavior))]
interface SFMCSdkAppBecameActive
{
}

// @interface SFMCSdkAppForegrounded : SFMCSdkBehavior
[BaseType (typeof(SFMCSdkBehavior))]
interface SFMCSdkAppForegrounded
{
}

// @interface SFMCSdkAppTerminated : SFMCSdkBehavior
[BaseType (typeof(SFMCSdkBehavior))]
interface SFMCSdkAppTerminated
{
}

// @interface SFMCSdkAppVersionChanged : SFMCSdkBehavior
[BaseType (typeof(SFMCSdkBehavior))]
interface SFMCSdkAppVersionChanged
{
}

// @interface SFMCSdkAuthHeader : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkAuthHeader
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull key;
	[Export ("key")]
	string Key { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull value;
	[Export ("value")]
	string Value { get; }

	// -(instancetype _Nonnull)initWithKey:(NSString * _Nonnull)key value:(NSString * _Nonnull)value __attribute__((objc_designated_initializer));
	[Export ("initWithKey:value:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string key, string value);
}

// SWIFT_PROTOCOL_NAMED("Authenticator")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkAuthenticator
{
	// @required @property (readonly, nonatomic, strong) NSLock * _Nonnull lock;
	[Abstract]
	[Export ("lock", ArgumentSemantic.Strong)]
	NSLock Lock { get; }

	// @required -(SFMCSdkAuthHeader * _Nullable)getCachedTokenHeader __attribute__((warn_unused_result("")));
	[Abstract]
	[NullAllowed, Export ("getCachedTokenHeader")]
	SFMCSdkAuthHeader CachedTokenHeader { get; }

	// @required -(SFMCSdkAuthHeader * _Nullable)refreshAuthTokenHeader __attribute__((warn_unused_result("")));
	[Abstract]
	[NullAllowed, Export ("refreshAuthTokenHeader")]
	SFMCSdkAuthHeader RefreshAuthTokenHeader ();

	// @required -(void)deleteCachedToken;
	[Abstract]
	[Export ("deleteCachedToken")]
	void DeleteCachedToken ();
}

// @interface SFMCSdkBehaviorManager : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkBehaviorManager
{
}

interface ISFMCSdkBehaviorObserver
{
}

// SWIFT_PROTOCOL_NAMED("BehaviorObserver")
[Protocol]
interface SFMCSdkBehaviorObserver
{
	// @required @property (readonly, copy, nonatomic) NSSet<NSNumber *> * _Nonnull behaviorTypesToObserve;
	[Abstract]
	[Export ("behaviorTypesToObserve", ArgumentSemantic.Copy)]
	NSSet<NSNumber> BehaviorTypesToObserve { get; }

	// @required -(void)onBehaviorWithBehavior:(SFMCSdkBehavior * _Nonnull)behavior;
	[Abstract]
	[Export ("onBehaviorWithBehavior:")]
	void OnBehavior (SFMCSdkBehavior behavior);
}

interface ICdpInterface
{
}

// SWIFT_PROTOCOL("_TtP7SFMCSDK12CdpInterface_")
[Model, Protocol (Name = "_TtP7SFMCSDK12CdpInterface_")]
interface CdpInterface
{
	// @required -(enum SFMCSdkConsent)getConsent __attribute__((warn_unused_result("")));
	[Abstract]
	[Export ("getConsent")]
	SFMCSdkConsent Consent { get; }

	// @required -(void)setConsentWithConsent:(enum SFMCSdkConsent)consent;
	[Abstract]
	[Export ("setConsentWithConsent:")]
	void SetConsent (SFMCSdkConsent consent);

	// @required -(void)setLocationWithCoordinates:(id<SFMCSdkCoordinates> _Nullable)coordinates expiresIn:(NSInteger)expiresIn;
	[Abstract]
	[Export ("setLocationWithCoordinates:expiresIn:")]
	void SetLocation ([NullAllowed] ISFMCSdkCoordinates coordinates, nint expiresIn);

	// @required -(id<SFMCSdkModuleIdentity> _Nullable)getIdentity __attribute__((warn_unused_result("")));
	[Abstract]
	[NullAllowed, Export ("getIdentity")]
	ISFMCSdkModuleIdentity Identity { get; }
}

// @interface SFMCSdkCDP : NSObject <CdpInterface>
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkCDP : CdpInterface
{
	// -(enum SFMCSdkModuleStatus)getStatus __attribute__((warn_unused_result("")));
	[Export ("getStatus")]
	SFMCSdkModuleStatus Status { get; }

	// -(id<SFMCSdkModuleIdentity> _Nullable)getIdentity __attribute__((warn_unused_result("")));
	[NullAllowed, Export ("getIdentity")]
	ISFMCSdkModuleIdentity Identity { get; }

	// -(enum SFMCSdkConsent)getConsent __attribute__((warn_unused_result("")));
	[Export ("getConsent")]
	SFMCSdkConsent Consent { get; }

	// -(void)setConsentWithConsent:(enum SFMCSdkConsent)consent;
	[Export ("setConsentWithConsent:")]
	void SetConsent (SFMCSdkConsent consent);

	// -(void)setLocationWithCoordinates:(id<SFMCSdkCoordinates> _Nullable)coordinates expiresIn:(NSInteger)expiresIn;
	[Export ("setLocationWithCoordinates:expiresIn:")]
	void SetLocation ([NullAllowed] ISFMCSdkCoordinates coordinates, nint expiresIn);
}

// @interface SFMCSdkOrderEvent : SFMCSdkEngagementEvent
[BaseType (typeof(SFMCSdkEngagementEvent))]
interface SFMCSdkOrderEvent
{
	// @property (readonly, nonatomic, strong) SFMCSdkOrder * _Nonnull order;
	[Export ("order", ArgumentSemantic.Strong)]
	SFMCSdkOrder Order { get; }
}

// @interface SFMCSdkCancelOrderEvent : SFMCSdkOrderEvent
[BaseType (typeof(SFMCSdkOrderEvent))]
interface SFMCSdkCancelOrderEvent
{
	// -(instancetype _Nonnull)initWithOrder:(SFMCSdkOrder * _Nonnull)order __attribute__((objc_designated_initializer));
	[Export ("initWithOrder:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkOrder order);
}

// @interface SFMCSdkCatalogObject : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkCatalogObject
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
	[Export ("type")]
	string Type { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
	[Export ("id")]
	string Id { get; }

	// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nonnull attributes;
	[Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Attributes { get; }

	// @property (readonly, copy, nonatomic) NSDictionary<NSString *,NSArray<NSString *> *> * _Nonnull relatedCatalogObjects;
	[Export ("relatedCatalogObjects", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSArray<NSString>> RelatedCatalogObjects { get; }

	// -(instancetype _Nonnull)initWithType:(NSString * _Nonnull)type id:(NSString * _Nonnull)id attributes:(NSDictionary<NSString *,id> * _Nonnull)attributes relatedCatalogObjects:(NSDictionary<NSString *,NSArray<NSString *> *> * _Nonnull)relatedCatalogObjects __attribute__((objc_designated_initializer));
	[Export ("initWithType:id:attributes:relatedCatalogObjects:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string type, string id, NSDictionary<NSString, NSObject> attributes, NSDictionary<NSString, NSArray<NSString>> relatedCatalogObjects);
}

// @interface SFMCSdkCatalogObjectEvent : SFMCSdkEngagementEvent
[BaseType (typeof(SFMCSdkEngagementEvent))]
interface SFMCSdkCatalogObjectEvent
{
	// @property (readonly, nonatomic, strong) SFMCSdkCatalogObject * _Nonnull catalogObject;
	[Export ("catalogObject", ArgumentSemantic.Strong)]
	SFMCSdkCatalogObject CatalogObject { get; }
}

// @interface SFMCSdkCommentCatalogObjectEvent : SFMCSdkCatalogObjectEvent
[BaseType (typeof(SFMCSdkCatalogObjectEvent))]
interface SFMCSdkCommentCatalogObjectEvent
{
	// -(instancetype _Nonnull)initWithCatalogObject:(SFMCSdkCatalogObject * _Nonnull)catalogObject __attribute__((objc_designated_initializer));
	[Export ("initWithCatalogObject:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkCatalogObject catalogObject);
}

// @interface SFMCSdkCompatibility : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkCompatibility
{
}

// @interface SFMCSdkCompletedCall : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkCompletedCall
{
	// @property (readonly, nonatomic, strong) SFMCSdkWrappedRequest * _Nonnull wrappedRequest;
	[Export ("wrappedRequest", ArgumentSemantic.Strong)]
	SFMCSdkWrappedRequest WrappedRequest { get; }

	// @property (readonly, nonatomic, strong) SFMCSdkWrappedResponse * _Nonnull wrappedResponse;
	[Export ("wrappedResponse", ArgumentSemantic.Strong)]
	SFMCSdkWrappedResponse WrappedResponse { get; }

	// -(instancetype _Nonnull)init:(SFMCSdkWrappedRequest * _Nonnull)wrappedRequest :(SFMCSdkWrappedResponse * _Nonnull)wrappedResponse __attribute__((objc_designated_initializer));
	[Export ("init::")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkWrappedRequest wrappedRequest, SFMCSdkWrappedResponse wrappedResponse);
}

// @interface SFMCSdkConfig : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkConfig
{
}

// @interface SFMCSdkConfigBuilder : NSObject
[BaseType (typeof(NSObject))]
interface SFMCSdkConfigBuilder
{
	// -(SFMCSdkConfigBuilder * _Nonnull)setCdpWithConfig:(id<SFMCSdkModuleConfig> _Nonnull)config __attribute__((warn_unused_result("")));
	[Export ("setCdpWithConfig:")]
	SFMCSdkConfigBuilder SetCdp (ISFMCSdkModuleConfig config);

	// -(SFMCSdkConfigBuilder * _Nonnull)setEngagementWithConfig:(id<SFMCSdkModuleConfig> _Nonnull)config __attribute__((warn_unused_result("")));
	[Export ("setEngagementWithConfig:")]
	SFMCSdkConfigBuilder SetEngagement (ISFMCSdkModuleConfig config);

	// -(SFMCSdkConfigBuilder * _Nonnull)setPersonalizationWithConfig:(id<SFMCSdkModuleConfig> _Nonnull)config __attribute__((warn_unused_result("")));
	[Export ("setPersonalizationWithConfig:")]
	SFMCSdkConfigBuilder SetPersonalization (ISFMCSdkModuleConfig config);

	// -(SFMCSdkConfigBuilder * _Nonnull)setPushFeatureWithConfig:(id<SFMCSdkModuleConfig> _Nonnull)config __attribute__((warn_unused_result("")));
	[Export ("setPushFeatureWithConfig:")]
	SFMCSdkConfigBuilder SetPushFeature (ISFMCSdkModuleConfig config);

	// -(SFMCSdkConfigBuilder * _Nonnull)setMAMWithConfig:(id<SFMCSdkModuleConfig> _Nonnull)config __attribute__((warn_unused_result("")));
	[Export ("setMAMWithConfig:")]
	SFMCSdkConfigBuilder SetMAM (ISFMCSdkModuleConfig config);

	// -(SFMCSdkConfigBuilder * _Nonnull)setInAppMessagingFeatureWithConfig:(id<SFMCSdkModuleConfig> _Nonnull)config __attribute__((warn_unused_result("")));
	[Export ("setInAppMessagingFeatureWithConfig:")]
	SFMCSdkConfigBuilder SetInAppMessagingFeature (ISFMCSdkModuleConfig config);

	// -(SFMCSdkConfig * _Nonnull)build __attribute__((warn_unused_result("")));
	[Export ("build")]
	SFMCSdkConfig Build ();
}

interface ISFMCSdkCoordinates
{
}

// SWIFT_PROTOCOL_NAMED("Coordinates")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkCoordinates
{
	// @required @property (readonly, nonatomic) double latitude;
	[Abstract]
	[Export ("latitude")]
	double Latitude { get; }

	// @required @property (readonly, nonatomic) double longitude;
	[Abstract]
	[Export ("longitude")]
	double Longitude { get; }
}

interface ISFMCSdkUserEvent
{
}

// SWIFT_PROTOCOL_NAMED("UserEvent")
// @protocol SFMCSdkUserEvent <SFMCSdkEvent>
[Protocol, Model]
interface SFMCSdkUserEvent : SFMCSdkEvent
{
	// @required @property (readonly, copy, nonatomic) NSString * _Nullable userId;
	[Abstract]
	[NullAllowed, Export ("userId")]
	string UserId { get; }
}

// @interface SFMCSdkCustomEvent : NSObject <SFMCSdkUserEvent>
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkCustomEvent : SFMCSdkUserEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
	[Export ("id")]
	string Id { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
	[Export ("name")]
	string Name { get; }

	// @property (readonly, nonatomic) enum SFMCSdkEventCategory category;
	[Export ("category")]
	SFMCSdkEventCategory Category { get; }

	// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nullable attributes;
	[NullAllowed, Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Attributes { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nullable userId;
	[NullAllowed, Export ("userId")]
	string UserId { get; }

	// @property (readonly, nonatomic) enum SFMCSdkCustomEventPriority priority;
	[Export ("priority")]
	SFMCSdkCustomEventPriority Priority { get; }

	// -(instancetype _Nullable)initWithName:(NSString * _Nonnull)name attributes:(NSDictionary<NSString *,id> * _Nullable)attributes;
	[Export ("initWithName:attributes:")]
	NativeHandle Constructor (string name, [NullAllowed] NSDictionary<NSString, NSObject> attributes);
}

// @interface SFMCSdkDeliverOrderEvent : SFMCSdkOrderEvent
[BaseType (typeof(SFMCSdkOrderEvent))]
interface SFMCSdkDeliverOrderEvent
{
	// -(instancetype _Nonnull)initWithOrder:(SFMCSdkOrder * _Nonnull)order __attribute__((objc_designated_initializer));
	[Export ("initWithOrder:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkOrder order);
}

// @interface SFEncryptedBlobTransformer : NSValueTransformer
[BaseType (typeof(NSValueTransformer))]
[DisableDefaultCtor]
interface SFEncryptedBlobTransformer
{
	// -(id _Nullable)transformedValue:(id _Nullable)value __attribute__((warn_unused_result("")));
	[New]
	[Export ("transformedValue:")]
	[return: NullAllowed]
	NSObject TransformedValue ([NullAllowed] NSObject value);

	// -(id _Nullable)reverseTransformedValue:(id _Nullable)value __attribute__((warn_unused_result("")));
	[New]
	[Export ("reverseTransformedValue:")]
	[return: NullAllowed]
	NSObject ReverseTransformedValue ([NullAllowed] NSObject value);

	// +(BOOL)allowsReverseTransformation __attribute__((warn_unused_result("")));
	[New]
	[Static]
	[Export ("allowsReverseTransformation")]
	bool AllowsReverseTransformation { get; }

	// +(Class _Nonnull)transformedValueClass __attribute__((warn_unused_result("")));
	[New]
	[Static]
	[Export ("transformedValueClass")]
	Class TransformedValueClass { get; }
}

// @interface SFEncryptedStringTransformer : NSValueTransformer
[BaseType (typeof(NSValueTransformer))]
[DisableDefaultCtor]
interface SFEncryptedStringTransformer
{
	// -(id _Nullable)transformedValue:(id _Nullable)value __attribute__((warn_unused_result("")));
	[New]
	[Export ("transformedValue:")]
	[return: NullAllowed]
	NSObject TransformedValue ([NullAllowed] NSObject value);

	// -(id _Nullable)reverseTransformedValue:(id _Nullable)value __attribute__((warn_unused_result("")));
	[New]
	[Export ("reverseTransformedValue:")]
	[return: NullAllowed]
	NSObject ReverseTransformedValue ([NullAllowed] NSObject value);

	// +(BOOL)allowsReverseTransformation __attribute__((warn_unused_result("")));
	[New]
	[Static]
	[Export ("allowsReverseTransformation")]
	bool AllowsReverseTransformation { get; }

	// +(Class _Nonnull)transformedValueClass __attribute__((warn_unused_result("")));
	[New]
	[Static]
	[Export ("transformedValueClass")]
	Class TransformedValueClass { get; }
}

// @interface SFMCSdkEncryptionManager : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkEncryptionManager
{
	// -(NSData * _Nullable)encryptWithString:(NSString * _Nonnull)string __attribute__((warn_unused_result("")));
	[Export ("encryptWithString:")]
	[return: NullAllowed]
	NSData Encrypt (string value);

	// -(NSString * _Nullable)decryptWithStringData:(NSData * _Nonnull)stringData __attribute__((warn_unused_result("")));
	[Export ("decryptWithStringData:")]
	[return: NullAllowed]
	string Decrypt (NSData stringData);
}

// @interface SFMCSdkEventBus : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkEventBus
{
	// +(void)publishWithProducer:(enum SFMCSdkMessageProducer)producer event:(id<SFMCSdkEvent> _Nonnull)event;
	[Static]
	[Export ("publishWithProducer:event:")]
	void Publish (SFMCSdkMessageProducer producer, ISFMCSdkEvent @event);

	// +(void)publishToTarget:(enum SFMCSdkModuleName)target producer:(enum SFMCSdkMessageProducer)producer event:(id<SFMCSdkEvent> _Nonnull)event;
	[Static]
	[Export ("publishToTarget:producer:event:")]
	void PublishToTarget (SFMCSdkModuleName target, SFMCSdkMessageProducer producer, ISFMCSdkEvent @event);

	// +(void)subscribeWithSubscriber:(id<Subscriber> _Nonnull)subscriber;
	[Static]
	[Export ("subscribeWithSubscriber:")]
	void Subscribe (ISubscriber subscriber);

	// +(void)unsubscribeWithSubscriber:(id<Subscriber> _Nonnull)subscriber;
	[Static]
	[Export ("unsubscribeWithSubscriber:")]
	void Unsubscribe (ISubscriber subscriber);
}

// @interface SFMCSdkExchangeOrderEvent : SFMCSdkOrderEvent
[BaseType (typeof(SFMCSdkOrderEvent))]
interface SFMCSdkExchangeOrderEvent
{
	// -(instancetype _Nonnull)initWithOrder:(SFMCSdkOrder * _Nonnull)order __attribute__((objc_designated_initializer));
	[Export ("initWithOrder:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkOrder order);
}

// @interface SFMCSdkFakeEvent : NSObject <SFMCSdkEvent>
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkFakeEvent : SFMCSdkEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
	[Export ("id")]
	string Id { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
	[Export ("name")]
	string Name { get; }

	// @property (readonly, nonatomic) enum SFMCSdkEventCategory category;
	[Export ("category")]
	SFMCSdkEventCategory Category { get; }

	// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nullable attributes;
	[NullAllowed, Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Attributes { get; }

	// -(instancetype _Nullable)initWithName:(NSString * _Nonnull)name __attribute__((objc_designated_initializer));
	[Export ("initWithName:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string name);
}

// @interface SFMCSdkFavoriteCatalogObjectEvent : SFMCSdkCatalogObjectEvent
[BaseType (typeof(SFMCSdkCatalogObjectEvent))]
interface SFMCSdkFavoriteCatalogObjectEvent
{
	// -(instancetype _Nonnull)initWithCatalogObject:(SFMCSdkCatalogObject * _Nonnull)catalogObject __attribute__((objc_designated_initializer));
	[Export ("initWithCatalogObject:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkCatalogObject catalogObject);
}

// SWIFT_CLASS_NAMED("FlushEvents")
// @interface SFMCSdkFlushEvent : NSObject <SFMCSdkEvent>
[BaseType (typeof(NSObject))]
interface SFMCSdkFlushEvent : SFMCSdkEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
	[Export ("id")]
	string Id { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
	[Export ("name")]
	string Name { get; }

	// @property (readonly, nonatomic) enum SFMCSdkEventCategory category;
	[Export ("category")]
	SFMCSdkEventCategory Category { get; }

	// @property (copy, nonatomic) NSDictionary<NSString *,id> * _Nullable attributes;
	[NullAllowed, Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Attributes { get; set; }
}

// SWIFT_CLASS_NAMED("Identity")
// @interface SFIdentity : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFIdentity
{
	// -(NSString * _Nonnull)toJson __attribute__((warn_unused_result("")));
	[Export ("toJson")]
	string ToJson ();

	// -(void)editWithIdentity:(id<SFIdentityModifier> _Nonnull (^ _Nonnull)(id<SFIdentityModifier> _Nonnull))updater;
	// The block is typed with plain NSObject and kept internal; the public
	// Edit (Func<ISFIdentityModifier, ISFIdentityModifier>) lives in SFIdentity.Additions.cs.
	// Neither generated typing works across every band: the .NET 10 binding generator refuses a
	// protocol interface inside a block trampoline (CS1503 in Trampolines.g.cs), and typing the
	// block with the [Model] class makes the .NET 10 managed static registrar try to construct
	// the abstract model class for the native modifier the block receives ("Cannot create an
	// instance of SFMCSDK.SFIdentityModifier because it is an abstract class"). NSObject
	// trampolines on every band; the addition wraps the handle in the protocol wrapper, which
	// calls the native selectors.
	[Internal]
	[Export ("editWithIdentity:")]
	void _Edit (Func<NSObject, NSObject> updater);

	// -(id<SFIdentityInfo> _Nullable)get __attribute__((warn_unused_result("")));
	[Export ("get")]
	[return: NullAllowed]
	ISFIdentityInfo Get ();
}

interface ISFMCSdkIdentityEventProtocol
{
}

// SWIFT_PROTOCOL_NAMED("IdentityEventProtocol")
// @protocol SFMCSdkIdentityEventProtocol <SFMCSdkEvent>
[Protocol, Model]
interface SFMCSdkIdentityEventProtocol : SFMCSdkEvent
{
	// @required @property (readonly, nonatomic, strong) id<SFIdentityEventData> _Nullable identityData;
	[Abstract]
	[NullAllowed, Export ("identityData", ArgumentSemantic.Strong)]
	ISFIdentityEventData IdentityData { get; }
}

// @interface SFMCSdkIdentityEvent : NSObject <SFMCSdkIdentityEventProtocol>
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkIdentityEvent : SFMCSdkIdentityEventProtocol
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
	[Export ("id")]
	string Id { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
	[Export ("name")]
	string Name { get; }

	// @property (readonly, nonatomic) enum SFMCSdkEventCategory category;
	[Export ("category")]
	SFMCSdkEventCategory Category { get; }

	// @property (readonly, nonatomic, strong) id<SFIdentityEventData> _Nullable identityData;
	[NullAllowed, Export ("identityData", ArgumentSemantic.Strong)]
	ISFIdentityEventData IdentityData { get; }

	// @property (copy, nonatomic) NSDictionary<NSString *,id> * _Nullable attributes;
	[NullAllowed, Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Attributes { get; set; }

	// -(instancetype _Nonnull)initWithData:(id<SFIdentityEventData> _Nonnull)data __attribute__((objc_designated_initializer));
	[Export ("initWithData:")]
	[DesignatedInitializer]
	NativeHandle Constructor (ISFIdentityEventData data);

	// -(instancetype _Nonnull)initWithAttributes:(NSDictionary<NSString *,id> * _Nonnull)attributes __attribute__((objc_designated_initializer));
	[Export ("initWithAttributes:")]
	[DesignatedInitializer]
	NativeHandle Constructor (NSDictionary<NSString, NSObject> attributes);
}

interface ISFIdentityEventData
{
}

// SWIFT_PROTOCOL_NAMED("IdentityEventData")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFIdentityEventData
{
	// @required @property (copy, nonatomic) NSString * _Nullable profileId;
	[Abstract]
	[NullAllowed, Export ("profileId")]
	string ProfileId { get; set; }

	// @required @property (copy, nonatomic) NSDictionary<NSString *,NSString *> * _Nonnull attributes;
	[Abstract]
	[Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSString> Attributes { get; set; }

	// @required @property (copy, nonatomic) NSString * _Nullable partyIdentificationNumber;
	[Abstract]
	[NullAllowed, Export ("partyIdentificationNumber")]
	string PartyIdentificationNumber { get; set; }

	// @required @property (copy, nonatomic) NSString * _Nullable partyIdentificationName;
	[Abstract]
	[NullAllowed, Export ("partyIdentificationName")]
	string PartyIdentificationName { get; set; }

	// @required @property (copy, nonatomic) NSString * _Nullable partyIdentificationType;
	[Abstract]
	[NullAllowed, Export ("partyIdentificationType")]
	string PartyIdentificationType { get; set; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nonnull description;
	// NOTE: not bound; satisfied by NSObject.Description (same selector).
}

interface ISFIdentityInfo
{
}

// SWIFT_PROTOCOL_NAMED("IdentityInfo")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFIdentityInfo
{
	// @required @property (readonly, copy, nonatomic) NSString * _Nullable profileId;
	[Abstract]
	[NullAllowed, Export ("profileId")]
	string ProfileId { get; }

	// @required @property (readonly, copy, nonatomic) NSDictionary<NSString *,NSString *> * _Nonnull attributes;
	[Abstract]
	[Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSString> Attributes { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable partyIdentificationNumber;
	[Abstract]
	[NullAllowed, Export ("partyIdentificationNumber")]
	string PartyIdentificationNumber { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable partyIdentificationName;
	[Abstract]
	[NullAllowed, Export ("partyIdentificationName")]
	string PartyIdentificationName { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable partyIdentificationType;
	[Abstract]
	[NullAllowed, Export ("partyIdentificationType")]
	string PartyIdentificationType { get; }
}

interface ISFIdentityModifier
{
}

// SWIFT_PROTOCOL_NAMED("IdentityModifier")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFIdentityModifier
{
	// @required @property (copy, nonatomic) NSString * _Nonnull profileId;
	[Abstract]
	[Export ("profileId")]
	string ProfileId { get; set; }

	// @required @property (readonly, copy, nonatomic) NSDictionary<NSString *,NSString *> * _Nonnull attributes;
	[Abstract]
	[Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSString> Attributes { get; }

	// @required @property (copy, nonatomic) NSString * _Nullable partyIdentificationNumber;
	[Abstract]
	[NullAllowed, Export ("partyIdentificationNumber")]
	string PartyIdentificationNumber { get; set; }

	// @required @property (copy, nonatomic) NSString * _Nullable partyIdentificationName;
	[Abstract]
	[NullAllowed, Export ("partyIdentificationName")]
	string PartyIdentificationName { get; set; }

	// @required @property (copy, nonatomic) NSString * _Nullable partyIdentificationType;
	[Abstract]
	[NullAllowed, Export ("partyIdentificationType")]
	string PartyIdentificationType { get; set; }

	// @required -(void)addAttributeWithKey:(NSString * _Nonnull)key value:(NSString * _Nonnull)value;
	[Abstract]
	[Export ("addAttributeWithKey:value:")]
	void AddAttribute (string key, string value);

	// @required -(void)addAttributesWithAttributes:(NSDictionary<NSString *,NSString *> * _Nonnull)attributes;
	[Abstract]
	[Export ("addAttributesWithAttributes:")]
	void AddAttributes (NSDictionary<NSString, NSString> attributes);

	// @required -(void)clearAttributeWithKey:(NSString * _Nonnull)key;
	[Abstract]
	[Export ("clearAttributeWithKey:")]
	void ClearAttribute (string key);

	// @required -(void)clearAttributesWithKeys:(NSArray<NSString *> * _Nonnull)keys;
	[Abstract]
	[Export ("clearAttributesWithKeys:")]
	void ClearAttributes (string[] keys);

	// @required -(void)clearAllAttributes;
	[Abstract]
	[Export ("clearAllAttributes")]
	void ClearAllAttributes ();
}

interface ISFMCSdkInAppMessageAnimation
{
}

// SWIFT_PROTOCOL_NAMED("InAppMessageAnimation")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkInAppMessageAnimation
{
	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageAnimationType type;
	[Abstract]
	[Export ("type")]
	SFMCSdkInAppMessageAnimationType Type { get; }

	// @required @property (readonly, nonatomic) NSInteger duration;
	[Abstract]
	[Export ("duration")]
	nint Duration { get; }
}

interface ISFMCSdkInAppMessageBorder
{
}

// SWIFT_PROTOCOL_NAMED("InAppMessageBorder")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkInAppMessageBorder
{
	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageSize width;
	[Abstract]
	[Export ("width")]
	SFMCSdkInAppMessageSize Width { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable color;
	[Abstract]
	[NullAllowed, Export ("color")]
	string Color { get; }
}

interface ISFMCSdkInAppMessageButton
{
}

// SWIFT_PROTOCOL_NAMED("InAppMessageButton")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkInAppMessageButton
{
	// @required @property (readonly, copy, nonatomic) NSString * _Nullable id;
	[Abstract]
	[NullAllowed, Export ("id")]
	string Id { get; }

	// @required @property (readonly, nonatomic) NSInteger index;
	[Abstract]
	[Export ("index")]
	nint Index { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable text;
	[Abstract]
	[NullAllowed, Export ("text")]
	string Text { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageAlignment textAlignment;
	[Abstract]
	[Export ("textAlignment")]
	SFMCSdkInAppMessageAlignment TextAlignment { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable backgroundColor;
	[Abstract]
	[NullAllowed, Export ("backgroundColor")]
	string BackgroundColor { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageSize cornerRadius;
	[Abstract]
	[Export ("cornerRadius")]
	SFMCSdkInAppMessageSize CornerRadius { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageButtonActionType actionType;
	[Abstract]
	[Export ("actionType")]
	SFMCSdkInAppMessageButtonActionType ActionType { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable action;
	[Abstract]
	[NullAllowed, Export ("action")]
	string Action { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable fontFamily;
	[Abstract]
	[NullAllowed, Export ("fontFamily")]
	string FontFamily { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageSize fontSize;
	[Abstract]
	[Export ("fontSize")]
	SFMCSdkInAppMessageSize FontSize { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable fontStyle;
	[Abstract]
	[NullAllowed, Export ("fontStyle")]
	string FontStyle { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable fontColor;
	[Abstract]
	[NullAllowed, Export ("fontColor")]
	string FontColor { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageBorder> _Nullable border;
	[Abstract]
	[NullAllowed, Export ("border", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageBorder Border { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageSpacing> _Nullable margin;
	[Abstract]
	[NullAllowed, Export ("margin", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageSpacing Margin { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageSpacing> _Nullable additionalMargin;
	[Abstract]
	[NullAllowed, Export ("additionalMargin", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageSpacing AdditionalMargin { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageShadow> _Nullable shadow;
	[Abstract]
	[NullAllowed, Export ("shadow", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageShadow Shadow { get; }
}

// SWIFT_CLASS_NAMED("InAppMessageCloseAction")
// @interface SFMCSdkInAppMessageCloseAction : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkInAppMessageCloseAction
{
}

interface ISFMCSdkInAppMessageCloseButton
{
}

// SWIFT_PROTOCOL_NAMED("InAppMessageCloseButton")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkInAppMessageCloseButton
{
	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageCloseButtonAlignment alignment;
	[Abstract]
	[Export ("alignment")]
	SFMCSdkInAppMessageCloseButtonAlignment Alignment { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable iconColor;
	[Abstract]
	[NullAllowed, Export ("iconColor")]
	string IconColor { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable backgroundColor;
	[Abstract]
	[NullAllowed, Export ("backgroundColor")]
	string BackgroundColor { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageSpacing> _Nullable margin;
	[Abstract]
	[NullAllowed, Export ("margin", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageSpacing Margin { get; }
}

interface ISFMCSdkInAppMessageDetails
{
}

// SWIFT_PROTOCOL_NAMED("InAppMessageDetails")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkInAppMessageDetails
{
	// @required @property (readonly, copy, nonatomic) NSString * _Nonnull id;
	[Abstract]
	[Export ("id")]
	string Id { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageType type;
	[Abstract]
	[Export ("type")]
	SFMCSdkInAppMessageType Type { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageLayoutOrder layoutOrder;
	[Abstract]
	[Export ("layoutOrder")]
	SFMCSdkInAppMessageLayoutOrder LayoutOrder { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageButtonConfiguration buttonConfiguration;
	[Abstract]
	[Export ("buttonConfiguration")]
	SFMCSdkInAppMessageButtonConfiguration ButtonConfiguration { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageText> _Nullable title;
	[Abstract]
	[NullAllowed, Export ("title", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageText Title { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageText> _Nullable body;
	[Abstract]
	[NullAllowed, Export ("body", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageText Body { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageTertiaryText> _Nullable tertiaryText;
	[Abstract]
	[NullAllowed, Export ("tertiaryText", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageTertiaryText TertiaryText { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageMedia> _Nullable media;
	[Abstract]
	[NullAllowed, Export ("media", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageMedia Media { get; }

	// @required @property (readonly, copy, nonatomic) NSArray<id<SFMCSdkInAppMessageButton>> * _Nullable buttons;
	[Abstract]
	[NullAllowed, Export ("buttons", ArgumentSemantic.Copy)]
	ISFMCSdkInAppMessageButton[] Buttons { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageCloseButton> _Nullable closeButton;
	[Abstract]
	[NullAllowed, Export ("closeButton", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageCloseButton CloseButton { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable backgroundColor;
	[Abstract]
	[NullAllowed, Export ("backgroundColor")]
	string BackgroundColor { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable windowColor;
	[Abstract]
	[NullAllowed, Export ("windowColor")]
	string WindowColor { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageSize cornerRadius;
	[Abstract]
	[Export ("cornerRadius")]
	SFMCSdkInAppMessageSize CornerRadius { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageShadow> _Nullable shadow;
	[Abstract]
	[NullAllowed, Export ("shadow", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageShadow Shadow { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageSpacing> _Nullable additionalMargin;
	[Abstract]
	[NullAllowed, Export ("additionalMargin", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageSpacing AdditionalMargin { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageBorder> _Nullable border;
	[Abstract]
	[NullAllowed, Export ("border", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageBorder Border { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageAnimation> _Nullable animation;
	[Abstract]
	[NullAllowed, Export ("animation", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageAnimation Animation { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable source;
	[Abstract]
	[NullAllowed, Export ("source")]
	string Source { get; }

	// @required @property (readonly, nonatomic) NSInteger displayCount;
	[Abstract]
	[Export ("displayCount")]
	nint DisplayCount { get; }

	// @required @property (readonly, nonatomic) NSInteger displayLimit;
	[Abstract]
	[Export ("displayLimit")]
	nint DisplayLimit { get; }

	// @required @property (readonly, nonatomic) BOOL displayLimitOverride;
	[Abstract]
	[Export ("displayLimitOverride")]
	bool DisplayLimitOverride { get; }

	// @required @property (readonly, copy, nonatomic) NSDate * _Nullable startDateUtc;
	[Abstract]
	[NullAllowed, Export ("startDateUtc", ArgumentSemantic.Copy)]
	NSDate StartDateUtc { get; }

	// @required @property (readonly, copy, nonatomic) NSDate * _Nullable endDateUtc;
	[Abstract]
	[NullAllowed, Export ("endDateUtc", ArgumentSemantic.Copy)]
	NSDate EndDateUtc { get; }

	// @required @property (readonly, copy, nonatomic) NSDate * _Nullable modifiedDateUtc;
	[Abstract]
	[NullAllowed, Export ("modifiedDateUtc", ArgumentSemantic.Copy)]
	NSDate ModifiedDateUtc { get; }

	// @required @property (readonly, nonatomic) NSInteger displayDuration;
	[Abstract]
	[Export ("displayDuration")]
	nint DisplayDuration { get; }

	// @required @property (readonly, nonatomic) NSInteger messageDelaySec;
	[Abstract]
	[Export ("messageDelaySec")]
	nint MessageDelaySec { get; }

	// @required @property (readonly, nonatomic) NSInteger priority;
	[Abstract]
	[Export ("priority")]
	nint Priority { get; }

	// @required @property (readonly, copy, nonatomic) NSArray<NSString *> * _Nullable displaySuppressionAction;
	[Abstract]
	[NullAllowed, Export ("displaySuppressionAction", ArgumentSemantic.Copy)]
	string[] DisplaySuppressionAction { get; }
}

interface ISFMCSdkInAppMessageEventDelegate
{
}

// SWIFT_PROTOCOL_NAMED("InAppMessageEventDelegate")
// @protocol SFMCSdkInAppMessageEventDelegate
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkInAppMessageEventDelegate
{
	// @optional -(BOOL)shouldShowInAppMessage:(id<SFMCSdkInAppMessageDetails> _Nonnull)message __attribute__((warn_unused_result("")));
	[Export ("shouldShowInAppMessage:"), EventArgs("InAppMessageShouldShowInAppMessage")]
	bool ShouldShowInAppMessage (ISFMCSdkInAppMessageDetails message);

	// @optional -(void)didShowInAppMessage:(id<SFMCSdkInAppMessageDetails> _Nonnull)message;
	[Export ("didShowInAppMessage:"), EventArgs("InAppMessageDidShowInAppMessage")]
	void DidShowInAppMessage (ISFMCSdkInAppMessageDetails message);

	// @optional -(void)didCloseInAppMessage:(id<SFMCSdkInAppMessageDetails> _Nonnull)message action:(SFMCSdkInAppMessageCloseAction * _Nonnull)action;
	[Export ("didCloseInAppMessage:action:"), EventArgs("InAppMessageDidCloseInAppMessage")]
	void DidCloseInAppMessage (ISFMCSdkInAppMessageDetails message, SFMCSdkInAppMessageCloseAction action);
}

interface ISFMCSdkInAppMessageMedia
{
}

// SWIFT_PROTOCOL_NAMED("InAppMessageMedia")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkInAppMessageMedia
{
	// @required @property (readonly, copy, nonatomic) NSString * _Nullable url;
	[Abstract]
	[NullAllowed, Export ("url")]
	string Url { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable altText;
	[Abstract]
	[NullAllowed, Export ("altText")]
	string AltText { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageImageSize size;
	[Abstract]
	[Export ("size")]
	SFMCSdkInAppMessageImageSize Size { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageSize cornerRadius;
	[Abstract]
	[Export ("cornerRadius")]
	SFMCSdkInAppMessageSize CornerRadius { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable altMedia;
	[Abstract]
	[NullAllowed, Export ("altMedia")]
	string AltMedia { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageSize insetSize;
	[Abstract]
	[Export ("insetSize")]
	SFMCSdkInAppMessageSize InsetSize { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageBorder> _Nullable border;
	[Abstract]
	[NullAllowed, Export ("border", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageBorder Border { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageShadow> _Nullable shadow;
	[Abstract]
	[NullAllowed, Export ("shadow", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageShadow Shadow { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageSpacing> _Nullable margin;
	[Abstract]
	[NullAllowed, Export ("margin", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageSpacing Margin { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageSpacing> _Nullable additionalMargin;
	[Abstract]
	[NullAllowed, Export ("additionalMargin", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageSpacing AdditionalMargin { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable aspectRatio;
	[Abstract]
	[NullAllowed, Export ("aspectRatio")]
	string AspectRatio { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageImageContentMode contentMode;
	[Abstract]
	[Export ("contentMode")]
	SFMCSdkInAppMessageImageContentMode ContentMode { get; }
}

interface ISFMCSdkInAppMessageShadow
{
}

// SWIFT_PROTOCOL_NAMED("InAppMessageShadow")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkInAppMessageShadow
{
	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageSize length;
	[Abstract]
	[Export ("length")]
	SFMCSdkInAppMessageSize Length { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable color;
	[Abstract]
	[NullAllowed, Export ("color")]
	string Color { get; }
}

interface ISFMCSdkInAppMessageSpacing
{
}

// SWIFT_PROTOCOL_NAMED("InAppMessageSpacing")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkInAppMessageSpacing
{
	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageSize top;
	[Abstract]
	[Export ("top")]
	SFMCSdkInAppMessageSize Top { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageSize bottom;
	[Abstract]
	[Export ("bottom")]
	SFMCSdkInAppMessageSize Bottom { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageSize left;
	[Abstract]
	[Export ("left")]
	SFMCSdkInAppMessageSize Left { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageSize right;
	[Abstract]
	[Export ("right")]
	SFMCSdkInAppMessageSize Right { get; }
}

interface ISFMCSdkInAppMessageTertiaryText
{
}

// SWIFT_PROTOCOL_NAMED("InAppMessageTertiaryText")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkInAppMessageTertiaryText
{
	// @required @property (readonly, copy, nonatomic) NSString * _Nullable text;
	[Abstract]
	[NullAllowed, Export ("text")]
	string Text { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageAlignment alignment;
	[Abstract]
	[Export ("alignment")]
	SFMCSdkInAppMessageAlignment Alignment { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable fontFamily;
	[Abstract]
	[NullAllowed, Export ("fontFamily")]
	string FontFamily { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageSize fontSize;
	[Abstract]
	[Export ("fontSize")]
	SFMCSdkInAppMessageSize FontSize { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable fontStyle;
	[Abstract]
	[NullAllowed, Export ("fontStyle")]
	string FontStyle { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable fontColor;
	[Abstract]
	[NullAllowed, Export ("fontColor")]
	string FontColor { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageSpacing> _Nullable margin;
	[Abstract]
	[NullAllowed, Export ("margin", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageSpacing Margin { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageSpacing> _Nullable additionalMargin;
	[Abstract]
	[NullAllowed, Export ("additionalMargin", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageSpacing AdditionalMargin { get; }

	// @required @property (readonly, copy, nonatomic) NSArray<NSString *> * _Nullable hyperlinks;
	[Abstract]
	[NullAllowed, Export ("hyperlinks", ArgumentSemantic.Copy)]
	string[] Hyperlinks { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable linkColor;
	[Abstract]
	[NullAllowed, Export ("linkColor")]
	string LinkColor { get; }
}

interface ISFMCSdkInAppMessageText
{
}

// SWIFT_PROTOCOL_NAMED("InAppMessageText")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkInAppMessageText
{
	// @required @property (readonly, copy, nonatomic) NSString * _Nullable text;
	[Abstract]
	[NullAllowed, Export ("text")]
	string Text { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageAlignment alignment;
	[Abstract]
	[Export ("alignment")]
	SFMCSdkInAppMessageAlignment Alignment { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable fontFamily;
	[Abstract]
	[NullAllowed, Export ("fontFamily")]
	string FontFamily { get; }

	// @required @property (readonly, nonatomic) enum SFMCSdkInAppMessageSize fontSize;
	[Abstract]
	[Export ("fontSize")]
	SFMCSdkInAppMessageSize FontSize { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable fontStyle;
	[Abstract]
	[NullAllowed, Export ("fontStyle")]
	string FontStyle { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable fontColor;
	[Abstract]
	[NullAllowed, Export ("fontColor")]
	string FontColor { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageSpacing> _Nullable margin;
	[Abstract]
	[NullAllowed, Export ("margin", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageSpacing Margin { get; }

	// @required @property (readonly, nonatomic, strong) id<SFMCSdkInAppMessageSpacing> _Nullable additionalMargin;
	[Abstract]
	[NullAllowed, Export ("additionalMargin", ArgumentSemantic.Strong)]
	ISFMCSdkInAppMessageSpacing AdditionalMargin { get; }
}

// SWIFT_CLASS_NAMED("InAppMessagingEvent")
// @interface SFMCSdkInAppMessagingEvent : NSObject <SFMCSdkEvent>
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkInAppMessagingEvent : SFMCSdkEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
	[Export ("id")]
	string Id { get; }

	// @property (readonly, nonatomic) enum SFMCSdkEventCategory category;
	[Export ("category")]
	SFMCSdkEventCategory Category { get; }

	// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nullable attributes;
	[NullAllowed, Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Attributes { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
	[Export ("name")]
	string Name { get; }
}

// SWIFT_CLASS_NAMED("InAppMessagingAnalyticsEvent")
// @interface SFMCSdkInAppMessagingAnalyticsEvent : SFMCSdkInAppMessagingEvent
[BaseType (typeof(SFMCSdkInAppMessagingEvent))]
[DisableDefaultCtor]
interface SFMCSdkInAppMessagingAnalyticsEvent
{
	// @property (readonly, nonatomic) enum SFMCInAppMessagingAnalyticsEventType eventType;
	[Export ("eventType")]
	SFMCInAppMessagingAnalyticsEventType EventType { get; }

	// -(instancetype _Nonnull)initWithType:(enum SFMCInAppMessagingAnalyticsEventType)type __attribute__((objc_designated_initializer));
	[Export ("initWithType:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCInAppMessagingAnalyticsEventType type);
}

// SWIFT_CLASS_NAMED("InAppMessagingConfigEvent")
// @interface SFMCSdkInAppMessagingConfigEvent : SFMCSdkInAppMessagingEvent
[BaseType (typeof(SFMCSdkInAppMessagingEvent))]
[DisableDefaultCtor]
interface SFMCSdkInAppMessagingConfigEvent
{
	// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nullable configuration;
	[NullAllowed, Export ("configuration", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Configuration { get; }

	// @property (readonly, nonatomic) enum SFMCSdkModuleName source;
	[Export ("source")]
	SFMCSdkModuleName Source { get; }

	// -(instancetype _Nonnull)initWithConfiguration:(NSDictionary<NSString *,id> * _Nullable)configuration source:(enum SFMCSdkModuleName)source __attribute__((objc_designated_initializer));
	[Export ("initWithConfiguration:source:")]
	[DesignatedInitializer]
	NativeHandle Constructor ([NullAllowed] NSDictionary<NSString, NSObject> configuration, SFMCSdkModuleName source);
}

// SWIFT_CLASS_NAMED("InAppMessagingDataEvent")
// @interface SFMCSdkInAppMessagingDataEvent : SFMCSdkInAppMessagingEvent
[BaseType (typeof(SFMCSdkInAppMessagingEvent))]
[DisableDefaultCtor]
interface SFMCSdkInAppMessagingDataEvent
{
	// @property (readonly, copy, nonatomic) NSArray<NSDictionary<NSString *,id> *> * _Nullable triggers;
	[NullAllowed, Export ("triggers", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject>[] Triggers { get; }

	// @property (readonly, copy, nonatomic) NSArray<NSDictionary<NSString *,id> *> * _Nullable messages;
	[NullAllowed, Export ("messages", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject>[] Messages { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nullable watermark;
	[NullAllowed, Export ("watermark")]
	string Watermark { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nullable userId;
	[NullAllowed, Export ("userId")]
	string UserId { get; }

	// @property (readonly, nonatomic) enum SFMCSdkModuleName source;
	[Export ("source")]
	SFMCSdkModuleName Source { get; }

	// -(instancetype _Nonnull)initWithTriggers:(NSArray<NSDictionary<NSString *,id> *> * _Nullable)triggers messages:(NSArray<NSDictionary<NSString *,id> *> * _Nullable)messages watermark:(NSString * _Nullable)watermark userId:(NSString * _Nullable)userId source:(enum SFMCSdkModuleName)source __attribute__((objc_designated_initializer));
	[Export ("initWithTriggers:messages:watermark:userId:source:")]
	[DesignatedInitializer]
	NativeHandle Constructor ([NullAllowed] NSDictionary<NSString, NSObject>[] triggers, [NullAllowed] NSDictionary<NSString, NSObject>[] messages, [NullAllowed] string watermark, [NullAllowed] string userId, SFMCSdkModuleName source);
}

// SWIFT_CLASS_NAMED("InAppMessagingDataProcessedEvent")
// @interface SFMCSdkInAppMessagingDataProcessedEvent : SFMCSdkInAppMessagingEvent
[BaseType (typeof(SFMCSdkInAppMessagingEvent))]
[DisableDefaultCtor]
interface SFMCSdkInAppMessagingDataProcessedEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nullable watermark;
	[NullAllowed, Export ("watermark")]
	string Watermark { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nullable userId;
	[NullAllowed, Export ("userId")]
	string UserId { get; }

	// @property (readonly, nonatomic) enum SFMCSdkModuleName source;
	[Export ("source")]
	SFMCSdkModuleName Source { get; }

	// -(instancetype _Nonnull)initWithWatermark:(NSString * _Nullable)watermark userId:(NSString * _Nullable)userId source:(enum SFMCSdkModuleName)source __attribute__((objc_designated_initializer));
	[Export ("initWithWatermark:userId:source:")]
	[DesignatedInitializer]
	NativeHandle Constructor ([NullAllowed] string watermark, [NullAllowed] string userId, SFMCSdkModuleName source);
}

// SWIFT_CLASS_NAMED("InAppMessagingDisplayCompletedEvent")
// @interface SFMCSdkInAppMessagingDisplayCompletedEvent : SFMCSdkInAppMessagingAnalyticsEvent
[BaseType (typeof(SFMCSdkInAppMessagingAnalyticsEvent))]
[DisableDefaultCtor]
interface SFMCSdkInAppMessagingDisplayCompletedEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull messageId;
	[Export ("messageId")]
	string MessageId { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull activityInstanceId;
	[Export ("activityInstanceId")]
	string ActivityInstanceId { get; }

	// @property (readonly, nonatomic) enum SFMCSdkInAppMessageDismissReason dismissReason;
	[Export ("dismissReason")]
	SFMCSdkInAppMessageDismissReason DismissReason { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull duration;
	[Export ("duration")]
	string Duration { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nullable buttonId;
	[NullAllowed, Export ("buttonId")]
	string ButtonId { get; }

	// -(instancetype _Nonnull)initWithMessageId:(NSString * _Nonnull)messageId activityInstanceId:(NSString * _Nonnull)activityInstanceId dismissReason:(enum SFMCSdkInAppMessageDismissReason)dismissReason duration:(NSString * _Nonnull)duration buttonId:(NSString * _Nullable)buttonId __attribute__((objc_designated_initializer));
	[Export ("initWithMessageId:activityInstanceId:dismissReason:duration:buttonId:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string messageId, string activityInstanceId, SFMCSdkInAppMessageDismissReason dismissReason, string duration, [NullAllowed] string buttonId);
}

// SWIFT_CLASS_NAMED("InAppMessagingDownloadEvent")
// @interface SFMCSdkInAppMessagingDownloadEvent : SFMCSdkInAppMessagingAnalyticsEvent
[BaseType (typeof(SFMCSdkInAppMessagingAnalyticsEvent))]
[DisableDefaultCtor]
interface SFMCSdkInAppMessagingDownloadEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull messageId;
	[Export ("messageId")]
	string MessageId { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull activityInstanceId;
	[Export ("activityInstanceId")]
	string ActivityInstanceId { get; }

	// -(instancetype _Nonnull)initWithMessageId:(NSString * _Nonnull)messageId activityInstanceId:(NSString * _Nonnull)activityInstanceId __attribute__((objc_designated_initializer));
	[Export ("initWithMessageId:activityInstanceId:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string messageId, string activityInstanceId);
}

interface IInAppMessagingFeatureProtocol
{
}

// SWIFT_PROTOCOL("_TtP7SFMCSDK29InAppMessagingFeatureProtocol_")
[Model, Protocol (Name = "_TtP7SFMCSDK29InAppMessagingFeatureProtocol_"), BaseType (typeof(NSObject))]
interface InAppMessagingFeatureProtocol
{
	// @required -(id<SFMCSdkModuleIdentity> _Nullable)getIdentity __attribute__((warn_unused_result("")));
	[Abstract]
	[NullAllowed, Export ("getIdentity")]
	ISFMCSdkModuleIdentity Identity { get; }

	// @required -(void)internalTearDown;
	[Abstract]
	[Export ("internalTearDown")]
	void InternalTearDown ();
}

// SWIFT_CLASS_NAMED("InAppMessagingPresentedEvent")
// @interface SFMCSdkInAppMessagingPresentedEvent : SFMCSdkInAppMessagingAnalyticsEvent
[BaseType (typeof(SFMCSdkInAppMessagingAnalyticsEvent))]
[DisableDefaultCtor]
interface SFMCSdkInAppMessagingPresentedEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull messageId;
	[Export ("messageId")]
	string MessageId { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull activityInstanceId;
	[Export ("activityInstanceId")]
	string ActivityInstanceId { get; }

	// -(instancetype _Nonnull)initWithMessageId:(NSString * _Nonnull)messageId activityInstanceId:(NSString * _Nonnull)activityInstanceId __attribute__((objc_designated_initializer));
	[Export ("initWithMessageId:activityInstanceId:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string messageId, string activityInstanceId);
}

// SWIFT_CLASS_NAMED("InAppMessagingTriggerEvent")
// @interface SFMCSdkInAppMessagingTriggerEvent : SFMCSdkInAppMessagingAnalyticsEvent
[BaseType (typeof(SFMCSdkInAppMessagingAnalyticsEvent))]
[DisableDefaultCtor]
interface SFMCSdkInAppMessagingTriggerEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull messageId;
	[Export ("messageId")]
	string MessageId { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull activityInstanceId;
	[Export ("activityInstanceId")]
	string ActivityInstanceId { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull triggerId;
	[Export ("triggerId")]
	string TriggerId { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull outcomeType;
	[Export ("outcomeType")]
	string OutcomeType { get; }

	// -(instancetype _Nonnull)initWithMessageId:(NSString * _Nonnull)messageId activityInstanceId:(NSString * _Nonnull)activityInstanceId triggerId:(NSString * _Nonnull)triggerId outcomeType:(NSString * _Nonnull)outcomeType __attribute__((objc_designated_initializer));
	[Export ("initWithMessageId:activityInstanceId:triggerId:outcomeType:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string messageId, string activityInstanceId, string triggerId, string outcomeType);
}

// SWIFT_CLASS_NAMED("InAppMessagingValidationErrorEvent")
// @interface SFMCSdkInAppMessagingValidationErrorEvent : SFMCSdkInAppMessagingAnalyticsEvent
[BaseType (typeof(SFMCSdkInAppMessagingAnalyticsEvent))]
[DisableDefaultCtor]
interface SFMCSdkInAppMessagingValidationErrorEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull messageId;
	[Export ("messageId")]
	string MessageId { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull activityInstanceId;
	[Export ("activityInstanceId")]
	string ActivityInstanceId { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull platform;
	[Export ("platform")]
	string Platform { get; }

	// @property (readonly, copy, nonatomic) NSArray<NSString *> * _Nonnull reasons;
	[Export ("reasons", ArgumentSemantic.Copy)]
	string[] Reasons { get; }

	// -(instancetype _Nonnull)initWithMessageId:(NSString * _Nonnull)messageId activityInstanceId:(NSString * _Nonnull)activityInstanceId platform:(NSString * _Nonnull)platform reasons:(NSArray<NSString *> * _Nonnull)reasons __attribute__((objc_designated_initializer));
	[Export ("initWithMessageId:activityInstanceId:platform:reasons:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string messageId, string activityInstanceId, string platform, string[] reasons);
}

// SWIFT_CLASS_NAMED("PrivacyStatusChangeEvent")
// @interface SFMCSdkPrivacyStatusChangeEvent : NSObject <SFMCSdkEvent>
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkPrivacyStatusChangeEvent : SFMCSdkEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
	[Export ("id")]
	string Id { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
	[Export ("name")]
	string Name { get; }

	// @property (readonly, nonatomic) enum SFMCSdkEventCategory category;
	[Export ("category")]
	SFMCSdkEventCategory Category { get; }

	// @property (copy, nonatomic) NSDictionary<NSString *,id> * _Nullable attributes;
	[NullAllowed, Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Attributes { get; set; }

	// -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name isBlocked:(BOOL)isBlocked shouldDeleteData:(BOOL)shouldDeleteData attributes:(NSDictionary<NSString *,id> * _Nullable)attributes __attribute__((objc_designated_initializer));
	[Export ("initWithName:isBlocked:shouldDeleteData:attributes:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string name, bool isBlocked, bool shouldDeleteData, [NullAllowed] NSDictionary<NSString, NSObject> attributes);
}

// SWIFT_CLASS_NAMED("InAppPrivacyStatusChangeEvent")
// @interface SFMCSdkInAppPrivacyStatusChangeEvent : SFMCSdkPrivacyStatusChangeEvent
[BaseType (typeof(SFMCSdkPrivacyStatusChangeEvent))]
[DisableDefaultCtor]
interface SFMCSdkInAppPrivacyStatusChangeEvent
{
	// -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name source:(enum SFMCSdkModuleName)source isBlocked:(BOOL)isBlocked shouldDeleteData:(BOOL)shouldDeleteData attributes:(NSDictionary<NSString *,id> * _Nullable)attributes __attribute__((objc_designated_initializer));
	[Export ("initWithName:source:isBlocked:shouldDeleteData:attributes:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string name, SFMCSdkModuleName source, bool isBlocked, bool shouldDeleteData, [NullAllowed] NSDictionary<NSString, NSObject> attributes);
}

// @interface SFMCSdkLineItem : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkLineItem
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull catalogObjectType;
	[Export ("catalogObjectType")]
	string CatalogObjectType { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull catalogObjectId;
	[Export ("catalogObjectId")]
	string CatalogObjectId { get; }

	// @property (readonly, nonatomic) NSInteger quantity;
	[Export ("quantity")]
	nint Quantity { get; }

	// @property (readonly, nonatomic, strong) NSDecimalNumber * _Nullable price;
	[NullAllowed, Export ("price", ArgumentSemantic.Strong)]
	NSDecimalNumber Price { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nullable currency;
	[NullAllowed, Export ("currency")]
	string Currency { get; }

	// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nullable attributes;
	[NullAllowed, Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Attributes { get; }

	// -(instancetype _Nonnull)initWithCatalogObjectType:(NSString * _Nonnull)catalogObjectType catalogObjectId:(NSString * _Nonnull)catalogObjectId quantity:(NSInteger)quantity price:(NSDecimalNumber * _Nullable)price currency:(NSString * _Nullable)currency attributes:(NSDictionary<NSString *,id> * _Nonnull)attributes __attribute__((objc_designated_initializer));
	[Export ("initWithCatalogObjectType:catalogObjectId:quantity:price:currency:attributes:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string catalogObjectType, string catalogObjectId, nint quantity, [NullAllowed] NSDecimalNumber price, [NullAllowed] string currency, NSDictionary<NSString, NSObject> attributes);
}

interface ISFMCSdkLocationDelegate
{
}

// SWIFT_PROTOCOL_NAMED("LocationDelegate")
// @protocol SFMCSdkLocationDelegate
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkLocationDelegate
{
	// @required -(BOOL)sfmc_shouldShowLocationMessage:(NSDictionary * _Nonnull)message forRegion:(NSDictionary * _Nonnull)region __attribute__((warn_unused_result("")));
	[Abstract]
	[Export ("sfmc_shouldShowLocationMessage:forRegion:"), EventArgs("ShouldShowLocationMessageForRegion")]
	bool ShouldShowLocationMessageForRegion (NSDictionary<NSString, NSObject> message, NSDictionary region);
}

// @interface SFMCSdkLogOutputter : NSObject
[BaseType (typeof(NSObject))]
interface SFMCSdkLogOutputter
{
	// -(void)outWithLevel:(enum SFMCSdkLogLevel)level subsystem:(NSString * _Nonnull)subsystem category:(enum SFMCSdkLoggerCategory)category message:(NSString * _Nonnull)message;
	[Export ("outWithLevel:subsystem:category:message:")]
	void Out (SFMCSdkLogLevel level, string subsystem, SFMCSdkLoggerCategory category, string message);
}

interface ILogger
{
}

// SWIFT_PROTOCOL("_TtP7SFMCSDK6Logger_")
[Model, Protocol (Name = "_TtP7SFMCSDK6Logger_")]
interface Logger
{
	// @required @property (readonly, copy, nonatomic) NSArray<NSString *> * _Nonnull redactedValues;
	[Abstract]
	[Export ("redactedValues", ArgumentSemantic.Copy)]
	string[] RedactedValues { get; }
}

interface IMarketingCloudSdkProtocol
{
}

// SWIFT_PROTOCOL("_TtP7SFMCSDK25MarketingCloudSdkProtocol_")
[Model, Protocol (Name = "_TtP7SFMCSDK25MarketingCloudSdkProtocol_"), BaseType (typeof(NSObject))]
interface MarketingCloudSdkProtocol
{
	// @required -(id<SFMCSdkModuleIdentity> _Nullable)getIdentity __attribute__((warn_unused_result("")));
	[Abstract]
	[NullAllowed, Export ("getIdentity")]
	ISFMCSdkModuleIdentity Identity { get; }

	// @required -(void)internalTearDown;
	[Abstract]
	[Export ("internalTearDown")]
	void InternalTearDown ();
}

// @interface SFMCSdkMessage : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkMessage
{
}

interface IMobileAppMessagingProtocol
{
}

// SWIFT_PROTOCOL("_TtP7SFMCSDK26MobileAppMessagingProtocol_")
[Model, Protocol (Name = "_TtP7SFMCSDK26MobileAppMessagingProtocol_"), BaseType (typeof(NSObject))]
interface MobileAppMessagingProtocol
{
	// @required -(id<SFMCSdkModuleIdentity> _Nullable)getIdentity __attribute__((warn_unused_result("")));
	[Abstract]
	[NullAllowed, Export ("getIdentity")]
	ISFMCSdkModuleIdentity Identity { get; }

	// @required -(void)internalTearDown;
	[Abstract]
	[Export ("internalTearDown")]
	void InternalTearDown ();
}

interface ISFMCSdkModuleConfig
{
}

// SWIFT_PROTOCOL_NAMED("ModuleConfig")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkModuleConfig
{
	// @required @property (readonly, nonatomic) enum SFMCSdkModuleName name;
	[Abstract]
	[Export ("name")]
	SFMCSdkModuleName Name { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nonnull appId;
	[Abstract]
	[Export ("appId")]
	string AppId { get; }

	// @required @property (readonly, nonatomic) BOOL trackScreens;
	[Abstract]
	[Export ("trackScreens")]
	bool TrackScreens { get; }
}

interface ISFMCSdkModuleIdentity
{
}

// SWIFT_PROTOCOL_NAMED("ModuleIdentity")
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkModuleIdentity
{
	// @required @property (readonly, copy, nonatomic) NSString * _Nonnull applicationId;
	[Abstract]
	[Export ("applicationId")]
	string ApplicationId { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable profileId;
	[Abstract]
	[NullAllowed, Export ("profileId")]
	string ProfileId { get; }

	// @required @property (readonly, copy, nonatomic) NSString * _Nullable installationId;
	[Abstract]
	[NullAllowed, Export ("installationId")]
	string InstallationId { get; }

	// @required @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nullable customProperties;
	[Abstract]
	[NullAllowed, Export ("customProperties", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> CustomProperties { get; }

	// @required -(NSString * _Nullable)customPropertiesToJson __attribute__((warn_unused_result("")));
	[Abstract]
	[NullAllowed, Export ("customPropertiesToJson")]
	string CustomPropertiesToJson ();
}

// SWIFT_CLASS_NAMED("ModuleInitStatus")
// @interface SFMCModuleInitStatus : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCModuleInitStatus
{
	// @property (readonly, nonatomic) enum SFMCSdkModuleName moduleName;
	[Export ("moduleName")]
	SFMCSdkModuleName ModuleName { get; }

	// @property (readonly, nonatomic) enum SFMCSdkOperationResult initStatus;
	[Export ("initStatus")]
	SFMCSdkOperationResult InitStatus { get; }
}

// @interface SFMCSdkModuleLogger : NSObject <Logger>
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkModuleLogger : Logger
{
	// @property (copy, nonatomic) NSArray<NSString *> * _Nonnull redactedValues;
	[Export ("redactedValues", ArgumentSemantic.Copy)]
	string[] RedactedValues { get; set; }

	// -(instancetype _Nonnull)initWithModule:(enum SFMCSdkModuleName)module_ redactedValues:(NSArray<NSString *> * _Nonnull)redactedValues __attribute__((objc_designated_initializer));
	[Export ("initWithModule:redactedValues:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkModuleName module_, string[] redactedValues);

	// -(void)setWithRedactedValues:(NSArray<NSString *> * _Nonnull)redactedValues;
	[Export ("setWithRedactedValues:")]
	void Set (string[] redactedValues);

	// -(void)debugWithCategory:(enum SFMCSdkLoggerCategory)category message:(NSString * _Nonnull)message;
	[Export ("debugWithCategory:message:")]
	void Debug (SFMCSdkLoggerCategory category, string message);

	// -(void)warningWithCategory:(enum SFMCSdkLoggerCategory)category message:(NSString * _Nonnull)message;
	[Export ("warningWithCategory:message:")]
	void Warning (SFMCSdkLoggerCategory category, string message);

	// -(void)errorWithCategory:(enum SFMCSdkLoggerCategory)category message:(NSString * _Nonnull)message;
	[Export ("errorWithCategory:message:")]
	void Error (SFMCSdkLoggerCategory category, string message);

	// -(void)faultWithCategory:(enum SFMCSdkLoggerCategory)category message:(NSString * _Nonnull)message;
	[Export ("faultWithCategory:message:")]
	void Fault (SFMCSdkLoggerCategory category, string message);
}

// @interface SFMCSdkNetworkManager : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkNetworkManager
{
	// -(instancetype _Nonnull)initWithNetworkPreferences:(SFMCSdkSecurePrefs * _Nonnull)networkPreferences authenticator:(id<SFMCSdkAuthenticator> _Nullable)authenticator __attribute__((objc_designated_initializer));
	[Export ("initWithNetworkPreferences:authenticator:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkSecurePrefs networkPreferences, [NullAllowed] SFMCSdkAuthenticator authenticator);

	// -(SFMCSdkCompletedCall * _Nonnull)executeSync:(SFMCSdkWrappedRequest * _Nonnull)wrappedRequest __attribute__((warn_unused_result("")));
	[Export ("executeSync:")]
	SFMCSdkCompletedCall ExecuteSync (SFMCSdkWrappedRequest wrappedRequest);

	// -(void)executeAsync:(SFMCSdkWrappedRequest * _Nonnull)wrappedRequest completionHandler:(void (^ _Nonnull)(SFMCSdkCompletedCall * _Nonnull))completionHandler;
	[Export ("executeAsync:completionHandler:")]
	void ExecuteAsync (SFMCSdkWrappedRequest wrappedRequest, Action<SFMCSdkCompletedCall> completionHandler);

	// -(BOOL)isBlockedByRetryAfter:(NSString * _Nonnull)requestName __attribute__((warn_unused_result("")));
	[Export ("isBlockedByRetryAfter:")]
	bool IsBlockedByRetryAfter (string requestName);
}

// @interface SFMCSdkOrder : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkOrder
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
	[Export ("id")]
	string Id { get; }

	// @property (readonly, copy, nonatomic) NSArray<SFMCSdkLineItem *> * _Nonnull lineItems;
	[Export ("lineItems", ArgumentSemantic.Copy)]
	SFMCSdkLineItem[] LineItems { get; }

	// @property (readonly, nonatomic, strong) NSDecimalNumber * _Nullable totalValue;
	[NullAllowed, Export ("totalValue", ArgumentSemantic.Strong)]
	NSDecimalNumber TotalValue { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nullable currency;
	[NullAllowed, Export ("currency")]
	string Currency { get; }

	// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nonnull attributes;
	[Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Attributes { get; }

	// -(instancetype _Nonnull)initWithId:(NSString * _Nonnull)id lineItems:(NSArray<SFMCSdkLineItem *> * _Nonnull)lineItems totalValue:(NSDecimalNumber * _Nullable)totalValue currency:(NSString * _Nullable)currency attributes:(NSDictionary<NSString *,id> * _Nonnull)attributes __attribute__((objc_designated_initializer));
	[Export ("initWithId:lineItems:totalValue:currency:attributes:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string id, SFMCSdkLineItem[] lineItems, [NullAllowed] NSDecimalNumber totalValue, [NullAllowed] string currency, NSDictionary<NSString, NSObject> attributes);
}

// SWIFT_CLASS_NAMED("PERSONALIZATION")
// @interface SFMCSdkPersonalization : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkPersonalization
{
	// -(enum SFMCSdkModuleStatus)getStatus __attribute__((warn_unused_result("")));
	[Export ("getStatus")]
	SFMCSdkModuleStatus Status { get; }

	// -(id<SFMCSdkModuleIdentity> _Nullable)getIdentity __attribute__((warn_unused_result("")));
	[NullAllowed, Export ("getIdentity")]
	ISFMCSdkModuleIdentity Identity { get; }
}

// @interface SFMCSdkPreorderEvent : SFMCSdkOrderEvent
[BaseType (typeof(SFMCSdkOrderEvent))]
interface SFMCSdkPreorderEvent
{
	// -(instancetype _Nonnull)initWithOrder:(SFMCSdkOrder * _Nonnull)order __attribute__((objc_designated_initializer));
	[Export ("initWithOrder:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkOrder order);
}

// @interface SFMCSdkPurchaseOrderEvent : SFMCSdkOrderEvent
[BaseType (typeof(SFMCSdkOrderEvent))]
interface SFMCSdkPurchaseOrderEvent
{
	// -(instancetype _Nonnull)initWithOrder:(SFMCSdkOrder * _Nonnull)order __attribute__((objc_designated_initializer));
	[Export ("initWithOrder:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkOrder order);
}

// SWIFT_CLASS_NAMED("PushFeatureEventBase")
// @interface SFMCSdkPushFeatureEventBase : NSObject <SFMCSdkEvent>
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkPushFeatureEventBase : SFMCSdkEvent
{
	// @property (readonly, nonatomic) enum SFMCPushFeatureEventType pushFeatureEventType;
	[Export ("pushFeatureEventType")]
	SFMCPushFeatureEventType PushFeatureEventType { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
	[Export ("id")]
	string Id { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
	[Export ("name")]
	string Name { get; }

	// @property (readonly, nonatomic) enum SFMCSdkEventCategory category;
	[Export ("category")]
	SFMCSdkEventCategory Category { get; }

	// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nullable attributes;
	[NullAllowed, Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Attributes { get; }

	// -(instancetype _Nonnull)initWithPushFeatureEventType:(enum SFMCPushFeatureEventType)pushFeatureEventType __attribute__((objc_designated_initializer));
	[Export ("initWithPushFeatureEventType:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCPushFeatureEventType pushFeatureEventType);
}

// SWIFT_CLASS_NAMED("PushClickEvent")
// @interface SFMCSdkPushClickEvent : SFMCSdkPushFeatureEventBase
[BaseType (typeof(SFMCSdkPushFeatureEventBase))]
[DisableDefaultCtor]
interface SFMCSdkPushClickEvent
{
	// @property (readonly, copy, nonatomic) NSDictionary * _Nonnull userInfo;
	[Export ("userInfo", ArgumentSemantic.Copy)]
	NSDictionary UserInfo { get; }

	// @property (readonly, nonatomic, strong) UNNotificationResponse * _Nullable notificationResponse;
	[NullAllowed, Export ("notificationResponse", ArgumentSemantic.Strong)]
	UNNotificationResponse NotificationResponse { get; }

	// @property (readonly, nonatomic, strong) SFMCPNContextInfo * _Nonnull pnContextInfo;
	[Export ("pnContextInfo", ArgumentSemantic.Strong)]
	SFMCPNContextInfo PnContextInfo { get; }

	// -(instancetype _Nonnull)initWithUserInfo:(NSDictionary * _Nonnull)userInfo response:(UNNotificationResponse * _Nullable)response pnContextInfo:(SFMCPNContextInfo * _Nonnull)pnContextInfo __attribute__((objc_designated_initializer));
	[Export ("initWithUserInfo:response:pnContextInfo:")]
	[DesignatedInitializer]
	NativeHandle Constructor (NSDictionary userInfo, [NullAllowed] UNNotificationResponse response, SFMCPNContextInfo pnContextInfo);
}

// SWIFT_CLASS_NAMED("PushEnabledInGlobalSettingsEvent")
// @interface SFMCSdkPushEnabledInGlobalSettingsEvent : SFMCSdkPushFeatureEventBase
[BaseType (typeof(SFMCSdkPushFeatureEventBase))]
[DisableDefaultCtor]
interface SFMCSdkPushEnabledInGlobalSettingsEvent
{
	// @property (readonly, nonatomic) BOOL isPushEnabledInGlobalSettings;
	[Export ("isPushEnabledInGlobalSettings")]
	bool IsPushEnabledInGlobalSettings { get; }

	// -(instancetype _Nonnull)initWithIsPushEnabledInGlobalSettings:(BOOL)isPushEnabledInGlobalSettings __attribute__((objc_designated_initializer));
	[Export ("initWithIsPushEnabledInGlobalSettings:")]
	[DesignatedInitializer]
	NativeHandle Constructor (bool isPushEnabledInGlobalSettings);
}

// SWIFT_CLASS_NAMED("PushEnabledInSDKEvent")
// @interface SFMCSdkPushEnabledInSDKEvent : SFMCSdkPushFeatureEventBase
[BaseType (typeof(SFMCSdkPushFeatureEventBase))]
[DisableDefaultCtor]
interface SFMCSdkPushEnabledInSDKEvent
{
	// @property (readonly, nonatomic) BOOL isPushEnabledInSDK;
	[Export ("isPushEnabledInSDK")]
	bool IsPushEnabledInSDK { get; }

	// -(instancetype _Nonnull)initWithIsPushEnabledInSDK:(BOOL)isPushEnabledInSDK __attribute__((objc_designated_initializer));
	[Export ("initWithIsPushEnabledInSDK:")]
	[DesignatedInitializer]
	NativeHandle Constructor (bool isPushEnabledInSDK);
}

interface IPushFeatureProtocol
{
}

// SWIFT_PROTOCOL("_TtP7SFMCSDK19PushFeatureProtocol_")
[Model, Protocol (Name = "_TtP7SFMCSDK19PushFeatureProtocol_"), BaseType (typeof(NSObject))]
interface PushFeatureProtocol
{
	// @required -(id<SFMCSdkModuleIdentity> _Nullable)getIdentity __attribute__((warn_unused_result("")));
	[Abstract]
	[NullAllowed, Export ("getIdentity")]
	ISFMCSdkModuleIdentity Identity { get; }

	// @required -(void)internalTearDown;
	[Abstract]
	[Export ("internalTearDown")]
	void InternalTearDown ();
}

// SWIFT_CLASS_NAMED("PushTokenEvent")
// @interface SFMCSdkPushTokenEvent : SFMCSdkPushFeatureEventBase
[BaseType (typeof(SFMCSdkPushFeatureEventBase))]
[DisableDefaultCtor]
interface SFMCSdkPushTokenEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull token;
	[Export ("token")]
	string Token { get; }

	// -(instancetype _Nonnull)initWithToken:(NSString * _Nonnull)token __attribute__((objc_designated_initializer));
	[Export ("initWithToken:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string token);
}

// SWIFT_CLASS_NAMED("PushURLHandlingEvent")
// @interface SFMCSdkPushURLHandlingEvent : SFMCSdkPushFeatureEventBase
[BaseType (typeof(SFMCSdkPushFeatureEventBase))]
[DisableDefaultCtor]
interface SFMCSdkPushURLHandlingEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull urlString;
	[Export ("urlString")]
	string UrlString { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
	[Export ("type")]
	string Type { get; }

	// -(instancetype _Nonnull)initWithUrlString:(NSString * _Nonnull)urlString type:(NSString * _Nonnull)type __attribute__((objc_designated_initializer));
	[Export ("initWithUrlString:type:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string urlString, string type);
}

// @interface SFMCSdkQuickViewCatalogObjectEvent : SFMCSdkCatalogObjectEvent
[BaseType (typeof(SFMCSdkCatalogObjectEvent))]
interface SFMCSdkQuickViewCatalogObjectEvent
{
	// -(instancetype _Nonnull)initWithCatalogObject:(SFMCSdkCatalogObject * _Nonnull)catalogObject __attribute__((objc_designated_initializer));
	[Export ("initWithCatalogObject:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkCatalogObject catalogObject);
}

// SWIFT_CLASS_NAMED("QuietPushEnabledEvent")
// @interface SFMCSdkQuietPushEnabledEvent : SFMCSdkPushFeatureEventBase
[BaseType (typeof(SFMCSdkPushFeatureEventBase))]
[DisableDefaultCtor]
interface SFMCSdkQuietPushEnabledEvent
{
	// @property (readonly, nonatomic) BOOL isQuietPushEnabled;
	[Export ("isQuietPushEnabled")]
	bool IsQuietPushEnabled { get; }

	// -(instancetype _Nonnull)initWithIsQuietPushEnabled:(BOOL)isQuietPushEnabled __attribute__((objc_designated_initializer));
	[Export ("initWithIsQuietPushEnabled:")]
	[DesignatedInitializer]
	NativeHandle Constructor (bool isQuietPushEnabled);
}

// @interface SFMCSdkRemoveFromCartEvent : SFMCSdkCartEvent
[BaseType (typeof(SFMCSdkCartEvent))]
interface SFMCSdkRemoveFromCartEvent
{
	// -(instancetype _Nonnull)initWithLineItem:(SFMCSdkLineItem * _Nonnull)lineItem __attribute__((objc_designated_initializer));
	[Export ("initWithLineItem:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkLineItem lineItem);
}

// @interface SFMCSdkReplaceCartEvent : SFMCSdkCartEvent
[BaseType (typeof(SFMCSdkCartEvent))]
interface SFMCSdkReplaceCartEvent
{
	// -(instancetype _Nonnull)initWithLineItems:(NSArray<SFMCSdkLineItem *> * _Nonnull)lineItems __attribute__((objc_designated_initializer));
	[Export ("initWithLineItems:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkLineItem[] lineItems);
}

// @interface SFMCSdkReturnOrderEvent : SFMCSdkOrderEvent
[BaseType (typeof(SFMCSdkOrderEvent))]
interface SFMCSdkReturnOrderEvent
{
	// -(instancetype _Nonnull)initWithOrder:(SFMCSdkOrder * _Nonnull)order __attribute__((objc_designated_initializer));
	[Export ("initWithOrder:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkOrder order);
}

// @interface SFMCSdkReviewCatalogObjectEvent : SFMCSdkCatalogObjectEvent
[BaseType (typeof(SFMCSdkCatalogObjectEvent))]
interface SFMCSdkReviewCatalogObjectEvent
{
	// -(instancetype _Nonnull)initWithCatalogObject:(SFMCSdkCatalogObject * _Nonnull)catalogObject __attribute__((objc_designated_initializer));
	[Export ("initWithCatalogObject:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkCatalogObject catalogObject);
}

interface ISFMCModule
{
}

// SWIFT_PROTOCOL("_TtP7SFMCSDK10SFMCModule_")
[Model, Protocol (Name = "_TtP7SFMCSDK10SFMCModule_"), BaseType (typeof(NSObject))]
interface SFMCModule
{
	// @required @property (readonly, nonatomic) enum SFMCSdkModuleName name;
	[Abstract]
	[Export ("name")]
	SFMCSdkModuleName Name { get; }

	// @required @property (readonly, copy, nonatomic, class) NSString * _Nonnull moduleVersion;
	[Static, Abstract]
	[Export ("moduleVersion")]
	string ModuleVersion { get; }

	// @required @property (copy, nonatomic, class) NSDictionary<NSString *,NSString *> * _Nullable stateProperties;
	[Static, Abstract]
	[NullAllowed, Export ("stateProperties", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSString> StateProperties { get; set; }

	// @required +(id<SFMCModule> _Nullable)initModuleWithConfig:(id<SFMCSdkModuleConfig> _Nonnull)config components:(SFMCSdkComponents * _Nonnull)components __attribute__((objc_method_family("none"))) __attribute__((warn_unused_result("")));
	[Static, Abstract]
	[Export ("initModuleWithConfig:components:")]
	[return: NullAllowed]
	ISFMCModule InitModule (ISFMCSdkModuleConfig config, SFMCSdkComponents components);
}

// SWIFT_CLASS("_TtC7SFMCSDK17SFMCPNContextInfo")
// @interface SFMCPNContextInfo : NSObject
[BaseType (typeof(NSObject), Name = "_TtC7SFMCSDK17SFMCPNContextInfo")]
[DisableDefaultCtor]
interface SFMCPNContextInfo
{
	// @property (readonly, nonatomic) enum SFMCPNContextSource source;
	[Export ("source")]
	SFMCPNContextSource Source { get; }

	// @property (readonly, copy, nonatomic) NSArray<NSNumber *> * _Nonnull destinationsArray;
	[Export ("destinationsArray", ArgumentSemantic.Copy)]
	NSNumber[] DestinationsArray { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nullable businessUnitId;
	[NullAllowed, Export ("businessUnitId")]
	string BusinessUnitId { get; }

	// -(instancetype _Nonnull)initWithSource:(enum SFMCPNContextSource)source destinationsArray:(NSArray<NSNumber *> * _Nonnull)destinationsArray businessUnitId:(NSString * _Nullable)businessUnitId __attribute__((objc_designated_initializer));
	[Export ("initWithSource:destinationsArray:businessUnitId:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCPNContextSource source, NSNumber[] destinationsArray, [NullAllowed] string businessUnitId);

	// -(BOOL)isInavlidOrAbsentContext __attribute__((warn_unused_result("")));
	[Export ("isInavlidOrAbsentContext")]
	bool IsInavlidOrAbsentContext ();

	// @property (readonly, copy, nonatomic) NSString * _Nonnull description;
	// NOTE: not bound; satisfied by NSObject.Description (same selector).
}

// @interface SFMCSdk : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdk
{
	// @property (nonatomic, strong, class) SFMCSdkCDP * _Nonnull cdp;
	[Static]
	[Export ("cdp", ArgumentSemantic.Strong)]
	SFMCSdkCDP Cdp { get; set; }

	// @property (nonatomic, strong, class) SFMCSdkPersonalization * _Nonnull personalization;
	[Static]
	[Export ("personalization", ArgumentSemantic.Strong)]
	SFMCSdkPersonalization Personalization { get; set; }

	// @property (readonly, nonatomic, strong, class) SFIdentity * _Nonnull identity;
	[Static]
	[Export ("identity", ArgumentSemantic.Strong)]
	SFIdentity Identity { get; }

	// @property (copy, nonatomic, class) NSString * _Nonnull sdkVersion;
	[Static]
	[Export ("sdkVersion")]
	string SdkVersion { get; set; }

	// +(void)initializeSdk:(SFMCSdkConfig * _Nonnull)configuration completion:(void (^ _Nullable)(NSArray<SFMCModuleInitStatus *> * _Nonnull))completion;
	[Static]
	[Export ("initializeSdk:completion:")]
	void InitializeSdk (SFMCSdkConfig configuration, [NullAllowed] Action<SFMCModuleInitStatus[]> completion);

	// +(NSString * _Nonnull)state __attribute__((warn_unused_result("")));
	[Static]
	[Export ("state")]
	string State { get; }

	// +(void)tearDownModuleWithName:(enum SFMCSdkModuleName)name __attribute__((deprecated("The SFMCSdk takes care of tear downs during initializations, making this method unnecessary. Its usage should be avoided to prevent race conditions.")));
	[Static]
	[Obsolete("The SFMCSdk takes care of tear downs during initializations, making this method unnecessary. Its usage should be avoided to prevent race conditions.")]
	[Export ("tearDownModuleWithName:")]
	void TearDownModule (SFMCSdkModuleName name);

	// +(void)setLoggerWithLogLevel:(enum SFMCSdkLogLevel)logLevel logOutputter:(SFMCSdkLogOutputter * _Nullable)logOutputter;
	[Static]
	[Export ("setLoggerWithLogLevel:logOutputter:")]
	void SetLogger (SFMCSdkLogLevel logLevel, [NullAllowed] SFMCSdkLogOutputter logOutputter);

	// +(enum SFMCSdkLogLevel)getLogLevel __attribute__((warn_unused_result("")));
	[Static]
	[Export ("getLogLevel")]
	SFMCSdkLogLevel LogLevel { get; }

	// +(void)clearLoggerFilters;
	[Static]
	[Export ("clearLoggerFilters")]
	void ClearLoggerFilters ();

	// +(void)setAutoMergePolicyOnCompletion:(void (^ _Nonnull)(BOOL))onCompletion;
	[Static]
	[Export ("setAutoMergePolicyOnCompletion:")]
	void SetAutoMergePolicyOnCompletion (Action<bool> onCompletion);

	// +(void)setManualMergePolicyWithHandler:(void (^ _Nonnull)(NSDictionary * _Nonnull, NSDictionary * _Nonnull))handler;
	[Static]
	[Export ("setManualMergePolicyWithHandler:")]
	void SetManualMergePolicy (Action<NSDictionary, NSDictionary> handler);

	// +(enum SFMCSDKDataMergePolicy)getDataMergePolicy __attribute__((warn_unused_result("")));
	[Static]
	[Export ("getDataMergePolicy")]
	SFMCSDKDataMergePolicy DataMergePolicy { get; }

	// +(BOOL)resetDataPolicyWithAppId:(NSString * _Nonnull)appId __attribute__((warn_unused_result("")));
	[Static]
	[Export ("resetDataPolicyWithAppId:")]
	bool ResetDataPolicy (string appId);

	// +(void (^ _Nullable)(BOOL))getAutoDataPolicyCallBack __attribute__((warn_unused_result("")));
	[Static]
	[NullAllowed, Export ("getAutoDataPolicyCallBack")]
	Action<bool> AutoDataPolicyCallBack { get; }

	// +(void (^ _Nullable)(NSDictionary * _Nonnull, NSDictionary * _Nonnull))getManualDataPolicyCallBack __attribute__((warn_unused_result("")));
	[Static]
	[NullAllowed, Export ("getManualDataPolicyCallBack")]
	Action<NSDictionary, NSDictionary> ManualDataPolicyCallBack { get; }

	// +(void)setFileProtectionTypeWithFileProtectionType:(NSFileProtectionType _Nullable)fileProtectionType;
	[Static]
	[Export ("setFileProtectionTypeWithFileProtectionType:")]
	void SetFileProtectionType ([NullAllowed] string fileProtectionType);

	// +(NSFileProtectionType _Nullable)fileProtectionType __attribute__((warn_unused_result("")));
	[Static]
	[NullAllowed, Export ("fileProtectionType")]
	string FileProtectionType { get; }

	// +(void)requestPersonalizationSdk:(void (^ _Nonnull)(void))callback;
	[Static]
	[Export ("requestPersonalizationSdk:")]
	void RequestPersonalizationSdk (Action callback);

	// +(void)requestCdpSdk:(void (^ _Nonnull)(id<CdpInterface> _Nonnull))callback;
	[Static]
	[Export ("requestCdpSdk:")]
	void RequestCdpSdk (Action<ICdpInterface> callback);

	// +(void)trackWithEvent:(id<SFMCSdkEvent> _Nonnull)event;
	[Static]
	[Export ("trackWithEvent:")]
	void Track (ISFMCSdkEvent @event);

	// +(void)sendImmediateWithEvent:(id<SFMCSdkEvent> _Nonnull)event;
	[Static]
	[Export ("sendImmediateWithEvent:")]
	void SendImmediate (ISFMCSdkEvent @event);

	// +(void)flush;
	[Static]
	[Export ("flush")]
	void Flush ();
}

// @interface SFMCSdkComponents : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkComponents
{
	// @property (readonly, nonatomic, strong) SFMCSdkEncryptionManager * _Nonnull encryptionManager;
	[Export ("encryptionManager", ArgumentSemantic.Strong)]
	SFMCSdkEncryptionManager EncryptionManager { get; }

	// @property (readonly, nonatomic, strong) SFMCSdkStorageManager * _Nonnull storageManager;
	[Export ("storageManager", ArgumentSemantic.Strong)]
	SFMCSdkStorageManager StorageManager { get; }

	// @property (readonly, nonatomic, strong) SFMCSdkBehaviorManager * _Nonnull behaviorManager;
	[Export ("behaviorManager", ArgumentSemantic.Strong)]
	SFMCSdkBehaviorManager BehaviorManager { get; }

	// @property (readonly, nonatomic, strong) SFMCSdkSessionManager * _Nonnull sessionManager;
	[Export ("sessionManager", ArgumentSemantic.Strong)]
	SFMCSdkSessionManager SessionManager { get; }

	// -(SFMCSdkNetworkManager * _Nonnull)createNetworkManagerWithAuthenticator:(id<SFMCSdkAuthenticator> _Nullable)authenticator __attribute__((warn_unused_result("")));
	[Export ("createNetworkManagerWithAuthenticator:")]
	SFMCSdkNetworkManager CreateNetworkManager ([NullAllowed] SFMCSdkAuthenticator authenticator);
}

// @interface SFMCSdkLogger : NSObject <Logger>
[BaseType (typeof(NSObject), Name = "_TtC7SFMCSDK13SFMCSdkLogger")]
interface SFMCSdkLogger : Logger
{
	// @property (readonly, nonatomic, strong, class) SFMCSdkLogger * _Nonnull shared;
	[Static]
	[Export ("shared", ArgumentSemantic.Strong)]
	SFMCSdkLogger Shared { get; }

	// @property (readonly, copy, nonatomic) NSArray<NSString *> * _Nonnull redactedValues;
	[Export ("redactedValues", ArgumentSemantic.Copy)]
	string[] RedactedValues { get; }

	// -(enum SFMCSdkLogLevel)getLogLevel __attribute__((warn_unused_result("")));
	[Export ("getLogLevel")]
	SFMCSdkLogLevel LogLevel { get; }

	// -(void)debugWithCategory:(enum SFMCSdkLoggerCategory)category message:(NSString * _Nonnull)message;
	[Export ("debugWithCategory:message:")]
	void Debug (SFMCSdkLoggerCategory category, string message);

	// -(void)warningWithCategory:(enum SFMCSdkLoggerCategory)category message:(NSString * _Nonnull)message;
	[Export ("warningWithCategory:message:")]
	void Warning (SFMCSdkLoggerCategory category, string message);

	// -(void)errorWithCategory:(enum SFMCSdkLoggerCategory)category message:(NSString * _Nonnull)message;
	[Export ("errorWithCategory:message:")]
	void Error (SFMCSdkLoggerCategory category, string message);

	// -(void)faultWithCategory:(enum SFMCSdkLoggerCategory)category message:(NSString * _Nonnull)message;
	[Export ("faultWithCategory:message:")]
	void Fault (SFMCSdkLoggerCategory category, string message);
}

// @interface SFMCSdkScreenEntry : SFMCSdkBehavior
[BaseType (typeof(SFMCSdkBehavior))]
interface SFMCSdkScreenEntry
{
}

// @interface SFMCSdkSecurePrefs : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkSecurePrefs
{
	// -(void)setString:(NSString * _Nonnull)value for:(NSString * _Nonnull)key;
	[Export ("setString:for:")]
	void SetString (string value, string key);

	// -(void)setInt:(NSInteger)value for:(NSString * _Nonnull)key;
	[Export ("setInt:for:")]
	void SetInt (nint value, string key);

	// -(void)setBool:(BOOL)value for:(NSString * _Nonnull)key;
	[Export ("setBool:for:")]
	void SetBool (bool value, string key);

	// -(void)setFloat:(float)value for:(NSString * _Nonnull)key;
	[Export ("setFloat:for:")]
	void SetFloat (float value, string key);

	// -(void)setDouble:(double)value for:(NSString * _Nonnull)key;
	[Export ("setDouble:for:")]
	void SetDouble (double value, string key);

	// -(NSString * _Nullable)stringForKey:(NSString * _Nonnull)key __attribute__((warn_unused_result("")));
	[Export ("stringForKey:")]
	[return: NullAllowed]
	string StringForKey (string key);

	// -(void)removeWithKey:(NSString * _Nonnull)key;
	[Export ("removeWithKey:")]
	void Remove (string key);

	// -(void)clearAll;
	[Export ("clearAll")]
	void ClearAll ();
}

interface ISessionManagerProtocol
{
}

// SWIFT_PROTOCOL("_TtP7SFMCSDK22SessionManagerProtocol_")
[Model, Protocol (Name = "_TtP7SFMCSDK22SessionManagerProtocol_"), BaseType (typeof(NSObject))]
interface SessionManagerProtocol
{
	// @required -(NSString * _Nonnull)getSessionId __attribute__((warn_unused_result("")));
	[Abstract]
	[Export ("getSessionId")]
	string SessionId { get; }
}

// SWIFT_CLASS_NAMED("SessionManager")
// @interface SFMCSdkSessionManager : NSObject <SessionManagerProtocol>
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkSessionManager : SessionManagerProtocol
{
	// -(NSString * _Nonnull)getSessionId __attribute__((warn_unused_result("")));
	[Export ("getSessionId")]
	string SessionId { get; }
}

// @interface SFMCSdkShareCatalogObjectEvent : SFMCSdkCatalogObjectEvent
[BaseType (typeof(SFMCSdkCatalogObjectEvent))]
interface SFMCSdkShareCatalogObjectEvent
{
	// -(instancetype _Nonnull)initWithCatalogObject:(SFMCSdkCatalogObject * _Nonnull)catalogObject __attribute__((objc_designated_initializer));
	[Export ("initWithCatalogObject:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkCatalogObject catalogObject);
}

// @interface SFMCSdkShipOrderEvent : SFMCSdkOrderEvent
[BaseType (typeof(SFMCSdkOrderEvent))]
interface SFMCSdkShipOrderEvent
{
	// -(instancetype _Nonnull)initWithOrder:(SFMCSdkOrder * _Nonnull)order __attribute__((objc_designated_initializer));
	[Export ("initWithOrder:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkOrder order);
}

// SWIFT_CLASS_NAMED("SilentPushEvent")
// @interface SFMCSdkSilentPushEvent : SFMCSdkPushFeatureEventBase
[BaseType (typeof(SFMCSdkPushFeatureEventBase))]
[DisableDefaultCtor]
interface SFMCSdkSilentPushEvent
{
	// @property (readonly, copy, nonatomic) NSDictionary * _Nonnull userInfo;
	[Export ("userInfo", ArgumentSemantic.Copy)]
	NSDictionary UserInfo { get; }

	// @property (readonly, nonatomic, strong) SFMCPNContextInfo * _Nonnull pnContextInfo;
	[Export ("pnContextInfo", ArgumentSemantic.Strong)]
	SFMCPNContextInfo PnContextInfo { get; }

	// -(instancetype _Nonnull)initWithUserInfo:(NSDictionary * _Nonnull)userInfo pnContextInfo:(SFMCPNContextInfo * _Nonnull)pnContextInfo __attribute__((objc_designated_initializer));
	[Export ("initWithUserInfo:pnContextInfo:")]
	[DesignatedInitializer]
	NativeHandle Constructor (NSDictionary userInfo, SFMCPNContextInfo pnContextInfo);
}

// @interface SFMCSdkStorageManager : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkStorageManager
{
	// -(NSString * _Nonnull)getRegistrationId __attribute__((warn_unused_result("")));
	[Export ("getRegistrationId")]
	string RegistrationId { get; }

	// -(SFMCSdkSecurePrefs * _Nonnull)getOrCreateSecurePrefsWithName:(NSString * _Nonnull)name __attribute__((warn_unused_result("")));
	[Export ("getOrCreateSecurePrefsWithName:")]
	SFMCSdkSecurePrefs GetOrCreateSecurePrefs (string name);

	// -(NSString * _Nonnull)getFilenameForModuleInstallationWithFileName:(NSString * _Nonnull)fileName __attribute__((warn_unused_result("")));
	[Export ("getFilenameForModuleInstallationWithFileName:")]
	string GetFilenameForModuleInstallation (string fileName);

	// +(void)setFileSystemProtectionTypeWithFileProtectionType:(NSFileProtectionType _Nullable)fileProtectionType;
	[Static]
	[Export ("setFileSystemProtectionTypeWithFileProtectionType:")]
	void SetFileSystemProtectionType ([NullAllowed] string fileProtectionType);

	// +(NSFileProtectionType _Nullable)fileSystemProtectionType __attribute__((warn_unused_result("")));
	[Static]
	[NullAllowed, Export ("fileSystemProtectionType")]
	string FileSystemProtectionType { get; }
}

interface ISubscriber
{
}

// SWIFT_PROTOCOL("_TtP7SFMCSDK10Subscriber_")
[Model, Protocol (Name = "_TtP7SFMCSDK10Subscriber_"), BaseType (typeof(NSObject))]
interface Subscriber
{
	// @required @property (readonly, nonatomic) enum SFMCSdkModuleName name;
	[Abstract]
	[Export ("name")]
	SFMCSdkModuleName Name { get; }

	// @required -(void)receiveWithMessage:(SFMCSdkMessage * _Nonnull)message;
	[Abstract]
	[Export ("receiveWithMessage:")]
	void Receive (SFMCSdkMessage message);
}

// @interface SFMCSdkSystemEvent : NSObject <SFMCSdkUserEvent>
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkSystemEvent : SFMCSdkUserEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
	[Export ("id")]
	string Id { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
	[Export ("name")]
	string Name { get; }

	// @property (readonly, nonatomic) enum SFMCSdkEventCategory category;
	[Export ("category")]
	SFMCSdkEventCategory Category { get; }

	// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nullable attributes;
	[NullAllowed, Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Attributes { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nullable userId;
	[NullAllowed, Export ("userId")]
	string UserId { get; }

	// -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name attributes:(NSDictionary<NSString *,id> * _Nullable)attributes __attribute__((objc_designated_initializer));
	[Export ("initWithName:attributes:")]
	[DesignatedInitializer]
	NativeHandle Constructor (string name, [NullAllowed] NSDictionary<NSString, NSObject> attributes);
}

// SWIFT_CLASS_NAMED("TraceEvent")
// @interface SFMCSdkTraceEvent : NSObject <SFMCSdkEvent>
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkTraceEvent : SFMCSdkEvent
{
	// @property (readonly, copy, nonatomic) NSString * _Nonnull id;
	[Export ("id")]
	string Id { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
	[Export ("name")]
	string Name { get; }

	// @property (readonly, nonatomic) enum SFMCSdkEventCategory category;
	[Export ("category")]
	SFMCSdkEventCategory Category { get; }

	// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nullable attributes;
	[NullAllowed, Export ("attributes", ArgumentSemantic.Copy)]
	NSDictionary<NSString, NSObject> Attributes { get; }

	// @property (readonly, nonatomic) enum SFMCSdkTraceEventType type;
	[Export ("type")]
	SFMCSdkTraceEventType Type { get; }

	// -(instancetype _Nonnull)initWithType:(enum SFMCSdkTraceEventType)type attributes:(NSDictionary<NSString *,id> * _Nullable)attributes __attribute__((objc_designated_initializer));
	[Export ("initWithType:attributes:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkTraceEventType type, [NullAllowed] NSDictionary<NSString, NSObject> attributes);
}

interface ISFMCSdkURLHandlingDelegate
{
}

// SWIFT_PROTOCOL_NAMED("URLHandlingDelegate")
// @protocol SFMCSdkURLHandlingDelegate
[Protocol, Model, BaseType (typeof(NSObject))]
interface SFMCSdkURLHandlingDelegate
{
	// @required -(void)sfmc_handleURL:(NSURL * _Nonnull)url type:(NSString * _Nonnull)type;
	[Abstract]
	[Export ("sfmc_handleURL:type:"), EventArgs("URLHandlingHandleURL")]
	void HandleURL (NSUrl url, string type);
}

// @interface SFMCSdkViewCatalogObjectDetailEvent : SFMCSdkCatalogObjectEvent
[BaseType (typeof(SFMCSdkCatalogObjectEvent))]
interface SFMCSdkViewCatalogObjectDetailEvent
{
	// -(instancetype _Nonnull)initWithCatalogObject:(SFMCSdkCatalogObject * _Nonnull)catalogObject __attribute__((objc_designated_initializer));
	[Export ("initWithCatalogObject:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkCatalogObject catalogObject);
}

// @interface SFMCSdkViewCatalogObjectEvent : SFMCSdkCatalogObjectEvent
[BaseType (typeof(SFMCSdkCatalogObjectEvent))]
interface SFMCSdkViewCatalogObjectEvent
{
	// -(instancetype _Nonnull)initWithCatalogObject:(SFMCSdkCatalogObject * _Nonnull)catalogObject __attribute__((objc_designated_initializer));
	[Export ("initWithCatalogObject:")]
	[DesignatedInitializer]
	NativeHandle Constructor (SFMCSdkCatalogObject catalogObject);
}

// @interface SFMCSdkWrappedRequest : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkWrappedRequest
{
	// @property (copy, nonatomic) NSURLRequest * _Nonnull request;
	[Export ("request", ArgumentSemantic.Copy)]
	NSUrlRequest Request { get; set; }

	// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
	[Export ("name")]
	string Name { get; }

	// @property (readonly, nonatomic) NSTimeInterval rateLimit;
	[Export ("rateLimit")]
	double RateLimit { get; }

	// @property (readonly, copy, nonatomic) NSString * _Nullable tag;
	[NullAllowed, Export ("tag")]
	string Tag { get; }
}

// SWIFT_CLASS_NAMED("Builder")
// @interface SFMCSdkWrappedRequestBuilder : NSObject
[BaseType (typeof(NSObject))]
interface SFMCSdkWrappedRequestBuilder
{
	// -(SFMCSdkWrappedRequestBuilder * _Nonnull)method:(NSString * _Nonnull)method __attribute__((warn_unused_result("")));
	[Export ("method:")]
	SFMCSdkWrappedRequestBuilder Method (string method);

	// -(SFMCSdkWrappedRequestBuilder * _Nonnull)url:(NSString * _Nonnull)url __attribute__((warn_unused_result("")));
	[Export ("url:")]
	SFMCSdkWrappedRequestBuilder Url (string url);

	// -(SFMCSdkWrappedRequestBuilder * _Nonnull)urlWithBase:(NSString * _Nonnull)base path:(NSString * _Nonnull)path __attribute__((warn_unused_result("")));
	[Export ("urlWithBase:path:")]
	SFMCSdkWrappedRequestBuilder Url (string @base, string path);

	// -(SFMCSdkWrappedRequestBuilder * _Nonnull)addOrReplaceHeaderWithKey:(NSString * _Nonnull)key value:(NSString * _Nonnull)value __attribute__((warn_unused_result("")));
	[Export ("addOrReplaceHeaderWithKey:value:")]
	SFMCSdkWrappedRequestBuilder AddOrReplaceHeader (string key, string value);

	// -(SFMCSdkWrappedRequestBuilder * _Nonnull)body:(NSData * _Nonnull)body __attribute__((warn_unused_result("")));
	[Export ("body:")]
	SFMCSdkWrappedRequestBuilder Body (NSData body);

	// -(SFMCSdkWrappedRequestBuilder * _Nonnull)timeout:(NSTimeInterval)seconds __attribute__((warn_unused_result("")));
	[Export ("timeout:")]
	SFMCSdkWrappedRequestBuilder Timeout (double seconds);

	// -(SFMCSdkWrappedRequestBuilder * _Nonnull)rateLimit:(NSTimeInterval)seconds __attribute__((warn_unused_result("")));
	[Export ("rateLimit:")]
	SFMCSdkWrappedRequestBuilder RateLimit (double seconds);

	// -(SFMCSdkWrappedRequestBuilder * _Nonnull)name:(NSString * _Nonnull)name __attribute__((warn_unused_result("")));
	[Export ("name:")]
	SFMCSdkWrappedRequestBuilder Name (string name);

	// -(SFMCSdkWrappedRequestBuilder * _Nonnull)tag:(NSString * _Nonnull)tag __attribute__((warn_unused_result("")));
	[Export ("tag:")]
	SFMCSdkWrappedRequestBuilder Tag (string tag);

	// -(SFMCSdkWrappedRequest * _Nullable)build __attribute__((warn_unused_result("")));
	[NullAllowed, Export ("build")]
	SFMCSdkWrappedRequest Build ();
}

// @interface SFMCSdkWrappedResponse : NSObject
[BaseType (typeof(NSObject))]
[DisableDefaultCtor]
interface SFMCSdkWrappedResponse
{
	// @property (readonly, copy, nonatomic) NSData * _Nullable data;
	[NullAllowed, Export ("data", ArgumentSemantic.Copy)]
	NSData Data { get; }

	// @property (readonly, nonatomic, strong) NSHTTPURLResponse * _Nullable response;
	[NullAllowed, Export ("response", ArgumentSemantic.Strong)]
	NSHttpUrlResponse Response { get; }

	// @property (readonly, nonatomic) NSError * _Nullable error;
	[NullAllowed, Export ("error")]
	NSError Error { get; }

	// @property (readonly, nonatomic) BOOL isSuccess;
	[Export ("isSuccess")]
	bool IsSuccess { get; }

	// @property (readonly, nonatomic) BOOL isUnauthorized;
	[Export ("isUnauthorized")]
	bool IsUnauthorized { get; }

	// @property (readonly, nonatomic) int64_t timeToExecute;
	[Export ("timeToExecute")]
	long TimeToExecute { get; }

	// -(instancetype _Nonnull)initWithData:(NSData * _Nullable)data response:(NSURLResponse * _Nullable)response error:(NSError * _Nullable)error startTimeMillis:(int64_t)startTimeMillis endTimeMillis:(int64_t)endTimeMillis __attribute__((objc_designated_initializer));
	[Export ("initWithData:response:error:startTimeMillis:endTimeMillis:")]
	[DesignatedInitializer]
	NativeHandle Constructor ([NullAllowed] NSData data, [NullAllowed] NSUrlResponse response, [NullAllowed] NSError error, long startTimeMillis, long endTimeMillis);
}
}
