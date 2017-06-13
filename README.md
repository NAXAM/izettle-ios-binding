# Xamarin iZettle iOS SDK Binding Library

The iZettle SDK makes it possible to accept card payments with an iZettle card reader from any iOS app.
When triggered, it will display a screen over the host application where all interaction takes place. It 
is designed to be easy to implement and use. 

#### Main features
- Take card payments with an iZettle card reader.
- Refund card payments.
- Receive information about a payment.
- Login/logout of iZettle accounts and simple switching between multiple accounts.
- Settings screen where the user can handle card readers and access help and support.

#### Limitations:
- The SDK will only work on markets where iZettle is operating, please visit izettle.com for more information.
- It does not currently support other payment methods than cards.

#### Requirements
* iOS 8 or later
* Xcode 7 (iOS 9 SDK)
* An iZettle API Key (please visit [http://developer.izettle.com](http://developer.izettle.com/) in order to obtain one)


## Installation

### 1. Install NUGET package
```
Install-Package Naxam.iZettle.iOS
```

### 2. Setup external accessory protocols in info.plist

Add/modify the property "Supported external accessory protocols" and add *com.miura.shuttle.izettle*

This is what it should look like in the "source code" view of your info.plist:

    <key>UISupportedExternalAccessoryProtocols</key>
    <array>
        <string>com.izettle.cardreader-one</string>
        <string>com.miura.shuttle.izettle</string>
    </array>
    
**Important**

The iZettle bluetooth card readers are part of the Apple MFi program. In order to release apps supporting
accessories that are part of the MFi Program, you have to apply at Apple. Please contact us at 
[sdk@izettle.com](mailto:sdk@izettle.com) and we will help you with this process.

### 3. Setup CLLocationManager texts in info.plist

iZettle will prompt the user for permission during the first payment if the merchant haven't already 
granted your app this permission. On iOS8, iZettle will execute CLLocationManagers method 
`requestWhenInUseAuthorization`.

Add the keys:

- **NSLocationUsageDescription** (iOS7)
- **NSLocationWhenInUseUsageDescription** (iOS8)

Suggested value for the above keys is `"You need to allow this to be able to accept card payments."`

iZettle won't accept payments without these texts implemented.

### 4. Include the framework headers and start the SDK

Before you execute any operations in iZettle SDK, you have to start the SDK with your API key. This is 
typically done in your AppDelegates method `application:didFinishLaunchingWithOptions:`.

```c#
iZettleSDK.Shared.StartWithAPIKey("{YOUR_KEY_COME_HERE}");
```
    
Example:

```c#
public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
{
    iZettleSDK.Shared.StartWithAPIKey("{YOUR_KEY_COME_HERE}");
    return true;
}
```

## Operations

**Important:** Only use the singleton instance returned from `iZettleSDK.Shared` when calling the methods below.

Asynchronous operations have a completion block as an argument, the completion block is called when an 
operation is considered complete, cancelled or failed. See **iZettleOperationCompletion** for more information.

iZettle SDK will handle presentation and dismissal of its views. Operations with UI will accept a UIViewController 
as an argument from which iZettle SDK will be presented.

If the user isn't yet authenticated with iZettle when an operation is presented, a login screen will automatically
be displayed.

### Enforced User Account

- **EnforcedUserAccount** _(optional)_: If set, operations will be restricted to only work for the specified iZettle username.

By setting the shared instance property **EnforcedUserAccount** to an iZettle username, subsequent operations will be 
restricted to only be performed on that account. If the set enforced user account was not previosuly logged in, an 
iZettle login prompt will be presented with a readonly email field prefilled with the enforced user account. If the 
enforced user account was already logged in (even though another account has been used in between), the account will 
be switched to use the enforced user account account instead. 

If **EnforcedUserAccount** is set to nil, any iZettle account can be used, and the email field will be editable.

Enforced user account can be changed between operations to allow switching between different users for different 
operations. This is useful for integrators supporting multiple accounts. 

Preferably integrating apps will provide a settings page where the user can enter their iZettle account (used to 
set the enforced user account).

### Charge

Perform a payment with an amount and a reference.

```c#
void ChargeAmount(
    NSDecimalNumber amount, 
    string currency, 
    string reference, 
    UIViewController viewController,
    iZettleOperationCompletion completion);
```

- **amount**: The amount to be charged in the logged in users currency.
- **currency** _(optional)_: Only used for validation. If the value of this parameter doesn't match the users currency the user will be notified and then logged out. For a complete list of valid currency codes, see [ISO 4217](http://www.xe.com/iso4217.php)
- **reference** _(optional)_: The payment reference. Used to identify an iZettle payment, used when retrieving payment information at a later time or performing a refund. Max length 128.

### Refund
 
Refund an amount from a payment with a given reference.
 
```c#
void RefundAmount(
    NSDecimalNumber amount, 
    string reference, 
    string refundReference, 
    UIViewController viewController,  
    iZettleSDKOperationCompletion completion);
```
	
- **amount** _(optional)_: The amount to be refunded from the payment (passing `nil` will refund full amount of original payment)
- **reference**: The reference of the payment that is to be refunded.
- **refundReference** _(optional)_: The reference of the refund. Max length 128.

### Retrieve payment info

Query iZettle for payment information of a payment with a given reference.

```c#
void RetrievePaymentInfoForReference(
    string reference,
    iZettleOperationCompletion completion);
```

### Present settings

Present iZettle settings view. The user can switch account, access the iZettle FAQ, view card reader settings etc.

```c#
void PresentSettingsFromViewController(UIViewController viewController);
```
 
### Abort operation

Attempt aborting the ongoing operation. Only use this if absolutely necessary. The state of the payment will 
be unknown to the user after this call.

```c#
void AbortOperation();
```

## iZettleOperationCompletion

### iZettlePaymentInfo

Object that contains information about a payment and the card used.

- **ReferenceNumber** - iZettles reference to the payment (not to be confused with the reference provided by you during a charge or refund operation)
- **EntryMode** - EMV, CONTACTLESS_EMV, MAGSTRIPE_CONTACTLESS, MAGSTRIPE etc. More entry modes might be added independent of SDK version
- **ObfuscatedPan** - e.g. _"\*\*\*\* \*\*\*\* \*\*\*\* 1111"_
- **PanHash** - a hash of the pan
- **CardBrand**
- **AuthorizationCode**
- **AID***
- **TSI***
- **TVR***
- **ApplicationName***
- **NumberOfInstallments***
- **InstallmentAmount***

\* These fields are only available for some entry modes. Don't rely on them being present.

#### Example of a card reader chip payment:

	entryMode = EMV
	obfuscatedPan = "**** **** **** 0640"
	panHash = 0092C7D95900033B84CE08B43F7E973485FB7081
	cardBrand = MASTERCARD
    authorizationCode = 007602
    AID = A0000000041010
    TSI = 4000
    TVR = 8000000000
    applicationName = MasterCard

#### Example of a card reader contactless payment:

	entryMode = CONTACTLESS_EMV
	obfuscatedPan = "**** **** **** 0640"
	panHash = 0092C7D95900033B84CE08B43F7E973485FB7081
	cardBrand = MASTERCARD
    authorizationCode = 007602
    AID = A0000000041010
    TVR = 8000000000
    applicationName = MasterCard
        
#### Example of a card reader swipe payment:

    entryMode = MAGSTRIPE
    obfuscatedPan = "**** **** **** 2481"
    panHash = 99426D012C6740D9AEC8E26580E8640A196E3C27
    cardBrand = MASTERCARD
    authorizationCode = 004601
 
### Errors

iZettle will display any errors that occur during an operation to the user, the NSError-object returned in 
the operation completion block is only intended to be used by developers for debugging, diagnostics and 
logging to be able to better communicate errors to iZettle. You should never present the returned errors to
the end user.
