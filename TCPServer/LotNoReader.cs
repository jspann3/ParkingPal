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
        public int incorrectTags { get; set; }

        public LotNoReader(string ID, int[] Colors, int MaxSpaces)
        {
            id = ID;
            colors = Colors;
            maxSpaces = MaxSpaces;
            tagList = new List<Tag>();
            incorrectTags = 0;
        }

        public int SpacesLeft()
        {
            return maxSpaces - spotsFilled;
        }

        public void AddTag(Tag t)
        {
            tagList.Add(t);
            bool correctColor = false;
            foreach (int color in colors)
            {
                if (t.color == color)
                {
                    correctColor = true;
                    break;
                }
            }

            if (!correctColor)
            {
                incorrectTags++;
            }
        }

        public void RemoveTags()
        {
            List<Tag> tagsToRemove = new List<Tag>();
            foreach (Tag t in tagList)
            {
                if (t.flagged)
                {
                    tagsToRemove.Add(t);

                    bool correctColor = false;
                    foreach (int color in colors)
                    {
                        if (t.color == color)
                        {
                            correctColor = true;
                            break;
                        }
                    }

                    if (!correctColor)
                    {
                        incorrectTags--;
                    }
                }                
            }

            tagList.RemoveAll(tag => tag.flagged);
        }
    }
}

