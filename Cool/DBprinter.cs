using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
namespace Cool
{
    class DBprinter
    {

        Convert12 Con12 = new Convert12();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usernumber"></param>
        /// <returns></returns>
        public string  getUsername(string usernumber)//获取指纹特征值
        {
            //string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            con.Open();
            string sqlFinger = "select gu.USER_REALNAME from gunark_user gu left join gunark_user_finger guf on gu.USER_POLICENUMB = guf.USER_POLICENUMB where guf.ID = '" + usernumber + "'";
            DataSet ds = DatabaseManager.ExecAndGetDs(sqlFinger, "gunark_fingerprint",con);
            DatabaseManager.Clear(con);
            return ds.Tables[0].Rows[0][0].ToString() ;
        }

        /// <summary>
        ///  获取服务器上下载的用户指纹信息
        /// </summary>
        /// <param name="usernumber">userID</param>
        /// <returns></returns>
        /// 
        public DataSet getfingerID(string usernumber)//获取指纹特征值
        {
            //string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            con.Open();
            string sqlFinger = "select gf.FINGER_NUMBER ,gf.USER_POLICENUMB from gunark_fingerprint gf left join gunark_user_finger guf on gf.USER_POLICENUMB = guf.USER_POLICENUMB where guf.ID = '" + usernumber + "' and gf.IS_UPDATE !=  '1'";
            DataSet ds = DatabaseManager.ExecAndGetDs(sqlFinger, "gunark_fingerprint",con);
            DatabaseManager.Clear(con);
            return ds;
        }
      

        /// <summary>
        ///  获取服务器上下载的用户指纹信息
        /// </summary>
        /// <param name="usernumber">userID</param>
        /// <returns></returns>
        /// 
        public string getfingerAndID(string usernumber,string fingerID)//获取指纹特征值
        {
            //string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            string sqlFinger = "select gf.USER_FINGERPRINT from gunark_fingerprint gf left join gunark_user_finger guf on gf.USER_POLICENUMB = guf.USER_POLICENUMB where guf.ID = " + usernumber + " and gf.IS_UPDATE !=  '1'" + " and gf.FINGER_NUMBER = '" + fingerID+"'";
            MySqlCommand cmd = new MySqlCommand(sqlFinger, conn);
     
            System.Data.Common.DbDataReader reader = cmd.ExecuteReader();
            byte[] buffer = null;
            String fingerString = null;
            if (reader.HasRows)
            {
                reader.Read();
                long len = reader.GetBytes(0, 0, null, 0, 0);
                buffer = new byte[len];
                len = reader.GetBytes(0, 0, buffer, 0, (int)len);
                fingerString += System.Text.Encoding.Default.GetString(buffer);
            }
            conn.Close();//关闭连接
            fingerString = fingerString.Substring(0, fingerString.Length);

            return fingerString;
        }
      
