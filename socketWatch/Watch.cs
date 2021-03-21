﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace SocketWatch
{
    public partial class Watch : Form
    {
        delegate void SetTextCallBack(string text);
        public Watch()
        {
            InitializeComponent();
        }
        Thread threadWatch = null;// 负责监听客户端的线程
        Socket socketWatch = null;// 负责监听客户端的套接字
        Socket clientConnection = null;// 负责和客户端通信的套接字
        private void btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ipAddress.Text.ToString()))
            {
                MessageBox.Show("监听ip地址不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(port.Text.ToString()))
            {
                MessageBox.Show("监听端口不能为空！");
                return;
            }
            // 定义一个套接字用于监听客户端发来的消息，包含三个参数（ipv4寻址协议，流式连接，tcp协议）
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 服务端发送消息需要一个ip地址和端口号
            IPAddress ip = IPAddress.Parse(ipAddress.Text.Trim());
            // 把ip地址和端口号绑定在网路节点endport上
            IPEndPoint endPort = new IPEndPoint(ip, int.Parse(port.Text.Trim()));

            // 监听绑定的网路节点
            socketWatch.Bind(endPort);
            // 将套接字的监听队列长度设置限制为0，0表示无限
            socketWatch.Listen(0);
            // 创建一个监听线程
            threadWatch = new Thread(WatchConnecting);
            threadWatch.IsBackground = true;
            threadWatch.Start();
            chatContent.AppendText("成功启动监听！ip：" + ip + "，端口：" + port.Text.Trim() + "\r\n");

        }

        /// <summary>
        ///  监听客户端发来的请求
        /// </summary>
        private void WatchConnecting()
        {
            //持续不断监听客户端发来的请求
            while (true)
            {
                clientConnection = socketWatch.Accept();
                //chatContent.AppendText("客户端连接成功！" + "\r\n");
                SetText("客户端连接成功！" + "\r\n");

                // 创建一个通信线程
                ParameterizedThreadStart pts = new ParameterizedThreadStart(acceptMsg);
                Thread thr = new Thread(pts);
                thr.IsBackground = true;
                thr.Start(clientConnection);
            }
        }



        /// <summary>
        ///  接收客户端发来的消息
        /// </summary>
        /// <param name="socket">客户端套接字对象</param>
        private void acceptMsg(object socket)
        {
            Socket socketServer = socket as Socket;
            socketServer.Blocking = true;
            //创建一个内存缓冲区 其大小为1024*1024字节  即1M
            byte[] recMsg = new byte[1024 * 1024];
            //将接收到的信息存入到内存缓冲区,并返回其字节数组的长度
            int length = socketServer.Receive(recMsg);
            socketServer.Send(PackHandShakeData(GetSecKeyAccetp(recMsg, length)));
            Console.WriteLine("已经发送握手协议了....");

            //接收用户姓名信息
            length = socketServer.Receive(recMsg);
            string xm = AnalyticData(recMsg, length);

            socketServer.Send(PackData("连接时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            while (true)
            {
                //创建一个内存缓冲区 其大小为1024*1024字节  即1M
                recMsg = new byte[1024 * 1024];
                //将接收到的信息存入到内存缓冲区,并返回其字节数组的长度
                length = socketServer.Receive(recMsg);
                if (length == 0)
                {
                    break;
                }               
                //将机器接受到的字节数组转换为人可以读懂的字符串
                //string msg = Encoding.UTF8.GetString(recMsg, 0, length);
                string msg = AnalyticData(recMsg, length);
                //chatContent.AppendText("客户端(" + GetCurrentTime() + "):" + msg + "\r\n");
                SetText("客户端(" + GetCurrentTime() + "):" + msg + "\r\n");
            }
        }
        /// <summary>
        /// 打包服务器数据
        /// </summary>
        /// <param name="message">数据</param>
        /// <returns>数据包</returns>
        private static byte[] PackData(string message)
        {
            byte[] contentBytes = null;
            byte[] temp = Encoding.UTF8.GetBytes(message);

            if (temp.Length < 126)
            {
                contentBytes = new byte[temp.Length + 2];
                contentBytes[0] = 0x81;
                contentBytes[1] = (byte)temp.Length;
                Array.Copy(temp, 0, contentBytes, 2, temp.Length);
            }
            else if (temp.Length < 0xFFFF)
            {
                contentBytes = new byte[temp.Length + 4];
                contentBytes[0] = 0x81;
                contentBytes[1] = 126;
                contentBytes[2] = (byte)(temp.Length & 0xFF);
                contentBytes[3] = (byte)(temp.Length >> 8 & 0xFF);
                Array.Copy(temp, 0, contentBytes, 4, temp.Length);
            }
            else
            {
                // 暂不处理超长内容  
            }

            return contentBytes;
        }
        /// <summary>
        /// 解析客户端数据包
        /// </summary>
        /// <param name="recBytes">服务器接收的数据包</param>
        /// <param name="recByteLength">有效数据长度</param>
        /// <returns></returns>
        private static string AnalyticData(byte[] recBytes, int recByteLength)
        {
            if (recByteLength < 2) { return string.Empty; }

            bool fin = (recBytes[0] & 0x80) == 0x80; // 1bit，1表示最后一帧  
            if (!fin)
            {
                return string.Empty;// 超过一帧暂不处理 
            }

            bool mask_flag = (recBytes[1] & 0x80) == 0x80; // 是否包含掩码  
            if (!mask_flag)
            {
                return string.Empty;// 不包含掩码的暂不处理
            }

            int payload_len = recBytes[1] & 0x7F; // 数据长度  

            byte[] masks = new byte[4];
            byte[] payload_data;

            if (payload_len == 126)
            {
                Array.Copy(recBytes, 4, masks, 0, 4);
                payload_len = (UInt16)(recBytes[2] << 8 | recBytes[3]);
                payload_data = new byte[payload_len];
                Array.Copy(recBytes, 8, payload_data, 0, payload_len);

            }
            else if (payload_len == 127)
            {
                Array.Copy(recBytes, 10, masks, 0, 4);
                byte[] uInt64Bytes = new byte[8];
                for (int i = 0; i < 8; i++)
                {
                    uInt64Bytes[i] = recBytes[9 - i];
                }
                UInt64 len = BitConverter.ToUInt64(uInt64Bytes, 0);

                payload_data = new byte[len];
                for (UInt64 i = 0; i < len; i++)
                {
                    payload_data[i] = recBytes[i + 14];
                }
            }
            else
            {
                Array.Copy(recBytes, 2, masks, 0, 4);
                payload_data = new byte[payload_len];
                Array.Copy(recBytes, 6, payload_data, 0, payload_len);

            }

            for (var i = 0; i < payload_len; i++)
            {
                payload_data[i] = (byte)(payload_data[i] ^ masks[i % 4]);
            }

            return Encoding.UTF8.GetString(payload_data);
        }
        /// <summary>
        /// 打包握手信息
        /// </summary>
        /// <param name="secKeyAccept">Sec-WebSocket-Accept</param>
        /// <returns>数据包</returns>
        private static byte[] PackHandShakeData(string secKeyAccept)
        {
            var responseBuilder = new StringBuilder();
            responseBuilder.Append("HTTP/1.1 101 Switching Protocols" + Environment.NewLine);
            responseBuilder.Append("Upgrade: websocket" + Environment.NewLine);
            responseBuilder.Append("Connection: Upgrade" + Environment.NewLine);
            responseBuilder.Append("Sec-WebSocket-Accept: " + secKeyAccept + Environment.NewLine + Environment.NewLine);
            //如果把上一行换成下面两行，才是thewebsocketprotocol-17协议，但居然握手不成功，目前仍没弄明白！
            //responseBuilder.Append("Sec-WebSocket-Accept: " + secKeyAccept + Environment.NewLine);
            //responseBuilder.Append("Sec-WebSocket-Protocol: chat" + Environment.NewLine);

            return Encoding.UTF8.GetBytes(responseBuilder.ToString());
        }
        /// <summary>
        /// 生成Sec-WebSocket-Accept
        /// </summary>
        /// <param name="handShakeText">客户端握手信息</param>
        /// <returns>Sec-WebSocket-Accept</returns>
        private static string GetSecKeyAccetp(byte[] handShakeBytes, int bytesLength)
        {
            string handShakeText = Encoding.UTF8.GetString(handShakeBytes, 0, bytesLength);
            string key = string.Empty;
            Regex r = new Regex(@"Sec\-WebSocket\-Key:(.*?)\r\n");
            Match m = r.Match(handShakeText);
            if (m.Groups.Count != 0)
            {
                key = Regex.Replace(m.Value, @"Sec\-WebSocket\-Key:(.*?)\r\n", "$1").Trim();
            }
            byte[] encryptionString = SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(key + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"));
            return Convert.ToBase64String(encryptionString);
        }
        /// <summary>
        ///  发送消息到客户端
        /// </summary>
        /// <param name="msg"></param>
        private void serverSendMsg(string msg)
        {
            byte[] sendMsg = Encoding.UTF8.GetBytes(msg);
            clientConnection.Send(sendMsg);
            chatContent.AppendText("服务端(" + GetCurrentTime() + "):" + msg + "\r\n");
        }

        /// <summary>
        /// 获取当前系统时间的方法
        /// </summary>
        /// <returns>当前时间</returns>
        private DateTime GetCurrentTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serverSendMsg(this.richTextBox1.Text);
        }
        private void SetText(string text)
        {
            if (this.chatContent.InvokeRequired)
            {
                SetTextCallBack stcb = new SetTextCallBack(SetText);
                this.Invoke(stcb, new object[] { text });
            }
            else
            {
                this.chatContent.AppendText(text);
            }
        }
    }
}
