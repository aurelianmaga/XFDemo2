using Xamarin.Forms;

namespace XFDemo2.UI
{
	[Navigation.PageViewModel(typeof(ViewModels.TopicPageViewModel))]
	public partial class TopicPage : ContentPage
	{
		public TopicPage()
		{
			InitializeComponent();
		}
	}
}

