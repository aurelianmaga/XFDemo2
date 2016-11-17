using System.Globalization;

namespace XFDemo2.Localization
{
  public abstract class LocalizeBase : ILocalize
  {
    public abstract CultureInfo GetCurrentCultureInfo();
    public abstract void SetLocale(CultureInfo cultureInfo);

    protected CultureInfo GetCurrentCultureInfoFor(string netLanguage)
    {
      // this gets called a lot - try/catch can be expensive so consider caching or something
      CultureInfo currentCultureInfo = null;
      try
      {
        currentCultureInfo = new System.Globalization.CultureInfo(netLanguage);
      }
      catch (CultureNotFoundException)
      {
        // iOS locale not valid .NET culture (eg. "en-ES" : English in Spain)
        // fallback to first characters, in this case "en"
        try
        {
          var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
          currentCultureInfo = new CultureInfo(fallback);
        }
        catch (CultureNotFoundException)
        {
          currentCultureInfo = new CultureInfo("en");
        }
      }

      return currentCultureInfo;
    }

    protected string PlatformToDotnetLanguage(string platformLanguage)
    {
      var netLanguage = platformLanguage;

      //certain languages need to be converted to CultureInfo equivalent
      switch (platformLanguage)
      {
        case "ms-BN":   // "Malaysian (Brunei)" not supported .NET culture
        case "ms-MY":   // "Malaysian (Malaysia)" not supported .NET culture
        case "ms-SG":   // "Malaysian (Singapore)" not supported .NET culture
          netLanguage = "ms"; // closest supported
          break;
        case "in-ID":  // "Indonesian (Indonesia)" has different code in  .NET
          netLanguage = "id-ID"; // correct code for .NET
          break;
        case "gsw-CH":  // "Schwiizertüütsch (Swiss German)" not supported .NET culture
          netLanguage = "de-CH"; // closest supported
          break;
          // add more application-specific cases here (if required)
          // ONLY use cultures that have been tested and known to work
      }
      return netLanguage;
    }

    protected string ToDotnetFallbackLanguage(PlatformCulture platformCulture)
    {
      var netLanguage = platformCulture.LanguageCode; // use the first part of the identifier (two chars, usually);
      switch (platformCulture.LanguageCode)
      {
        case "pt":
          netLanguage = "pt-PT"; // fallback to Portuguese (Portugal)
          break;
        case "gsw":
          netLanguage = "de-CH"; // equivalent to German (Switzerland) for this app
          break;
          // add more application-specific cases here (if required)
          // ONLY use cultures that have been tested and known to work
      }
      return netLanguage;
    }
  }
}
