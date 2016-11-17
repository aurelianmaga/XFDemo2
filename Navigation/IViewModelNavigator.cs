using System.Threading.Tasks;
using XFDemo2.ViewModels;

namespace XFDemo2.Navigation
{
	public interface IViewModelNavigator
	{
		Task<ViewModel> PopAsync();

		Task<ViewModel> PopModalAsync();

		Task PopToRootAsync();

		Task PushAsync(ViewModel viewModel);

		Task PushModalAsync(ViewModel viewModel);
	}
}

