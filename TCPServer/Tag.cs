using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    public class Tag
    {
        public string id { get; }
        public int color { get; }
        public DateTime lastReadTime { get; }

        public Tag(string ID, int Color, DateTime Time)
        {
            id = ID;
            color = Color;
            lastReadTime = Time;
        }
    }
}
