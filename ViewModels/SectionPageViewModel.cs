using System.Windows.Input;
using Xamarin.Forms;
using XFDemo2.Localization;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace XFDemo2.ViewModels
{
  public class Section : ViewModel
  {
    public string SectionId { get; set; }
    public string SectionName { get; set; }

    private bool _hasSubSections;
    public virtual bool HasSubSections
    {
      get
      {
        return _hasSubSections;
      }

      set
      {
        _hasSubSections = value;
        OnPropertyChanged();
      }
    }

    public override string ToString()
    {
      return SectionId;
    }
  }

  public class EndSection : Section
  {
    public EndSection()
    {
      HasSubSections = false;
    }
  }

  public class TelSection : EndSection
  {
    public string PhoneNumber { get; set; }
  }

  public class TopicSection : Section
  {
    public TopicSection()
    {
      HasSubSections = true;
    }
  }


  public class SectionPageViewModel : ViewModel
  {
    public SectionPageViewModel(string sectionId)
    {
      BackCommand = new Command(Back);
      ShowSectionCommand = new Command<Section>(ShowSection);
      ShowSectionPageCommand = new Command<string>(ShowSectionPage);

      SectionName = string.Format("{0}", sectionId).LocalizeResource();
      ImageName = string.Format("{0}Small.png", sectionId);

      Sections = LoadSubSectionsBy(sectionId);
      _selectedSection = null;
    }

    List<Section> LoadSubSectionsBy(string sectionId)
    {
      var subSections = new List<Section>();

      int subSectionNumber = 0;
      do
      {
        var subSection = LoadSubSectionBySubSection(sectionId, subSectionNumber, "{0}.{1}");

        if (subSection == null)
        {
          subSection = LoadSubSectionByEndSection(sectionId, subSectionNumber, "{0}.{1}.End");
        }

        if (subSection == null)
        {
          subSection = LoadSubSectionByTelSection(sectionId, subSectionNumber, "{0}.{1}.Tel");
        }


        if (subSection == null)
        {
          subSection = LoadSubSectionByTopic(sectionId, subSectionNumber, "{0}.{1}.Topic");
        }

        if (subSection == null)
        {
          break;
        }

        subSections.Add(subSection);
        subSectionNumber++;
      } while (true);

      return subSections;
    }

    Section LoadSubSectionBySubSection(string sectionId, int subSectionNumber, string subSectionTemplate)
    {
      var subSectionId = string.Format(subSectionTemplate, sectionId, subSectionNumber);

      var subSectionName = subSectionId.LocalizeResource();
      if (subSectionName.Equals(subSectionId))
      {
        return null;
      }

      return new Section { SectionId = subSectionId, SectionName = subSectionName, HasSubSections = true };
    }

    Section LoadSubSectionByEndSection(string sectionId, int subSectionNumber, string subSectionTemplate)
    {
      var subSectionId = string.Format(subSectionTemplate, sectionId, subSectionNumber);

      var subSectionName = subSectionId.LocalizeResource();
      if (subSectionName.Equals(subSectionId))
      {
        return null;
      }

      return new EndSection { SectionId = subSectionId, SectionName = subSectionName };
    }

    Section LoadSubSectionByTopic(string sectionId, int subSectionNumber, string subSectionTemplate)
    {
      var subSectionId = string.Format(subSectionTemplate, sectionId, subSectionNumber);

      var subSectionName = subSectionId.LocalizeResource();
      if (subSectionName.Equals(subSectionId))
      {
        return null;
      }

      return new TopicSection { SectionId = subSectionId, SectionName = subSectionName };
    }

    Section LoadSubSectionByTelSection(string sectionId, int subSectionNumber, string subSectionTemplate)
    {
      var subSectionId = string.Format(subSectionTemplate, sectionId, subSectionNumber);

      var subSectionName = subSectionId.LocalizeResource();
      if (subSectionName.Equals(subSectionId))
      {
        return null;
      }

      return new TelSection { SectionId = subSectionId, SectionName = subSectionName, PhoneNumber = ParsePhoneNumberFromString(subSectionName) };
    }

    private string ParsePhoneNumberFromString(string phoneNumberAsString)
    {
      if (string.IsNullOrWhiteSpace(phoneNumberAsString))
      {
        return string.Empty;
      }

      // remove charachters, keep + and numbers
      var phoneNumber = Regex.Replace(phoneNumberAsString, "[^+0-9]", string.Empty);
      return phoneNumber;
    }

    public List<Section> Sections { get; set; }

    public string SectionName { get; set; }
    public string ImageName { get; set; }

    public int FooterHeight { get; set; }

    // TODO: replace with EventToCommand behavior
    Section _selectedSection;
    public Section SelectedSection
    {
      set
      {
        if (value == null)
          return;

        _selectedSection = null;
        OnPropertyChanged();
        ShowSection(value);
      }

      get
      {
        return _selectedSection;
      }
    }

    // ICommand implementations
    public ICommand BackCommand { private set; get; }
    public ICommand ShowSectionCommand { private set; get; }

    private void Back()
    {
      ViewModelNavigator.PopAsync();
    }

    private void ShowSection(Section section)
    {
      if (section is TopicSection)
      {
        ViewModelNavigator.PushAsync(new TopicPageViewModel(section.SectionId));
        return;
      }

      var telSection = section as TelSection;
      if (telSection != null)
      {
        //Device.OpenUri(new Uri(string.Format("tel:{0}", telSection.PhoneNumber)));
        ViewModelNavigator.PushAsync(new TopicPageViewModel("Calling...", telSection.PhoneNumber));
        return;
      }

      if (section is EndSection)
      {
        return;
      }

      if (section.HasSubSections)
      {
        ViewModelNavigator.PushAsync(new SectionPageViewModel(section.SectionId));
        return;
      }
    }

    // ICommand implementations
    public ICommand ShowSectionPageCommand { private set; get; }

    private void ShowSectionPage(string sectionId)
    {
      ViewModelNavigator.PushAsync(new SectionPageViewModel(sectionId));
    }
  }
}

