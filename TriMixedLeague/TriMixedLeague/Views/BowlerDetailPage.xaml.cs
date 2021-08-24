using System.ComponentModel;
using TriMixedLeague.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TriMixedLeague.Views
{
    public partial class BowlerDetailPage : ContentPage
    {
        public BowlerDetailPage()
        {
            InitializeComponent();
            BindingContext = new BowlerDetailViewModel();
        }
        private async void BowlerHistory(object sender, System.EventArgs e)
        {
            await Browser.OpenAsync(Global.bowlerhist);
        }

        private async void TeamHistory(object sender, System.EventArgs e)
        {
            await Browser.OpenAsync(Global.teamhist);
        }
    }
}