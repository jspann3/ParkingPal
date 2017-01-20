using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    class Lot
    {
        public int id { get; }
        public int[] colors { get; }
        public int maxSpaces { get; }

        public Lot(int ID, int[] Colors, int MaxSpaces)
        {
            id = ID;
            colors = Colors;
            maxSpaces = MaxSpaces;
        }

        
    }
}
