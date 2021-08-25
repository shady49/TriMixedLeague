using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using BowlerLib;
using TriMixedLeague.Views;
using Xamarin.Forms;

namespace TriMixedLeague.ViewModels
{
	//Rick Shadle
	public class LeagueInfoModel : INotifyPropertyChanged
	{
		//public LeagueInfo league = new LeagueInfo();
		public Extract extract = new Extract();
		public AboutViewModel about = new AboutViewModel();
		public List<string> ListHttps = new List<string>();
		public List<BowlersList> bowlerslist = new List<BowlersList>();
		public List<City> CitiesList { get; set; }
		public List<Center> CentersList { get; set; }
		public List<League> LeagueList { get; set; }
		public List<League> TeamList { get; set; }
		public List<Bowler_> BowlerList { get; set; }

		public string Http { get; set; }


		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private string _myCity;
		public string MyCity
		{
			get { return _myCity; }
			set
			{
				if (_myCity != value)
				{
					_myCity = value;
					OnPropertyChanged();
				}
			}
		}
		private string _myCenter;
		public string MyCenter
		{
			get { return _myCenter; }
			set
			{
				if (_myCenter != value)
				{
					_myCenter = value;
					OnPropertyChanged();
				}
			}
		}

		private string _myLeague;
		public string MyLeague
		{
			get { return _myLeague; }
			set
			{
				if (_myLeague != value)
				{
					_myLeague = value;
					OnPropertyChanged();
				}
			}
		}
		private string _myTeams;
		public string MyTeams
		{
			get { return _myTeams; }
			set
			{
				if (_myTeams != value)
				{
					_myTeams = value;
					OnPropertyChanged();
				}
			}
		}
		private string _myLeagueInfo;
		public string MyLeagueInfo
		{
			get { return _myLeagueInfo; }
			set
			{
				if (_myLeagueInfo != value)
				{
					_myLeagueInfo = value;
					OnPropertyChanged();
				}
			}
		}
		private string _myTeamsInfo;
		public string MyTeamsInfo
		{
			get { return _myTeamsInfo; }
			set
			{
				if (_myTeamsInfo != value)
				{
					_myTeamsInfo = value;
					OnPropertyChanged();
				}
			}
		}
		private string _myBowlerInfo;
		public string MyBowlerInfo
		{
			get { return _myBowlerInfo; }
			set
			{
				if (_myBowlerInfo != value)
				{
					_myBowlerInfo = value;
					OnPropertyChanged();
				}
			}
		}
		private string _myBowlers;
		public string MyBowlers
		{
			get { return _myBowlers; }
			set
			{
				if (_myBowlers != value)
				{
					_myBowlers = value;
					OnPropertyChanged();
				}
			}
		}
		private City _selectedCity { get; set; }
		public City SelectedCity
		{
			get { return _selectedCity; }
			set
			{
				if (_selectedCity != value)
				{
					if (value != null) _selectedCity = value;
					// Do whatever functionality you want...When a selectedItem is changed..
					// write code here..
					MyCity = "Selected Location : " + _selectedCity.Value;
					MyCenter = "Select a Bowling Center";
					//int key = 1;
					//string strOutput = extract.getcenters("https://www.leaguesecretary.com" + _selectedCity.Http);
					//string[] loc = strOutput.Split('\r');
					Global.http = "https://www.leaguesecretary.com" + _selectedCity.Http;
					//Global.http = Http;
					Global.SelectedLeagueInfoList.Add(new Global.SelectedLeagueInfo
					{
						Value = _selectedCity.Value,
						Http = Global.http,
						ListHttp = ListHttps[0]
					});
					if (CentersList != null) CentersList.Clear();
					//LeagueInfo. = "Select a Bowling Center";
					CentersList = GetCenters(Global.http);
					//var pickercenter = new Picker { Title = "Select a bowling center", TitleColor = Color.Red };
					//pickercenter.ItemsSource = CentersList;
				}
			}
		}

