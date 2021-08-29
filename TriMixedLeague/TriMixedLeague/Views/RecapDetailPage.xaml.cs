using BowlLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriMixedLeague.Models;
using TriMixedLeague.Services;
using TriMixedLeague.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TriMixedLeague.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecapDetailPage : ContentPage
    {
        RecapDetailViewModel calcpts = new RecapDetailViewModel();
        public RecapDetailPage()
        {
            InitializeComponent();
            BindingContext = new RecapDetailViewModel();
            
        }

       

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (e.OldTextValue != null)
            //{
            //    if (e.NewTextValue.Length < e.OldTextValue.Length) return;
            //}
            
            //List<Team> teamlist;
            if (Global.indicator == "b")
            {
                if (Int32.Parse(Hcp.Text) == 0 && Int32.Parse(Game1.Text) > 0 && Int32.Parse(Game2.Text) > 0 && Int32.Parse(Game3.Text) > 0)
                {
                    double hcp = Int32.Parse(Hcp.Text);
                    int ave= ((Int32.Parse(Game1.Text) + Int32.Parse(Game2.Text) + Int32.Parse(Game3.Text)) / 3);
                    hcp = (220 - (Int32.Parse(Game1.Text) + Int32.Parse(Game2.Text) + Int32.Parse(Game3.Text)) / 3) * .9;
                    Hcp.Text = Convert.ToInt32(hcp).ToString();
                    Ave.Text = ave.ToString();
                    calcpts.Handicap = (int)hcp;
                    Hcap1.Text = Convert.ToInt32(hcp).ToString();
                    CalculateSeries(Game1.Text, Hcap1.Text);
                    Hcap2.Text = Convert.ToInt32(hcp).ToString();
                    CalculateSeries(Game2.Text, Hcap2.Text);
                    Hcap3.Text = Convert.ToInt32(hcp).ToString();
                    CalculateSeries(Game3.Text, Hcap3.Text);

                    int dx = 0;
                    do
                    {
                        if (MockDataStore.bowlers[dx].Name.ToLower().Contains(e.NewTextValue.ToLower())) break;
                        dx++;
                    } while (dx < 6);


                    MockDataStore.bowlers[dx].Game1 = Convert.ToInt32(Game1.Text);
                    MockDataStore.bowlers[dx].Game2 = Convert.ToInt32(Game2.Text);
                    MockDataStore.bowlers[dx].Game3 = Convert.ToInt32(Game3.Text);
                    MockDataStore.bowlers[dx].Handicap = Convert.ToInt32(hcp);

                    dx = 0;
                    do
                    {
                        if (BowlerDataStore.bowlers[dx].Name.ToLower().Contains(e.NewTextValue.ToLower())) break;
                        dx++;
                    } while (dx < BowlerDataStore.bowlers.Count);
                    BowlerDataStore.bowlers[dx].Handicap = Convert.ToInt32(hcp);
                    BowlerDataStore.bowlers[dx].Average = ave;
                    BowlerDataStore.bowlers[dx].Game1 = Convert.ToInt32(Game1.Text);
                    BowlerDataStore.bowlers[dx].Game2 = Convert.ToInt32(Game2.Text);
                    BowlerDataStore.bowlers[dx].Game3 = Convert.ToInt32(Game3.Text);

                    calcpts.CheckPts();
                }
                List<Bowler> bowllist;
                var bs = BowlerDataStore.bowlers.Where(b => b.Name.ToLower().Contains(e.NewTextValue.ToLower()));
                bowllist = bs.ToList();
                if (bowllist.Count == 1)
                {
                  calcpts.LoadItemId(bowllist[0].Id);
                }
                
            }
            else
            {
                List<Team> teamlist;
                var bs = Global.teamlist.Where(t => t.TeamName.ToLower().Contains(e.NewTextValue.ToLower()));
                teamlist = bs.ToList();
                if (teamlist.Count == 1)
                {
                    Name.Text = teamlist[0].TeamName;
                    //calcpts.LoadItemId(teamlist[0].TeamName);
                }
            }
            
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            CalculateSeries(Game1.Text, Hcp.Text);
        }

        

        private void Entry_Completed_1(object sender, EventArgs e)
        {
            CalculateSeries(Game2.Text, Hcp.Text);
        }

        private void Entry_Completed_2(object sender, EventArgs e)
        {
            CalculateSeries(Game3.Text, Hcp.Text);
        }
        
        private void CalculateSeries(string Game, string Hcp)
        {
            int game;
            int series;
            int hcpgame;

            
            
            game = Int32.Parse(Game);
            series = Int32.Parse(Series.Text);
            series =+ game;
            Series.Text = (Int32.Parse(Game1.Text) + Int32.Parse(Game2.Text) + Int32.Parse(Game3.Text)).ToString(); 
            hcpgame = Int32.Parse(Game) + Int32.Parse(Hcp);
            HandicapSeries.Text = (Int32.Parse(Hcp) * 3).ToString();

            Gm1Hcp.Text = (Int32.Parse(Game1.Text) + Int32.Parse(Hcp)).ToString();
            Gm2Hcp.Text = (Int32.Parse(Game2.Text) + Int32.Parse(Hcp)).ToString();
            Gm3Hcp.Text = (Int32.Parse(Game3.Text) + Int32.Parse(Hcp)).ToString();
            //hcpseries = Int32.Parse(SeriesHcp.Text);
            //hcpseries =+ hcpgame;
            ////HcpSeries.Text = hcpseries.ToString();
            //gamehcp = Int32.Parse(Game) + Int32.Parse(Hcp);
            //serieshcp = Int32.Parse(SeriesHcp.Text);
            //serieshcp =+ gamehcp;
            SeriesHcp.Text = (Int32.Parse(Gm1Hcp.Text)+ Int32.Parse(Gm2Hcp.Text) + Int32.Parse(Gm3Hcp.Text)).ToString();
            calcpts.CheckPts();
            calcpts.LoadItemId(Global.itemId);
        }
        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var entry = sender as Entry;
                entry.CursorPosition = 0;
                entry.SelectionLength = entry.Text.Length;
            });
        }
        //public async void LoadItemId(string itemId)
        //{
        //    try
        //    {
        //        var item = await DataStore.GetItemAsync(itemId);
        //        Id = item.Id;
        //        Bowler = item.Name;
        //        Average = item.Average;
        //        Handicap = item.Handicap;
        //        Game1 = item.Game1;
        //        Game2 = item.Game2;
        //        Game3 = item.Game3;
        //    }
        //    catch (Exception)
        //    {
        //        Debug.WriteLine("Failed to Load Item");
        //    }
        //}
    }
}