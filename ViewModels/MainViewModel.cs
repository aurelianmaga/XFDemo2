using System.Windows.Input;
using Xamarin.Forms;

namespace XFDemo2.ViewModels
{
	public class MainViewModel : ViewModel
	{
		public MainViewModel()
		{
			ShowSectionPageCommand = new Command<string>(ShowSectionPage);
		}

		// ICommand implementations
		public ICommand ShowSectionPageCommand { private set; get; }

		private void ShowSectionPage(string sectionId)
		{
			ViewModelNavigator.PushAsync(new SectionPageViewModel(sectionId));
		}
	}
}

