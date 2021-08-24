using System;
using System.ComponentModel;
using Xamarin.Forms;
using BowlerLib;
using TriMixedLeague.Models;
using Xamarin.Forms.Xaml;
using TriMixedLeague.ViewModels;
using System.IO;

namespace TriMixedLeague.Views
{
    public partial class AboutPage : ContentPage
    {
        public Extract extract = new Extract();
        public AboutPage()
        {
            InitializeComponent();
			BindingContext = new AboutViewModel();
			var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "LeagueData.txt");
			if (File.Exists(backingFile))
			{
				Global.GetStoredData();
				Week_Completed_Void();
			}
			

			LeagueNum.Text = Global.leaguenum= Extract.leaguenum;
			Year.Text = Global.year= Extract.leagueyear;
			Week.Text = Global.week = Extract.leagueweek;
			
			Year.Completed += Year_Completed;
            Week.Completed += Week_Completed;
			
			//Global.teamlist = extract.getteams(Extract.leaguecenter, Extract.leaguename, Global.week, Global.year, Extract.leagueseason, Global.leaguenum);
			//LeagueNum.Text = Global.leaguenum;
		}
        private void Week_Completed(object sender, EventArgs e)
        {
			Week_Completed_Void();
			
        }

		private void Week_Completed_Void()
		{
			int dx;
			//Global.year = null;
			if (Global.storeddata ==true)
			{
				Year.Text = Global.year;
				Week.Text = Global.week;
				LeagueNum.Text = Global.leaguenum;
			}
			else
			{
				Global.year = Year.Text;
				Global.week = Week.Text;
				Global.leaguenum = LeagueNum.Text;
			}
			
			extract.bowlershttp.Clear();
			//Global.teamlist = extract.getteams(Global.week, Global.year);
			//if (string.IsNullOrEmpty(Global.http)) return;
			if (string.IsNullOrEmpty(Global.leaguenum))
			{
				dx = Global.http.IndexOf("bowler-list/");
			}
			extract.extract(Extract.leaguecenter, Extract.leaguename, Global.week, Global.year, Extract.leagueseason, Global.leaguenum, Extract.bowlerslisthttp);



			if (extract.bowlerslist.Count == 0)
			{
				do
				{
					if ((Int32.Parse(Global.week) - 1) == 0)
					{
						int yr = Int32.Parse(Global.year);
						string y;
						yr--;
						y = yr.ToString();
						Year.Text = y;
						Global.year = y;
						Global.week = "28";
					}

					Week.Text = (Int32.Parse(Global.week) - 1).ToString();
					Global.week = Week.Text;
					extract.extract(Extract.leaguecenter, Extract.leaguename, Global.week, Global.year, Extract.leagueseason, Global.leaguenum, Extract.bowlerslisthttp);
				} while (extract.bowlerslist.Count == 0);
				//Global.teamlist=extract.getteams(Global.week, Global.year, Extract.leagueseason, Global.leaguenum);
			}
			//Global.week = Week.Text;
			//Global.year = Year.Text;
			Global.teamlist = extract.getteams(Extract.leaguecenter, Extract.leaguename, Global.week, Global.year, Extract.leagueseason, Global.leaguenum);
			Global.bowlershttp = extract.bowlershttp;
			Global.bowlerslist = extract.bowlerslist;
		}

		private void Year_Completed(object sender, EventArgs e)
        {
            if (Year.Text.Length != 4) { return; }
            Global.year = Year.Text;
            Global.week = Week.Text;
            int yr = DateTime.Now.Year;
            if (yr < Int32.Parse(Year.Text))
            {
                Year.Text = yr.ToString();
                Global.year = Year.Text;
            }
            Global.week = Week.Text;
            Global.year = Year.Text;
        }

		private void LeagueNum_TextChanged(object sender, TextChangedEventArgs e)
		{
			
		}
		private void YearNum_TextChanged(object sender, TextChangedEventArgs e)
		{
			
		}
		private void WeekNum_TextChanged(object sender, TextChangedEventArgs e)
		{
			
		}
	}
}