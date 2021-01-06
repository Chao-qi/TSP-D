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

        //实例化串口对象
        SerialPort serialPort = new SerialPort();

        public Manual()
        {
            InitializeComponent();
        }

        //初始化串口界面参数设置
        private void Init_Port_Confs()
        {
            /*------串口界面参数设置------*/

            //检查是否含有串口
            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                MessageBox.Show("本机没有串口！", "Error");
                return;
            }
            //添加串口
            foreach (string s in str)
            {
                comboBox1.Items.Add(s);
            }
            //设置默认串口选项
            comboBox1.SelectedIndex = 0;

        }

        private void Manual_Load(object sender, EventArgs e)
        {
            Init_Port_Confs();

            Control.CheckForIllegalCrossThreadCalls = false;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);


            //准备就绪              
            serialPort.DtrEnable = true;
            serialPort.RtsEnable = true;
            //设置数据读取超时为1秒
            serialPort.ReadTimeout = 1000;

            serialPort.Close();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)//串口处于关闭状态
            {

                try
                {

                    if (comboBox1.SelectedIndex == -1)
                    {
                        MessageBox.Show("Error: 无效的端口,请重新选择", "Error");
                        return;
                    }
                    string strSerialName = comboBox1.SelectedItem.ToString();

                 

                    serialPort.PortName = strSerialName;//串口号
                    serialPort.BaudRate =9600;//波特率
                    serialPort.DataBits =8;//数据位
                    serialPort.StopBits = StopBits.One;
                    serialPort.Parity = Parity.None;

                    //打开串口
                    serialPort.Open();

                    //打开串口后设置将不再有效
                    button9.Text = "关闭串口";

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Error");
                    return;
                }
            }
            else //串口处于打开状态
            {

                serialPort.Close();//关闭串口
                //串口关闭时设置有效
                comboBox1.Enabled = true;

                button9.Text = "打开串口";            

            }
        }
        private void dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort.IsOpen)
            {
               
                DateTime dateTimeNow = DateTime.Now;
                
                textBox1.Text += string.Format("{0}\r", dateTimeNow);
               
                textBox1.ForeColor = Color.Red;    //改变字体的颜色             
                String input = serialPort.ReadLine();
                textBox1.Text += input + "\r\n";   
                                                                                          
              //  textBox1.SelectionStart = textBox1.Text.Length;
               // textBox1.ScrollToCaret();//滚动到光标处
               // serialPort.DiscardInBuffer(); //清空SerialPort控件的Buffer 
            } 
            else
            {
                MessageBox.Show("请打开某个串口", "错误提示");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                MessageBox.Show("请先打开串口", "Error");
                return;
            }

            String strSend = "get.ver";//发送框数据
            serialPort.WriteLine(strSend);//发送一行数据 
        }
    }

}
