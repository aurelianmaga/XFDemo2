namespace XFDemo2.Localization
{
  public static class LocalizationHelper
  {
    public static string LocalizeResource(this string resourceName)
    {
      var extention = new LocalizationExtension();
      extention.ResourceName = resourceName;
      return extention.ProvideValue(null).ToString();
    }
  }
}
