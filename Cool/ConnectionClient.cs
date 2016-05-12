using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace Cool
{
    public class ConnectionClient
    {
        Socket sokMsg;
        DGShowMsg dgShowMsg;//负责 向主窗体文本框显示消息的方法委托
        DGShowMsg dgRemoveConnection;// 负责 从主窗体 中移除 当前连接
        Thread threadMsg;
        PrinterProtocol PrtPro = new PrinterProtocol();
        DBprinter DBP = new DBprinter();
        Convert12 con12 = new Convert12();
        public static byte[] CheckuserGroup0 = new byte[2];
        public static byte[] CheckuserGroup1 = new byte[2];
        public static byte[] CheckuserGroup2 = new byte[2];
        byte[] workGroup0 = new byte[2];
        byte[] workGroup1 = new byte[2];
        byte[] workGroup2 = new byte[2];
        byte[] workGroup = new byte[100];// {0x04,0x00,0x27,0x00};
        public static string gunStatus = "";//查询柜子里报警温度等信息；
        public static string gunCheckSt = "";//查询柜子里枪支信息
        public static bool enableStaus = false;
        public static bool markStaus = false;
        public static bool doorStaus = false;
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sokMsg">通信套接字</param>
        /// <param name="dgShowMsg">向主窗体文本框显示消息的方法委托</param>
        public ConnectionClient(Socket sokMsg, DGShowMsg dgShowMsg, DGShowMsg dgRemoveConnection)
        {
            this.sokMsg = sokMsg;
            this.dgShowMsg = dgShowMsg;
            this.dgRemoveConnection = dgRemoveConnection;

            this.threadMsg = new Thread(RecMsg);
            this.threadMsg.IsBackground = true;
            this.threadMsg.Start();
        }
        #endregion

        bool isRec = true;
        #region 02负责监听客户端发送来的消息
        void RecMsg()
        {
            while (isRec)
            {
                //byte[] arrMsg = new byte[1024 * 1024 * 2];
                byte[] arrMsg = new byte[1024 * 1024 * 2];
                int length = -1;
            
                try
                {
                    //接收 对应 客户端发来的消息
                     
                     length = sokMsg.Receive(arrMsg);
                     if (arrMsg[5] == 0x50)
                     {
                         workGroup = DBP.WorkUserID();
                         CheckuserGroup0 = new byte[] { arrMsg[17], arrMsg[18] };
                         CheckuserGroup1 =new byte[] {arrMsg[19],arrMsg[20]};
                         CheckuserGroup2 =new byte[] {arrMsg[21],arrMsg[22]};
                         bool IsCheckSuccess_01 = false;
                         bool IsCheckSuccess_02 = false;
                         for (int i = 0; i < workGroup.Length; i++)
                         {
                             if (ByteToBytebyte(CheckuserGroup0, new byte[] { workGroup[i], 0x00 }))
                             {
                                 IsCheckSuccess_01 = true;
                             }
                             if (ByteToBytebyte(CheckuserGroup1, new byte[] { workGroup[i], 0x00 }))
                             {
                                 IsCheckSuccess_02 = true;
                             }
                         }

                         if (IsCheckSuccess_01 == true && IsCheckSuccess_02 == true)
                         {
                             PrtPro.AnswerToOpen(0x02);
                             frmMain.OpenDoorState = true;
                         }
                         else
                         {
                             System.Windows.Forms.MessageBox.Show("您不是当班员，指纹验证失败！！！", "提示", System.Windows.Forms.MessageBoxButtons.RetryCancel, System.Windows.Forms.MessageBoxIcon.Warning);
                         }
                     }

                }
                catch (Exception ex)
                {
                    isRec = false;
                    //从主窗体中 移除 下拉框中对应的客户端选择项，同时 移除 集合中对应的 ConnectionClient对象
                    dgRemoveConnection(sokMsg.RemoteEndPoint.ToString());
                    break;
                }
                //将接收到的消息数组里真实消息转成字符串
                string strMsg = System.Text.Encoding.UTF8.GetString(arrMsg);
                //通过委托 显示消息到 窗体的文本框
                if (!string.IsNullOrEmpty(strMsg))
                {
                    //： YFXG5202OK;
                    //   YFXG5302OK;
                    //   YFXGFFFFFF;
                    switch (strMsg.Substring(4, 6))
	                {
                        case "5202OK":
                            enableStaus= true;
                            break;
                        case "5302OK":
                            markStaus= true;
                            break;
                        case "FFFFFF":
                           
                            break;
                        default:
                            break;
	                }
                 
                    switch (strMsg.Substring(4, 2))
                    {
                        case "57":
                            gunStatus = strMsg.Substring(8, 36);
                            doorStaus = true;
                            break;
                        case "2D":
                            gunCheckSt = strMsg.Substring(8, 24);
                            break;
                        default:
                            break;
                    }
                 
                    if (strMsg.Substring(0, 1) == "P")
                    {
                        dgShowMsg(strMsg);
                        sokMsg.Send(Kaimen(0x02));
                    }
                    dgShowMsg(strMsg);
                }
            }
        }
        #endregion

        #region 03向客户端发送消息
        /// <summary>
        /// 向客户端发送消息
        /// </summary>
        /// <param name="strMsg"></param>
        public void Send(string strMsg)
        {
            byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(strMsg);
            byte[] arrMsgFinal = new byte[arrMsg.Length];
            arrMsg.CopyTo(arrMsgFinal, 0);
            System.Threading.Thread.Sleep(500);
            sokMsg.Send(arrMsgFinal);
        }
        #endregion

        #region 04向客户端发送消息
        /// <summary>
        /// 向客户端发送消息
        /// </summary>
        /// <param name="strMsg"></param>
        public void Send1(byte[] strMsg)
        {
            byte[] arrMsgFinal = new byte[strMsg.Length];

            //arrMsgFinal[0] = 0;//设置 数据标识位等于0，代表 发送的是 文字
            strMsg.CopyTo(arrMsgFinal, 0);

            sokMsg.Send(arrMsgFinal);
        }
        #endregion


        #region 05关闭与客户端连接
        /// <summary>
        /// 关闭与客户端连接
        /// </summary>
        public void CloseConnection()
        {
            isRec = false;
        }
        #endregion

        public Socket getSocket()
        {
            return sokMsg;
        }
       /// <summary>
       /// 开门
       /// </summary>
       /// <param name="FingerID"></param>
       /// <param name="UserID"></param>
       /// <returns></returns>
        public byte[] Kaimen( byte kaimen)
        {
         
            byte[] data = new byte[] { 0x50, 0x4c, 0x4d, 0x07, 0x00, 0x50, 0x41, 0x5d, 0x5f, 0x4d, 0x58, 0x57, 0x00, 0x01, 0x00, 0x00 };
            byte extraData = 0x00;
            byte checkSum = Sum(data);
            byte[] PtProtocol50 = new byte[18];

            extraData = kaimen;
         

            for (int i = 0; i < 17; i++)
            {
                if (i < 16)
                {
                    PtProtocol50[i] = data[i];
                }
                else
                {
                    PtProtocol50[i] = checkSum;
                }

            }
          
                PtProtocol50[17] = extraData;
           
            return PtProtocol50;
        }
        public byte Sum(byte[] data)
        {
            int n = data.Length;
            byte checksum = 0x00;
            for (int i = 0; i < n; i++)//len-4表示校验之前的位置
            {
                checksum += (byte)data[i];
            }
            return checksum;
        }
        /// <summary>
        /// 判断Byte数组a，b是否相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        bool ByteToBytebyte(byte[] a, byte[] b)
        {
            bool isEqual = true;
            if (a.Length != b.Length)
            {
                isEqual = false;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    isEqual = false;
            }
            return isEqual;
        }
    }
}
