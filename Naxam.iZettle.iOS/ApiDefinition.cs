using Foundation;
using UIKit;
using ObjCRuntime;

namespace iZettle
{
	// typedef void (^iZettleSDKOperationCompletion)(iZettleSDKPaymentInfo * _Nullable, NSError * _Nullable);
	delegate void iZettleSDKOperationCompletion ([NullAllowed] iZettleSDKPaymentInfo arg0, [NullAllowed] NSError arg1);

	// @interface iZettleSDK : NSObject
	[BaseType (typeof(NSObject))]
	interface iZettleSDK
	{
		// @property (readonly, nonatomic) NSString * _Nonnull version;
		[Export ("version")]
		string Version { get; }

		// +(iZettleSDK * _Nonnull)shared;
		[Static]
		[Export ("shared")]
		iZettleSDK Shared { get; }

		// -(void)startWithAPIKey:(NSString * _Nonnull)apiKey;
		[Export ("startWithAPIKey:")]
		void StartWithAPIKey (string apiKey);

		// @property (copy, nonatomic) NSString * _Nullable enforcedUserAccount;
		[NullAllowed, Export ("enforcedUserAccount")]
		string EnforcedUserAccount { get; set; }
	}

	// @interface Operations (iZettleSDK)
	[Category]
	[BaseType (typeof(iZettleSDK))]
	interface iZettleSDK_Operations
	{
		// -(void)chargeAmount:(NSDecimalNumber * _Nonnull)amount currency:(NSString * _Nullable)currency enableTipping:(BOOL)enableTipping reference:(NSString * _Nullable)reference presentFromViewController:(UIViewController * _Nonnull)viewController completion:(iZettleSDKOperationCompletion _Nonnull)completion;
		[Export ("chargeAmount:currency:enableTipping:reference:presentFromViewController:completion:")]
		void ChargeAmount (NSDecimalNumber amount, [NullAllowed] string currency, bool enableTipping, [NullAllowed] string reference, UIViewController viewController, iZettleSDKOperationCompletion completion);

		// -(void)refundAmount:(NSDecimalNumber * _Nullable)amount ofPaymentWithReference:(NSString * _Nonnull)reference refundReference:(NSString * _Nullable)refundReference presentFromViewController:(UIViewController * _Nonnull)viewController completion:(iZettleSDKOperationCompletion _Nonnull)completion;
		[Export ("refundAmount:ofPaymentWithReference:refundReference:presentFromViewController:completion:")]
		void RefundAmount ([NullAllowed] NSDecimalNumber amount, string reference, [NullAllowed] string refundReference, UIViewController viewController, iZettleSDKOperationCompletion completion);

		// -(void)retrievePaymentInfoForReference:(NSString * _Nonnull)reference presentFromViewController:(UIViewController * _Nonnull)viewController completion:(iZettleSDKOperationCompletion _Nonnull)completion;
		[Export ("retrievePaymentInfoForReference:presentFromViewController:completion:")]
		void RetrievePaymentInfoForReference (string reference, UIViewController viewController, iZettleSDKOperationCompletion completion);

		// -(void)presentSettingsFromViewController:(UIViewController * _Nonnull)viewController;
		[Export ("presentSettingsFromViewController:")]
		void PresentSettingsFromViewController (UIViewController viewController);

		// -(void)abortOperation;
		[Export ("abortOperation")]
		void AbortOperation ();

		// - (void)chargeAmount:(NSDecimalNumber *)amount
		//             currency:(nullable NSString *)currency
		//            reference:(nullable NSString *)reference
		// presentFromViewController:(UIViewController *)viewController
		//           completion:(iZettleSDKOperationCompletion)completion
		// __attribute__((deprecated("Use chargeAmount:currency:enableTipping:reference:presentFromViewController:completion: instead")));
        [Deprecated(PlatformName.iOS, PlatformArchitecture.All, "Use chargeAmount:currency:enableTipping:reference:presentFromViewController:completion: instead"), 
         Export ("chargeAmount:currency:reference:presentFromViewController:completion:")]
		void ChargeAmount (NSDecimalNumber amount, [NullAllowed] string currency, [NullAllowed] string reference, UIViewController presentFromViewController, iZettleSDKOperationCompletion completion);
	}

	// @interface iZettleSDKPaymentInfo : NSObject
	[BaseType (typeof(NSObject))]
	interface iZettleSDKPaymentInfo
	{
		// @property (readonly, nonatomic) NSDictionary<NSString *,id> * _Nonnull dictionary;
		[Export ("dictionary")]
		NSDictionary<NSString, NSObject> Dictionary { get; }

		// @property (readonly, nonatomic) NSDecimalNumber * _Nonnull amount;
		[Export ("amount")]
		NSDecimalNumber Amount { get; }

		// @property (readonly, nonatomic) NSDecimalNumber * _Nullable gratuityAmount;
		[NullAllowed, Export ("gratuityAmount")]
		NSDecimalNumber GratuityAmount { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull referenceNumber;
		[Export ("referenceNumber")]
		string ReferenceNumber { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull entryMode;
		[Export ("entryMode")]
		string EntryMode { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull authorizationCode;
		[Export ("authorizationCode")]
		string AuthorizationCode { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull obfuscatedPan;
		[Export ("obfuscatedPan")]
		string ObfuscatedPan { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull panHash;
		[Export ("panHash")]
		string PanHash { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull cardBrand;
		[Export ("cardBrand")]
		string CardBrand { get; }

		// @property (readonly, nonatomic) NSString * _Nullable AID;
		[NullAllowed, Export ("AID")]
		string AID { get; }

		// @property (readonly, nonatomic) NSString * _Nullable TSI;
		[NullAllowed, Export ("TSI")]
		string TSI { get; }

		// @property (readonly, nonatomic) NSString * _Nullable TVR;
		[NullAllowed, Export ("TVR")]
		string TVR { get; }

		// @property (readonly, nonatomic) NSString * _Nullable applicationName;
		[NullAllowed, Export ("applicationName")]
		string ApplicationName { get; }

		// @property (readonly, nonatomic) NSNumber * _Nullable numberOfInstallments;
		[NullAllowed, Export ("numberOfInstallments")]
		NSNumber NumberOfInstallments { get; }

		// @property (readonly, nonatomic) NSDecimalNumber * _Nullable installmentAmount;
		[NullAllowed, Export ("installmentAmount")]
		NSDecimalNumber InstallmentAmount { get; }
	}
}
