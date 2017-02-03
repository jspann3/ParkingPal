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
        public int[] colors { get; }
        public int spotsRemaining { get; set; }

        public string whichProximity { get; }

        public SimpleLot(string ID, int[] Colors, int SpotsRemaining, string WhichProximity)
        {
            id = ID;
            colors = Colors;
            spotsRemaining = SpotsRemaining;
            whichProximity = WhichProximity;
        }
    }
}
