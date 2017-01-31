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
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(IPAddress.Any, 3335));
                serverSocket.Listen(100);
                testLot = new Lot("12", new int[] { 0 }, 35);

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
                }
            }
            LotNoReader lnr = new LotNoReader(id, colors, maxSpaces);
            lotNoReaderList.Add(lnr);
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
                current.EndSend(ar);
                AppendToTextBox("Server sent msg");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
