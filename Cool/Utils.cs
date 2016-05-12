using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Cool
{
    class Utils
    {
        public static bool isAlive = true;
        /// <summary>
        /// //获取用户名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string getUserById(string id)
        {
            string userName = "";
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            string sql = "select USER_REALNAME from gunark_user_finger where ID = '" + id + "'";
            DataSet ds = DatabaseManager.ExecAndGetDs(sql, "gunark_user_finger",con);
            userName = ds.Tables[0].Rows[0][0].ToString();
            DatabaseManager.Clear(con);
            return userName;
        }

        /// <summary>
        /// //获取用户警号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string getUserPoliceNumById(string id)
        {
            string policeNum = "";
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            string sql = "select USER_POLICENUMB from gunark_user_finger where ID = '" + id + "'";
            DataSet ds = DatabaseManager.ExecAndGetDs(sql, "gunark_user_finger",con);
            policeNum = ds.Tables[0].Rows[0][0].ToString();
            DatabaseManager.Clear(con);
            return policeNum;
        }

        
        
        /// <summary>
        /// //清空任务执行的几个静态数据
        /// </summary>
        /// <param name=></param>
        /// <returns></returns>
        public static void  clearData()
        {
            for (int i = 0; i < Task_Execute_Detail.count; i++)
            {
                Task_Execute_Detail.gunid[i] = "";
                Task_Execute_Detail.gunpositionid[i] = "";
                Task_Execute_Detail.gunpositionnum[i] = 0;
                Task_Execute_Detail.magazinenum[i] = 0;
            }
            Task_Execute_Detail.count = 0;

            
        }
    }
}
