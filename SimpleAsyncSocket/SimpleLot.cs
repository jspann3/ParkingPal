using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    public class SimpleLot
    {
        public string id { get; }
        public string color { get; }
        public int spotsRemaining { get; set; }

        public SimpleLot(string ID, string Color, int SpotsRemaining)
        {
            id = ID;
            color = Color;
            spotsRemaining = SpotsRemaining;
        }
    }
}
