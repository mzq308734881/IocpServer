using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace IocpClient
{
    public partial class Form1 : Form
    {
        private Socket socketClient;
        private Thread threadClient;
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnConnectOne_Click(object sender, EventArgs e)
        {
            string ip = textBox1.Text.Trim();
            int port = Convert.ToInt32( textBox2.Text.Trim());
            ClientConnect(ip,port);
        }

        private void btnConnectMore_Click(object sender, EventArgs e)
        {             
            string ip = textBox1.Text.Trim();
            int port = Convert.ToInt32(textBox2.Text.Trim());
            for (int i = 0; i < 99; i++)
            {
                ClientConnect(ip, port);
            }
        }

        #region 连接服务端方法
        private void ClientConnect(string IP, int Port)
        {
            //定义一个套字节监听  包含3个参数(IP4寻址协议,流式连接,TCP协议)
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //需要获取文本框中的IP地址
            IPAddress ipaddress = IPAddress.Parse(IP);
            //将获取的ip地址和端口号绑定到网络节点endpoint上
            IPEndPoint endpoint = new IPEndPoint(ipaddress, Port);

            //这里客户端套接字连接到网络节点(服务端)用的方法是Connect 而不是Bind
            socketClient.Connect(endpoint);

            //创建一个线程 用于监听服务端发来的消息
            threadClient = new Thread(showClient);
            listBox1.Items.Add(socketClient.RemoteEndPoint);
            //将窗体线程设置为与后台同步
            threadClient.IsBackground = true;

            //启动线程
            threadClient.Start();
        }
        #endregion

        public void showClient()
        {
           //listBox1.Items.Add("11");
        }
    }
}