		private Center _selectedCenter { get; set; }
		public Center SelectedCenter
		{
			get { return _selectedCenter; }
			set
			{
				if (_selectedCenter != value)
				{
					_selectedCenter = value;
					// Do whatever functionality you want...When a selectedItem is changed..
					// write code here..
					MyCenter = "Selected Center : " + _selectedCenter.ValueCenter;
					MyLeague = "Select a League";
					//int key = 1;
					//string strOutput = extract.getcenters("https://www.leaguesecretary.com" + _selectedCity.Http);
					//string[] loc = strOutput.Split('\r');
					Global.http = "https://www.leaguesecretary.com" + _selectedCenter.HttpCenter;
					Global.SelectedLeagueInfoList.Add(new Global.SelectedLeagueInfo
					{
						Value = _selectedCenter.ValueCenter,
						Http = Global.http,
						ListHttp=  ListHttps[1]
					});
					//Global.http = Http;
					LeagueList = GetLeagues(ListHttps[1]).OrderBy(t => t.ValueLeague).ToList();
				}
			}
		}
		private League _selectedLeague { get; set; }
		public League SelectedLeague
		{
			
			get { return _selectedLeague; }
			set
			{
				if (_selectedLeague != value)
				{
					_selectedLeague = value;
					// Do whatever functionality you want...When a selectedItem is changed..
					// write code here..
					MyLeague = "Selected League : " + _selectedLeague.ValueLeague;
					MyBowlerInfo = "Select Bowler Info";
					//int key = 1;
					//string strOutput = extract.getcenters("https://www.leaguesecretary.com" + _selectedCity.Http);
					//string[] loc = strOutput.Split('\r');
					Global.http = "https://www.leaguesecretary.com" + _selectedLeague.HttpLeague;
					Global.SelectedLeagueInfoList.Add(new Global.SelectedLeagueInfo
					{
						Value = _selectedLeague.ValueLeague,
						Http = Global.http,
						ListHttp= ListHttps[2]
					}) ;
					string[] temp = Global.http.Split('/');
					int dx = 0;
					while (temp[dx]!= "bowling-centers")
					{
						dx++;
					}
					
					Extract.leaguecenter = temp[dx+1];
					temp = Global.http.Split('/');
					dx = 0;
					while (temp[dx] != "bowling-leagues")
					{
						dx++;
					}
					Extract.leaguename = temp[dx + 1];
					//Global.http = Http;
					//LeagueList = GetLeagues();
				}
			}
		}

		private Teams _selectedTeams { get; set; }
		public Teams SelectedTeams
		{

			get { return _selectedTeams; }
			set
			{
				if (_selectedTeams != value)
				{
					_selectedTeams = value;
					// Do whatever functionality you want...When a selectedItem is changed..
					// write code here..
					MyTeams = "Selected League : " + _selectedLeague.ValueLeague;
					MyTeamsInfo = "Select Team Info";
					//int key = 1;
					//string strOutput = extract.getcenters("https://www.leaguesecretary.com" + _selectedCity.Http);
					//string[] loc = strOutput.Split('\r');
					Global.http = "https://www.leaguesecretary.com" + _selectedLeague.HttpLeague;
					Global.SelectedTeamsInfoList.Add(new Global.SelectedTeamsInfo
					{
						Value = _selectedTeams.ValueTeams,
						Http = Global.http
					});
					string[] temp = Global.http.Split('/');
					int dx = 0;
					while (temp[dx] != "bowling-centers")
					{
						dx++;
					}

					Extract.leaguecenter = temp[dx + 1];
					temp = Global.http.Split('/');
					dx = 0;
					while (temp[dx] != "bowling-leagues")
					{
						dx++;
					}
					Extract.leaguename = temp[dx + 1];
					//Global.http = Http;
					//LeagueList = GetLeagues();
				}
			}
		}

