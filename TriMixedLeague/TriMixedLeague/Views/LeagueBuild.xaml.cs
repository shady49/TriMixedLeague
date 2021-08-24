using BowlerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriMixedLeague.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LeagueBuild : ContentPage
	{
		public Extract extract = new Extract();
		public List<Data> DataList { get; set; }
		public List<Name> NameList { get; set; }
		public LeagueBuild()
		{
			InitializeComponent();
		}

		public class Data
		{
			public int Key { get; set; }
			public string Value { get; set; }
			public string Http { get; set; }
		}
		public class Name
		{
			public string name { get; set; }
		}

		private void Select_Area_Click(object sender, EventArgs e)
		{
			//DataList=GetData();
			//datalist.ItemDisplayBinding = Data.Value;
			List<Name> centers = new List<Name>();
			DataList = GetData();
			System.Collections.IList list = DataList;
			for (int i = 0; i < list.Count; i++)
			{
				string center = DataList[i].Value; //(string)list[i];
				centers.Add(new Name
				{
					name = center
				});
			}
			datalist.ItemsSource = centers;
		}

		private void Button_Clicked_Center(object sender, EventArgs e)
		{

		}

		private void Button_Clicked_League(object sender, EventArgs e)
		{

		}

		private void Picker_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
		private void SelectItem(object sender, EventArgs e)
		{

		}

		private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{

		}
		public List<Data> GetData()
		{
			int key = 1;
			string strOutput = extract.getlocations("https://www.leaguesecretary.com/bowling-centers/search");
			string[] loc = strOutput.Split('\n');
			int dx;
			int dy;
			int ddx;
			int ddy;

			List<Data> data = new List<Data>();
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
						data.Add(new Data
						{
							Key = key,
							Value = city.Substring(dx, dy),
							Http = city.Substring(ddx, ddy)
						});
						key++;
					}


				}
			};

			return data;
		}
	}
}