using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TriMixedLeague.ViewModels
{
	public class BuildLeagueModel : INotifyPropertyChanged
	{
		public List<string> source = new List<string>();
		public event PropertyChangedEventHandler PropertyChanged;
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
		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			var changed = PropertyChanged;
			if (changed == null)
				return;

			changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	
}
