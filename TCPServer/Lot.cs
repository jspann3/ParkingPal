using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    public class Lot
    {
        public int id { get; }
        public int[] colors { get; }
        public int maxSpaces { get; }

        private List<Tag> tagList = new List<Tag>();

        public Lot(int ID, int[] Colors, int MaxSpaces)
        {
            id = ID;
            colors = Colors;
            maxSpaces = MaxSpaces;
        }

        public int GetTagListLength()
        {
            return tagList.Count;
        }

        public int SpacesLeft()
        {
            return maxSpaces - GetTagListLength();
        }

        public void AddTag(Tag t)
        {
            tagList.Add(t);
        }
    }
}
