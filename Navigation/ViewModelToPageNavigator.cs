using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

using XFDemo2.ViewModels;

namespace XFDemo2.Navigation
{
	public class ViewModelToPageNavigator : IViewModelNavigator
	{
		readonly Dictionary<Type, Type> ViewModelTypeToPageType;

		readonly NavigationPage _navigationPage;

		public ViewModelToPageNavigator(ViewModel rootViewModel, Dictionary<Type, Type> viewModelTypeToPageTypeMap)
		{
			ViewModelTypeToPageType = viewModelTypeToPageTypeMap;
			_navigationPage = new NavigationPage(CreatePageForViewModel(rootViewModel));
		}

		public Page Root { get { return _navigationPage; } }

		public async Task<ViewModel> PopAsync()
		{
			var currentViewModel = CurrentViewModel;
			await _navigationPage.PopAsync();
			return currentViewModel as ViewModel;
		}

		public async Task<ViewModel> PopModalAsync()
		{
			var poppedPage = await _navigationPage.Navigation.PopModalAsync();
			return poppedPage.BindingContext as ViewModel;
		}

		public Task PopToRootAsync()
		{
			return _navigationPage.PopToRootAsync();
		}

		public Task PushAsync(ViewModel viewModel)
		{
			return _navigationPage.PushAsync(CreatePageForViewModel(viewModel));
		}

		public Task PushModalAsync(ViewModel viewModel)
		{
			return _navigationPage.Navigation.PushModalAsync(CreatePageForViewModel(viewModel));
		}

		public object CurrentViewModel
		{
			get
			{
				return _navigationPage.CurrentPage.BindingContext;
			}
		}

		private Page CreatePageForViewModel(ViewModel viewModel)
		{
			Type newPageType = null;

			if (!ViewModelTypeToPageType.TryGetValue(viewModel.GetType(), out newPageType))
			{
				throw new ArgumentException("Trying to create a Page for an unrecognized view model type.");
			}

			var newPage = (Page)Activator.CreateInstance(ViewModelTypeToPageType[viewModel.GetType()]);
			newPage.BindingContext = viewModel;

			NavigationPage.SetHasNavigationBar(newPage, false);

			SetViewModelNavigator(viewModel, this);

			return newPage;
		}

		private static void SetViewModelNavigator(ViewModel viewModel, IViewModelNavigator frame)
		{
			if (viewModel == null)
			{
				return;
			}

			viewModel.ViewModelNavigator = frame;
		}
	}
}

