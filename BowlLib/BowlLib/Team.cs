using System;
using System.Collections.Generic;
using System.Text;

namespace BowlLib
{
    public class Team //:IComparable<Team>
    {
        public string TeamNo { get; set; }
        public string TeamName { get; set; }
        public string Won { get; set; }
        public string Loss { get; set; }
        public string Pct { get; set; }
        public string Ytd { get; set; }
        public string Avg { get; set; }
        public string Pins { get; set; }
        public string Hsg { get; set; }
        public string Hss { get; set; }

        //public int CompareTo(Team other)
        //{
        //    if (this.TeamName > other.TeamName)
        //        return 1;
        //    else if (this.TeamName < other.TeamName)
        //        return -1;
        //    else
        //        return 0;
        //}
    }
}
