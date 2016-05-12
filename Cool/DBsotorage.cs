using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Cool
{
    class DBsotorage
    {
        public void InsertDBipSocket(string IPsocket, string IP)//录入通信嵌套字
        {
            ///////////
           
            string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            //string sqlFinger = "insert into gunark_finger_storage (USER_FINGERPRINT)values(" + @"""" + @StrFinger(finger) + @"""" + " )";
            string sqlFinger = "update gunark_gunark_storage set GUNARK_SOCKET = " + @"""" + IPsocket + @"""" + " where GUNARK_IP =  '" + IP + "'";

            MySqlCommand cmd = new MySqlCommand(sqlFinger, conn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            conn.Close();//关闭连接
        }

        public void UpdateDBipSocket(string IPsocket, string IP)//录入通信嵌套字
        {
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            con.Open();
            string sqlFinger = "update gunark_gunark_storage set GUNARK_SOCKET = " + @"""" + IPsocket + @"""" + " where GUNARK_IP =  '" + IP + "'";
            DatabaseManager.Exec(sqlFinger,con);
            DatabaseManager.Clear(con);
        }

        public void InsertDBip(string IP)//录入通信嵌套字
        {
            ///////////

            string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            //string sqlFinger = "insert into gunark_finger_storage (USER_FINGERPRINT)values(" + @"""" + @StrFinger(finger) + @"""" + " )";
            string sqlFinger = "insert ignore into gunark_gunark_storage (GUNARK_IP)values('"+IP +"')";

            MySqlCommand cmd = new MySqlCommand(sqlFinger, conn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            conn.Close();//关闭连接
        }

        public string getDBipSocket(string gunark_id)//获取通信嵌套字
        {
            string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            string sqlFinger = "select GUNARK_SOCKET from gunark_gunark_storage where GUNARK_ID =  '" + gunark_id + "'";
            MySqlCommand cmd = new MySqlCommand(sqlFinger, conn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集
            DataTable dt = ds.Tables[0];
            conn.Close();//关闭连接
            try
            {
                string update = ds.Tables[0].Rows[0][0].ToString();
                return update;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取枪柜IP
        /// </summary>2016-3-23
        /// <param name="gunarkid"> 枪柜编号</param>
        /// <returns></returns>
        public string getDBip(string ip)//获取枪柜IP
        {
            string IP = "";
            string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            string sqlFinger = "select GUNARK_SOCKET from gunark_gunark_storage where GUNARK_ip =  '" + ip + "'";
            MySqlCommand cmd = new MySqlCommand(sqlFinger, conn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集
            DataTable dt = ds.Tables[0];
            if (ds.Tables[0].Rows.Count != 0)
            {
                 IP = ds.Tables[0].Rows[0][0].ToString();
            }
            conn.Close();//关闭连接
            return IP;
        }
    }
}
