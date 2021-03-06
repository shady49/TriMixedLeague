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
    public partial class BowlersListPage : ContentPage
    {
        BowlerViewModel _viewModel;
        public BowlersListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new BowlerViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}