        public string getDBfinger(string usernumber,int fingerNum)//获取指纹特征值
        {
            //string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            string sqlFinger = "select USER_FINGERPRINT_"+fingerNum+" from gunark_user_finger where ID =  " + usernumber + "";
            MySqlCommand cmd = new MySqlCommand(sqlFinger, conn);

            System.Data.Common.DbDataReader reader = cmd.ExecuteReader();
            byte[] buffer = null;
            String fingerString = null;
            if (reader.HasRows)
            {
                reader.Read();
                long len = reader.GetBytes(0, 0, null, 0, 0);
                buffer = new byte[len];
                len = reader.GetBytes(0, 0, buffer, 0, (int)len);
                fingerString += System.Text.Encoding.Default.GetString(buffer);
            }


            conn.Close();//关闭连接
            fingerString = fingerString.Substring(0, fingerString.Length);

            return fingerString;
        }
        /// <summary>
        /// 录入指纹
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void InsertDBfinger(byte[] finger,int user_number,int fingerNum)
        {
            ///////////
      
            string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            //string sqlFinger = "insert into gunark_finger_storage (USER_FINGERPRINT)values(" + @"""" + @StrFinger(finger) + @"""" + " )";
            string sqlFinger ="update gunark_user_finger set USER_FINGERPRINT_"+fingerNum+" = " + @"""" + @StrFinger(finger) + @"""" +" where ID =  " + user_number + "";
            
            MySqlCommand cmd = new MySqlCommand(sqlFinger, conn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            conn.Close();

            SetIsInputFinger(user_number, fingerNum);
        }

        public void SetIsInputFinger(int user_number,int fingerNum)
        {
            string isInuputFinger = "";
            switch(fingerNum)
            {
                case 1:
                    isInuputFinger = "【常用】";
                    break;
                case 2:
                    isInuputFinger = "【备用】";
                    break;
                case 3:
                    isInuputFinger = "【报警】";
                    break;
                default:
                    break;
            }
                
            //连接数据库
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            con.Open();
            string sql = "update gunark_user_finger set IsInputFinger_"+fingerNum+" = '"+isInuputFinger+"' where ID = " + user_number;
            //执行SQL语句
            DatabaseManager.Exec(sql,con);
            //关闭数据库连接
            DatabaseManager.Clear(con);
        }

        public byte[] getDBUserID()// 获取用户组别
        {
            byte[] UserID = new byte[10];
            for (int i = 0; i < UserID.Length; i++)
            {
                UserID[i] = 0;
            }

            //  string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            string sqlFinger = "select ID from gunark_fingerprint ";
            MySqlCommand cmd = new MySqlCommand(sqlFinger, conn);



            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集
            DataTable dt = ds.Tables[0];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                UserID[i] = (byte)int.Parse(ds.Tables[0].Rows[i][0].ToString());
            }



            //System.Data.Common.DbDataReader reader = cmd.ExecuteReader();
            //byte[] buffer = null;
            //String downUserID = null;
            //if (reader.HasRows)
            //{
            //    reader.Read();
            //    long len = reader.GetBytes(0, 0, null, 0, 0);
            //    buffer = new byte[len];
            //    len = reader.GetBytes(0, 0, buffer, 0, (int)len);
            //    downUserID += System.Text.Encoding.Default.GetString(buffer);
            //}


            conn.Close();//关闭连接
            //fingerString = fingerString.Substring(0, fingerString.Length - 3);

            return UserID;
        }


        public void setupdateFinger(string usernumber, string fingernumber)//更改下载状态
        {

            string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            string sqlFinger = "update gunark_fingerprint set IS_UPDATE = '1' where  USER_POLICENUMB =  '" + usernumber + "' and FINGER_NUMBER = '" + fingernumber +"'";
            MySqlCommand cmd = new MySqlCommand(sqlFinger, conn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集
            conn.Close();//关闭连接

        }
        public string getupdatefinger(string id)//获取下载状态
        {
            string str = "Server=127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            string sqlFinger = "select IS_UPDATE from gunark_fingerprint  where ID =  '" + id + "'";
            MySqlCommand cmd = new MySqlCommand(sqlFinger, conn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集
            DataTable dt = ds.Tables[0];

            string update = ds.Tables[0].Rows[0][0].ToString();
            conn.Close();//关闭连接
            return update;
        }
        string StrFinger(byte[] finger)
        {
            string userFinger = "";
            int n = finger.Length;
            for (int i = 0; i < n; i++)
            {
                userFinger += finger[i].ToString() + " ";

            }
            userFinger = userFinger.Trim();
            return userFinger;
        }

        public byte [] WorkUserID()
        {
            byte[] workUserID = new byte[100] ;
            string str = "Server = 127.0.0.1;User ID=root;Password=123456;Database=qdgl;CharSet=utf8";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();

            string sql = "select gunark_user_finger.ID from gunark_user_finger left join gunark_admin on gunark_admin.USERID=gunark_user_finger.USER_POLICENUMB where gunark_admin.ADMIN_STATUS = 1";
            MySqlCommand cdm = new MySqlCommand(sql,conn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cdm);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //string ss = dt.Rows[i][0].ToString();
                //MessageBox.Show(ss);
                 workUserID[i] =(byte)int.Parse( dt.Rows[i][0].ToString());
            }
            conn.Close();
            return workUserID;
        }

    }
}
