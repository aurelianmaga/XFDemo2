using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LCA.Navigation
{
	public class AppPageNavigator : IPageNavigator
	{
		private App _app;
		private Page _root;
		private Stack<Page> _pageStack;


		private AppPageNavigator(App app, Page mainPage)
		{
			_app = app;
			_root = mainPage;
			_pageStack = new Stack<Page>(5);
		}

		static public IPageNavigator GetPageNavigator(App app, Page mainPage)
		{
			if (_navigator != null)
			{
				return _navigator;
			}

			_navigator = new AppPageNavigator(app, mainPage);

			return _navigator;
		}

		private static IPageNavigator _navigator;
		public static IPageNavigator Navigator { get { return _navigator; } }

		public Page Root { get { return _root; } }

		public void Push(Page page)
		{
			_pageStack.Push(_app.MainPage);
			_app.MainPage = page;
		}

		public void Pop()
		{
			var page = _pageStack.Pop();
			_app.MainPage = page;
		}
	}
}

