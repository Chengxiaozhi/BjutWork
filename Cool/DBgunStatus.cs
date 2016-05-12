using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Cool
{
    class DBgunStatus
    {
        public void SetGunID( string gunark_id)//录入柜子ID
        {
            ///////////

            string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            //string sqlFinger = "insert into gunark_finger_storage (USER_FINGERPRINT)values(" + @"""" + @StrFinger(finger) + @"""" + " )";
            string sqlstr = "insert into gunark_position_status (GUNARK_MARK,GUNARK_ENABLE, GUNARK_ID,GUNARK_DATE)values( 'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF','00000000000000000000000000000000','" + gunark_id + "','" + System.DateTime.Now.ToString() + "') ";
           // string sqlstr = "update gunark_position_status set GUNARK_MARK= '" + newGunStatus + "', GUNARK_DATE = '" + System.DateTime.Now.ToString() + "'where GUNARK_ID = '" + gunark_id + "'";
            MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            conn.Close();//关闭连接
        }
        public void InsGunMark(string newGunStatus, string gunark_id)//录入Mark标记位字
        {
            ///////////

            string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            //string sqlFinger = "insert into gunark_finger_storage (USER_FINGERPRINT)values(" + @"""" + @StrFinger(finger) + @"""" + " )";
            //string sqlstr = "insert into gunark_position_status (GUNARK_MARK, GUNARK_DATE)values('" + newGunStatus + "','"+ System.DateTime.Now.ToString()+"') ";
            string sqlstr = "update gunark_position_status set GUNARK_MARK= '" + newGunStatus + "', GUNARK_DATE = '" + System.DateTime.Now.ToString() + "'where GUNARK_ID = '" + gunark_id + "'";
            MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            conn.Close();//关闭连接
        }

        public void InsGunEnable(string newGunStatus, string gunark_id)//录入使能标记位字
        {
            ///////////

            string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            //string sqlFinger = "insert into gunark_finger_storage (USER_FINGERPRINT)values(" + @"""" + @StrFinger(finger) + @"""" + " )";
            //string sqlstr = "insert into gunark_position_status (GUNARK_ENABLE, GUNARK_DATE)values('" + newGunStatus + "','" + System.DateTime.Now.ToString() + "')";
            string sqlstr = "update gunark_position_status set GUNARK_ENABLE= '" + newGunStatus + "', GUNARK_DATE = '" + System.DateTime.Now.ToString() + "'where GUNARK_ID = '" + gunark_id + "'";

            MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            conn.Close();//关闭连接
        }


        public string GetGunEnabe(string gunark_id)//获取数据库中枪柜使能选通信息
        {
            DateTime now = DateTime.Now;
            string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            string sqlFinger = "select GUNARK_ENABLE from gunark_position_status where GUNARK_ID  = '" + gunark_id + "'";
            MySqlCommand cmd = new MySqlCommand(sqlFinger, conn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            DataTable dt = ds.Tables[0];
            int len1 = ds.Tables[0].Rows.Count - 1;
            string mark1 = "";
            string gunEnable = "";
            for (int i = len1; i >= 0; i--)
            {

                if (ds.Tables[0].Rows[i][0].ToString() != "")
                {
                    mark1 = ds.Tables[0].Rows[i][0].ToString();
                    break;
                }

            }
            // MessageBox.Show(mark1);
            gunEnable = mark1;
            mark1 = "";
            conn.Close();
            return gunEnable;
        }

        public string GetGunMark(string gunark_id)//获取数据库中枪柜标记信息
        {
            DateTime now = DateTime.Now;
            string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            string sqlFinger = "select GUNARK_MARK from gunark_position_status where GUNARK_ID  = '" + gunark_id + "'";
            MySqlCommand cmd = new MySqlCommand(sqlFinger, conn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            DataTable dt = ds.Tables[0];
            int len1 = ds.Tables[0].Rows.Count - 1;
            string mark1 = "";
            string gunMark = "";
            for (int i = len1; i >= 0; i--)
            {

                if (ds.Tables[0].Rows[i][0].ToString() != "")
                {
                    mark1 = ds.Tables[0].Rows[i][0].ToString();
                    break;
                }

            }
            //string mark1 = ds.Tables[0].Rows[len1][0].ToString();
            // MessageBox.Show(mark1);
            gunMark = mark1;
            mark1 = "";
            conn.Close();


            return gunMark;
        }
    }
}
