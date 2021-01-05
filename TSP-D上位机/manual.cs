using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace TSP_D上位机
{
    public partial class Manual : Form
    {
       
        public Manual()
        {
            InitializeComponent();
        }
        public static SerialPort serialPort = new SerialPort();

        public static void seriop(string portname)
        {

            serialPort.PortName = portname;
            serialPort.BaudRate = 9600;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Parity = Parity.None;
            serialPort.Open();

        }

        private void button1_Click(object sender, EventArgs e)
        {     
            seriop("COM3");
            Thread.Sleep(1000);
            string send = "get.ver";
            serialPort.WriteLine(send);
        }
        private void dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                String input = serialPort.ReadLine();
                textBox1.Text += input + "\r\n";
            }
        }
    }
}
