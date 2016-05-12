using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Cool
{
    class GunProtocol
    {
        public static List<string> error_gun_position_ID = null;
        string Header = null;
        string CmdID = null;
        string ExtraDataLen = null;
        string ExtraData = null;
        string realProtocol;
        DBgunStatus DbSt = new DBgunStatus();
        DBsotorage DBstor = new DBsotorage();
         Convert12 Cver12 = new Convert12();
        /// <summary>
        /// 按格式生成指定格式
        /// </summary>
        /// <param name="protValue"></param>
        /// <returns></returns>
        public string CreateProtocol(string[] protValue)
        {
            Header = "YFXG";
            CmdID = protValue[0];
            ExtraDataLen = protValue[1];
            ExtraData = protValue[2];
            return realProtocol = Header + CmdID + ExtraDataLen + ExtraData + ";";
        }
        /// <summary>
        /// 开门指令YFXG500102;
        /// </summary>>>返回指令 YFXG57120000000000003FFFFFFFFFFF80208020000A;
        /// <returns></returns>
        public string OpenDoor()
        {
            string[] open = new string[]{"50","01","02"};
            return CreateProtocol(open);
        }
        public string GunEnableInit()
        {
          
            char[] package = new char[32];
            for (int i = 0; i < 32; i++)
            {

                package [i] = '0';

                
            }
            string aftEnablestring = string.Join("", package.Select(t => t.ToString()).ToArray());
            string[] enable = new string[] { "52", "16", aftEnablestring };
            return CreateProtocol(enable);
        }
        /// <summary>
        /// 修改IP
        /// </summary>"C0A8013AFFFFFF00C0A80101" 
        /// <param name="?"></param>
        /// <returns></returns>
        public string NewIP(string IP, string submark, string gateway)
        {
            string ss = IP + submark + gateway;
            string[] open = new string[] { "21", "12", ss };
            return CreateProtocol(open);
        }

        /// <summary>
        /// 选通抢位
        /// </summary>
        /// <returns></returns> YFXG5202OK;
        public string GunEnable(byte[] gunState,string gunarkid)
        {
          
           // string preEnable = "00000000000000000000000000000000";
            string preEnable = DbSt.GetGunEnabe(gunarkid);
            char[] pre = preEnable.ToCharArray();
            char[] package = pre;
            byte[] numberTohex = new byte[400];
            for (int i = 0; i < gunState.Length; i++)
            {

                if (gunState[i] == 0)
                    break;
                byte number = gunState[i];

                numberTohex[number - 1] = gunarkENa(number);
            }
            byte[] numberTohex8 = { numberTohex[0], numberTohex[1], numberTohex[2], numberTohex[3], numberTohex[4], numberTohex[5], numberTohex[6], numberTohex[7] };
            byte gun_number8 = numberOr(numberTohex8);
            byte pre0 = (byte)length2(package[0], package[1]);
            gun_number8 = (byte)(gun_number8 + pre0);
            bool high = true;
            package[0] = convert(gun_number8, high);

            package[1] = convert(gun_number8, !high);

            byte[] numberTohex16 = { numberTohex[8], numberTohex[9], numberTohex[10], numberTohex[11], numberTohex[12], numberTohex[13], numberTohex[14], numberTohex[15] };
            byte gun_number16 = numberOr(numberTohex16);
            byte pre2 = (byte)length2(package[2], package[3]);
            gun_number16 = (byte)(gun_number16 + pre2);
            package[2] = convert(gun_number16, high);

            package[3] = convert(gun_number16, !high);

            byte[] numberTohex24 = { numberTohex[16], numberTohex[17], numberTohex[18], numberTohex[19], numberTohex[20], numberTohex[21], numberTohex[22], numberTohex[23] };
            byte gun_number24 = numberOr(numberTohex24);
            byte pre4 = (byte)length2(package[4], package[5]);
            gun_number24 = (byte)(gun_number24 + pre4);

            package[4] = convert(gun_number24, high);

            package[5] = convert(gun_number24, !high);

            byte[] numberTohex32 = { numberTohex[24], numberTohex[25], numberTohex[26], numberTohex[27], numberTohex[28], numberTohex[29], numberTohex[30], numberTohex[31] };
            byte gun_number32 = numberOr(numberTohex32);
            byte pre6 = (byte)length2(package[6], package[7]);
            gun_number32 = (byte)(gun_number32 + pre6);
            package[6] = convert(gun_number32, high);

            package[7] = convert(gun_number32, !high);

            byte[] numberTohex40 = { numberTohex[32], numberTohex[33], numberTohex[34], numberTohex[35], numberTohex[36], numberTohex[37], numberTohex[38], numberTohex[39] };
            byte gun_number40 = numberOr(numberTohex40);

            byte pre8 = (byte)length2(package[8], package[9]);
            gun_number40 = (byte)(gun_number40 + pre8);
            package[8] = convert(gun_number40, high);

            package[9] = convert(gun_number40, !high);

            byte[] numberTohex48 = { numberTohex[40], numberTohex[41], numberTohex[42], numberTohex[43], numberTohex[44], numberTohex[45], numberTohex[46], numberTohex[47] };
            byte gun_number48 = numberOr(numberTohex48);


            byte pre10 = (byte)length2(package[10], package[11]);
            gun_number48 = (byte)(gun_number48 + pre10);


            package[10] = convert(gun_number48, high);

            package[11] = convert(gun_number48, !high);

            byte[] numberTohex56 = { numberTohex[48], numberTohex[49], numberTohex[50], numberTohex[51], numberTohex[52], numberTohex[53], numberTohex[54], numberTohex[55] };
            byte gun_number56 = numberOr(numberTohex56);

            byte pre12 = (byte)length2(package[12], package[13]);
            gun_number56 = (byte)(gun_number56 + pre12);
            package[12] = convert(gun_number56, high);

            package[13] = convert(gun_number56, !high);



            byte[] numberTohex64 = { numberTohex[56], numberTohex[57], numberTohex[58], numberTohex[59], numberTohex[60], numberTohex[61], numberTohex[62], numberTohex[63] };
            byte gun_number64 = numberOr(numberTohex64);


            byte pre14 = (byte)length2(package[14], package[15]);
            gun_number64 = (byte)(gun_number64 + pre14);
            package[14] = convert(gun_number64, high);

            package[15] = convert(gun_number64, !high);

            byte[] numberTohex72 = { numberTohex[64], numberTohex[65], numberTohex[66], numberTohex[67], numberTohex[68], numberTohex[69], numberTohex[70], numberTohex[71] };
            byte gun_number72 = numberOr(numberTohex72);


            byte pre16 = (byte)length2(package[16], package[17]);
            gun_number72 = (byte)(gun_number72 + pre16);
            package[16] = convert(gun_number72, high);

            package[17] = convert(gun_number72, !high);

            byte[] numberTohex80 = { numberTohex[72], numberTohex[73], numberTohex[74], numberTohex[75], numberTohex[76], numberTohex[77], numberTohex[78], numberTohex[79] };
            byte gun_number80 = numberOr(numberTohex80);
            byte pre18 = (byte)length2(package[18], package[19]);
            gun_number80 = (byte)(gun_number80 + pre18);

            package[18] = convert(gun_number80, high);

            package[19] = convert(gun_number80, !high);

            byte[] numberTohex88 = { numberTohex[80], numberTohex[81], numberTohex[82], numberTohex[83], numberTohex[84], numberTohex[85], numberTohex[86], numberTohex[87] };
            byte gun_number88 = numberOr(numberTohex88);
            byte pre20 = (byte)length2(package[20], package[21]);
            gun_number88 = (byte)(gun_number88 + pre20);
            package[20] = convert(gun_number88, high);

            package[21] = convert(gun_number88, !high);

            byte[] numberTohex96 = { numberTohex[88], numberTohex[89], numberTohex[90], numberTohex[91], numberTohex[92], numberTohex[93], numberTohex[94], numberTohex[95] };
            byte gun_number96 = numberOr(numberTohex96);
            byte pre22 = (byte)length2(package[22], package[23]);
            gun_number96 = (byte)(gun_number96 + pre22);

            package[22] = convert(gun_number96, high);

            package[23] = convert(gun_number96, !high);
            /////
            byte[] numberTohex104 = { numberTohex[96], numberTohex[97], numberTohex[98], numberTohex[99], numberTohex[100], numberTohex[101], numberTohex[102], numberTohex[103] };
            byte gun_number104 = numberOr(numberTohex104);

            byte pre24 = (byte)length2(package[24], package[25]);
            gun_number104 = (byte)(gun_number104 + pre24);

            package[24] = convert(gun_number104, high);

            package[25] = convert(gun_number104, !high);
            byte[] numberTohex112 = { numberTohex[104], numberTohex[105], numberTohex[106], numberTohex[107], numberTohex[108], numberTohex[109], numberTohex[110], numberTohex[111] };
            byte gun_number112 = numberOr(numberTohex112);

            byte pre26 = (byte)length2(package[26], package[27]);
            gun_number112 = (byte)(gun_number112 + pre26);

            package[26] = convert(gun_number112, high);

            package[27] = convert(gun_number112, !high);

            byte[] numberTohex120 = { numberTohex[112], numberTohex[113], numberTohex[114], numberTohex[115], numberTohex[116], numberTohex[117], numberTohex[118], numberTohex[119] };
            byte gun_number120 = numberOr(numberTohex120);

            byte pre28 = (byte)length2(package[28], package[29]);
            gun_number120 = (byte)(gun_number120 + pre28);

            package[28] = convert(gun_number120, high);

            package[29] = convert(gun_number120, !high);

            byte[] numberTohex128 = { numberTohex[120], numberTohex[121], numberTohex[122], numberTohex[123], numberTohex[124], numberTohex[125], numberTohex[126], numberTohex[127] };
            byte gun_number128 = numberOr(numberTohex128);


            byte pre30 = (byte)length2(package[30], package[31]);
            gun_number128 = (byte)(gun_number128 + pre30);

            package[30] = convert(gun_number128, high);

            package[31] = convert(gun_number128, !high);
            ////


            string aftEnablestring = string.Join("", package.Select(t => t.ToString()).ToArray());
            string[] enable = new string[] { "52", "16", aftEnablestring };
            return CreateProtocol(enable);
        }
        /// <summary>
        ///    取消选通
        /// </summary>
        /// <param name="gunState"> 枪位ID数组 </param>
        /// <param name="gunarkid"> 枪柜号 </param>
        public string cancel_Enable(byte[] gunState, string gunarkid)//枪柜选通使能抢位
        {
            // string preEnable = "00000000000000000000000000000000";
            string preEnable = DbSt.GetGunEnabe(gunarkid);
            char[] pre = preEnable.ToCharArray();
            char[] package = pre;
            byte[] numberTohex = new byte[400];
            for (int i = 0; i < gunState.Length; i++)
            {

                if (gunState[i] == 0)
                    break;
                byte number = gunState[i];

                numberTohex[number - 1] = gunarkENa(number);
            }
            byte[] numberTohex8 = { numberTohex[0], numberTohex[1], numberTohex[2], numberTohex[3], numberTohex[4], numberTohex[5], numberTohex[6], numberTohex[7] };
            byte gun_number8 = numberOr(numberTohex8);
            byte pre0 = (byte)length2(package[0], package[1]);
            gun_number8 = (byte)( - gun_number8 + pre0);
            bool high = true;
            package[0] = convert(gun_number8, high);

            package[1] = convert(gun_number8, !high);

            byte[] numberTohex16 = { numberTohex[8], numberTohex[9], numberTohex[10], numberTohex[11], numberTohex[12], numberTohex[13], numberTohex[14], numberTohex[15] };
            byte gun_number16 = numberOr(numberTohex16);
            byte pre2 = (byte)length2(package[2], package[3]);
            gun_number16 = (byte)( - gun_number16 + pre2);
            package[2] = convert(gun_number16, high);

            package[3] = convert(gun_number16, !high);

            byte[] numberTohex24 = { numberTohex[16], numberTohex[17], numberTohex[18], numberTohex[19], numberTohex[20], numberTohex[21], numberTohex[22], numberTohex[23] };
            byte gun_number24 = numberOr(numberTohex24);
            byte pre4 = (byte)length2(package[4], package[5]);
            gun_number24 = (byte)( - gun_number24 + pre4);

            package[4] = convert(gun_number24, high);

            package[5] = convert(gun_number24, !high);

            byte[] numberTohex32 = { numberTohex[24], numberTohex[25], numberTohex[26], numberTohex[27], numberTohex[28], numberTohex[29], numberTohex[30], numberTohex[31] };
            byte gun_number32 = numberOr(numberTohex32);
            byte pre6 = (byte)length2(package[6], package[7]);
            gun_number32 = (byte)( - gun_number32 + pre6);
            package[6] = convert(gun_number32, high);

            package[7] = convert(gun_number32, !high);

            byte[] numberTohex40 = { numberTohex[32], numberTohex[33], numberTohex[34], numberTohex[35], numberTohex[36], numberTohex[37], numberTohex[38], numberTohex[39] };
            byte gun_number40 = numberOr(numberTohex40);

            byte pre8 = (byte)length2(package[8], package[9]);
            gun_number40 = (byte)(-gun_number40 + pre8);
            package[8] = convert(gun_number40, high);

            package[9] = convert(gun_number40, !high);

            byte[] numberTohex48 = { numberTohex[40], numberTohex[41], numberTohex[42], numberTohex[43], numberTohex[44], numberTohex[45], numberTohex[46], numberTohex[47] };
            byte gun_number48 = numberOr(numberTohex48);


            byte pre10 = (byte)length2(package[10], package[11]);
            gun_number48 = (byte)(- gun_number48 + pre10);


            package[10] = convert(gun_number48, high);

            package[11] = convert(gun_number48, !high);

            byte[] numberTohex56 = { numberTohex[48], numberTohex[49], numberTohex[50], numberTohex[51], numberTohex[52], numberTohex[53], numberTohex[54], numberTohex[55] };
            byte gun_number56 = numberOr(numberTohex56);

            byte pre12 = (byte)length2(package[12], package[13]);
            gun_number56 = (byte)( - gun_number56 + pre12);
            package[12] = convert(gun_number56, high);

            package[13] = convert(gun_number56, !high);



            byte[] numberTohex64 = { numberTohex[56], numberTohex[57], numberTohex[58], numberTohex[59], numberTohex[60], numberTohex[61], numberTohex[62], numberTohex[63] };
            byte gun_number64 = numberOr(numberTohex64);


            byte pre14 = (byte)length2(package[14], package[15]);
            gun_number64 = (byte)( - gun_number64 + pre14);
            package[14] = convert(gun_number64, high);

            package[15] = convert(gun_number64, !high);

            byte[] numberTohex72 = { numberTohex[64], numberTohex[65], numberTohex[66], numberTohex[67], numberTohex[68], numberTohex[69], numberTohex[70], numberTohex[71] };
            byte gun_number72 = numberOr(numberTohex72);


            byte pre16 = (byte)length2(package[16], package[17]);
            gun_number72 = (byte)( - gun_number72 + pre16);
            package[16] = convert(gun_number72, high);

            package[17] = convert(gun_number72, !high);

            byte[] numberTohex80 = { numberTohex[72], numberTohex[73], numberTohex[74], numberTohex[75], numberTohex[76], numberTohex[77], numberTohex[78], numberTohex[79] };
            byte gun_number80 = numberOr(numberTohex80);
            byte pre18 = (byte)length2(package[18], package[19]);
            gun_number80 = (byte)( - gun_number80 + pre18);

            package[18] = convert(gun_number80, high);

            package[19] = convert(gun_number80, !high);

            byte[] numberTohex88 = { numberTohex[80], numberTohex[81], numberTohex[82], numberTohex[83], numberTohex[84], numberTohex[85], numberTohex[86], numberTohex[87] };
            byte gun_number88 = numberOr(numberTohex88);
            byte pre20 = (byte)length2(package[20], package[21]);
            gun_number88 = (byte)( - gun_number88 + pre20);
            package[20] = convert(gun_number88, high);

            package[21] = convert(gun_number88, !high);

            byte[] numberTohex96 = { numberTohex[88], numberTohex[89], numberTohex[90], numberTohex[91], numberTohex[92], numberTohex[93], numberTohex[94], numberTohex[95] };
            byte gun_number96 = numberOr(numberTohex96);
            byte pre22 = (byte)length2(package[22], package[23]);
            gun_number96 = (byte)( - gun_number96 + pre22);

            package[22] = convert(gun_number96, high);

            package[23] = convert(gun_number96, !high);
            /////
            byte[] numberTohex104 = { numberTohex[96], numberTohex[97], numberTohex[98], numberTohex[99], numberTohex[100], numberTohex[101], numberTohex[102], numberTohex[103] };
            byte gun_number104 = numberOr(numberTohex104);

            byte pre24 = (byte)length2(package[24], package[25]);
            gun_number104 = (byte)( - gun_number104 + pre24);

            package[24] = convert(gun_number104, high);

            package[25] = convert(gun_number104, !high);
            byte[] numberTohex112 = { numberTohex[104], numberTohex[105], numberTohex[106], numberTohex[107], numberTohex[108], numberTohex[109], numberTohex[110], numberTohex[111] };
            byte gun_number112 = numberOr(numberTohex112);

            byte pre26 = (byte)length2(package[26], package[27]);
            gun_number112 = (byte)( - gun_number112 + pre26);

            package[26] = convert(gun_number112, high);

            package[27] = convert(gun_number112, !high);

            byte[] numberTohex120 = { numberTohex[112], numberTohex[113], numberTohex[114], numberTohex[115], numberTohex[116], numberTohex[117], numberTohex[118], numberTohex[119] };
            byte gun_number120 = numberOr(numberTohex120);

            byte pre28 = (byte)length2(package[28], package[29]);
            gun_number120 = (byte)( - gun_number120 + pre28);

            package[28] = convert(gun_number120, high);

            package[29] = convert(gun_number120, !high);

            byte[] numberTohex128 = { numberTohex[120], numberTohex[121], numberTohex[122], numberTohex[123], numberTohex[124], numberTohex[125], numberTohex[126], numberTohex[127] };
            byte gun_number128 = numberOr(numberTohex128);


            byte pre30 = (byte)length2(package[30], package[31]);
            gun_number128 = (byte)( - gun_number128 + pre30);

            package[30] = convert(gun_number128, high);

            package[31] = convert(gun_number128, !high);
            ////


            string aftEnablestring = string.Join("", package.Select(t => t.ToString()).ToArray());
            string[] enable = new string[] { "52", "16", aftEnablestring };
            return CreateProtocol(enable);
        }
        /// <summary>
        /// 枪位选通枪支入库相应标记为值为零
        /// </summary>
        /// <param name="gunState"></param>
        /// <returns></returns>
        public string SetToZero(byte[] gunState,string gunarkid)
        {
            //string preEnable = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
            string preEnable = DbSt.GetGunMark(gunarkid);
            char[] pre = preEnable.ToCharArray();
            char[] package = pre;
            byte[] numberTohex = new byte[400];
            for (int i = 0; i < gunState.Length; i++)
            {

                if (gunState[i] == 0)
                    break;
                byte number = gunState[i];

                numberTohex[number - 1] = gunarkENa(number);
            }
            byte[] numberTohex8 = { numberTohex[0], numberTohex[1], numberTohex[2], numberTohex[3], numberTohex[4], numberTohex[5], numberTohex[6], numberTohex[7] };
            byte gun_number8 = numberOr(numberTohex8);
            byte pre0 = (byte)length2(package[0], package[1]);
            gun_number8 = (byte)(gun_number8 ^ pre0);
            bool high = true;
            package[0] = convert(gun_number8, high);

            package[1] = convert(gun_number8, !high);

            byte[] numberTohex16 = { numberTohex[8], numberTohex[9], numberTohex[10], numberTohex[11], numberTohex[12], numberTohex[13], numberTohex[14], numberTohex[15] };
            byte gun_number16 = numberOr(numberTohex16);
            byte pre2 = (byte)length2(package[2], package[3]);
            gun_number16 = (byte)(gun_number16 ^ pre2);
            package[2] = convert(gun_number16, high);

            package[3] = convert(gun_number16, !high);

            byte[] numberTohex24 = { numberTohex[16], numberTohex[17], numberTohex[18], numberTohex[19], numberTohex[20], numberTohex[21], numberTohex[22], numberTohex[23] };
            byte gun_number24 = numberOr(numberTohex24);
            byte pre4 = (byte)length2(package[4], package[5]);
            gun_number24 = (byte)(gun_number24 ^ pre4);

            package[4] = convert(gun_number24, high);

            package[5] = convert(gun_number24, !high);

            byte[] numberTohex32 = { numberTohex[24], numberTohex[25], numberTohex[26], numberTohex[27], numberTohex[28], numberTohex[29], numberTohex[30], numberTohex[31] };
            byte gun_number32 = numberOr(numberTohex32);
            byte pre6 = (byte)length2(package[6], package[7]);
            gun_number32 = (byte)(gun_number32 ^ pre6);
            package[6] = convert(gun_number32, high);

            package[7] = convert(gun_number32, !high);

            byte[] numberTohex40 = { numberTohex[32], numberTohex[33], numberTohex[34], numberTohex[35], numberTohex[36], numberTohex[37], numberTohex[38], numberTohex[39] };
            byte gun_number40 = numberOr(numberTohex40);

            byte pre8 = (byte)length2(package[8], package[9]);
            gun_number40 = (byte)(gun_number40 ^ pre8);
            package[8] = convert(gun_number40, high);

            package[9] = convert(gun_number40, !high);

            byte[] numberTohex48 = { numberTohex[40], numberTohex[41], numberTohex[42], numberTohex[43], numberTohex[44], numberTohex[45], numberTohex[46], numberTohex[47] };
            byte gun_number48 = numberOr(numberTohex48);


            byte pre10 = (byte)length2(package[10], package[11]);
            gun_number48 = (byte)(gun_number48 ^ pre10);


            package[10] = convert(gun_number48, high);

            package[11] = convert(gun_number48, !high);

            byte[] numberTohex56 = { numberTohex[48], numberTohex[49], numberTohex[50], numberTohex[51], numberTohex[52], numberTohex[53], numberTohex[54], numberTohex[55] };
            byte gun_number56 = numberOr(numberTohex56);

            byte pre12 = (byte)length2(package[12], package[13]);
            gun_number56 = (byte)(gun_number56 ^ pre12);
            package[12] = convert(gun_number56, high);

            package[13] = convert(gun_number56, !high);



            byte[] numberTohex64 = { numberTohex[56], numberTohex[57], numberTohex[58], numberTohex[59], numberTohex[60], numberTohex[61], numberTohex[62], numberTohex[63] };
            byte gun_number64 = numberOr(numberTohex64);


            byte pre14 = (byte)length2(package[14], package[15]);
            gun_number64 = (byte)(gun_number64 ^ pre14);
            package[14] = convert(gun_number64, high);

            package[15] = convert(gun_number64, !high);

            byte[] numberTohex72 = { numberTohex[64], numberTohex[65], numberTohex[66], numberTohex[67], numberTohex[68], numberTohex[69], numberTohex[70], numberTohex[71] };
            byte gun_number72 = numberOr(numberTohex72);


            byte pre16 = (byte)length2(package[16], package[17]);
            gun_number72 = (byte)(gun_number72 ^ pre16);
            package[16] = convert(gun_number72, high);

            package[17] = convert(gun_number72, !high);

            byte[] numberTohex80 = { numberTohex[72], numberTohex[73], numberTohex[74], numberTohex[75], numberTohex[76], numberTohex[77], numberTohex[78], numberTohex[79] };
            byte gun_number80 = numberOr(numberTohex80);
            byte pre18 = (byte)length2(package[18], package[19]);
            gun_number80 = (byte)(gun_number80 ^ pre18);

            package[18] = convert(gun_number80, high);

            package[19] = convert(gun_number80, !high);

            byte[] numberTohex88 = { numberTohex[80], numberTohex[81], numberTohex[82], numberTohex[83], numberTohex[84], numberTohex[85], numberTohex[86], numberTohex[87] };
            byte gun_number88 = numberOr(numberTohex88);
            byte pre20 = (byte)length2(package[20], package[21]);
            gun_number88 = (byte)(gun_number88 ^ pre20);
            package[20] = convert(gun_number88, high);

            package[21] = convert(gun_number88, !high);

            byte[] numberTohex96 = { numberTohex[88], numberTohex[89], numberTohex[90], numberTohex[91], numberTohex[92], numberTohex[93], numberTohex[94], numberTohex[95] };
            byte gun_number96 = numberOr(numberTohex96);
            byte pre22 = (byte)length2(package[22], package[23]);
            gun_number96 = (byte)(gun_number96 ^ pre22);

            package[22] = convert(gun_number96, high);

            package[23] = convert(gun_number96, !high);
            /////
            byte[] numberTohex104 = { numberTohex[96], numberTohex[97], numberTohex[98], numberTohex[99], numberTohex[100], numberTohex[101], numberTohex[102], numberTohex[103] };
            byte gun_number104 = numberOr(numberTohex104);

            byte pre24 = (byte)length2(package[24], package[25]);
            gun_number104 = (byte)(gun_number104 ^ pre24);

            package[24] = convert(gun_number104, high);

            package[25] = convert(gun_number104, !high);
            byte[] numberTohex112 = { numberTohex[104], numberTohex[105], numberTohex[106], numberTohex[107], numberTohex[108], numberTohex[109], numberTohex[110], numberTohex[111] };
            byte gun_number112 = numberOr(numberTohex112);

            byte pre26 = (byte)length2(package[26], package[27]);
            gun_number112 = (byte)(gun_number112 ^ pre26);

            package[26] = convert(gun_number112, high);

            package[27] = convert(gun_number112, !high);

            byte[] numberTohex120 = { numberTohex[112], numberTohex[113], numberTohex[114], numberTohex[115], numberTohex[116], numberTohex[117], numberTohex[118], numberTohex[119] };
            byte gun_number120 = numberOr(numberTohex120);

            byte pre28 = (byte)length2(package[28], package[29]);
            gun_number120 = (byte)(gun_number120 ^ pre28);

            package[28] = convert(gun_number120, high);

            package[29] = convert(gun_number120, !high);

            byte[] numberTohex128 = { numberTohex[120], numberTohex[121], numberTohex[122], numberTohex[123], numberTohex[124], numberTohex[125], numberTohex[126], numberTohex[127] };
            byte gun_number128 = numberOr(numberTohex128);


            byte pre30 = (byte)length2(package[30], package[31]);
            gun_number128 = (byte)(gun_number128 ^ pre30);

            package[30] = convert(gun_number128, high);

            package[31] = convert(gun_number128, !high);
            ////

            string aftEnablestring = string.Join("", package.Select(t => t.ToString()).ToArray());
            string[] enable = new string[] { "53", "16", aftEnablestring };
            return CreateProtocol(enable);
           
        }
        /// <summary>
        /// 枪位初始化 枪锁全部打开
        /// </summary>
        /// <param name="gunState"></param>
        /// <returns></returns>
        public string OpenGunStateInit()
        {
            char[] package = new char[16];
            for (int i = 0; i < 16; i++)
            {
                package[i] = 'F'; 
            }
            string aftEnablestring = string.Join("", package.Select(t => t.ToString()).ToArray());
            string[] enable = new string[] { "53", "16", aftEnablestring };
            return CreateProtocol(enable);
        }
        public string OpenBulletState(byte[] gunState, string gunarkid)//开枪位
        {
            string preEnable = "00000000000000000000000000000000";
            
            char[] pre = preEnable.ToCharArray();
            char[] package = pre;
            byte[] numberTohex = new byte[400];
            for (int i = 0; i < gunState.Length; i++)
            {

                if (gunState[i] == 0)
                    break;
                byte number = gunState[i];

                numberTohex[number - 1] = gunarkENa(number);
            }
            byte[] numberTohex8 = { numberTohex[0], numberTohex[1], numberTohex[2], numberTohex[3], numberTohex[4], numberTohex[5], numberTohex[6], numberTohex[7] };
            byte gun_number8 = numberOr(numberTohex8);
            byte pre0 = (byte)length2(package[0], package[1]);
            gun_number8 = (byte)(gun_number8 + pre0);
            bool high = true;
            package[0] = convert(gun_number8, high);

            package[1] = convert(gun_number8, !high);

            byte[] numberTohex16 = { numberTohex[8], numberTohex[9], numberTohex[10], numberTohex[11], numberTohex[12], numberTohex[13], numberTohex[14], numberTohex[15] };
            byte gun_number16 = numberOr(numberTohex16);
            byte pre2 = (byte)length2(package[2], package[3]);
            gun_number16 = (byte)(gun_number16 + pre2);
            package[2] = convert(gun_number16, high);

            package[3] = convert(gun_number16, !high);

            byte[] numberTohex24 = { numberTohex[16], numberTohex[17], numberTohex[18], numberTohex[19], numberTohex[20], numberTohex[21], numberTohex[22], numberTohex[23] };
            byte gun_number24 = numberOr(numberTohex24);
            byte pre4 = (byte)length2(package[4], package[5]);
            gun_number24 = (byte)(gun_number24 + pre4);

            package[4] = convert(gun_number24, high);

            package[5] = convert(gun_number24, !high);

            byte[] numberTohex32 = { numberTohex[24], numberTohex[25], numberTohex[26], numberTohex[27], numberTohex[28], numberTohex[29], numberTohex[30], numberTohex[31] };
            byte gun_number32 = numberOr(numberTohex32);
            byte pre6 = (byte)length2(package[6], package[7]);
            gun_number32 = (byte)(gun_number32 + pre6);
            package[6] = convert(gun_number32, high);

            package[7] = convert(gun_number32, !high);

            byte[] numberTohex40 = { numberTohex[32], numberTohex[33], numberTohex[34], numberTohex[35], numberTohex[36], numberTohex[37], numberTohex[38], numberTohex[39] };
            byte gun_number40 = numberOr(numberTohex40);

            byte pre8 = (byte)length2(package[8], package[9]);
            gun_number40 = (byte)(gun_number40 + pre8);
            package[8] = convert(gun_number40, high);

            package[9] = convert(gun_number40, !high);

            byte[] numberTohex48 = { numberTohex[40], numberTohex[41], numberTohex[42], numberTohex[43], numberTohex[44], numberTohex[45], numberTohex[46], numberTohex[47] };
            byte gun_number48 = numberOr(numberTohex48);


            byte pre10 = (byte)length2(package[10], package[11]);
            gun_number48 = (byte)(gun_number48 + pre10);


            package[10] = convert(gun_number48, high);

            package[11] = convert(gun_number48, !high);

            byte[] numberTohex56 = { numberTohex[48], numberTohex[49], numberTohex[50], numberTohex[51], numberTohex[52], numberTohex[53], numberTohex[54], numberTohex[55] };
            byte gun_number56 = numberOr(numberTohex56);

            byte pre12 = (byte)length2(package[12], package[13]);
            gun_number56 = (byte)(gun_number56 + pre12);
            package[12] = convert(gun_number56, high);

            package[13] = convert(gun_number56, !high);



            byte[] numberTohex64 = { numberTohex[56], numberTohex[57], numberTohex[58], numberTohex[59], numberTohex[60], numberTohex[61], numberTohex[62], numberTohex[63] };
            byte gun_number64 = numberOr(numberTohex64);


            byte pre14 = (byte)length2(package[14], package[15]);
            gun_number64 = (byte)(gun_number64 + pre14);
            package[14] = convert(gun_number64, high);

            package[15] = convert(gun_number64, !high);


            ////

            byte[] numberTohex72 = { numberTohex[64], numberTohex[65], numberTohex[66], numberTohex[67], numberTohex[68], numberTohex[69], numberTohex[70], numberTohex[71] };
            byte gun_number72 = numberOr(numberTohex72);


            byte pre16 = (byte)length2(package[16], package[17]);
            gun_number72 = (byte)(gun_number72 + pre16);
            package[16] = convert(gun_number72, high);

            package[17] = convert(gun_number72, !high);

            byte[] numberTohex80 = { numberTohex[72], numberTohex[73], numberTohex[74], numberTohex[75], numberTohex[76], numberTohex[77], numberTohex[78], numberTohex[79] };
            byte gun_number80 = numberOr(numberTohex80);
            byte pre18 = (byte)length2(package[18], package[19]);
            gun_number80 = (byte)(gun_number80 + pre18);

            package[18] = convert(gun_number80, high);

            package[19] = convert(gun_number80, !high);

            byte[] numberTohex88 = { numberTohex[80], numberTohex[81], numberTohex[82], numberTohex[83], numberTohex[84], numberTohex[85], numberTohex[86], numberTohex[87] };
            byte gun_number88 = numberOr(numberTohex88);
            byte pre20 = (byte)length2(package[20], package[21]);
            gun_number88 = (byte)(gun_number88 + pre20);
            package[20] = convert(gun_number88, high);

            package[21] = convert(gun_number88, !high);

            byte[] numberTohex96 = { numberTohex[88], numberTohex[89], numberTohex[90], numberTohex[91], numberTohex[92], numberTohex[93], numberTohex[94], numberTohex[95] };
            byte gun_number96 = numberOr(numberTohex96);
            byte pre22 = (byte)length2(package[22], package[23]);
            gun_number96 = (byte)(gun_number96 + pre22);

            package[22] = convert(gun_number96, high);

            package[23] = convert(gun_number96, !high);
            /////
            byte[] numberTohex104 = { numberTohex[96], numberTohex[97], numberTohex[98], numberTohex[99], numberTohex[100], numberTohex[101], numberTohex[102], numberTohex[103] };
            byte gun_number104 = numberOr(numberTohex104);

            byte pre24 = (byte)length2(package[24], package[25]);
            gun_number104 = (byte)(gun_number104 + pre24);

            package[24] = convert(gun_number104, high);

            package[25] = convert(gun_number104, !high);
            byte[] numberTohex112 = { numberTohex[104], numberTohex[105], numberTohex[106], numberTohex[107], numberTohex[108], numberTohex[109], numberTohex[110], numberTohex[111] };
            byte gun_number112 = numberOr(numberTohex112);

            byte pre26 = (byte)length2(package[26], package[27]);
            gun_number112 = (byte)(gun_number112 + pre26);

            package[26] = convert(gun_number112, high);

            package[27] = convert(gun_number112, !high);

            byte[] numberTohex120 = { numberTohex[112], numberTohex[113], numberTohex[114], numberTohex[115], numberTohex[116], numberTohex[117], numberTohex[118], numberTohex[119] };
            byte gun_number120 = numberOr(numberTohex120);

            byte pre28 = (byte)length2(package[28], package[29]);
            gun_number120 = (byte)(gun_number120 + pre28);

            package[28] = convert(gun_number120, high);

            package[29] = convert(gun_number120, !high);

            byte[] numberTohex128 = { numberTohex[120], numberTohex[121], numberTohex[122], numberTohex[123], numberTohex[124], numberTohex[125], numberTohex[126], numberTohex[127] };
            byte gun_number128 = numberOr(numberTohex128);


            byte pre30 = (byte)length2(package[30], package[31]);
            gun_number128 = (byte)(gun_number128 + pre30);

            package[30] = convert(gun_number128, high);

            package[31] = convert(gun_number128, !high);

            string aftEnablestring = string.Join("", package.Select(t => t.ToString()).ToArray());
            string[] enable = new string[] { "53", "16", aftEnablestring };
            return CreateProtocol(enable);
        }
        /// <summary>
        /// 打开制定的的枪位
        /// </summary>
        /// <param name="gunarkNb"></param>
        public string OpenGunState(byte[] gunState,string gunarkid)//开枪位
        {
            //string preEnable = "00000000000000000000000000000000";
            string preEnable = DbSt.GetGunMark(gunarkid);
            char[] pre = preEnable.ToCharArray();
            char[] package = pre ;
            byte[] numberTohex = new byte[400];
            for (int i = 0; i < gunState.Length; i++)
            {

                if (gunState[i] == 0)
                    break;
                byte number = gunState[i];

                numberTohex[number - 1] = gunarkENa(number);
            }
            byte[] numberTohex8 = { numberTohex[0], numberTohex[1], numberTohex[2], numberTohex[3], numberTohex[4], numberTohex[5], numberTohex[6], numberTohex[7] };
            byte gun_number8 = numberOr(numberTohex8);
            byte pre0 = (byte)length2(package[0], package[1]);
            gun_number8 = (byte)(gun_number8 + pre0);
            bool high = true;
            package[0] = convert(gun_number8, high);

            package[1] = convert(gun_number8, !high);

            byte[] numberTohex16 = { numberTohex[8], numberTohex[9], numberTohex[10], numberTohex[11], numberTohex[12], numberTohex[13], numberTohex[14], numberTohex[15] };
            byte gun_number16 = numberOr(numberTohex16);
            byte pre2 = (byte)length2(package[2], package[3]);
            gun_number16 = (byte)(gun_number16 + pre2);
            package[2] = convert(gun_number16, high);

            package[3] = convert(gun_number16, !high);

            byte[] numberTohex24 = { numberTohex[16], numberTohex[17], numberTohex[18], numberTohex[19], numberTohex[20], numberTohex[21], numberTohex[22], numberTohex[23] };
            byte gun_number24 = numberOr(numberTohex24);
            byte pre4 = (byte)length2(package[4], package[5]);
            gun_number24 = (byte)(gun_number24 + pre4);

            package[4] = convert(gun_number24, high);

            package[5] = convert(gun_number24, !high);

            byte[] numberTohex32 = { numberTohex[24], numberTohex[25], numberTohex[26], numberTohex[27], numberTohex[28], numberTohex[29], numberTohex[30], numberTohex[31] };
            byte gun_number32 = numberOr(numberTohex32);
            byte pre6 = (byte)length2(package[6], package[7]);
            gun_number32 = (byte)(gun_number32 + pre6);
            package[6] = convert(gun_number32, high);

            package[7] = convert(gun_number32, !high);

            byte[] numberTohex40 = { numberTohex[32], numberTohex[33], numberTohex[34], numberTohex[35], numberTohex[36], numberTohex[37], numberTohex[38], numberTohex[39] };
            byte gun_number40 = numberOr(numberTohex40);

            byte pre8 = (byte)length2(package[8], package[9]);
            gun_number40 = (byte)(gun_number40 + pre8);
            package[8] = convert(gun_number40, high);

            package[9] = convert(gun_number40, !high);

            byte[] numberTohex48 = { numberTohex[40], numberTohex[41], numberTohex[42], numberTohex[43], numberTohex[44], numberTohex[45], numberTohex[46], numberTohex[47] };
            byte gun_number48 = numberOr(numberTohex48);


            byte pre10 = (byte)length2(package[10], package[11]);
            gun_number48 = (byte)(gun_number48 + pre10);


            package[10] = convert(gun_number48, high);

            package[11] = convert(gun_number48, !high);

            byte[] numberTohex56 = { numberTohex[48], numberTohex[49], numberTohex[50], numberTohex[51], numberTohex[52], numberTohex[53], numberTohex[54], numberTohex[55] };
            byte gun_number56 = numberOr(numberTohex56);

            byte pre12 = (byte)length2(package[12], package[13]);
            gun_number56 = (byte)(gun_number56 + pre12);
            package[12] = convert(gun_number56, high);

            package[13] = convert(gun_number56, !high);



            byte[] numberTohex64 = { numberTohex[56], numberTohex[57], numberTohex[58], numberTohex[59], numberTohex[60], numberTohex[61], numberTohex[62], numberTohex[63] };
            byte gun_number64 = numberOr(numberTohex64);


            byte pre14 = (byte)length2(package[14], package[15]);
            gun_number64 = (byte)(gun_number64 + pre14);
            package[14] = convert(gun_number64, high);

            package[15] = convert(gun_number64, !high);


            ////

            byte[] numberTohex72 = { numberTohex[64], numberTohex[65], numberTohex[66], numberTohex[67], numberTohex[68], numberTohex[69], numberTohex[70], numberTohex[71] };
            byte gun_number72 = numberOr(numberTohex72);


            byte pre16 = (byte)length2(package[16], package[17]);
            gun_number72 = (byte)(gun_number72 + pre16);
            package[16] = convert(gun_number72, high);

            package[17] = convert(gun_number72, !high);

            byte[] numberTohex80 = { numberTohex[72], numberTohex[73], numberTohex[74], numberTohex[75], numberTohex[76], numberTohex[77], numberTohex[78], numberTohex[79] };
            byte gun_number80 = numberOr(numberTohex80);
            byte pre18 = (byte)length2(package[18], package[19]);
            gun_number80 = (byte)(gun_number80 + pre18);

            package[18] = convert(gun_number80, high);

            package[19] = convert(gun_number80, !high);

            byte[] numberTohex88 = { numberTohex[80], numberTohex[81], numberTohex[82], numberTohex[83], numberTohex[84], numberTohex[85], numberTohex[86], numberTohex[87] };
            byte gun_number88 = numberOr(numberTohex88);
            byte pre20 = (byte)length2(package[20], package[21]);
            gun_number88 = (byte)(gun_number88 + pre20);
            package[20] = convert(gun_number88, high);

            package[21] = convert(gun_number88, !high);

            byte[] numberTohex96 = { numberTohex[88], numberTohex[89], numberTohex[90], numberTohex[91], numberTohex[92], numberTohex[93], numberTohex[94], numberTohex[95] };
            byte gun_number96 = numberOr(numberTohex96);
            byte pre22 = (byte)length2(package[22], package[23]);
            gun_number96 = (byte)(gun_number96 + pre22);

            package[22] = convert(gun_number96, high);

            package[23] = convert(gun_number96, !high);
            /////
            byte[] numberTohex104 = { numberTohex[96], numberTohex[97], numberTohex[98], numberTohex[99], numberTohex[100], numberTohex[101], numberTohex[102], numberTohex[103] };
            byte gun_number104 = numberOr(numberTohex104);

            byte pre24 = (byte)length2(package[24], package[25]);
            gun_number104 = (byte)(gun_number104 + pre24);

            package[24] = convert(gun_number104, high);

            package[25] = convert(gun_number104, !high);
            byte[] numberTohex112 = { numberTohex[104], numberTohex[105], numberTohex[106], numberTohex[107], numberTohex[108], numberTohex[109], numberTohex[110], numberTohex[111] };
            byte gun_number112 = numberOr(numberTohex112);

            byte pre26 = (byte)length2(package[26], package[27]);
            gun_number112 = (byte)(gun_number112 + pre26);

            package[26] = convert(gun_number112, high);

            package[27] = convert(gun_number112, !high);

            byte[] numberTohex120 = { numberTohex[112], numberTohex[113], numberTohex[114], numberTohex[115], numberTohex[116], numberTohex[117], numberTohex[118], numberTohex[119] };
            byte gun_number120 = numberOr(numberTohex120);

            byte pre28 = (byte)length2(package[28], package[29]);
            gun_number120 = (byte)(gun_number120 + pre28);

            package[28] = convert(gun_number120, high);

            package[29] = convert(gun_number120, !high);

            byte[] numberTohex128 = { numberTohex[120], numberTohex[121], numberTohex[122], numberTohex[123], numberTohex[124], numberTohex[125], numberTohex[126], numberTohex[127] };
            byte gun_number128 = numberOr(numberTohex128);


            byte pre30 = (byte)length2(package[30], package[31]);
            gun_number128 = (byte)(gun_number128 + pre30);

            package[30] = convert(gun_number128, high);

            package[31] = convert(gun_number128, !high);

            string aftEnablestring =string.Join("",package.Select(t=>t.ToString()).ToArray());
            string[] enable = new string[] {"53","16",aftEnablestring};
            return CreateProtocol(enable);
        }
        /// <summary>
        /// 归还指定枪支
        /// </summary>
        /// <param name="gunState"></param>
        /// <returns></returns>
        public string ReturnGun(byte[]gunState,string gunarkid)
        {
            //string preGunstate = "00000000000000000000000000000000";
            string preGunstate = DbSt.GetGunMark(gunarkid);
            char[] pre = preGunstate.ToCharArray();
            char[] package = pre;
            byte[] numberTohex = new byte[400];
            for (int i = 0; i < gunState.Length; i++)
            {

                if (gunState[i] == 0)
                    break;
                byte number = gunState[i];

                numberTohex[number - 1] = gunarkENa(number);
            }
            byte[] numberTohex8 = { numberTohex[0], numberTohex[1], numberTohex[2], numberTohex[3], numberTohex[4], numberTohex[5], numberTohex[6], numberTohex[7] };
            byte gun_number8 = numberOr(numberTohex8);
            byte pre0 = (byte)length2(package[0], package[1]);
            gun_number8 = (byte)(-gun_number8 + pre0);
            bool high = true;
            package[0] = convert(gun_number8, high);

            package[1] = convert(gun_number8, !high);

            byte[] numberTohex16 = { numberTohex[8], numberTohex[9], numberTohex[10], numberTohex[11], numberTohex[12], numberTohex[13], numberTohex[14], numberTohex[15] };
            byte gun_number16 = numberOr(numberTohex16);
            byte pre2 = (byte)length2(package[2], package[3]);
            gun_number16 = (byte)(-gun_number16 + pre2);
            package[2] = convert(gun_number16, high);

            package[3] = convert(gun_number16, !high);

            byte[] numberTohex24 = { numberTohex[16], numberTohex[17], numberTohex[18], numberTohex[19], numberTohex[20], numberTohex[21], numberTohex[22], numberTohex[23] };
            byte gun_number24 = numberOr(numberTohex24);
            byte pre4 = (byte)length2(package[4], package[5]);
            gun_number24 = (byte)(-gun_number24 + pre4);

            package[4] = convert(gun_number24, high);

            package[5] = convert(gun_number24, !high);

            byte[] numberTohex32 = { numberTohex[24], numberTohex[25], numberTohex[26], numberTohex[27], numberTohex[28], numberTohex[29], numberTohex[30], numberTohex[31] };
            byte gun_number32 = numberOr(numberTohex32);
            byte pre6 = (byte)length2(package[6], package[7]);
            gun_number32 = (byte)(-gun_number32 + pre6);
            package[6] = convert(gun_number32, high);

            package[7] = convert(gun_number32, !high);

            byte[] numberTohex40 = { numberTohex[32], numberTohex[33], numberTohex[34], numberTohex[35], numberTohex[36], numberTohex[37], numberTohex[38], numberTohex[39] };
            byte gun_number40 = numberOr(numberTohex40);

            byte pre8 = (byte)length2(package[8], package[9]);
            gun_number40 = (byte)(-gun_number40 + pre8);
            package[8] = convert(gun_number40, high);

            package[9] = convert(gun_number40, !high);

            byte[] numberTohex48 = { numberTohex[40], numberTohex[41], numberTohex[42], numberTohex[43], numberTohex[44], numberTohex[45], numberTohex[46], numberTohex[47] };
            byte gun_number48 = numberOr(numberTohex48);


            byte pre10 = (byte)length2(package[10], package[11]);
            gun_number48 = (byte)(-gun_number48 + pre10);


            package[10] = convert(gun_number48, high);

            package[11] = convert(gun_number48, !high);

            byte[] numberTohex56 = { numberTohex[48], numberTohex[49], numberTohex[50], numberTohex[51], numberTohex[52], numberTohex[53], numberTohex[54], numberTohex[55] };
            byte gun_number56 = numberOr(numberTohex56);

            byte pre12 = (byte)length2(package[12], package[13]);
            gun_number56 = (byte)(-gun_number56 + pre12);
            package[12] = convert(gun_number56, high);

            package[13] = convert(gun_number56, !high);



            byte[] numberTohex64 = { numberTohex[56], numberTohex[57], numberTohex[58], numberTohex[59], numberTohex[60], numberTohex[61], numberTohex[62], numberTohex[63] };
            byte gun_number64 = numberOr(numberTohex64);


            byte pre14 = (byte)length2(package[14], package[15]);
            gun_number64 = (byte)(-gun_number64 + pre14);
            package[14] = convert(gun_number64, high);

            package[15] = convert(gun_number64, !high);

            ////
            byte[] numberTohex72 = { numberTohex[64], numberTohex[65], numberTohex[66], numberTohex[67], numberTohex[68], numberTohex[69], numberTohex[70], numberTohex[71] };
            byte gun_number72 = numberOr(numberTohex72);


            byte pre16 = (byte)length2(package[16], package[17]);
            gun_number72 = (byte)(-gun_number72 + pre16);
            package[16] = convert(gun_number72, high);

            package[17] = convert(gun_number72, !high);

            byte[] numberTohex80 = { numberTohex[72], numberTohex[73], numberTohex[74], numberTohex[75], numberTohex[76], numberTohex[77], numberTohex[78], numberTohex[79] };
            byte gun_number80 = numberOr(numberTohex80);
            byte pre18 = (byte)length2(package[18], package[19]);
            gun_number80 = (byte)(-gun_number80 + pre18);

            package[18] = convert(gun_number80, high);

            package[19] = convert(gun_number80, !high);

            byte[] numberTohex88 = { numberTohex[80], numberTohex[81], numberTohex[82], numberTohex[83], numberTohex[84], numberTohex[85], numberTohex[86], numberTohex[87] };
            byte gun_number88 = numberOr(numberTohex88);
            byte pre20 = (byte)length2(package[20], package[21]);
            gun_number88 = (byte)(-gun_number88 + pre20);
            package[20] = convert(gun_number88, high);

            package[21] = convert(gun_number88, !high);

            byte[] numberTohex96 = { numberTohex[88], numberTohex[89], numberTohex[90], numberTohex[91], numberTohex[92], numberTohex[93], numberTohex[94], numberTohex[95] };
            byte gun_number96 = numberOr(numberTohex96);
            byte pre22 = (byte)length2(package[22], package[23]);
            gun_number96 = (byte)(-gun_number96 + pre22);

            package[22] = convert(gun_number96, high);

            package[23] = convert(gun_number96, !high);
            /////
            byte[] numberTohex104 = { numberTohex[96], numberTohex[97], numberTohex[98], numberTohex[99], numberTohex[100], numberTohex[101], numberTohex[102], numberTohex[103] };
            byte gun_number104 = numberOr(numberTohex104);

            byte pre24 = (byte)length2(package[24], package[25]);
            gun_number104 = (byte)(-gun_number104 + pre24);

            package[24] = convert(gun_number104, high);

            package[25] = convert(gun_number104, !high);
            byte[] numberTohex112 = { numberTohex[104], numberTohex[105], numberTohex[106], numberTohex[107], numberTohex[108], numberTohex[109], numberTohex[110], numberTohex[111] };
            byte gun_number112 = numberOr(numberTohex112);

            byte pre26 = (byte)length2(package[26], package[27]);
            gun_number112 = (byte)(-gun_number112 + pre26);

            package[26] = convert(gun_number112, high);

            package[27] = convert(gun_number112, !high);

            byte[] numberTohex120 = { numberTohex[112], numberTohex[113], numberTohex[114], numberTohex[115], numberTohex[116], numberTohex[117], numberTohex[118], numberTohex[119] };
            byte gun_number120 = numberOr(numberTohex120);

            byte pre28 = (byte)length2(package[28], package[29]);
            gun_number120 = (byte)(-gun_number120 + pre28);

            package[28] = convert(gun_number120, high);

            package[29] = convert(gun_number120, !high);

            byte[] numberTohex128 = { numberTohex[120], numberTohex[121], numberTohex[122], numberTohex[123], numberTohex[124], numberTohex[125], numberTohex[126], numberTohex[127] };
            byte gun_number128 = numberOr(numberTohex128);


            byte pre30 = (byte)length2(package[30], package[31]);
            gun_number128 = (byte)(-gun_number128 + pre30);

            package[30] = convert(gun_number128, high);

            package[31] = convert(gun_number128, !high);

            string aftEnablestring = string.Join("", package.Select(t => t.ToString()).ToArray());
            string[] enable = new string[] { "53", "16", aftEnablestring };
            return CreateProtocol(enable);
        }
        /// <summary>
        /// 实时查询柜子里枪支信息
        /// </summary>
        /// <returns></returns>
        public string CheckStatus()
        {
            string[] checkSt = new string[] { "2D", "00", "" };
            return CreateProtocol(checkSt);
        }
        /// <summary>
        /// 查询柜子信息
        /// </summary>
        /// <returns></returns>
        public string Require()
        {
            string[] require = new string[] { "57", "00", "" };
            return CreateProtocol(require);
        }
         /// <summary>
         /// 查询柜子中已经入柜枪支编号
         /// </summary>
         /// <param name="gunark_id"> 需要查的柜子编号</param>
         /// <returns></returns>
        public bool  GunBullet_Status(string gunark_id)
        {
           
            bool task_status = true;//正常
            List<byte> gunNumber = new List<byte>();
            List<byte> gunNumber_mysql = new List<byte>();
            List<string> gunNumberID_mysql = new List<string>();
            int count = 0;
            while (Utils.isAlive)
            {
                System.Threading.Thread.Sleep(1000);
                string require = Require();
                //string ip = DBstor.getDBip(gunark_id);
                string IpSocket = DBstor.getDBipSocket(gunark_id);
             
                string connectionSokKey = IpSocket;
                try
                {
                    frmMain.dictConn[connectionSokKey].Send(require);
                    System.Threading.Thread.Sleep(1000);
                }
                catch
                {
                    continue;
                }
                if (ConnectionClient.gunStatus != "")
                {

                    char[] GunStatus = null;
                    char[] doorStatus = null;
                    try
                    {
                        //GunStatus = ConnectionClient.gunStatus.Substring(0, 12).ToCharArray();
                        
                        doorStatus = ConnectionClient.gunStatus.Substring(24, 2).ToCharArray();
                    }
                    catch
                    {
                        continue;
                    }
                    ConnectionClient.gunStatus = "";
                    int cc1 = Cver12.StrToInt(doorStatus[0], doorStatus[1]);
                    string cc2 = Convert.ToString(cc1, 2);
                    char[] alarmCC = cc2.ToCharArray();
                    
                    if(alarmCC[7] == '1')
                    {
                        count = count+1;
                        continue;
                        
                    }
                    if (count != 0)
                    {
                        count = 0;
                        GunStatus = GetGunSt(connectionSokKey);
                        byte[] data2 = new byte[24];
                        for (int n = 0; n < 24; n++)
                        {
                            data2[n] = (byte)Cver12.ASCIItoHEX((byte)GunStatus[n]);
                        }
                        string[] data3 = Cver12.DECtoBIN(data2);
                        string data4 = "";
                        for (int n = 0; n < data3.Length; n++)
                        {
                            data4 = data4 + data3[n];

                        }
                        char[] data5 = data4.ToCharArray();

                        for (int j = 0; j < data5.Length; j++)
                        {
                            if (data5[j] == '1')
                            {
                                byte gunbulletnumber = (byte)(j + 1);
                                gunNumber.Add(gunbulletnumber);
                            }
                        }
                        System.Threading.Thread.Sleep(500);
                        //if 判断枪支状态是否正常；
                        string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                        MySqlConnection con = new MySqlConnection(str);//实例化链接
                        con.Open();//开启连接
                        string strcmd = "SELECT GUN_POSITION_NUMBER,GUN_POSITION_INFO_ID FROM gunark_position_info g WHERE g.GUN_POSITION_STATUS = 3 and g.GUNARK_ID = '" + gunark_id + "' ORDER BY g.GUN_POSITION_NUMBER;";
                        MySqlCommand cmd = new MySqlCommand(strcmd, con);
                        MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        ada.Fill(ds);//查询结果填充数据集
                        con.Close();
                        error_gun_position_ID = new List<string>();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string gunbulletnumber_mysql = ds.Tables[0].Rows[i][0].ToString();
                            byte gunbulletnumber_mysql1 = (byte)int.Parse(gunbulletnumber_mysql);
                            gunNumber_mysql.Add(gunbulletnumber_mysql1);

                            string gunbulletnumberID_mysql = ds.Tables[0].Rows[i][1].ToString();

                            gunNumberID_mysql.Add(gunbulletnumberID_mysql);
                        }
                        if (gunNumber.Count != gunNumber_mysql.Count)
                        {

                            for (int n = 0; n < gunNumber_mysql.Count; n++)
                                if (gunNumber.Contains(gunNumber_mysql[n]))
                                {
                                    //task_status = false;

                                }
                                else
                                {

                                    error_gun_position_ID.Add(gunNumberID_mysql[n]);
                                    task_status = false;

                                }

                        }
                        else
                            task_status = true;
                        //task_status = false;
                        break;
                    }
                }

            }
            return task_status;

        }
        public char[] GetGunSt(string IpSocket)
        {
            char[] GunStatus = null;
            while (true)
            {
                System.Threading.Thread.Sleep(3000);
                string check = CheckStatus();
                //string ip = DBstor.getDBip(gunark_id);
                //string IpSocket = DBstor.getDBipSocket(ip);

                string connectionSokKey = IpSocket;
                try
                {
                    frmMain.dictConn[connectionSokKey].Send(check);
                    System.Threading.Thread.Sleep(2000);
                }
                catch
                {
                    continue;
                }
                if (ConnectionClient.gunCheckSt != "")
                {
                    GunStatus = ConnectionClient.gunCheckSt.ToCharArray();
                    ConnectionClient.gunCheckSt = "";
                    break;
                }
            }
            return GunStatus;
        }
        

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
        public int length2(char high, char low)//指令长度计算
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
    }
}
