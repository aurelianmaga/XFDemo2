using Foundation;
using UIKit;

namespace XFDemo2.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			LoadApplication(new App());
      App.SetScreenDimensions((float)UIScreen.MainScreen.Bounds.Height, (float)UIScreen.MainScreen.Bounds.Width);

			return base.FinishedLaunching(app, options);
		}
	}
}

