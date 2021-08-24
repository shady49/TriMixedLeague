using System;
using System.Collections.Generic;
using System.ComponentModel;
using TriMixedLeague.Models;
using TriMixedLeague.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriMixedLeague.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Bowler Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}