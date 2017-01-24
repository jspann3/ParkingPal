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

        public USBReader reader;

        private List<Tag> tagList = new List<Tag>();
        private List<Tag> removedTagList = new List<Tag>();

        public Lot(int ID, int[] Colors, int MaxSpaces)
        {
            id = ID;
            colors = Colors;
            maxSpaces = MaxSpaces;
            reader = new USBReader(this);
        }

        public int GetTagListLength()
        {
            return tagList.Count;
        }

        public int GetRemovedTagListLength()
        {
            return removedTagList.Count;
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
            bool removeTag = false;
            int tagToRemoveIndex = -1;

            DateTime currentTime = DateTime.Now;
            //DateTime tagTime = tag.lastReadTime;

            foreach (Tag t in tagList)
            {
                if (tag.id == t.id)     // tag is already in tagList
                {
                    addTag = false;
                    DateTime tagTime = t.lastReadTime;
                    TimeSpan timeSpan = currentTime - tagTime;
                    if (timeSpan.Seconds > 5)
                    {
                        removeTag = true;
                        tagToRemoveIndex = tagList.IndexOf(t);
                        removedTagList.Add(tag);
                        break;
                    }
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

            if (removeTag)
                tagList.Remove(tagList[tagToRemoveIndex]);
        }

        public void RemoveListCheck()
        {
            DateTime currentTime = DateTime.Now;
            List<int> toRemoveListIndexes = new List<int>();
            
            foreach (Tag t in removedTagList)
            {
                TimeSpan timeSpan = currentTime - t.lastReadTime;
                if (timeSpan.Seconds > 10)
                {
                    toRemoveListIndexes.Add(removedTagList.IndexOf(t));
                }
            }
            
            foreach (int i in toRemoveListIndexes)
            {
                removedTagList.Remove(removedTagList[i]);
            }
        }
    }
}
