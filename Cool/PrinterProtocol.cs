using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cool
{
    class PrinterProtocol
    {
        byte[] Header = new byte[]{0x50, 0x4c, 0x4d};
        byte PID = 0x07;
        byte DID = 0x00;
        byte CID = 0x00;
        byte[] Password = new byte[7];
        byte[] ExtraDataLen =new byte[2];
        byte ErrorCode = 0x00;
        byte CheckSum = 0x00;
        /// <summary>
        /// CID01指令
        /// </summary>
        /// <returns></returns>
        public byte[] TextConnect()
        {
            byte[] data = new byte[] { 0x50, 0x4c, 0x4d, 0x07, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            byte checkSum = Sum(data);
            byte[] PtProtocol01 = new byte[17];
            for (int i = 0; i < 17; i++)
            {
                if(i<16)
                {
                    PtProtocol01[i] = data[i];
                }
                else
                {
                    PtProtocol01[i] = checkSum;
                }

            }
            return PtProtocol01;
        }
        /// <summary>
        /// CID03指纹仪初始化
        /// </summary>
        /// <returns></returns>
        public byte[] PtInit()
        {

            byte[] data = new byte[] { 0x50, 0x4c, 0x4d, 0x07, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00 };
            byte[] extraData = new byte[] { 0x01, 0x01 };
            byte checkSum = Sum(data);
            byte[] PtProtocol03 = new byte[19];
            for (int i = 0; i < 17; i++)
            {
                if (i < 16)
                {
                    PtProtocol03[i] = data[i];
                }
                else
                {
                    PtProtocol03[i] = checkSum;
                }
                PtProtocol03[17] = extraData[0];
                PtProtocol03[18] = extraData[1];
            }

            return PtProtocol03;
        }
        /// <summary>
        /// 下载用户指纹CID05
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public byte[] DownloadUser(byte GroupID,byte[] UserID)
        {
            byte[] data = new byte[] { 0x50, 0x4c, 0x4d, 0x07, 0x00, 0x05, 0x41, 0x5d, 0x5f, 0x4d, 0x58, 0x57, 0x00, 0x27, 0x00, 0x00 };
            byte[] extraData = new byte[39];
            byte work_week = 0xFF;//每周上班时间
            byte work_state = 0x01;//判断用户是否有开门权限 1 是可以开门 0不可以
            byte[] work_time = new byte[] { 0x00, 0x00, 0x00, 0x17, 0x3B, 0x3B, 0x00, 0x00, 0x00, 0x17, 0x3B, 0x3B, 0x00, 0x00, 0x00, 0x17, 0x3B, 0x3B };//每天工作时间 00：00：00 - 23：59：59

            byte checkSum = Sum(data);
            byte[] PtProtocol05 = new byte[56];
            byte[] UserPwd = new byte[] { 0x30, 0x30, 0x30, 0x30, 0x30, 0x30 };
            extraData[0] = UserID[0];
            extraData[1] = UserID[1];
            extraData[2] = GroupID;
            extraData[13] = work_state;
            extraData[14] = work_week;

            for (int i = 0; i < 6; i++)
            {
                extraData[i + 7] = UserPwd[i];
            }
            for (int i = 0; i < 17; i++)
            {
                if (i < 16)
                {
                    PtProtocol05[i] = data[i];
                }
                else
                {
                    PtProtocol05[i] = checkSum;
                }

            }
            for (int i = 15; i < 33; i++)
            {
                extraData[i] = work_time[i - 15];
            }
            for (int n = 17; n < 56; n++)
            {
                PtProtocol05[n] = extraData[n - 17];
            }
            return PtProtocol05;
        }
        /// <summary>
        /// 下载用户指纹CID06
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="finger"></param>
        /// <returns></returns>
        public byte[] Severdownloadfinger(byte[] UserID, byte fingerId,string finger)
        {
            byte[] data = new byte[] { 0x50, 0x4c, 0x4d, 0x07, 0x00, 0x06, 0x41, 0x5d, 0x5f, 0x4d, 0x58, 0x57, 0x00, 0x43, 0x06, 0x00 };
            byte[] extraData = new byte[1603];
            byte checkSum = Sum(data);
            byte[] PtProtocol06 = new byte[1620];
            byte[] printer = StrToByte(finger);
            int j = 0;
            for (int i = 20; i < 1620; i++)
            {
                PtProtocol06[i] = printer[j];
                j = j + 1;
            }
            extraData[0] = UserID[0];
            extraData[1] = UserID[1];
            extraData[2] = fingerId;

            for (int i = 0; i < 17; i++)
            {
                    if (i < 16)
                    {
                        PtProtocol06[i] = data[i];
                    }
                    else
                    {
                        PtProtocol06[i] = checkSum;
                    }

            }
            for (int n = 17; n < 20; n++)
            {
                PtProtocol06[n] = extraData[n - 17];
            }
            return PtProtocol06;
        }
        /// <summary>
        /// 删除用户指纹CID07
        /// </summary>
        /// <param name="FingerID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public byte[] DeleteUserFinger(byte FingerID, byte[] UserID)
        {
            byte[] data = new byte[] { 0x50, 0x4c, 0x4d, 0x07, 0x00, 0x07, 0x41, 0x5d, 0x5f, 0x4d, 0x58, 0x57, 0x00, 0x03, 0x00, 0x00 };
            byte[] extraData = new byte[3];
            byte checkSum = Sum(data);
            byte[] PtProtocol07 = new byte[20];

            extraData[0] = UserID[0];
            extraData[1] = UserID[1];
            extraData[2] = FingerID;

            for (int i = 0; i < 17; i++)
            {
                if (i < 16)
                {
                    PtProtocol07[i] = data[i];
                }
                else
                {
                    PtProtocol07[i] = checkSum;
                }

            }
            for (int n = 17; n < 20; n++)
            {
                PtProtocol07[n] = extraData[n - 17];
            }
            return PtProtocol07;
        }
        /// <summary>
        /// 删除用户08
        /// </summary>
        /// <param name="FingerID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public byte[] DeleteUser(byte[] UserID)
        {
            byte[] data = new byte[] { 0x50, 0x4c, 0x4d, 0x07, 0x00, 0x08, 0x41, 0x5d, 0x5f, 0x4d, 0x58, 0x57, 0x00, 0x02, 0x00, 0x00 };
            byte[] extraData = new byte[2];
            byte checkSum = Sum(data);
            byte[] PtProtocol08 = new byte[19];

            extraData[0] = UserID[0];
            extraData[1] = UserID[1];

            for (int i = 0; i < 17; i++)
            {
                if (i < 16)
                {
                    PtProtocol08[i] = data[i];
                }
                else
                {
                    PtProtocol08[i] = checkSum;
                }

            }
            for (int n = 17; n < 19; n++)
            {
                PtProtocol08[n] = extraData[n - 17];
            }
            return PtProtocol08;
        }
       /// <summary>
       /// 采集指纹CID0E
       /// </summary>
       /// <returns></returns>
        public byte[] ClientDownloadfger()
        {
            byte[] data = new byte[] { 0x50, 0x4c, 0x4d, 0x07, 0x00, 0x0E, 0x41, 0x5d, 0x5f, 0x4d, 0x58, 0x57, 0x00, 0x00, 0x00, 0x00 };
            byte checkSum = Sum(data);
            byte[] PtProtocol0E = new byte[17];
            for (int i = 0; i < 17; i++)
            {
                if (i < 16)
                {
                    PtProtocol0E[i] = data[i];
                }
                else
                {
                    PtProtocol0E[i] = checkSum;
                }
            }

            return PtProtocol0E;
        }
        /// <summary>
        /// 获取用户制定指纹CID0F
        /// </summary>
        /// <param name="FingerID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public byte[] GetUserFinger(byte FingerID, byte[] UserID)
        {
            byte[] data = new byte[] { 0x50, 0x4c, 0x4d, 0x07, 0x00, 0x0F, 0x41, 0x5d, 0x5f, 0x4d, 0x58, 0x57, 0x00, 0x03, 0x00, 0x00 };
            byte[] extraData = new byte[3];
            byte checkSum = Sum(data);
            byte[] PtProtocol05 = new byte[20];
        
            extraData[0] = UserID[0];
            extraData[1] = UserID[1];
            extraData[2] = FingerID;
         
            for (int i = 0; i < 17; i++)
            {
                if (i < 16)
                {
                    PtProtocol05[i] = data[i];
                }
                else
                {
                    PtProtocol05[i] = checkSum;
                }

            }
            for (int n = 17; n < 20; n++)
            {
                PtProtocol05[n] = extraData[n - 17];
            }
            return PtProtocol05;
        }
        /// <summary>
        /// 是否开门
        /// </summary>
        /// <param name="judge"></param>
        /// <returns></returns>
        public byte[] AnswerToOpen(byte judge)
        {
            byte[] data = new byte[] { 0x50, 0x4c, 0x4d, 0x07, 0x00, 0x50, 0x41, 0x5d, 0x5f, 0x4d, 0x58, 0x57, 0x00, 0x01, 0x00, 0x00 };
            byte extraData = judge;
            byte checkSum = Sum(data);
            byte[] PtProtocol50 = new byte[18];

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
        /// <summary>
        /// 指纹特征值转为Byte数组
        /// </summary>
        /// <param name="finger"></param>
        /// <returns></returns>
        public byte[] StrToByte(string finger)
        {
            finger = finger.Trim();
            byte[] fingerByte = new byte[1600];
            //char[] s = finger.ToArray();
            string[] s = finger.Split(' ');
           
            for (int i = 0; i < s.Length; i++)
            {
                fingerByte[i] = Convert.ToByte(s[i]);
                //fingerByte[i] = Convert.ToByte(s[i]);
            }


            return fingerByte;
        }
        
        /// <summary>
        /// 校验和
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte Sum(byte[] data)
        {
            int n = data.Length;
            byte checksum= 0x00;
            for (int i = 0; i < n; i++)//len-4表示校验之前的位置
            {
              checksum += (byte)data[i];
            }
            return checksum;
        }
    
    }
}
