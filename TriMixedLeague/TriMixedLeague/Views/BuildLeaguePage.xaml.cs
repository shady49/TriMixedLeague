using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriMixedLeague.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriMixedLeague.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BuildLeaguePage : ContentPage
	{
		public BuildLeaguePage()
		{
			InitializeComponent();
			BindingContext = new BuildLeagueModel();
		}
		public void Select_Location(object sender, EventArgs e)
		{

		}
	}
}