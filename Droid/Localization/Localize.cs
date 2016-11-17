using System.Globalization;
using System.Threading;
using XFDemo2.Localization;
using Xamarin.Forms;

[assembly:Dependency(typeof(XFDemo2.Android.Localization.Localize))]
namespace XFDemo2.Android.Localization
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
      var androidLocale = Java.Util.Locale.Default;
      var netLanguage = PlatformToDotnetLanguage(androidLocale.ToString().Replace("_", "-"));

      return GetCurrentCultureInfoFor(netLanguage);
    }
  }
}