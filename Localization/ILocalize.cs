using System.Globalization;

namespace XFDemo2.Localization
{
  public interface ILocalize
  {
    CultureInfo GetCurrentCultureInfo();
    void SetLocale(CultureInfo cultureInfo);
  }
}