		private Bowler_ _selectedBowler { get; set; }
		public Bowler_ SelectedBowler
		{
			get { return _selectedBowler; }
			set
			{
				if (_selectedBowler != value)
				{
					_selectedBowler = value;
					// Do whatever functionality you want...When a selectedItem is changed..
					// write code here..
					MyBowlers = "Selected Bowlers : " + _selectedBowler.Value;
					MyBowlerInfo = "Select Bowler Info";
					//int key = 1;
					//string strOutput = extract.getcenters("https://www.leaguesecretary.com" + _selectedCity.Http);
					//string[] loc = strOutput.Split('\r');
					Extract.bowlerslisthttp = "https://www.leaguesecretary.com" + _selectedBowler.Http;
					Global.http = Extract.bowlerslisthttp;
					Global.SelectedLeagueInfoList.Add(new Global.SelectedLeagueInfo
					{
						Value = _selectedBowler.Value,
						Http = Global.http,
						ListHttp= _selectedBowler.Http
					});

					////Global.http = Http;
					//BowlerList = GetBowlers();
				}
			}
		}


		public LeagueInfoModel()
		{
			//Global.leagueinfo = new LeagueInfoModel();
			if (CitiesList != null) CitiesList.Clear();
			CitiesList = GetCities().OrderBy(t => t.Value).ToList();
			MyCity = "Selected Location : ";
			
			//CentersList = GetCenters();
		}


		public List<City> GetCities()
		{
			int key = 1;
			ListHttps.Add("https://www.leaguesecretary.com/bowling-centers/search");
			string strOutput = extract.getlocations("https://www.leaguesecretary.com/bowling-centers/search");
			string[] loc = strOutput.Split('\n');
			int dx;
			int dy;
			int ddx;
			int ddy;

			List<City> cities = new List<City>();
			{
				foreach (string city in loc)
				{
					if (city.Contains("<a href"))
					{
						dx = city.IndexOf(">") + 1;
						dy = city.IndexOf("</a");
						dy = dy - dx;
						ddx = city.IndexOf("<a href=") + 9;
						ddy = city.IndexOf(">") + 1;
						ddy = ddy - ddx - 2;
						cities.Add(new City
						{
							Key = key,
							Value = city.Substring(dx, dy),
							Http = city.Substring(ddx, ddy)
						});
						key++;
					}


				}


				//new City(){Key =  1, Value= "New York", Http="https://microsoftnews.msn.com/"},
				//new City(){Key =  2, Value= "Omaha"},
				//new City(){Key =  3, Value= "Los Angeles"},
				//new City(){Key =  4, Value= "Chicago"},
				//new City(){Key =  5, Value= "Watertown"},
				//new City(){Key =  6, Value= "Denison"},
				//new City(){Key =  7, Value= "Columbus"}
			};

			if (CitiesList != null) CitiesList.Clear();
			CitiesList = cities;
			return cities;
		}
		public List<Center> GetCenters(string http)
		{
			int key = 1;
			//Global.http= Global.SelectedLeagueInfoList[1].ListHttp;
			ListHttps.Add(Global.http);
			string strOutput = extract.getcenters(http);
			string[] loc = strOutput.Split('\r');
			int dx;
			int dy;
			int ddx;
			int ddy;

			List<Center> centers = new List<Center>();
			{
				foreach (string city in loc)
				{
					if (city.Contains("href"))
					{
						dx = city.IndexOf("href=") + 6;
						dy = city.IndexOf(">");
						dy = dy - dx;
						dy--;
						ddx = city.IndexOf(">") + 1;
						ddy = city.IndexOf("</a>");
						ddy = ddy - ddx;
						centers.Add(new Center
						{
							KeyCenter = key,
							ValueCenter = city.Substring(ddx, ddy),
							HttpCenter = city.Substring(dx, dy)
						}
						);
						key++;
					}
				}
			};

			if (CentersList != null) CentersList.Clear();
			CentersList = centers;
			return centers;
		}
		public List<League> GetLeagues(string http)
		{
			int key = 1;
			//Extract.teamhttp = Global.http;
			Global.http = Global.SelectedLeagueInfoList[1].Http;
			ListHttps.Add(Global.http);
			string strOutput = extract.getleagues(Global.SelectedLeagueInfoList[1].Http);
			//string strOutput = extract.getleagues(Global.http);
			string[] loc = strOutput.Split('\n');
			int dx;
			int dy;
			int ddx;
			int ddy;

			List<League> leagues = new List<League>();
			{
				foreach (string league in loc)
				{
					if (league.Contains("href"))
					{
						ddx = 0;
						ddy = league.IndexOf("href=") - 2;
						dx = league.IndexOf("href=") + 6;
						dy = league.Length - dx;
						leagues.Add(new League
						{
							KeyLeague = key,
							ValueLeague = league.Substring(ddx, ddy),
							HttpLeague = league.Substring(dx, dy)
						}
						);
						key++;
					}
				}
			};

			if (LeagueList != null) LeagueList.Clear();
			//LeagueList = leagues.OrderBy(t => t.ValueLeague).ToList();
			return leagues;
		}

