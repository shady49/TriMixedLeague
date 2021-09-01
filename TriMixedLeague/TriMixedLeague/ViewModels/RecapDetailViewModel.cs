using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TriMixedLeague.Models;
using TriMixedLeague.Services;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;


namespace TriMixedLeague.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class RecapDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string bowler;
        private int average;
        private int handicap;
        private int handicapseries;
        private int game1;
        private int game2;
        private int game3;
        private int series;
        private int game1hcp;
        private int game2hcp;
        private int game3hcp;
        private int serieshcp;
        private float pt1;
        private float pt2;
        private float pt3;
        private float ptseries;
        private int pfactor;


        public string Id { get; set; }

        public string Bowler
        {
            
            get => bowler;
            set 
            {
                SetProperty(ref bowler, value);
                if (BowlerDataStore.bowlers == null)
                {

                    BowlerDataStore.bowlers = new List<Bowler>();
                    for (int dx = 0; dx < Global.bowlerslist.Count; dx++)
                    {

                        BowlerDataStore.bowlers.Add(new Bowler
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = Global.bowlerslist[dx].Name,
                            Average = Int32.Parse(Global.bowlerslist[dx].AVG),
                            Handicap = Int32.Parse(Global.bowlerslist[dx].HCP),
                            BowlerHTTP = Global.bowlerslist[dx].HTTPbowler,
                            TeamHTTP = Global.bowlerslist[dx].HTTPteam
                        });

                    }
                }
                List<Bowler> bowllist;
                var bs = BowlerDataStore.bowlers.Where(b => b.Name.ToLower().Contains(value.ToLower()));
                bowllist = bs.ToList();
                if (bowllist.Count == 1)
                {
                  SaveItemId(itemId, bowllist);
                    SetProperty(ref bowler, bowllist[0].Name);
                }
            }
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

        //private async void SetHandicap(int value)
        //{
        //    if (itemId == null) return;
        //    var item = await DataStore.UpdateItemAsync(itemId);
        //}
        public int Game1
        {
            get => game1;
            set
            {
                SetProperty(ref game1, value);
                SetGame1(value);
                SetProperty(ref series, game1 + game2 + game3);
                CheckPts();
            }
        }

        private async void SetGame1(int value)
        {
            if (itemId == null) return;
            var item = await DataStore.GetItemAsync(itemId);
            item.Game1 = value;
            item.Series = item.Game1 + item.Game2 + item.Game3;
            Series = item.Game1 + item.Game2 + item.Game3;
            SeriesHcp = item.Game1 + item.Handicap + item.Game2 + item.Handicap + item.Game3 + item.Handicap;
            CheckPts();
        }

        public int Game2
        {
            get => game2;
            set
            {
                SetProperty(ref game2, value);
                SetGame2(value);
                SetProperty(ref series, game1 + game2 + game3);
                CheckPts();
            }
        }

        private async void SetGame2(int value)
        {
            if (itemId == null) return;
            var item = await DataStore.GetItemAsync(itemId);
            item.Game2 = value;
            item.Series = item.Game1 + item.Game2 + item.Game3;
            Series = item.Game1 + item.Game2 + item.Game3;
            SeriesHcp = item.Game1 + item.Handicap + item.Game2 + item.Handicap + item.Game3 + item.Handicap;
            CheckPts();
        }
        public int Game3
        {
            get => game3;
            set
            {
                SetProperty(ref game3, value);
                SetGame3(value);
                SetProperty(ref series, game1 + game2 + game3);
                CheckPts();
            }
        }
        private async void SetGame3(int value)
        {
            if (itemId == null) return;
            var item = await DataStore.GetItemAsync(itemId);
            item.Game3 = value;
            item.Series = item.Game1 + item.Game2 + item.Game3;
            Series = item.Game1 + item.Game2 + item.Game3;
            SeriesHcp = item.Game1 + item.Handicap + item.Game2 + item.Handicap + item.Game3 + item.Handicap;
            CheckPts();
        }

        public int Series
        {
            get => game1 + game2 + game3;
            set
            {
                SetProperty(ref series, game1 + game2 + game3);
                CheckPts();
            }
        }

        public int Game1Hcp
        {
            get => game1 + handicap;
            set
            {
                SetProperty(ref game1hcp, game1 + handicap);
                CheckPts();
            }
        }
        public int Game2Hcp
        {
            get => game2 + handicap;
            set
            {
                SetProperty(ref game2hcp, game2 + handicap);
                CheckPts();
            }
        }
        public int Game3Hcp
        {
            get => game3 + handicap;
            set
            {
                SetProperty(ref game3hcp, game3 + handicap);
                CheckPts();
            }
        }

        public int HandicapSeries
        {
            get => handicap + handicap + handicap;
            set
            {
                SetProperty(ref handicapseries, handicap + handicap + handicap);
                CheckPts();
            }
        }

        public int SeriesHcp
        {
            get => game1hcp + game2hcp + game3hcp;
            set
            {
                SetProperty(ref serieshcp, game1hcp + game2hcp + game3hcp);
                CheckPts();
            }
        }
        public float Pt1
        {
            get => pt1;
            set
            {
                SetProperty(ref pt1, value);
                //CheckPts();
            }
        }
        public float Pt2
        {
            get => pt2;
            set
            {
                SetProperty(ref pt2, value);
                //CheckPts();
            }
        }
        public float Pt3
        {
            get => pt3;
            set
            {
                SetProperty(ref pt3, value);
                //CheckPts();
            }
        }
        public float PtSeries
        {
            get => ptseries;
            set
            {
                SetProperty(ref ptseries, value);
                //CheckPts();
            }
        }
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

        public async void LoadItemId(string itemId)
        {
            try
            {
                if (itemId == null) return;
                var item = await DataStore.GetItemAsync(itemId);
                Global.itemId = itemId;
                if (item.Indicator == "t" && Global.teamid!=null) item.Name = Global.teamid;
                Id = item.Id;
                Bowler = item.Name;
                Average = item.Average;
                Handicap = item.Handicap;
                Game1 = item.Game1;
                Game2 = item.Game2;
                Game3 = item.Game3;
                Game1Hcp = item.Game1 + item.Handicap;
                Game2Hcp = item.Game2 + item.Handicap;
                Game3Hcp = item.Game3 + item.Handicap;
                HandicapSeries = item.Handicap * 3;
                Series = item.Game1 + item.Game2 + item.Game3;
                SeriesHcp = item.Game1 + item.Handicap + item.Game2 + item.Handicap + item.Game3 + item.Handicap;
                Pt1 = item.Pt1;
                Pt2 = item.Pt2;
                Pt3 = item.Pt3;
                PtSeries = item.PtSeries;
                CheckPts();
                
                
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public void CheckPts()
        {
            List<Bowler> bowllist;
            var ms = MockDataStore.bowlers;
            bowllist = ms.ToList();
            for (int dx = 0; dx < 4; dx++)
            {
                if (dx == 0)
                {
                    pfactor = 2;
                }
                else
                {
                    pfactor = 1;
                }
                if (bowllist[dx].Game1 + bowllist[dx].Handicap > bowllist[dx + 4].Game1 + bowllist[dx + 4].Handicap)
                {
                    
                    bowllist[dx].Pt1 = pfactor * 1;
                    bowllist[dx+4].Pt1 = 0;
                    //Pt1 = 1;
                }
                else
                {
                    if (bowllist[dx].Game1 + bowllist[dx].Handicap == bowllist[dx + 4].Game1 + bowllist[dx + 4].Handicap)
                    {
                        bowllist[dx].Pt1 = pfactor * .5f;
                        bowllist[dx + 4].Pt1 = pfactor * .5f;
                        //Pt1 = .5f;
                    }
                    else
                    {
                        bowllist[dx].Pt1 = 0;
                        bowllist[dx + 4].Pt1 = pfactor * 1;
                        //Pt1 = 0;
                    }
                }
            }
            for (int dx = 0; dx < 4; dx++)
            {
                if (dx == 0)
                {
                    pfactor = 2;
                }
                else
                {
                    pfactor = 1;
                }
                //if (bowllist[dx].Id != itemId) break;
                if (bowllist[dx].Game2 + bowllist[dx].Handicap > bowllist[dx + 4].Game2 + bowllist[dx + 4].Handicap)
                {
                    bowllist[dx].Pt2 = pfactor * 1;
                    bowllist[dx + 4].Pt2 = 0;
                    //Pt2 = 1;
                }
                else
                {
                    if (bowllist[dx].Game2 + bowllist[dx].Handicap == bowllist[dx + 4].Game2 + bowllist[dx + 4].Handicap)
                    {
                        bowllist[dx].Pt2 = pfactor * .5f;
                        bowllist[dx + 4].Pt2 = pfactor * .5f;
                        //Pt2 = .5f;
                    }
                    else
                    {
                        bowllist[dx].Pt2 = 0;
                        bowllist[dx + 4].Pt2 = pfactor * 1;
                        //Pt2 = 0;
                    }
                }
            }
            for (int dx = 0; dx < 4; dx++)
            {
                if (dx == 0)
                {
                    pfactor = 2;
                }
                else
                {
                    pfactor = 1;
                }
                //if (bowllist[dx].Id != itemId) break;
                if (bowllist[dx].Game3 + bowllist[dx].Handicap > bowllist[dx + 4].Game3 + bowllist[dx + 4].Handicap)
                {
                    bowllist[dx].Pt3 = pfactor * 1;
                    bowllist[dx + 4].Pt3 = 0;
                    //Pt3 = 1;
                }
                else
                {
                    if (bowllist[dx].Game3 + bowllist[dx].Handicap == bowllist[dx + 4].Game3 + bowllist[dx + 4].Handicap)
                    {
                        bowllist[dx].Pt3 = pfactor * .5f;
                        bowllist[dx + 4].Pt3 = pfactor * .5f;
                        //Pt3 = .5f;
                    }
                    else
                    {
                        bowllist[dx].Pt3 = 0;
                        bowllist[dx + 4].Pt3 = pfactor * 1;
                        //Pt3 = 0;
                    }
                }
            }
            for (int dx = 0; dx < 4; dx++)
            {
                if (dx == 0)
                {
                    pfactor = 2;
                }
                else
                {
                    pfactor = 1;
                }
                //if (bowllist[dx].Id != itemId) break;
                int series = bowllist[dx].Game1 + bowllist[dx].Handicap
                    + bowllist[dx].Game2 + bowllist[dx].Handicap
                    + bowllist[dx].Game3 + bowllist[dx].Handicap;
                int seriesopp = bowllist[dx+4].Game1 + bowllist[dx+4].Handicap
                    + bowllist[dx + 4].Game2 + bowllist[dx + 4].Handicap
                    + bowllist[dx + 4].Game3 + bowllist[dx + 4].Handicap;
                if (series > seriesopp)
                {
                    bowllist[dx].PtSeries = pfactor * 1;
                    bowllist[dx + 4].PtSeries = 0;
                    //PtSeries = 1;
                }
                else
                {
                    if (series == seriesopp)
                    {
                        bowllist[dx].PtSeries = pfactor * .5f;
                        bowllist[dx + 4].PtSeries = pfactor * .5f;
                        //PtSeries = .5f;
                    }
                    else
                    {
                        bowllist[dx].PtSeries = 0;
                        bowllist[dx + 4].PtSeries = pfactor * 1;
                        //PtSeries = 0;
                    }
                }
            }
            if (Id != null)
            {
                List<Bowler> blist;
                var bs = bowllist.Where(b => b.Id.ToLower().Contains(Id.ToLower()));
                blist = bs.ToList();
                if (blist.Count == 1)
                {
                    Pt1 = blist[0].Pt1;
                    Pt2 = blist[0].Pt2;
                    Pt3 = blist[0].Pt3;
                    PtSeries = blist[0].PtSeries;
                }
            }
            bowllist[0].Average = 0;
            bowllist[4].Average = 0;
            bowllist[0].Handicap = 0;
            bowllist[4].Handicap = 0;
            for (int dx = 0; dx < 5; dx += 4)
            {
                
                for (int dy = 1; dy < 4; dy++)
                {
                    if (dy==1) 
                    {
                        bowllist[dx].Game1 = 0;
                        bowllist[dx].Game2 = 0;
                        bowllist[dx].Game3 = 0;
                    }
                    bowllist[dx].Average += bowllist[dx + dy].Average;
                    bowllist[dx].Handicap += bowllist[dx + dy].Handicap;
                    bowllist[dx].Game1 += bowllist[dx + dy].Game1;
                    bowllist[dx].Game2 += bowllist[dx + dy].Game2;
                    bowllist[dx].Game3 += bowllist[dx + dy].Game3;
                    //bowllist[dx].Series += bowllist[dy].Series;
                }
                bowllist[dx].Average = bowllist[dx].Average / 3;
            }
        }

        public async void SaveItemId(string itemId, List<Bowler> bowllist)
        {
            try
            {
                if (itemId == null) return;
                var item = await DataStore.GetItemAsync(itemId);
                Global.itemId = itemId;
                //Id = item.Id;
                item.Name= bowllist[0].Name;
                item.Average= bowllist[0].Average;
                item.Handicap= bowllist[0].Handicap;
                //item.Game1= bowllist[0].Game1;
                //item.Game2= bowllist[0].Game2;
                //item.Game3= bowllist[0].Game3;
                //Bowler = item.Name;
                Average = item.Average;
                Handicap = item.Handicap;
                Game1 = item.Game1;
                Game2 = item.Game2;
                Game3 = item.Game3;
                Game1Hcp = item.Game1 + item.Handicap;
                Game2Hcp = item.Game2 + item.Handicap;
                Game3Hcp = item.Game3 + item.Handicap;
                HandicapSeries = item.Handicap * 3;
                Series = item.Game1 + item.Game2 + item.Game3;
                SeriesHcp = item.Game1 + item.Handicap + item.Game2 + item.Handicap + item.Game3 + item.Handicap;
                Pt1 = item.Pt1;
                Pt2 = item.Pt2;
                Pt3 = item.Pt3;
                PtSeries = item.PtSeries;
                CheckPts();
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Save Item");
            }
        }
    }
}
