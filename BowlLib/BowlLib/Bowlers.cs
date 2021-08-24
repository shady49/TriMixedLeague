using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlerLib
{
    public class Bowlers
    {
        public string Name { get; set; }
        public int Ave { get; set; }
        public int Hcp { get; set; }
        public int Game1 { get; set; }
        public int Game2 { get; set; }
        public int Game3 { get; set; }
        public int Series { get; set; }
        public int Game1Hcp { get; set; }
        public int Game2Hcp { get; set; }
        public int Game3Hcp { get; set; }
        public int SeriesHcp { get; set; }
        public Bowlers(string name,
                       int average,
                       int handicap,
                       int game1,
                       int game2,
                       int game3,
                       int series,
                       int game1Hcp,
                       int game2Hcp,
                       int game3Hcp,
                       int seriesHcp)
        {
            Name = name;
            Ave = average;
            Hcp = handicap;
            Game1 = game1;
            Game2 = game2;
            Game3 = game3;
            Series = series;
            Game1Hcp = game1Hcp + handicap;
            Game2Hcp = game2Hcp + handicap;
            Game3Hcp = game3Hcp + handicap;
            SeriesHcp = seriesHcp + handicap * 3;
        }
        public override string ToString()
        {
            return Name;
        }
    }

    public class TeamList
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
        public TeamList(
            string teamno,
            string teamname,
            string won,
            string loss,
            string pct,
            string ytd,
            string avg,
            string pins,
            string hsg,
            string hss)
        {
            TeamNo = teamno;
            TeamName = teamname;
            Won = won;
            Loss = loss;
            Pct = pct;
            Ytd = ytd;
            Avg = avg;
            Pins = pins;
            Hsg = hsg;
            Hss = hss;
        }

    }
    

        public class BowlersList
        {
            
            public string Name { get; set; }
            public string TM { get; set; }
            public string POS { get; set; }
            public string GMS { get; set; }
            public string PINS { get; set; }
            public string AVG { get; set; }
            public string HCP { get; set; }
            public string HHG { get; set; }
            public string HHS { get; set; }
            public string HSG { get; set; }
            public string HSS { get; set; }
            public string MIB { get; set; }
            public string Seq { get; set; }
            public string HTTPbowler { get; set; }
            public string HTTPteam { get; set; }

        public BowlersList( 
                           string name,
                           string tm,
                           string pos,
                           string gms,
                           string pins,
                           string avg,
                           string hcp,
                           string hhg,
                           string hhs,
                           string hsg,
                           string hss,
                           string mib,
                           string seq,
                           string mHTTPbowler,
                           string mHTTPteam)
            {
                
                Name = name;
                TM = tm;
                POS = pos;
                GMS = gms;
                PINS = pins;
                AVG = avg;
                HCP = hcp;
                HHG = hhg;
                HHS = hhs;
                HSG = hhg;
                HSS = hss;
                MIB = mib;
                Seq = seq;
                HTTPbowler = mHTTPbowler;
                HTTPteam = mHTTPteam;
        }
            public override string ToString()
            {
                return Name;
            }
        }
    public class BowlersHTTP
    {
        public string BowlerHTTP { get; set; }
        public string TeamHTTP { get; set; }

        public BowlersHTTP(
            string bowlerhttp, string teamhttp)
        {
            BowlerHTTP = bowlerhttp;
            TeamHTTP = teamhttp;
        }
        public override string ToString()
        {
            return BowlerHTTP;
        }
    }

}
