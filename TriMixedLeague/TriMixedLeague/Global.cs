using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BowlerLib;
using BowlLib;
using TriMixedLeague.Models;
using TriMixedLeague.ViewModels;

namespace TriMixedLeague
{

	public static class Global
	{
		public static string year;
		public static string week;
		public static string leaguenum;
		public static bool storeddata = false;
		public static List<Team> teamlist = new List<Team>();
		public static Extract extract = new Extract();
		public static List<BowlersList> bowlerslist = new List<BowlersList>();
		public static List<SelectedLeagueInfo> SelectedLeagueInfoList = new List<SelectedLeagueInfo>();
		public static List<SelectedTeamsInfo> SelectedTeamsInfoList = new List<SelectedTeamsInfo>();
		public static List<string> bowlershttp = new List<string>();
		public static string bowlerhist;
		public static string teamhist;
		public static string itemId;
		public static string indicator;
		public static string http;
		public static string teamid;
		public static LeagueInfoModel leagueinfo = new LeagueInfoModel();

		public static List<TriMixedLeague.Models.Bowler> Bowlers { get; private set; }
		public class SelectedLeagueInfo
		{
			public string Value { get; set; }
			public string Http { get; set; }
			public string ListHttp { get; set; }
		}
		public class SelectedTeamsInfo
		{
			public string Value { get; set; }
			public string Http { get; set; }
		}

		public static void GetStoredData()
		{
			var list = Global.SelectedLeagueInfoList;
			//List<SelectedLeagueInfo> stuff = new List<SelectedLeagueInfo>();
			var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "LeagueData.txt");
			//if (Centers.Text != null)
			//{
			//	using (var writer = File.CreateText(backingFile))
			//	{
			//		foreach (var line in list)
			//		{
			//			writer.WriteLine(line.Value);
			//			writer.WriteLine(line.Http);
			//		}

			//	}
			//}
			if (!File.Exists(backingFile)) return;
			storeddata = true;
			int dx = 0;
			using (StreamReader sr = new StreamReader(backingFile))
			{
				//string data;
				string value = string.Empty;
				string http = string.Empty;
				string httplist = string.Empty;
				Global.SelectedLeagueInfoList.Clear();
				while (value != null)
				{
					//Read the next line
					value = sr.ReadLine();

					if (value == null) break;
					http = sr.ReadLine();
					httplist = sr.ReadLine();
					Global.SelectedLeagueInfoList.Add(new Global.SelectedLeagueInfo
					{
						Value = value,
						Http = http,
						ListHttp=httplist
					});
					switch (dx)
					{
						case 0:
							leagueinfo.CitiesList = leagueinfo.GetCities();
							break;
						case 1:
							leagueinfo.CentersList = leagueinfo.GetCenters(httplist);
							break;
						case 2:
							leagueinfo.LeagueList= leagueinfo.GetLeagues(httplist);
							break;
					}
					dx++;
					


				}
				//close the file
				sr.Close();
				bowlerslist = extract.getbowlers(SelectedLeagueInfoList[2].Http);
				http = Extract.bowlerslisthttp;
				string[] temp = http.Split('/');
				//int dx = 0;
				//while (temp[dx] != "bowling-centers")
				//{
				//	dx++;
				//}

				
				////temp = SelectedLeagueInfoList[1].Http.Split('/');
				//dx = 0;
				//while (temp[dx] != "bowling-leagues")
				//{
				//	dx++;
				//}
				Extract.leaguecenter = temp[4];
				Extract.leaguename = temp[6];
				Extract.leagueyear = Global.year = temp[8];
				Extract.leagueseason = temp[9];
				Extract.leagueweek = Global.week = temp[10];
				Extract.leaguenum = Global.leaguenum = temp[11];
				
				//int dx = 0;

				//foreach (var line in Global.SelectedLeagueInfoList)
				//{
				//	switch (dx)
				//	{
				//		case 0:
				//			leagueinfo.SelectedLeague; picker.Title = "Selected Location : " + line.Value;
				//			Location.Text = "Selected Location : " + line.Value;
				//			break;
				//		case 1:
				//			pickercenter.Title = "Selected Center : " + line.Value;
				//			Centers.Text = "Selected Center : " + line.Value;
				//			break;
				//		case 2:
				//			pickerleagues.Title = "Selected League : " + line.Value;
				//			Leagues.Text = "Selected League : " + line.Value;
				//			BuildBowlers.Text = "Selected Bowlers : " + line.Value;
				//			Global.http = line.Http;
				//			break;
				//	}


				//	dx++;
			}


			}
		}

		//public static void CreateBowlerCollection()
		//{
		//    Bowlers = new List<TriMixed.Models.Bowler>();

		//    Bowlers.Add(new TriMixed.Models.Bowler
		//    {
		//        bowler = "Bowler1",
		//        average = 0,
		//        handicap = 0,
		//        game1 = 0,
		//        game2 = 0,
		//        game3 = 0
		//    }
		//    );
		//    Bowlers.Add(new TriMixed.Models.Bowler
		//    {
		//        bowler = "Bowler2",
		//        average = 0,
		//        handicap = 0,
		//        game1 = 0,
		//        game2 = 0,
		//        game3 = 0
		//    }
		//    );
		//    Bowlers.Add(new TriMixed.Models.Bowler
		//    {
		//        bowler = "Bowler3",
		//        average = 0,
		//        handicap = 0,
		//        game1 = 0,
		//        game2 = 0,
		//        game3 = 0
		//    }
		//    );
		//    Bowlers.Add(new TriMixed.Models.Bowler
		//    {
		//        bowler = "Bowler4",
		//        average = 0,
		//        handicap = 0,
		//        game1 = 0,
		//        game2 = 0,
		//        game3 = 0
		//    }
		//    );
		//    Bowlers.Add(new TriMixed.Models.Bowler
		//    {
		//        bowler = "Bowler5",
		//        average = 0,
		//        handicap = 0,
		//        game1 = 0,
		//        game2 = 0,
		//        game3 = 0
		//    }
		//    );
		//    Bowlers.Add(new TriMixed.Models.Bowler
		//    {
		//        bowler = "Bowler6",
		//        average = 0,
		//        handicap = 0,
		//        game1 = 0,
		//        game2 = 0,
		//        game3 = 0
		//    }
		//    );
		//}
	//}
}
