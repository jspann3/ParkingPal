using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    public class LotNoReader
    {
        public string id { get; }
        public int[] colors { get; }
        public int maxSpaces { get; }
        public int spotsFilled { get; set; }
        public List<Tag> tagList { get; set; }

        public LotNoReader(string ID, int[] Colors, int MaxSpaces)
        {
            id = ID;
            colors = Colors;
            maxSpaces = MaxSpaces;
            tagList = new List<Tag>();
        }

        public int SpacesLeft()
        {
            return maxSpaces - spotsFilled;
        }
    }
}

