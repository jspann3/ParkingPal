using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
        private Socket serverSocket, clientSocket;
        private byte[] buffer;
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        // Testing Purposes
        //public Lot lot1 = new Lot(0, new int[] { 0 }, 25);
        //public Lot lot2 = new Lot(1, new int[] { 0, 1 }, 50);

        public Lot testLot;

        public Server()
        {
            string path = @"C:\Users\BronzeMoss\Documents\visual studio 2015\Projects\SimpleAsyncSocket\SimpleAsyncSocket\bin\Debug\TCPClient.exe";
            Process.Start(path);
            InitializeComponent();
            //StartServer();
        }

        private void StartServer()
        {
            try
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(IPAddress.Any, 3353));
                serverSocket.Listen(100);
                testLot = new Lot(0, new int[] { 0 }, 35);
                //while (true)
                {
                    allDone.Reset();
                    serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
                    //allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                allDone.Set();
                clientSocket = serverSocket.EndAccept(ar);
                buffer = new byte[clientSocket.ReceiveBufferSize];
                clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
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
                int received = clientSocket.EndReceive(ar);

                if (received == 0)  //client has disconnected
                    return;

                Array.Resize(ref buffer, received);
                string text = Encoding.ASCII.GetString(buffer);

                if (text == "<EXIT>")
                {
                    clientSocket.Close();
                    serverSocket.Close();
                    Application.Exit();
                }

                
                if (text == "<LOT>")
                {
                    byte[] buffer = Encoding.ASCII.GetBytes(testLot.GetTagListLength().ToString());
                    clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
                }

                
                if (text == "<LOT1>")
                {
                    //should happen based on a timer
                    testLot.RemoveListCheck();

                    byte[] buffer = Encoding.ASCII.GetBytes(testLot.GetRemovedTagListLength().ToString());
                    clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
                }
                /*
                if (text == "<LOT2>")
                {
                    byte[] buffer = Encoding.ASCII.GetBytes(lot2.GetTagListLength().ToString());
                    clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
                }*/

                Console.WriteLine("Client says " + text);
                AppendToTextBox("Client says: " + text);
                Array.Resize(ref buffer, clientSocket.ReceiveBufferSize);
                clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
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
                clientSocket.EndSend(ar);
                Console.WriteLine("Server sent msg");
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
