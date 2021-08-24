using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriMixedLeague.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriMixedLeague.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LeagueInfo : ContentPage
	{
		public LeagueInfoModel leaguemodel = new LeagueInfoModel();
		//public LeagueInfoModel teammodel = new LeagueInfoModel();
		public AboutViewModel aboutmodel = new AboutViewModel();
		public ListView countries = new ListView();
		List<SelectedLeagueInfo> list = new List<SelectedLeagueInfo>();
		public class SelectedLeagueInfo
		{
			public string Value { get; set; }
			public string Http { get; set; }
		}
		private string leaguename;
		public string Leaguename
		{
			get { return leaguename; }
			set
			{
				if (value != null)
				{
					leaguename = value;
					pickercenter.Title = leaguename;
				}

			}
		}
		public LeagueInfo()
		{

			InitializeComponent();
			BindingContext = new LeagueInfoModel();
			picker.Title = "Select: States/Provinces/Countries";
			InitializeLeague();
			if (Global.teamlist.Count != 0)
			{
				//foreach ()
				//leaguemodel.TeamList = Global.teamlist;
			}
			leaguemodel.CitiesList = leaguemodel.GetCities();
			//leaguemodel.CentersList = leaguemodel.GetCenters();
			SelectSaveLeagueInfo.Text = "Create New or Retrieve Existing League";
			//picker.ItemsSource = leaguemodel.CitiesList;
		}

		private void InitializeLeague()
		{
			Global.GetStoredData();
			int dx = 0;

			foreach (var line in Global.SelectedLeagueInfoList)
			{
				switch (dx)
				{
					case 0:
						picker.Title = "Selected Location : " + line.Value;
						Location.Text = "Selected Location : " + line.Value;
						break;
					case 1:
						pickercenter.Title = "Selected Center : " + line.Value;
						Centers.Text = "Selected Center : " + line.Value;
						break;
					case 2:
						pickerleagues.Title = "Selected League : " + line.Value;
						Leagues.Text = "Selected League : " + line.Value;
						//BuildBowlers.Text = "Selected Bowlers : " + line.Value;
						Global.http = line.Http;
						break;
					case 3:
						pickerleagues.Title = "Selected Teams : " + line.Value;
						Leagues.Text = "Selected Teams : " + line.Value;
						//BuildBowlers.Text = "Selected Bowlers : " + line.Value;
						Global.http = line.Http;
						break;
				}


				dx++;
			}
		}

		private void Button_Clicked_States(object sender, EventArgs e)
		{
			picker.Title = "Select States/Provinces/Countries";
			leaguemodel.CitiesList = leaguemodel.GetCities();
			//picker.ItemsSource = leaguemodel.CitiesList;
		}

		private void Button_Clicked_Centers(object sender, EventArgs e)
		{
			pickercenter.Title = "Select a bowling center";
			leaguemodel.Http = Global.http;
			leaguemodel.CentersList = leaguemodel.GetCenters(Global.http);
			try
			{
				pickercenter.ItemsSource = null;
				pickercenter.ItemsSource = leaguemodel.CentersList;
			}
			catch
			{
				return;
			}
		}
		private void Button_Clicked_Leagues(object sender, EventArgs e)
		{
			pickerleagues.Title = "Select a Bowling League";
			leaguemodel.Http = Global.http;
			leaguemodel.LeagueList = leaguemodel.GetLeagues(Global.http);
			try
			{
				pickerleagues.ItemsSource = leaguemodel.LeagueList.OrderBy(t => t.ValueLeague).ToList();
			}
			catch
			{
				return;
			}
		}
		private void Button_Clicked_Teams(object sender, EventArgs e)
		{
			//pickerteams.Title = "Select a Bowling Teams";
			leaguemodel.Http = Global.http;
			//leaguemodel.TeamList = leaguemodel.GetTeams();
			try
			{
				pickerleagues.ItemsSource = leaguemodel.LeagueList.OrderBy(t => t.ValueLeague).ToList();
			}
			catch
			{
				return;
			}
		}

		//private void SelectSaveLeagueInfo_ClickedAsync(object sender, EventArgs e)
		//{
		//	var list = Global.SelectedLeagueInfoList;
		//	var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "count.txt");
		//	using (var writer = File.CreateText(backingFile))
		//	{
		//		writer.WriteLine(list.ToString());
		//	}
		//}

		private void SelectSaveLeagueInfo_Clicked(object sender, EventArgs e)
		{
			
			var list = Global.SelectedLeagueInfoList;
			//List<SelectedLeagueInfo> stuff = new List<SelectedLeagueInfo>();
			var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "LeagueData.txt");
			if (Centers.Text != null)
			{
				using (var writer = File.CreateText(backingFile))
				{
					foreach (var line in list)
					{
						writer.WriteLine(line.Value);
						writer.WriteLine(line.Http);
						writer.WriteLine(line.ListHttp);
					}

				}
			}
			if (!File.Exists(backingFile)) return;
			Global.GetStoredData();
			//using (StreamReader sr = new StreamReader(backingFile))
			//{
			//	//string data;
			//	string value = string.Empty;
			//	string http = string.Empty;
			//	Global.SelectedLeagueInfoList.Clear();
			//	while (value != null)
			//	{
			//		//Read the next line
			//		value = sr.ReadLine();

			//		if (value == null) break;
			//		http = sr.ReadLine();
			//		Global.SelectedLeagueInfoList.Add(new Global.SelectedLeagueInfo
			//		{
			//			Value = value,
			//			Http = http
			//		});


			//	}
			//	//close the file
			//	sr.Close();
				int dx = 0;

				foreach (var line in Global.SelectedLeagueInfoList)
				{
					switch (dx)
					{
						case 0:
							picker.Title = "Selected Location : " + line.Value;
							Location.Text = "Selected Location : " + line.Value;
							break;
						case 1:
							pickercenter.Title = "Selected Center : " + line.Value;
							Centers.Text = "Selected Center : " + line.Value;
							break;
						case 2:
							pickerleagues.Title = "Selected League : " + line.Value;
							Leagues.Text = "Selected League : " + line.Value;
							//BuildBowlers.Text = "Selected Bowlers : " + line.Value;
							Global.http = line.Http;
							break;
					}


					dx++;
				}


			


		}

		private void BuildBowlers_Clicked(object sender, EventArgs e)
		{
			//pickerbowlers.Title = "Select a Bowling List";
			leaguemodel.Http = Global.http;
			leaguemodel.BowlerList = leaguemodel.GetBowlers();
			try
			{
				//pickerbowlers.ItemsSource = leaguemodel.BowlerList.OrderBy(t => t.Value).ToList();
			}
			catch
			{
				return;
			}
		}
		async void OnActionSheetSimpleClicked(object sender, EventArgs e)
		{
			var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "LeagueData.txt");
			string action = await DisplayActionSheet("File Action: ", "Cancel", null, "Save", "Delete");
			if (action == "Delete")
			{
				try
				{
					File.Delete(backingFile);
					leaguemodel.MyCity = "SELECTED LOCATION:";
					leaguemodel.MyCenter = "";
					leaguemodel.MyLeague = "";
					leaguemodel.MyBowlers = "";
					leaguemodel.MyBowlerInfo = "";
					Location.Text = leaguemodel.MyCity;
					Centers.Text = leaguemodel.MyCenter;
					Leagues.Text = leaguemodel.MyLeague;
					//BuildBowlers.Text = leaguemodel.MyBowlerInfo;
					picker.Title = Location.Text;
					pickercenter.Title = Centers.Text;
					pickerleagues.Title = Leagues.Text;
					//pickerbowlers.Title = BuildBowlers.Text;
					aboutmodel.LeagueNum = "";
					aboutmodel.WeekNum = "";
					aboutmodel.YearNum = "";
				}
				catch 
				{
				
				}
					return;
			}
			if (action == "Save")
			{
				var list = Global.SelectedLeagueInfoList;
				
				if (Centers.Text != null)
				{
					if (File.Exists(backingFile)) File.Delete(backingFile);
					using (var writer = File.CreateText(backingFile))
					{
						foreach (var line in list)
						{
							writer.WriteLine(line.Value);
							writer.WriteLine(line.Http);
							writer.WriteLine(line.ListHttp);
						}

					}
				}
				InitializeLeague();
				
				return;
			}
		}
	}

}
