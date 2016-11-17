using System;
namespace LCA.Navigation
{
	public interface INavigatingViewModel
	{
		IViewModelNavigator ViewModelNavigator { get; set; }
	}
}

