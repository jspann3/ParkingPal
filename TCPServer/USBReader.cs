using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ThingMagic;

namespace TCPServer
{
    public class USBReader
    {
        public Reader actualReader;

        public static ArrayList ids = new ArrayList();
        public static ArrayList dts = new ArrayList();
        public static ArrayList epc = new ArrayList();

        private Lot lot;

        public USBReader(Lot Lot)
        {
            lot = Lot;
            actualReader = create();
            connect(actualReader);
            setROOP(actualReader);
            //filter(reader);
            asyncRead(actualReader);
        }

        public ArrayList getEPC()
        {
            return epc;
        }

        public Reader create()
        {
            //create the Reader object
            string uri = "eapi:///com3";
            Reader reader = Reader.Create(uri);

            return reader;
        }

        public void connect(Reader reader)
        {
            //attempt to connect to the Reader
            try
            {
                reader.Connect();
            }
            catch (IOException e)
            {
                Console.WriteLine("failed to connect");
                return;
            }
        }

        public void setROOP(Reader reader)
        {
            //set the region of operation parameter (required)
            string[] list = reader.ParamList();
            Reader.Region[] regions = (Reader.Region[])reader.ParamGet("/reader/region/supportedRegions");

            reader.ParamSet(list[38], Reader.Region.NA);
        }

        public void asyncRead(Reader reader)
        {
            reader.StartReading();
            reader.TagRead += OnTagRead;
        }

        public void OnTagRead(object sender, TagReadDataEventArgs e)
        {

            string currentDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            string input = e.TagReadData.ToString();
            string id = input.Substring(4).Remove(24);
            string dt = input.Substring(48).Remove(23);

            DateTime timeRead = new DateTime(Convert.ToInt32(dt.Substring(0, 4)), Convert.ToInt32(dt.Substring(5, 2)), Convert.ToInt32(dt.Substring(8, 2)), Convert.ToInt32(dt.Substring(11, 2)),
                Convert.ToInt32(dt.Substring(14, 2)), Convert.ToInt32(dt.Substring(17, 2)), Convert.ToInt32(dt.Substring(20, 3)));

            Tag tag = new Tag(id, 0, timeRead);
            lot.TagRead(tag);

            

            string idANDdt = id + " " + dt;

            if (ids.Contains(id))
            {
                ids.Clear();
                dts.Clear();
                epc.Clear();
                ids.Add(id);
                dts.Add(dt);
                epc.Add(idANDdt);
            }
            else if (!ids.Contains(id))
            {
                ids.Add(id);
                dts.Add(dt);
                epc.Add(idANDdt);
            }
        }

        public void setNumberOfReads(Reader reader)
        {
            StopOnTagCount tagCount = new StopOnTagCount();
            tagCount.N = 10;
            StopTriggerReadPlan stopReadPlan = new StopTriggerReadPlan(tagCount);
            reader.ParamSet("/reader/read/plan", stopReadPlan);
        }
    }
}
