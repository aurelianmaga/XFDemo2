using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Resources;
using System.Globalization;
using System.Reflection;

namespace XFDemo2.Localization
{
	[ContentProperty("ResourceName")]
	public class LocalizationExtension : IMarkupExtension
	{
		readonly CultureInfo currentCultureInfo;
		const string ResourceId = "XFDemo2.AppResources";

		public LocalizationExtension()
		{
			currentCultureInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
		}

		public string ResourceName { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			if (ResourceName == null)
				return "";

			var resmgr = new ResourceManager(ResourceId
								, typeof(LocalizationExtension).GetTypeInfo().Assembly);

			var translationForResourceName = resmgr.GetString(ResourceName, currentCultureInfo);

			// pass name of resource back if not found
			if (translationForResourceName == null)
			{
				translationForResourceName = ResourceName;
			}

			return translationForResourceName;
		}
	}
}

