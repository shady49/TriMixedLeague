using System;

namespace TriMixedLeague.Models
{
    public class Bowler
    {
        public string Id { get; set; }
        public string Indicator { get; set; }
        public string Name { get; set; }
        public int Average { get; set; }
        public int Handicap { get; set; }
        public int Game1 { get; set; }
        public int Game2 { get; set; }
        public int Game3 { get; set; }
        public int Series { get; set; }
        public float Pt1 { get; set; }
        public float Pt2 { get; set; }
        public float Pt3 { get; set; }
        public float PtSeries { get; set; }

        public string BowlerHTTP { get; set; }
        public string TeamHTTP { get; set; }

    }
}