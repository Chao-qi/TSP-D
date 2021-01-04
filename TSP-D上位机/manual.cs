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
            serialPort.BaudRate = 38400;
            serialPort.Open();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            seriop("COM2");
            Thread.Sleep(1000);
            string send = "67 65 74 2E 76 65 72";
            byte[] sendcom = StrtoByte(send);
            serialPort.Write(sendcom, 0, sendcom.Length);
            Thread.Sleep(500);
            textBox1.Text = serialPort.ReadLine();
        }
        public static byte[] StrtoByte(string data)
        {

            string[] datas = data.Split(' ');
            List<byte> bytedata = new List<byte>();

            foreach (string str in datas)
            {
                bytedata.Add(byte.Parse(str, System.Globalization.NumberStyles.AllowHexSpecifier));
            }
            byte[] crcbuf = bytedata.ToArray();

            return crcbuf;
        }
    }
}
