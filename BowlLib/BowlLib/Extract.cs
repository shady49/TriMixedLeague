using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using BowlLib;

namespace BowlerLib
{
	public class Extract
	{
		public static List<BowlersList> savebowlers;
		public static int pages;
		public static string bowlerslisthttp;
		public static string teamhttp;
		public static string leaguenum;
		public static string leagueyear;
		public static string leagueweek;
		public static string leagueseason;
		public static string leaguename;
		public static string leaguecenter;

		public List<Bowlers> bowlers = new List<Bowlers>();
		public List<TeamList> teamlist = new List<TeamList>();
		public List<Team> team = new List<Team>();
		public List<Locations> location = new List<Locations>();
		public List<BowlersList> bowlerslist = new List<BowlersList>();
		public List<Bowler> bowler = new List<Bowler>();
		public List<string> bowlername = new List<string>();
		public List<string> bowlershttp = new List<string>();

		public string year;
		public string week;
		private string[] breakdown;

		Holiday holiday = new Holiday();


		private string mBowlerName;

		private string mTM = " ";
		private string mPOS = " ";
		private string mGMS = " ";
		private string mPINS = " ";
		private string mAVG = " ";
		private string mHCP = " ";
		private string mHHG = " ";
		private string mHHS = " ";
		private string mHSG = " ";
		private string mHSS = " ";
		private string mMIB = " ";
		private string mSeq = " ";
		private string mHTTPbowler = " ";
		private string mHTTPteam = " ";

		string[] formats = {"M/d/yyyy",
						 "MM/dd/yyyy",
						 "M/d/yyyy",
						 "M/d/yyyy",
						 "MM/dd/yyyy",
						 "MM/d/yyyy" };

