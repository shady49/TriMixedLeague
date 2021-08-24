using System;
using System.Collections.Generic;
using TriMixedLeague.ViewModels;
using TriMixedLeague.Views;
using Xamarin.Forms;

namespace TriMixedLeague
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RecapDetailPage), typeof(RecapDetailPage));
            Routing.RegisterRoute(nameof(BowlerDetailPage), typeof(BowlerDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
