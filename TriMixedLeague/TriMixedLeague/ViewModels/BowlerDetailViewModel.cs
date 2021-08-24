using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using TriMixedLeague.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TriMixedLeague.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class BowlerDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string bowler;
        private int average;
        private int handicap;
        //private string bowlerhttp;
        //private string teamhttp;

        private const string league = "https://www.leaguesecretary.com/";
        public string Id { get; set; }

        public string Bowler
        {
            get => bowler;
            set => SetProperty(ref bowler, value);
        }

        public int Average
        {
            get => average;
            set => SetProperty(ref average, value);
        }

        public int Handicap
        {
            get => handicap;
            set => SetProperty(ref handicap, value);
        }

        //public string Bowlerhttp
        //{
        //    get => bowlerhttp;
        //    set => SetProperty(ref bowlerhttp, value);
        //}

        //public string Teamhttp
        //{
        //    get => teamhttp;
        //    set => SetProperty(ref teamhttp, value);
        //}

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public BowlerDetailViewModel()
        {
            Title = "Bowler History";
            //Bowlerhttp = new Command(async () => await Browser.OpenAsync(league + item.BowlerHTTP));
        }

        public ICommand Bowlerhttp { get; set; }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await BowlerStore.GetItemAsync(itemId);
                Id = item.Id;
                Bowler = item.Name;
                Average = item.Average;
                Handicap = item.Handicap;
                Global.bowlerhist = league + item.BowlerHTTP;
                Global.teamhist = league + item.TeamHTTP;
                Bowlerhttp = new Command(async () => await Browser.OpenAsync(league + item.BowlerHTTP));
                //Bowlerhttp = league + item.BowlerHTTP;
                //Teamhttp = league + item.TeamHTTP;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
