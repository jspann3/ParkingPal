using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThingMagic;

namespace TCPServer
{
    public partial class Server : Form
    {
        private Socket serverSocket;
        private byte[] buffer;

        public Lot testLot;
        private List<LotNoReader> lotNoReaderList = new List<LotNoReader>();

        public Server()
        {
            string path = @"C:\Users\BronzeMoss\Source\Repos\ParkingPal\SimpleAsyncSocket\bin\Debug\TCPClient.exe";
            Process.Start(path);
            InitializeComponent();
        }

        private void StartServer()
        {
            try
            {
                //readLotNRs(@"C:\Users\BronzeMoss\Source\Repos\ParkingPal\TCPServer\Lots.txt");
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(IPAddress.Any, 3335));
                serverSocket.Listen(100);
                //testLot = new Lot("92", new int[] { 0 }, 35);

                serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void readLotNRs(string fileName)
        {
            string input = File.ReadAllText(fileName);

            string id = "";
            int[] colors = new int[0];
            int maxSpaces = -1;
            int i = 0;

            foreach (var row in input.Split('\n'))
            {
                i = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    if (i == 0)
                        id = col.Trim().ToString();
                    else if (i == 1)
                    {
                        string currColors = col.Trim().ToString();
                        colors = new int[currColors.Length];
                        for (int j = 0; j < colors.Length; j++)
                            colors[j] = int.Parse(currColors[j].ToString());
                    }
                    else if (i == 2)
                        maxSpaces = int.Parse(col.Trim());
                    i++;
                }
                LotNoReader lnr = new LotNoReader(id, colors, maxSpaces);
                lotNoReaderList.Add(lnr);
            }           
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = serverSocket.EndAccept(ar);
                buffer = new byte[clientSocket.ReceiveBufferSize];
                clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), clientSocket);
                serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

                AppendToTextBox("Client has connected");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                Socket current = (Socket)ar.AsyncState;
                int received = current.EndReceive(ar);

                if (received == 0)  //client has disconnected
                    return;

                Array.Resize(ref buffer, received);
                string text = Encoding.ASCII.GetString(buffer);
                
                if (text == "<LOT>")
                {
                    byte[] buffer = Encoding.ASCII.GetBytes(testLot.GetTagListLength().ToString());
                    current.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), current);
                }

                
                if (text == "<LO1>")
                {
                    //should happen based on a timer
                    try
                    {
                        testLot.RemoveListCheck();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    byte[] buffer = Encoding.ASCII.GetBytes(testLot.GetRemovedTagListLength().ToString());
                    current.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), current);
                }

                if (text == "<LNR>")
                {
                    try
                    {
                        byte[] buffer = Encoding.ASCII.GetBytes(lotNoReaderList[1].maxSpaces.ToString());
                        current.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), current);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (text.StartsWith("<SEND>"))
                {
                    try
                    {
                        byte[] buffer = Encoding.ASCII.GetBytes(LotNumbersToReturn(text.Substring(6)));     //start after word <SEND>
                        current.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), current);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                AppendToTextBox("Client says: " + text);
                Array.Resize(ref buffer, current.ReceiveBufferSize);
                current.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), current);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket current = (Socket)ar.AsyncState;
                AppendToTextBox("Server sent msg");
                current.EndSend(ar);
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string LotNumbersToReturn(string str)
        {
            string[] idsToSend = str.Split(' ');
            List<string> lotNumberToSend = new List<string>();
            foreach (String s in idsToSend)
            {
                foreach (LotNoReader lot in lotNoReaderList)
                {
                    if (s == lot.id)
                        lotNumberToSend.Add(lot.maxSpaces.ToString());
                }
            }

            string lotNumbersToReturn = "";
            foreach (string lotNum in lotNumberToSend)
                lotNumbersToReturn += lotNum + " ";

            return lotNumbersToReturn;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartServer();
        }

        private void AppendToTextBox(string text)
        {
            MethodInvoker invoker = new MethodInvoker(delegate
                {
                    textBox.Text += text + "\r\n";
                });

            this.Invoke(invoker);
        }

        private void ErrorCheck()
        {
            for (int i = 0; i < lotNoReaderList.Count - 1; i++)                 // Lot to check from
            {
                List<Tag> tagsToCheck = new List<Tag>();
                List<LotNoReader> lotsToCheck = new List<LotNoReader>();

                foreach (Tag checkFromTag in lotNoReaderList[i].tagList)        // Each Tag in (Lot to check from)
                {
                    for (int j = i + 1; j < lotNoReaderList.Count; j++)         // Lot to check
                    {
                        foreach (Tag checkTag in lotNoReaderList[j].tagList)    // Each Tag in (Lot to check)
                        {
                            if (checkTag.id == checkFromTag.id)
                            {
                                tagsToCheck.Add(checkTag);
                                lotsToCheck.Add(lotNoReaderList[j]);
                            }
                        }
                    }

                    if (tagsToCheck.Count > 0)
                    {
                        lotsToCheck.Add(lotNoReaderList[i]);    // Add tag to check from to check list
                        tagsToCheck.Add(checkFromTag);

                        RemoveCheck(tagsToCheck, lotsToCheck);

                        lotsToCheck.Clear();                    // Clear lists for next pass
                        tagsToCheck.Clear();
                    }
                }
            }

            foreach (LotNoReader lot in lotNoReaderList)
                lot.tagList.RemoveAll(tag => tag.flagged);
        }

        private void RemoveCheck(List<Tag> tList, List<LotNoReader> lList)
        {
            Tag latestTag = tList[0];
            LotNoReader latestLot = lList[0];

            for (int i = 0; i < tList.Count; i++)
            {
                if (tList[i].lastReadTime.CompareTo(latestTag.lastReadTime) > 0)  // > 0 means later
                {
                    latestTag = tList[i];
                    latestLot = lList[i];
                }
            }

            lList.Remove(latestLot);
            tList.Remove(latestTag);

            for (int i = 0; i < lotNoReaderList.Count; i++)
            {
                for (int j = 0; j < lList.Count; j++)
                {
                    if (lList[j].id == lotNoReaderList[i].id)
                    {
                        foreach (Tag t in lotNoReaderList[i].tagList)
                        {
                            if (t.id == latestTag.id)
                            {
                                t.flagged = true;
                            }
                        }
                    }
                }
            }
        }

        private void FillTestLots()
        {
            int index = 0;
            DateTime time = DateTime.Now;
            for (int lot = 0; lot < 50; lot++)
            {               
                LotNoReader currLot = new LotNoReader(lot.ToString(), new int[0], 200);

                for (int tag = index; tag < index + 200; tag++)
                {
                    currLot.tagList.Add(new Tag(tag.ToString(), 0, time));
                    time = time.AddSeconds(10);
                }

                lotNoReaderList.Add(currLot);
                index += 175;
            }            
        }

        private void btnFillTestLots_Click(object sender, EventArgs e)
        {
            FillTestLots();
        }

        private void btnErrorChk_Click(object sender, EventArgs e)
        {
            ErrorCheck();
        }
    }
}
