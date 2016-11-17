using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace XFDemo2.Navigation
{
	public static class NavigationHelper
	{
		static public Dictionary<Type, Type> MapViewModelTypeToPageType()
		{
			var ViewModelTypeToPageType = new Dictionary<Type, Type>();

			var currentDomain = typeof(string).GetTypeInfo().Assembly.GetType("System.AppDomain").GetRuntimeProperty("CurrentDomain").GetMethod.Invoke(null, new object[] { });
			var getAssemblies = currentDomain.GetType().GetRuntimeMethod("GetAssemblies", new Type[] { });
			var assemblies = getAssemblies.Invoke(currentDomain, new object[] { }) as Assembly[];

			var allTypes = assemblies.SelectMany(a => a.DefinedTypes);
			var typesWithRegisterAttributes = allTypes
				.Select(t => new { TypeInfo = t, Attribute = t.GetCustomAttribute<PageViewModelAttribute>() })
				.Where(p => p.Attribute != null);
			foreach (var pair in typesWithRegisterAttributes)
			{
				if (!typeof(Page).GetTypeInfo().IsAssignableFrom(pair.TypeInfo))
				{
					var message = string.Format(
						"PageViewModelAttribute applied to a class ({0}) that is not a Page",
						pair.TypeInfo.FullName);
					throw new InvalidOperationException(message);
				}
				if (ViewModelTypeToPageType.ContainsKey(pair.Attribute.ViewModelType))
				{
					var message = string.Format(
						"Multiple Page types (new = {0}, previous = {1}) registered for the same view model type ({2})",
						pair.TypeInfo.FullName,
						ViewModelTypeToPageType[pair.Attribute.ViewModelType].FullName,
						pair.Attribute.ViewModelType.FullName
					);
					throw new InvalidOperationException(message);
				}
				ViewModelTypeToPageType[pair.Attribute.ViewModelType] = pair.TypeInfo.AsType();
			}

			return ViewModelTypeToPageType;
		}
	}
}

