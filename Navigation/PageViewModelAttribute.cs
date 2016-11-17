using System;
namespace XFDemo2.Navigation
{
	[AttributeUsage(AttributeTargets.Class)]
	public class PageViewModelAttribute : Attribute
	{
		public Type ViewModelType { get; private set; }

		public PageViewModelAttribute(Type viewModelType)
		{
			ViewModelType = viewModelType;
		}
	}
}

