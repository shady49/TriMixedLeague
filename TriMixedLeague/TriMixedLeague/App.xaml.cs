using System;
using TriMixedLeague.Services;
using TriMixedLeague.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriMixedLeague
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<BowlerDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
