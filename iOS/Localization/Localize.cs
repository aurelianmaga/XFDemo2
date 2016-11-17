using System.Globalization;
using System.Threading;
using Foundation;
using XFDemo2.Localization;
using Xamarin.Forms;

[assembly:Dependency(typeof(XFDemo2.iOS.Localization.Localize))]
namespace XFDemo2.iOS.Localization
{
  public class Localize : LocalizeBase
  {
    public override void SetLocale(CultureInfo cultureInfo)
    {
      Thread.CurrentThread.CurrentCulture = cultureInfo;
      Thread.CurrentThread.CurrentUICulture = cultureInfo;
    }

    public override CultureInfo GetCurrentCultureInfo()
    {
      var netLanguage = "en";
      if (NSLocale.PreferredLanguages.Length > 0)
      {
        var pref = NSLocale.PreferredLanguages[0];
        netLanguage = PlatformToDotnetLanguage(pref);
      }

      return GetCurrentCultureInfoFor(netLanguage);
    }
  }
}