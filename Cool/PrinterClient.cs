using System;
using System.Net.NetworkInformation;
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
using System.Timers;

namespace Cool
{
    class PrinterClient
    {
        DBprinter DbPt = new DBprinter();
        public Socket newclient;
        public bool Connected;
        DGContPtSeSt dgContPtSeSt;
        public Thread myThread;
        public static  byte[] finger = new byte[1600];
        public static byte CID = 0x00;
        public static bool DownloadFinish = false;
        public static bool DownloadUserFinish = false;
        public static bool DownloadFingerFinish = false;

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sokMsg">通信套接字</param>
        /// <param name="dgShowMsg">向主窗体文本框显示消息的方法委托</param>
        public PrinterClient(Socket sokMsg,DGContPtSeSt dgContPtSeSt)
        {
            this.newclient = sokMsg;
            this.dgContPtSeSt = dgContPtSeSt;
            this.myThread = new Thread(ReceiveMsg);
            this.myThread.Start();
            this.myThread.IsBackground = true;
            
        }
        #endregion
        public delegate void MyInvoke(string str);
       
        public void Close()
        {
            byte[] data=new byte[4];
            data = System.Text.Encoding.Unicode.GetBytes("STOP");
            int i = newclient.Send(data);
            newclient.Close();
          
        }
        void ReceiveMsg()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024*4];
                    int recv = newclient.Receive(data);
                    string stringdata = Encoding.UTF8.GetString(data, 0, recv);
                    //dgContPtSeSt(stringdata + "\r\n");
                    CID = data[5];

                    if (data[5] == 0x0E & data[15] == 0x00)
                    {
                        System.Threading.Thread.Sleep(1000);
                        for (int i = 0; i < 1600; i++)
                        {
                            finger[i] = data[i + 17];
                        }
                        //DbPt.InsertDBfinger(finger, Fingerprint_input.user_number);
                        DbPt.InsertDBfinger(finger, Fingerprint_input.user_number,Form2.FingerNumber);
                        DownloadFinish = true;
                    }
                    else if (data[5] == 0x05 & data[15] == 0x00)
                    {
                        DownloadUserFinish = true;
                    }
                    else if (data[5] == 0x06 & data[15] == 0x00)
                    {
                        DownloadFingerFinish = true;
                    }
                }
               
            }
            catch (SocketException e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            
        }
        #region 1向客户端发送消息
        /// <summary>
        /// 向客户端发送消息
        /// </summary>
        /// <param name="strMsg"></param>
        public void Send(string strMsg)
        {
            try
            {
                byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(strMsg);
                newclient.Send(arrMsg);
            }
            catch
            {
                MessageBox.Show("服务器未连接");
            }
        }
        #endregion
       
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[] { 0x50, 0x4c, 0x4d, 0x07, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xf1 }; 
            int i = newclient.Send(data);
         
        }
        //public byte[] getFinger()
        //{
        //    return this.finger;

        //}
        //public void setFinger()
        //{
        //    for (int i = 0; i < 1600; i++)
        //    {
        //        this.finger[i] = 0x00; 
        //    }

        //}
    }
}
