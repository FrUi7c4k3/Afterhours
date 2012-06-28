using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace MiX.AM.DT.Diagnostics.AssetViewer.ViewModels
{
	public class DashboardViewModel : Conductor<IScreen>.Collection.OneActive
	{
		public DashboardViewModel()
		{
			DisplayName = "MiX Products";
		}


	}
}
