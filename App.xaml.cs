using Xamarin.Forms;
using XFDemo2.Navigation;
using XFDemo2.Localization;

namespace XFDemo2
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
      SetLocalLanguage();

			MainPage = CreateMainPage();
		}

		private static Page CreateMainPage()
		{
			var navigationMap = NavigationHelper.MapViewModelTypeToPageType();

			var mainViewModel = new ViewModels.MainViewModel();

			var viewModelToPageNavigator = new ViewModelToPageNavigator(mainViewModel, navigationMap);
			return viewModelToPageNavigator.Root;
		}


		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

    private void SetLocalLanguage()
    {
      if (Xamarin.Forms.Device.OS == TargetPlatform.iOS || Xamarin.Forms.Device.OS == TargetPlatform.Android)
      {
        var currentCultureInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        AppResources.Culture = currentCultureInfo; // set the RESX for resource localization
        DependencyService.Get<ILocalize>().SetLocale(currentCultureInfo); // set the Thread for locale-aware methods
      }
    }

    public static void SetScreenDimensions(float height, float width)
    {
      Device.Width = width;
      Device.Height = height;
    }

    public class Device
    {
      public static float Height { get; set; }
      public static float Width { get; set; }

      public static float StatusBarHeight
      {
        get
        {
          switch (Xamarin.Forms.Device.OS)
          {
            case TargetPlatform.Android:
              return 25; // 25 seems to be the standard height.
            default:
              return 0;
          }
        }
      }
    }
  }
}

