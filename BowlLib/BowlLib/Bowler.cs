using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlerLib
{
    public class Bowler
    {
        public bool Selected { get; set; }
        public string Name { get; set; }
        public string Average { get; set; }
        public string Handicap { get; set; }
        public int Game1 { get; set; }
        public int Game2 { get; set; }
        public int Game3 { get; set; }
        public int Series { get; set; }
        public int HcpSeries { get; set; }
    }
}
