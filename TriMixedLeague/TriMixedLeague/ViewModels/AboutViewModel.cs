using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TriMixedLeague.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
		
		private string leaguenum;
		public string LeagueNum
		{
			get { return leaguenum; }
			set { leaguenum = value; }
		}
		private string weeknum;
		public string WeekNum
		{
			get { return weeknum; }
			set { weeknum = value; }
		}
		private string yearnum;
		public string YearNum
		{
			get { return yearnum; }
			set { yearnum = value; }
		}
		public AboutViewModel()
        {
            Title = "League Secretary";
			//LeagueNum = "123456";
			//OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.leaguesecretary.com/bowling-centers/west-lanes-bowl-omaha-nebraska/bowling-leagues/tues-mut-mixed20/dashboard/40233"));
			OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.leaguesecretary.com/bowling-centers/search"));
			
        }

        public ICommand OpenWebCommand { get; }
    }
}