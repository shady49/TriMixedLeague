using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TriMixedLeague.Models;
using TriMixedLeague.Views;
using Xamarin.Forms;

namespace TriMixedLeague.ViewModels
{
    public class RecapViewModel : BaseViewModel
    {
        private Bowler _selectedItem;

        public ObservableCollection<Bowler> Bowlers { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Bowler> ItemTapped { get; }

        public RecapViewModel()
        {
            Title = "Recap";
            Bowlers = new ObservableCollection<Bowler>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Bowler>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Bowlers.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Bowlers.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Bowler SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Bowler item)
        {
            if (item == null)
                return;
            Global.indicator = item.Indicator;
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(RecapDetailPage)}?{nameof(RecapDetailViewModel.ItemId)}={item.Id}");
        }
    }
}