		public List<Teams> GetTeams()
		{
			int key = 1;
			string strOutput=string.Empty;
			//Extract.teamhttp = Global.http;
			//string strOutput = extract.getteams(Global.http);
			string[] loc = strOutput.Split('\n');
			int dx;
			int dy;
			int ddx;
			int ddy;

			List<Teams> teams = new List<Teams>();
			{
				foreach (string team in loc)
				{
					if (team.Contains("href"))
					{
						ddx = 0;
						ddy = team.IndexOf("href=") - 2;
						dx = team.IndexOf("href=") + 6;
						dy = team.Length - dx;
						teams.Add(new Teams
						{
							KeyTeams = key,
							ValueTeams = team.Substring(ddx, ddy),
							HttpTeams = team.Substring(dx, dy)
						}
						);
						key++;
					}
				}
			};

			if (LeagueList != null) LeagueList.Clear();
			//LeagueList = leagues.OrderBy(t => t.ValueLeague).ToList();
			return teams;
		}
		public List<Bowler_> GetBowlers()
		{
			int key = 1;
			bowlerslist = extract.getbowlers(Global.http);
			//about.LeagueNum = "123456";
			//string[] loc = strOutput.Split('\n');
			//int dx;
			//int dy;
			//int ddx;
			//int ddy;

			List<Bowler_> bowlers = new List<Bowler_>();
			{

				foreach (BowlersList bowler in bowlerslist)
				{
					//if (bowler.Contains("href"))
					//{
					//ddx = 0;
					//ddy = bowler.IndexOf("href=") - 2;
					//dx = bowler.IndexOf("href=") + 6;
					//dy = bowler.Length - dx;
					bowlers.Add(new Bowler_
					{
						Key = key,
						Value = bowler.Name,
						Http = bowler.HTTPbowler
					}
					);
					key++;
					//}
				}
			};

			if (LeagueList != null) LeagueList.Clear();
			//LeagueList = leagues.OrderBy(t => t.ValueLeague).ToList();
			return bowlers;
		}
	}


	public class City
	{
		public int Key { get; set; }
		public string Value { get; set; }
		public string Http { get; set; }
	}
	public class Center
	{
		public int KeyCenter { get; set; }
		public string ValueCenter { get; set; }
		public string HttpCenter { get; set; }
	}

	public class League
	{
		public int KeyLeague { get; set; }
		public string ValueLeague { get; set; }
		public string HttpLeague { get; set; }
	}

	public class Teams
	{
		public int KeyTeams { get; set; }
		public string ValueTeams { get; set; }
		public string HttpTeams { get; set; }
	}

	public class Bowler_
	{
		public int Key { get; set; }
		public string Value { get; set; }
		public string Http { get; set; }
	}

}
