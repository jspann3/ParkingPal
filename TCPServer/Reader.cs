using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThingMagic;
using System.Collections;

namespace TCPServer
{
    public class Reader
    {
        static ArrayList ids = new ArrayList();
        static ArrayList dts = new ArrayList();
        static ArrayList epc = new ArrayList();
        public Reader()
        {
            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            Reader reader = create();
            connect(reader);
            setROOP(reader);
            //filter(reader);
            asyncRead(reader);

        }

        public ArrayList tagsInRange()
        {
            Console.WriteLine("In range");
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
                Console.WriteLine(e.Message);
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
            //setNumberOfReads(reader);
            /*StreamWriter file = File.CreateText("C:/Users/John/Documents/Visual Studio 2015/Projects/ReaderExample/tagsAsync.txt");
            file.WriteLine("");
            file.Close();*/
            reader.TagRead += OnTagRead;
            //reader.StopReading();
        }

        public void OnTagRead(object sender, TagReadDataEventArgs e)
        {

            string currentDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            //StreamWriter file = File.AppendText("C:/Users/John/Documents/Visual Studio 2015/Projects/ReaderExample/tagsAsync.txt");
            //file.WriteLine(e.TagReadData.ToString() + "\\n");
            string input = e.TagReadData.ToString();
            string id = input.Substring(4).Remove(24);
            string dt = input.Substring(48).Remove(23);
            string idANDdt = id + " " + dt;

            if (ids.Contains(id))
            {
                /*for (int i = 0; i < epc.Count; i++)
                {
                    if (Convert.ToDateTime(dts[i]) < Convert.ToDateTime(currentDateTime))
                    {
                        //ids.RemoveAt(i);
                        //dts.RemoveAt(i);
                        //epc.RemoveAt(i);
                        
                    }
                }*/
                ids.Clear();
                dts.Clear();
                epc.Clear();
                //Console.WriteLine("ADDING IF");
                ids.Add(id);
                dts.Add(dt);
                epc.Add(idANDdt);
            }
            else if (!ids.Contains(id))
            {
                //Console.WriteLine("ADDING ELSE");
                ids.Add(id);
                dts.Add(dt);
                epc.Add(idANDdt);
            }
            /*Console.WriteLine("Tags in Reader's Range: ");
            for (int i = 0; i < epc.Count; i++)
            {
                //Console.WriteLine(epc.Count);
                Console.WriteLine(epc[i]);
            }*/
            //file.Close();
        }

        public void setNumberOfReads(Reader reader)
        {
            StopOnTagCount tagCount = new StopOnTagCount();
            tagCount.N = 10;
            StopTriggerReadPlan stopReadPlan = new StopTriggerReadPlan(tagCount);
            reader.ParamSet("/reader/read/plan", stopReadPlan);
        }

        public void filter(Reader reader)
        {
            TagProtocol protocol = TagProtocol.GEN2;
            TagData tag = new TagData("000000000000000000000001");
            SimpleReadPlan simplePlan = new SimpleReadPlan(null, protocol, tag, null, false, 1000);
            reader.ParamSet("/reader/read/plan", simplePlan);

        }

        public void write(TagFilter tf, TagData td)
        {
            //td = 000000000000000000000001;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Tags in Reader's Range: ");
            for (int i = 0; i < epc.Count; i++)
            {
                Console.WriteLine(epc[i]);
            }
            /*textBox.Text = "Tags in Reader's Range";
            for (int i = 0; i < epc.Count; i++)
            {
                Console.WriteLine(epc.Count);
                textBox.Text += epc[i].ToString();
            }*/
        }
    }
}
