using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Util;

namespace XFDemo2.Droid
{
	[Activity(Label = "XFDemo2", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
      var dimensions = GetDimensions();
      App.SetScreenDimensions(dimensions.Item1, dimensions.Item2);

			LoadApplication(new App());
		}

    private Tuple<float, float> GetDimensions()
    {
      var displaymetrics = new DisplayMetrics();
      WindowManager.DefaultDisplay.GetMetrics(displaymetrics);
      var height = (displaymetrics.HeightPixels / displaymetrics.Density);
      var width = displaymetrics.WidthPixels / displaymetrics.Density;

      return new Tuple<float, float>(height, width);
    }
	}
}

