using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Caliburn.Micro;

namespace MiX.AM.DT.Diagnostics.AssetViewer.ViewModels
{
	[Export(typeof(IDashboardViewModel))]
	public class DashboardViewModel : Screen, IDashboardViewModel
	{
		public DashboardViewModel()
		{
			DisplayName = "MiX Products";
		}
	}
}
