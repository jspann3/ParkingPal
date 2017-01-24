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
        private List<Tag> removedTagList = new List<Tag>();

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

        public void TagRead(Tag tag)
        {
            bool addTag = true;
            DateTime currentDateTime = DateTime.Now;
            DateTime tagTime = new DateTime()

            foreach (Tag t in tagList)
            {
                if (tag.id == t.id)     // tag is already in tagList
                {
                    addTag = false;
                    TimeSpan ts = currentDateTime - currentDateTime;
                }
            }

            foreach (Tag t in removedTagList)
            {
                if (tag.id == t.id)     // tag is already in removedTagList
                {
                    addTag = false;
                }
            }

            if (addTag)
                AddTag(tag);
        }
    }
}
