using Xamarin.Forms;

namespace XFDemo2
{
	[Navigation.PageViewModel(typeof(ViewModels.MainViewModel))]
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}
	}
}

