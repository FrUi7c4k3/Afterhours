using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace MiX.AM.DT.Diagnostics.AssetViewer.ViewModels
{
	[Export(typeof(ShellViewModel))]
	public class ShellViewModel : Conductor<Screen>
	{
		public static readonly DashboardViewModel _dashboard = new DashboardViewModel();
		
		[ImportingConstructor]
		public ShellViewModel()
		{
			ShowLoginScreen();
		}

		public void ShowLoginScreen()
		{
			ActivateItem(new LoginViewModel(ShowDashboard));
		}

		public void ShowDashboard()
		{
			ActivateItem(_dashboard);
		}
	}
}
