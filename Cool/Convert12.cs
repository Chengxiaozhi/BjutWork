using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cool
{
    class Convert12
    {
          public int StrToInt(char high, char low)//指令长度计算
        {
            int a = 0, b = 0;
            if (high < 58)
            {
                a = high - 48;
            }
            else
            {
                a = high - 55;
            }
            if (low < 58)
            {
                b = low - 48;
            }
            else
            {
                b = low - 55;
            }
            return a * 16 + b;
        }
        /// <summary>
        /// 枪位转换为制定的指令
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public byte gunarkENa(byte number)
        {
            byte gun_number = 0;
            int gun_n = 0;
            if (number > 0 && number < 9)
            {
                gun_number = (byte)(0x80 >> (number - 1));

            }
            else if (number > 8 && number < 17)
            {
                gun_n = number - 8;
                gun_number = (byte)(0x80 >> (gun_n - 1));

            }
            else if (number > 16 && number < 25)
            {
                gun_n = number - 16;
                gun_number = (byte)(0x80 >> (gun_n - 1));

            }
            else if (number > 24 && number < 33)
            {
                gun_n = number - 24;
                gun_number = (byte)(0x80 >> (gun_n - 1));

            }
            else if (number > 32 && number < 41)
            {
                gun_n = number - 32;
                gun_number = (byte)(0x80 >> (gun_n - 1));

            }
            else if (number > 40 && number < 49)
            {
                gun_n = number - 40;
                gun_number = (byte)(0x80 >> (gun_n - 1));

            }
            else if (number > 48 && number < 57)
            {
                gun_n = number - 48;
                gun_number = (byte)(0x80 >> (gun_n - 1));

            }


            else if (number > 56 && number < 65)
            {
                gun_n = number - 56;
                gun_number = (byte)(0x80 >> (gun_n - 1));

            }
            else if (number > 64 && number < 73)
            {
                gun_n = number - 64;
                gun_number = (byte)(0x80 >> (gun_n - 1));

            }
            else if (number > 72 && number < 81)
            {
                gun_n = number - 72;
                gun_number = (byte)(0x80 >> (gun_n - 1));

            }
            else if (number > 80 && number < 89)
            {
                gun_n = number - 80;
                gun_number = (byte)(0x80 >> (gun_n - 1));

            }
            else if (number > 88 && number < 97)
            {
                gun_n = number - 88;
                gun_number = (byte)(0x80 >> (gun_n - 1));

            }
            else if (number > 96 && number < 105)
            {
                gun_n = number - 96;
                gun_number = (byte)(0x80 >> (gun_n - 1));

            }
            else if (number > 104 && number < 113)
            {
                gun_n = number - 104;
                gun_number = (byte)(0x80 >> (gun_n - 1));

            }
            else if (number > 112 && number < 121)
            {
                gun_n = number - 112;
                gun_number = (byte)(0x80 >> (gun_n - 1));

            }
            else if (number > 120 && number < 129)
            {
                gun_n = number - 120;
                gun_number = (byte)(0x80 >> (gun_n - 1));

            }

            return gun_number;

        }
        public byte numberOr(byte[] number)//数组求或
        {
            byte NumberOr = 0;
            for (int i = 0; i < number.Length; i++)
            {
                NumberOr |= (byte)(number[i]);
            }
            return NumberOr;
        }
        public byte numberAnd(byte[] number)//数组求与
        {
            byte NumberAnd = 0xff;
            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] != 0)
                {
                    NumberAnd &= (byte)(number[i]);

                }
            }
            return NumberAnd;
        }
        public char convert(byte number, bool high) // 十六进制 高、低四位转为ASCII
        {

            char[] list = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            if (high)
            {
                return list[number >> 4];// 高四位
            }
            else
            {
                return list[number & 0x0f];//低四位
            }

        }
        public int length(char high, char low)//指令长度计算
        {
            int a = 0, b = 0;
            if (high < 58)
            {
                a = high - 48;
            }
            else
            {
                a = high - 55;
            }
            if (low < 58)
            {
                b = low - 48;
            }
            else
            {
                b = low - 55;
            }
            return a * 16 + b;
        }
        /// <summary>
        /// string 转换为byte数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public byte[] GetBytes(string str)
        {
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(str);
          
            return byteArray;
        }
        /// <summary>
        /// byte数组转换为string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
        /// <summary>
        /// string数 转换为十六进制  低位在前
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public byte[] NumToByteArr(string number)
        {
            byte[] userNumber = new byte[2];
            int number1 = Convert.ToInt32(number);
            int number_low = number1 & 0xff;
            int number_high = (number1 >> 8) & 0xff;
            userNumber[0] = (byte)number_low;
            userNumber[1] = (byte)number_high;
            return userNumber;
        }

        /// <summary>
        /// byte数组转String
        /// </summary>
        /// <param name="a"></param> A数组 表示低位在前 
        /// <returns></returns>
        public static string ByteArrToStr(byte[] a)//指令长度计算
        {
            int b = a[0] + a[1] * 16;
            string c = b.ToString();
            return c;
        }

        /// <summary>
        /// 将ip地址转换为16进制指令
        /// </summary>
        /// <param name="strIPAddress">IPv4格式的字符</param>
        /// <returns></returns>
        public string IPToNumber(string strIPAddress)
        {
            string tmpIpNumber;
            //将目标IP地址字符串strIPAddress转换为数字
            string[] arrayIP = strIPAddress.Split('.');
            int sip1 = Int32.Parse(arrayIP[0]);
            int sip2 = Int32.Parse(arrayIP[1]);
            int sip3 = Int32.Parse(arrayIP[2]);
            int sip4 = Int32.Parse(arrayIP[3]);

            tmpIpNumber = sip1.ToString("x2") + sip2.ToString("x2") + sip3.ToString("x2") + sip4.ToString("x2");
            return tmpIpNumber;
        }
        /// <summary>
        /// 十进制转为十六进制
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public string[] DECtoBIN(byte[] nums)//10进制转2进制
        {
            string[] toBIn = new string[24];
            for (int i = 0; i < 24; i++)
            {
                toBIn[i] = Convert.ToString(nums[i], 2);
                int len2 = toBIn[i].Length;
                if (len2 != 4)//不够四位的前面补0
                {
                    for (int j = 0; j < (4 - len2); j++)
                    {
                        toBIn[i] = "0" + toBIn[i];
                    }
                }
            }
            return toBIn;
        }
        /// <summary>
        /// ASCII转十六进制
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int ASCIItoHEX(byte num)//ASCII转十六进制
        {
            int a = 0;
            if (num < 58)
            {
                a = num - 48;
            }
            else
            {
                a = num - 55;
            }
            return a;
        }
    }
}
