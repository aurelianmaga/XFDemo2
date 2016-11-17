using System;
using Xamarin.Forms;

namespace XFDemo2.UI
{
	[Navigation.PageViewModel(typeof(ViewModels.SectionPageViewModel))]
	public partial class SectionPage : ContentPage
	{
		public SectionPage()
		{
			InitializeComponent();
		}

    protected override void OnBindingContextChanged()
    {
      base.OnBindingContextChanged();
      UpdateFooterHeight();
    }

    private void UpdateFooterHeight()
    {
      const int minHeight = 100;

      var sectionPageViewModel = SectionsListView.BindingContext as ViewModels.SectionPageViewModel;

      var requestedHeight = Math.Floor(App.Device.Height - (35 *  sectionPageViewModel.Sections.Count) - App.Device.StatusBarHeight - 150);
      ListViewFooterWrapper.HeightRequest = requestedHeight >= minHeight ? requestedHeight : minHeight;
    }
	}
}

