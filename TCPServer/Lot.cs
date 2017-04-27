using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThingMagic;

namespace TCPServer
{
    public class Lot
    {
        public string id { get; }
        public int[] colors { get; }
        public int maxSpaces { get; }

        public USBReader reader;

        private List<Tag> tagList = new List<Tag>();
        private List<Tag> removedTagList = new List<Tag>();

        public Lot(string ID, int[] Colors, int MaxSpaces)
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

        public void AddTag(Tag tag)
        {           
            //Write(true, tag);
            tagList.Add(tag);
        }

        public void RemoveTag(Tag tag)
        {
            //Write(false, tag);
            tagList.Remove(tag);
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
                    if (timeSpan.Seconds > 3)
                    {
                        removeTag = true;
                        tagToRemoveIndex = tagList.IndexOf(t);
                        removedTagList.Add(tag);
                        break;
                    }
                }
            }

            foreach (Tag t in removedTagList)
                if (tag.id == t.id)     // tag is already in removedTagList
                    addTag = false;

            if (addTag)
                AddTag(tag);

            if (removeTag)
                RemoveTag(tagList[tagToRemoveIndex]);
        }

        public void RemoveListCheck()
        {
            DateTime currentTime = DateTime.Now;
            List<Tag> toRemoveListIndexes = new List<Tag>();

            foreach (Tag t in removedTagList)
            {
                TimeSpan timeSpan = currentTime - t.lastReadTime;
                if (timeSpan.Seconds > 3)
                    toRemoveListIndexes.Add(t);
            }

            removedTagList.RemoveAll(t => toRemoveListIndexes.Contains(t));
            
        }

        public void Write(bool writeID, Tag tag)
        {
            string newTagID = "00";

            if (writeID)
                newTagID = id;

            foreach (Tag t in tagList)
            {
                if (t.id == tag.id)
                    t.id = newTagID + tag.id.Substring(2);
            }
            foreach (Tag t in removedTagList)
            {
                if (t.id == tag.id)
                    t.id = newTagID + tag.id.Substring(2);
            }

            TagData newEPC = new TagData(newTagID + tag.id.Substring(2));
            try
            {
                reader.actualReader.WriteTag(null, newEPC);
            }
            catch
            {
                Console.WriteLine("===========");
                Console.WriteLine("ERROR FOUND");
                Console.WriteLine("===========");
            }
        }
    }
}