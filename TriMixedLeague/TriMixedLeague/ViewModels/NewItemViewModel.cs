using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TriMixedLeague.Models;
using Xamarin.Forms;
using BowlerLib;

namespace TriMixedLeague.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        public Extract extract = new Extract();
        private string bowlername;
        private int average;
        //private List<Bowler> bowlers;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(bowlername);
               // && !String.IsNullOrWhiteSpace(description);
        }

        public string Text
        {
            get => bowlername;
            set => SetProperty(ref bowlername, value);
        }

        public int Description
        {
            get => average;
            set => SetProperty(ref average, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            extract.addbowler(Text);
            
            //await DataStore.AddItemAsync(newItem);
            //bowlers = new List<Bowlerl>();

            //Global.bowlerslist.Add(new Bowler
            //{
            //    Id = newItem.Id,
            //    Name = newItem.Name

            //});

            

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
