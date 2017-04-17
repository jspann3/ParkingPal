using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPClient;

namespace SimpleAsyncSocket
{
    public partial class Client : Form
    {
        private Socket clientSocket;
        public SimpleLot testLot;

        public Client()
        {
            InitializeComponent();
            testLot = new SimpleLot("01", new int[] { 1 }, 35, "Curris");
        }

        private byte[] buffer;

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (clientSocket != null && clientSocket.Connected)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.BeginConnect(new IPEndPoint(IPAddress.Parse("208.44.252.155"), 3335), new AsyncCallback(ConnectCallback), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndConnect(ar);
                UpdateControlStates(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateControlStates(bool toggle)
        {
            MethodInvoker invoker = new MethodInvoker(delegate
            {
                btnSend.Enabled = toggle;
                btnConnect.Enabled = !toggle;
            });

            this.Invoke(invoker);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(textBox.Text);
                clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateControlStates(false);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
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

                Array.Resize(ref buffer, received);
                string text = Encoding.ASCII.GetString(buffer);

                textBox.Text = text;
                Array.Resize(ref buffer, clientSocket.ReceiveBufferSize);
                //clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLot_Click(object sender, EventArgs e)
        {
            try
            {
                buffer = Encoding.ASCII.GetBytes("<LOT>");
                clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
                buffer = new byte[clientSocket.ReceiveBufferSize];
                clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLot1_Click(object sender, EventArgs e)
        {
            try
            {
                buffer = Encoding.ASCII.GetBytes("<LO1>");
                clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
                buffer = new byte[clientSocket.ReceiveBufferSize];
                clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLot2_Click(object sender, EventArgs e)
        {
            try
            {
                textBox.Text = testLot.spotsRemaining.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLotNR_Click(object sender, EventArgs e)
        {
            try
            {
                buffer = Encoding.ASCII.GetBytes("<LNR>");
                clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
                buffer = new byte[clientSocket.ReceiveBufferSize];
                clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static List<SimpleLot> allLots = new List<SimpleLot> { new SimpleLot("01", new int[] { 0, 1 }, 79, "QUAD"),
                                                                       new SimpleLot("02", new int[] { 0, 1, 2 }, 241, "QUAD"),
                                                                       new SimpleLot("03", new int[] { 0, 2 }, 177, "QUAD"),
                                                                       new SimpleLot("04", new int[] { 0, 1 }, 19, "QUAD"),
                                                                       new SimpleLot("05", new int[] { 0, 1, 2, 3 }, 260, "CURRIS"),
                                                                       new SimpleLot("06", new int[] { 0, 2, 3 }, 249, "DORMS") };
        private List<SimpleLot> currentLots;

        private void FilterLots(string proximity, int color)
        {
            List<SimpleLot> filterOut = new List<SimpleLot>();
            currentLots = new List<SimpleLot>(allLots);

            foreach (SimpleLot lot in currentLots)
            {
                bool match = false;
                if (proximity != "ALL")
                {
                    if (lot.whichProximity != proximity)
                    {
                        if (!filterOut.Contains(lot))
                            filterOut.Add(lot);
                    }
                }

                if (color != 0)
                {
                    foreach(int c in lot.colors)
                    {
                        if (c == color)
                            match = true;
                    }

                    if (!match)
                    {
                        if (!filterOut.Contains(lot))
                            filterOut.Add(lot);
                    }
                }
            }

            currentLots.RemoveAll(lot => filterOut.Contains(lot));
        }

        private string ConvertLotIDsToString(List<SimpleLot> lots)
        {
            string lotIDs = "";

            foreach (SimpleLot lot in lots)
                lotIDs += lot.id + " ";     //create space-separated string of IDs

            return lotIDs;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            FilterLots(listBoxProximities.Text, listBoxColors.SelectedIndex);

            try
            {
                buffer = Encoding.ASCII.GetBytes("<SEND>" + ConvertLotIDsToString(currentLots));
                clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
                buffer = new byte[clientSocket.ReceiveBufferSize];
                clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtResults.Clear();
            foreach (SimpleLot lot in currentLots)
                txtResults.AppendText(lot.id + "\n");
        }
    }
}
