using System.Windows.Input;
using Xamarin.Forms;
using XFDemo2.Localization;

namespace XFDemo2.ViewModels
{
  public class TopicPageViewModel : ViewModel
  {
    public TopicPageViewModel(string topicId)
    {
      BackCommand = new Command(Back);
      ShowSectionPageCommand = new Command<string>(ShowSectionPage);

      TopicName = topicId.LocalizeResource();

      var topicTextId = string.Format("{0}.Text", topicId);
      TopicText = topicTextId.LocalizeResource();
    }

    public TopicPageViewModel(string topicName, string topicText)
    {
      BackCommand = new Command(Back);

      TopicName = topicName;
      TopicText = topicText;
    }

    public string TopicName { get; set; }

    public string TopicText { get; set; }

    public HtmlWebViewSource HtmlContent { get; set; }

    // ICommand implementations
    public ICommand BackCommand { private set; get; }
    public ICommand ShowTopicCommand { private set; get; }

    private void Back()
    {
      ViewModelNavigator.PopAsync();
    }

    // ICommand implementations
    public ICommand ShowSectionPageCommand { private set; get; }

    private void ShowSectionPage(string sectionId)
    {
      ViewModelNavigator.PushAsync(new SectionPageViewModel(sectionId));
    }
  }
}

