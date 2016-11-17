using System.ComponentModel;
using System.Runtime.CompilerServices;
using XFDemo2.Navigation;

namespace XFDemo2.ViewModels
{
	public abstract class ViewModel : INotifyPropertyChanged
	{
		public IViewModelNavigator ViewModelNavigator { get; set; }

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

