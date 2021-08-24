using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriMixedLeague.Models;
using TriMixedLeague.ViewModels;
using TriMixedLeague.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriMixedLeague.Services;

namespace TriMixedLeague.Views
{
    public partial class RecapPage : ContentPage
    {
        RecapViewModel _viewModel;

        public RecapPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new RecapViewModel();
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                _viewModel.OnAppearing();
            }
            catch
            {
                Console.WriteLine("Error");
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Bowler> Bs;
            var bs = BowlerDataStore.bowlers.Where(b=>b.Name.ToLower().Contains(e.NewTextValue.ToLower()));
            Bs = bs.ToList();
            if (Bs.Count == 1)
            {
                
            }
        }
    }
}