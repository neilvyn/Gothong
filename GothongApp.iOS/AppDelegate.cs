using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace GothongApp.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            App.ScreenWidth = (float)UIScreen.MainScreen.Bounds.Width;
            App.ScreenHeight = (float)UIScreen.MainScreen.Bounds.Height;
            App.NativeScale = UIScreen.MainScreen.Scale;
            var StatusBarSize = UIApplication.SharedApplication.StatusBarFrame.Size;
            App.StatusBarHeight = Math.Min(StatusBarSize.Width, StatusBarSize.Height);

            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App(new iOSInitializer()));
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            Rg.Plugins.Popup.Popup.Init();

            return base.FinishedLaunching(app, options);
        }
    }

    #region prism platform init
    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
    #endregion
}
