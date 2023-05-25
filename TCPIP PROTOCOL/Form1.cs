using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Drawing.Text;
using System.IO;

namespace TCPIP_PROTOCOL
{
    public partial class Form1 : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public Form1()
        {
            InitializeComponent();
        }

        TcpClient clientSocket = new TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readdata = null;

        private void btnConnect_Click(object sender, EventArgs e)
        {
            clientSocket.Connect(txtIpAdres.Text, Int32.Parse(txtPort.Text));
            Thread ctThread = new Thread(getMessage);
            ctThread.Start();
        }
        private void getMessage()
        {
            Thread.Sleep(1000);
            string returndata;
            while (true)
            {
                serverStream = clientSocket.GetStream();
                var buffSize = clientSocket.ReceiveBufferSize;
                byte[] instream = new byte[buffSize];


                serverStream.Read(instream, 0, buffSize);

                returndata = Encoding.ASCII.GetString(instream);
                readdata += returndata;
                msg();
            }
        }
        private void msg()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                txtInMessage.Text = readdata;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            MultiPlexer();
        }
        public void MultiPlexer()
        {
            byte[] outstream = Encoding.ASCII.GetBytes(txtOutMessage.Text);
            serverStream.Write(outstream, 0, outstream.Length);
            serverStream.Flush();
        }
        private void DosyaVeriYaz(string data)
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\ibrahim.benli\Desktop\test.txt");
            sw.WriteLine("n/"+ readdata);
            sw.Close();
            txtInMessage.Clear();
        }

    }
}

