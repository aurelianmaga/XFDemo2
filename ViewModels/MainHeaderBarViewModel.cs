using Xamarin.Forms;
using System.Windows.Input;

namespace XFDemo2.ViewModels
{
	public class MainHeaderBarViewModel : ViewModel
	{
    public MainHeaderBarViewModel()
    {
      ShowSectionCommand = new Command<string>(ShowSection); 
    }
    
    // ICommand implementations
    public ICommand ShowSectionCommand { private set; get; }

    private void ShowSection(string sectionId)
    {
      ViewModelNavigator.PushAsync(new SectionPageViewModel(sectionId));
    }
	}
}

