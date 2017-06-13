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
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			iZettleSDK.Shared.StartWithAPIKey("{YOUR_KEY_COME_HERE}");

            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
