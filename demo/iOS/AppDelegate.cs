using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using iZettle;
using UIKit;

namespace iZettleXfQs.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        const string clientId = @"<client id from developer portal>";
        const string callbackURL = @"izettle-iZorn://login.callback";

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            NSError error = null;
            var authorizationProvider = new iZettleSDKAuthorization(
                clientId,
                NSUrl.FromString(callbackURL),
                error,
                () => AccountManager.Shared.EnforcedUserAccount);

            iZettleSDK.Shared.StartWithAuthorizationProvider(authorizationProvider);

            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }

    public class AccountManager
    {
        public string EnforcedUserAccount { get; set; }

        private static AccountManager accountManager;
        public static AccountManager Shared
        {
            get
            {
                if (accountManager == null)
                {
                    accountManager = new AccountManager();
                }

                return accountManager;
            }
        }
    }
}