		public string getlocations(string basehttp)
		{
			string strOutput;
			string savestrOutput;
			HttpWebRequest wrRequest = (HttpWebRequest)WebRequest.Create(basehttp);
			WebResponse wrResponse = wrRequest.GetResponse();

			using (StreamReader sr = new StreamReader(wrResponse.GetResponseStream()))
			{
				strOutput = sr.ReadToEnd();
				savestrOutput = strOutput;
				sr.Close();
			}
			wrResponse.Close();
			int first = strOutput.IndexOf("To search for a bowling center") + 464;// + "<tbody>".Length;
			strOutput = strOutput.Substring(first);
			strOutput = Regex.Replace(strOutput, @"<!(.|\s)*?>", "");
			//strOutput = Regex.Replace(strOutput, "</?[a-z][a-z0-9]*[^<>]*>", " ");
			strOutput = Regex.Replace(strOutput, @"<!--(.|\s)*?-->", "");
			strOutput = Regex.Replace(strOutput, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			strOutput = Regex.Replace(strOutput, "<style.*?</style>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			strOutput = Regex.Replace(strOutput, "<TBODY>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			strOutput = Regex.Replace(strOutput, "</TBODY", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			strOutput = Regex.Replace(strOutput, "<THEAD>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			strOutput = Regex.Replace(strOutput, "</THEAD>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			strOutput = strOutput.Replace("\t", "");
			strOutput = strOutput.Replace(" \r\n  \r\n  ", "\n");
			strOutput = strOutput.Replace(" \r\n \r\n", "");
			strOutput = strOutput.Replace("\r\n \r\n", "");
			strOutput = strOutput.Replace("\r\n  \r\n", "\n");
			strOutput = strOutput.Replace("    ", "   ");
			strOutput = strOutput.Replace("   ", "  ");
			strOutput = strOutput.Replace(@"<div class="" col-xs-6 col-sm-3  text-left text-nowrap"">", "");
			strOutput = strOutput.Replace("</div>", "");
			strOutput = strOutput.Replace("          \r\n", "");
			strOutput = strOutput.Replace("        \r\n", "");
			strOutput = strOutput.Replace("    \r\n", "");
			strOutput = strOutput.Replace("  \r\n", "");
			strOutput = strOutput.Replace(@"  <div class=""row"">&nbsp;\r\n", "");
			//strOutput = strOutput.Replace(@"  <div class=""row"">\r\n, "");
			strOutput = strOutput.Replace("            ", "");
			strOutput = strOutput.Replace("\r", "");
			int last = strOutput.IndexOf("United Kingdom</a>");
			strOutput = strOutput.Substring(1, last + 18);

			return strOutput;
		}
		public string getcenters(string basehttp)
		{
			string strOutput;
			string savestrOutput;

			HttpWebRequest wrRequest = (HttpWebRequest)WebRequest.Create(basehttp);
			WebResponse wrResponse = wrRequest.GetResponse();

			using (StreamReader sr = new StreamReader(wrResponse.GetResponseStream()))
			{
				strOutput = sr.ReadToEnd();
				savestrOutput = strOutput;
				sr.Close();
			}
			wrResponse.Close();
			int first = strOutput.IndexOf("Center Name") + 746;// + "<tbody>".Length;
			strOutput = strOutput.Substring(first);
			string[] lines = strOutput.Split('\n');
			strOutput = "";
			foreach (string line in lines)
			{
				if (line.IndexOf(@"td valign=""top""><b><a title=") > 0)
				{
					strOutput = strOutput + line.Substring(line.IndexOf("href"));
				}
			}
			strOutput = strOutput.Replace("\n", "");
			strOutput = strOutput.Substring(0, strOutput.Length - 2);
			return strOutput;
		}
		public string getleagues(string basehttp)
		{
			string strOutput;
			string savestrOutput;
			HttpWebRequest wrRequest = (HttpWebRequest)WebRequest.Create(basehttp);
			WebResponse wrResponse = wrRequest.GetResponse();

			using (StreamReader sr = new StreamReader(wrResponse.GetResponseStream()))
			{
				strOutput = sr.ReadToEnd();
				savestrOutput = strOutput;
				sr.Close();
			}
			wrResponse.Close();
			int first = strOutput.IndexOf(">Dashboard<");
			first = strOutput.IndexOf("href=", first) + 7;
			basehttp = "https://www.leaguesecretary.com/" + strOutput.Substring(first, strOutput.IndexOf("'", first) - first);
			Extract.teamhttp = basehttp;
			strOutput = GetWebsite(basehttp);
			first = strOutput.IndexOf("<tbody>") + 8;
			strOutput = strOutput.Substring(first, strOutput.IndexOf("</tbody>", first) - first);

			string[] lines = strOutput.Split('\n');
			strOutput = "";
			int len;
			string templine;
			foreach (string line in lines)
			{
				if (line.IndexOf("title=") > 0)
				{
					len = line.IndexOf("title=") + 6;
					templine = line.Substring(len);
					len = templine.IndexOf(">");
					strOutput = strOutput + templine.Substring(1, len - 2) + '\n';
				}
			}
			//strOutput = strOutput.Replace("\n", "");
			//strOutput = strOutput.Substring(0, strOutput.Length - 2);
			return strOutput;
		}
		public List<BowlersList> getbowlers(string basehttp)
		{
			string strOutput;
			string savestrOutput;
			//int totalpages;
			HttpWebRequest wrRequest = (HttpWebRequest)WebRequest.Create(basehttp);
			WebResponse wrResponse = wrRequest.GetResponse();

			using (StreamReader sr = new StreamReader(wrResponse.GetResponseStream()))
			{
				strOutput = sr.ReadToEnd();
				savestrOutput = strOutput;
				sr.Close();
			}
			wrResponse.Close();
			int first = strOutput.IndexOf("MainContent_DivBowlerList");
			first = strOutput.IndexOf("href=", first) + 7;
			basehttp = "https://www.leaguesecretary.com/" + strOutput.Substring(first, strOutput.IndexOf('"', first) - first);
			bowlerslisthttp = basehttp;
			
			strOutput = GetWebsite(basehttp);
			pages = strOutput.IndexOf("Page <strong>1</strong> of <strong>");
			if (pages == -1)
			{
				pages = 1;
			}
			else
			{
				pages = pages + "Page <strong>1</strong> of <strong>".Length;
				pages = Int32.Parse(strOutput.Substring(pages, strOutput.IndexOf("<", pages) - pages));
			}
			first = strOutput.IndexOf("<tbody>") + 7;
			breakdown = bowlerslisthttp.Split('/');
			leaguenum = breakdown[11];
			leagueyear = breakdown[8];
			leagueseason = breakdown[9];
			leagueweek =  breakdown[10];
			leaguecenter = breakdown[4];
			leaguename = breakdown[6];
			
			bowlerslist = extract(leaguecenter, leaguename, leagueweek, leagueyear, leagueseason, leaguenum, bowlerslisthttp);
			strOutput = strOutput.Substring(first, strOutput.IndexOf("</tbody>", first) - first);

			string[] lines = strOutput.Split('\n');
			strOutput = "";
			int len;
			string templine;
			foreach (string line in lines)
			{
				if (line.IndexOf("title=") > 0)
				{
					len = line.IndexOf("title=") + 6;
					templine = line.Substring(len);
					len = templine.IndexOf(">");
					strOutput = strOutput + templine.Substring(1, len - 2) + '\n';
				}
			}
			//strOutput = strOutput.Replace("\n", "");
			//strOutput = strOutput.Substring(0, strOutput.Length - 2);
			return bowlerslist;
		}

		private string GetWebsite(string basehttp)
		{
			string strOutput;
			HttpWebRequest wrRequest = (HttpWebRequest)WebRequest.Create(basehttp);
			WebResponse wrResponse = wrRequest.GetResponse();

			using (StreamReader sr = new StreamReader(wrResponse.GetResponseStream()))
			{
				strOutput = sr.ReadToEnd();
				//savestrOutput = strOutput;
				sr.Close();
			}
			wrResponse.Close();
			return strOutput;
		}

		public List<Team> getteams(string leaguecenter, string leaguename, string weekno, string year, string season, string leaguenum)
		{
			string strOutput;
			string savestrOutput;
			string lookup;
			string[] mleague;
			string[] mteam;

			//lookup = teamhttp;

			lookup = string.Format("https://www.leaguesecretary.com/bowling-centers/{0}/bowling-leagues/{1}/league-standings/{2}/{3}/{4}/{5}", leaguecenter,leaguename, year,season, weekno, leaguenum);
			//lookup = string.Format("https://www.leaguesecretary.com/bowling-centers/search");  

			HttpWebRequest wrRequest = (HttpWebRequest)WebRequest.Create(lookup);
			WebResponse wrResponse = wrRequest.GetResponse();

			using (StreamReader sr = new StreamReader(wrResponse.GetResponseStream()))
			{
				strOutput = sr.ReadToEnd();
				savestrOutput = strOutput;
				sr.Close();
			}
			wrResponse.Close();
			teamlist.Clear();
			int first = strOutput.IndexOf("<tbody>") + "<tbody>".Length;
			int last = strOutput.LastIndexOf("</tbody>");
			strOutput = strOutput.Substring(first, last - first);

			strOutput = Regex.Replace(strOutput, @"<!(.|\s)*?>", "");
			strOutput = Regex.Replace(strOutput, "</?[a-z][a-z0-9]*[^<>]*>", " ");
			strOutput = Regex.Replace(strOutput, @"<!--(.|\s)*?-->", "");
			strOutput = Regex.Replace(strOutput, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			strOutput = Regex.Replace(strOutput, "<style.*?</style>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			strOutput = Regex.Replace(strOutput, "<TBODY>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			strOutput = Regex.Replace(strOutput, "</TBODY", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			strOutput = Regex.Replace(strOutput, "<THEAD>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			strOutput = Regex.Replace(strOutput, "</THEAD>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

			strOutput = strOutput.Replace("\t", "");
			strOutput = strOutput.Replace(" \r\n  \r\n  ", "\n");
			strOutput = strOutput.Replace(" \r\n \r\n", "");
			strOutput = strOutput.Replace("\r\n \r\n", "");
			strOutput = strOutput.Replace("\r\n  \r\n", "\n");
			//strOutput = strOutput.Substring(5);
			strOutput = strOutput.Replace("    ", "   ");
			strOutput = strOutput.Replace("   ", "  ");
			strOutput = strOutput.Replace("  ", ";");
			strOutput = strOutput.Replace("\r", "");
			mleague = strOutput.Split('\n');
			foreach (string teams in mleague)
			{
				mteam = teams.Split(';');
				team.Add(new Team
				{
					TeamNo = mteam[1],
					TeamName = mteam[2],
					Won = mteam[3],
					Loss = mteam[4],
					Pct = mteam[5],
					Ytd = mteam[6],
					Avg = mteam[7],
					Pins = mteam[8],
					Hsg = mteam[9],
					Hss = mteam[10]
				});
				mBowlerName = mteam[2];
				mTM = mteam[1];
				mPOS = "0";
				mGMS = "0";
				//mPINS = mteam[8];
				mAVG = "0";
				mHCP = "0";
				mHHG = "0";
				mHHS = "0";
				mHSG = mteam[9];
				mHSS = mteam[10];
				mMIB = "0";
				mHTTPbowler = "0";
				mHTTPteam = "0";

				bowlerslist.Add(new BowlersList(mBowlerName,
								mTM,
								mPOS,
								mGMS,
								mPINS,
								mAVG,
								mHCP,
								mHHG,
								mHHS,
								mHSG,
								mHSS,
								mMIB,
								mSeq,
								mHTTPbowler,
								mHTTPteam
								));


			}
			//team.Sort
			
			return team;
		}

		public List<BowlersList> extract(string leaguecenter, string leaguename, string weekno, string year,string season, string leaguenum, string http) //, string suf)
		{
			string strOutput;
			string savestrOutput;
			string lookup;
			string[] mleague;
			string[] xleague;

			string[] mbowler;
			string[] suf = { "", "2", "3", "4", "5" };
			System.DateTime moment = System.DateTime.Now;

			//http = "https://www.leaguesecretary.com/bowling-centers/west-lanes-bowl-omaha-nebraska/bowling-leagues/tuesday-night-doubles20-21/bowler-list/2021/summer/11/105789/";
			pages = 2;

			savebowlers = new List<BowlersList>();

			if (weekno == "-1")
			{
				weekno = "28";
				year = (moment.Year - 1).ToString();
			}
			bowlerslist.Clear();

			for (int dx = 0; dx < pages; dx++)
			{
				//if (weekno == "0")
				//{
				//    lookup = string.Format("http://www.leaguesecretary.com/LeagueBowlerList.aspx?LID={2}&YearNum={0}&Season=f&WeekNum={1}", (Convert.ToInt32(year) - 1).ToString(), 28, leaguenum);
				//}
				//else
				//{
				//    lookup = string.Format("https://www.leaguesecretary.com/bowling-centers/west-lanes-bowl-omaha-nebraska/bowling-leagues/tues-mut-mixed19/bowler-list/{0}/fall/{1}/{3}{2}", year, weekno, suf[dx],leaguenum);
				//}
				//lookup = http + suf[dx];
				lookup = string.Format("https://www.leaguesecretary.com/bowling-centers/{0}/bowling-leagues/{1}/bowler-list/{2}/{3}/{4}/{5}/{6}", leaguecenter, leaguename, year,season,weekno,leaguenum, suf[dx]);
				HttpWebRequest wrRequest = (HttpWebRequest)WebRequest.Create(lookup);
				WebResponse wrResponse = wrRequest.GetResponse();

				using (StreamReader sr = new StreamReader(wrResponse.GetResponseStream()))
				{
					strOutput = sr.ReadToEnd();
					savestrOutput = strOutput;
					sr.Close();
				}
				wrResponse.Close();
				bowlers.Clear();
				int first = strOutput.IndexOf("<tbody>") + "<tbody>".Length;
				int last = strOutput.LastIndexOf("</tbody>");
				strOutput = strOutput.Substring(first, last - first);

				strOutput = Regex.Replace(strOutput, @"<!(.|\s)*?>", "");
				strOutput = Regex.Replace(strOutput, "</?[a-z][a-z0-9]*[^<>]*>", " ");
				strOutput = Regex.Replace(strOutput, @"<!--(.|\s)*?-->", "");
				strOutput = Regex.Replace(strOutput, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
				strOutput = Regex.Replace(strOutput, "<style.*?</style>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
				strOutput = Regex.Replace(strOutput, "<TBODY>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
				strOutput = Regex.Replace(strOutput, "</TBODY", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
				strOutput = Regex.Replace(strOutput, "<THEAD>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
				strOutput = Regex.Replace(strOutput, "</THEAD>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

				strOutput = strOutput.Replace("\t", "");
				strOutput = strOutput.Replace(" \r\n  \r\n  ", "\n");
				strOutput = strOutput.Replace(" \r\n \r\n", "");
				strOutput = strOutput.Substring(7);
				//strOutput = strOutput.Replace("    ", ";");
				//strOutput = strOutput.Replace("   ", ";");
				//strOutput = strOutput.Replace("  ", ";");
				strOutput = strOutput.Replace("\r", "");

				savestrOutput = savestrOutput.Substring(first, last - first);
				savestrOutput = Regex.Replace(savestrOutput, @"\t<tr class=\""rgRow\"" id=\""ctl00_MainContent_RadGrid1_ctl00__[0-9]\"">\r", "");
				savestrOutput = Regex.Replace(savestrOutput, @"\t</tr><tr class=\""rgAltRow\"" id=\""ctl00_MainContent_RadGrid1_ctl00__[0-9]\"">\r", "");
				savestrOutput = Regex.Replace(savestrOutput, @"\t</tr><tr class=\""rgRow\"" id=\""ctl00_MainContent_RadGrid1_ctl00__[0-9]\"">\r", "");
				savestrOutput = savestrOutput.Replace("\t", "");
				savestrOutput = savestrOutput.Replace("\r", "");
				//savestrOutput = savestrOutput.Replace("\n", "");

				mleague = strOutput.Split('\n');


				string regex = "href=\"(.*)\"";
				List<string> list = new List<string>();
				int db = 0;
				foreach (Match match in Regex.Matches(savestrOutput, regex))
				{
					bowlershttp.Add(match.Value);
					list.Add(match.Value);
					db++;
				}
				xleague = list.ToArray();

				//savestrOutput = match.Value;
				//xleague = savestrOutput.Split('\n');
				if (mleague[0] == "There was no data found for your request. " && dx > 0)
				{
					return bowlerslist;
				}
				if (mleague[0] == "There was no data found for your request. ")
				{
					bowlerslist.Clear();
					return bowlerslist;
				}
				int dm = 0;
				int idx;
				string temp;
				do
				{
					idx = mleague[dm].IndexOf("    ");
					temp = mleague[dm].Substring(idx + 4);
					temp = temp.Replace("  ", ";");
					mleague[dm] = mleague[dm].Substring(0, idx) + ";" + temp;
					idx = xleague[dm].IndexOf("href") + 6;
					temp = xleague[dm].Substring(idx, xleague[dm].IndexOf(">") - 7);
					mleague[dm] = mleague[dm] + ";" + temp;
					idx = xleague[dm].IndexOf("href", 6) + 6;
					temp = xleague[dm].Substring(idx);
					temp = temp.Substring(0, temp.Length - 1);
					mleague[dm] = mleague[dm] + ";" + temp;
					dm++;
				} while (dm < mleague.Count());
				if (dx == 0)
				{
					bowlerslist.Add(new BowlersList("New Bowler",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0"
										));
				}

				foreach (string bowler in mleague)
				{
					if (bowler != "" && mleague.Length > 1)
					{
						mbowler = bowler.Split(';');
						if (bowlerslist.Count > 2 && mbowler[0] == bowlerslist[1].Name) { break; }
						string[] firstlast = mbowler[0].Split(',');
						mBowlerName = firstlast[1].Trim() + " " + firstlast[0];
						mTM = mbowler[1];
						mPOS = mbowler[2];
						mGMS = mbowler[3];
						mPINS = mbowler[4];
						mAVG = mbowler[5];
						mHCP = mbowler[6];
						mHHG = mbowler[7];
						mHHS = mbowler[8];
						mHSG = mbowler[9];
						mHSS = mbowler[10];
						mMIB = mbowler[11];
						mHTTPbowler = mbowler[12];
						mHTTPteam = mbowler[13];

						bowlerslist.Add(new BowlersList(mBowlerName,
										mTM,
										mPOS,
										mGMS,
										mPINS,
										mAVG,
										mHCP,
										mHHG,
										mHHS,
										mHSG,
										mHSS,
										mMIB,
										mSeq,
										mHTTPbowler,
										mHTTPteam
										));
					}
				}
			}

			if (bowlerslist.Count > 0)
			{
				for (int dx = 0; dx < bowlerslist.Count; dx++)
				{
					if (Int32.Parse(bowlerslist[dx].GMS) > 0)
					{
						savebowlers = bowlerslist;
						return bowlerslist;
					}
				}
			}
			bowlerslist.Clear();
			return bowlerslist;
		}
		public List<BowlersList> addbowler(string bowler)
		{
			bowlerslist = savebowlers;
			bowlerslist.Add(new BowlersList(bowler,
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0",
										"0"
										));
			bowlerslist.Sort((x,y)=>x.Name.CompareTo(y.Name));
			return bowlerslist;
		}
	}

}
