using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using MySql.Data.MySqlClient;

/// <summary>
/// 数据库封装类，用于执行对数据库的操作
/// </summary>
static class DatabaseManager
{
    /// <summary>
    /// 静态链接字符串，连接Mysql数据库
    /// </summary>
    //private static string connection = "Driver={MySQL JDBC Driver};Server=localhost;Database=qdgl;User=root;Password=123456;Option=3;";
    // private static OdbcCommand cmd = new OdbcCommand();
    // private static OdbcConnection con = null;
    private static string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
    //private static MySqlConnection con = null;//实例化链接

    //string strcmd = "SELECT GUNARK_TYPE,GUNARK_NAME,GUNARK_ID from gunark_gunark;";
    private static MySqlCommand cmd = new MySqlCommand();
    /// <summary>
    /// Gets the con.获得数据库连接的方法
    /// </summary>
    /// <returns></returns>
    public static MySqlConnection GetCon(MySqlConnection con)
    {
        con = new MySqlConnection(str);
        //if (!(con.State == ConnectionState.Open))
        return con;
    }
    /// <summary>
    /// Clears this instance.关闭数据库连接的方法
    /// </summary>
    public static void Clear(MySqlConnection con)
    {
        if (con.State == ConnectionState.Open)
            con.Close();
    }
    /// <summary>
    /// Execs the specified STRSQL.执行SQL语句，不需要返回结果的方法
    /// </summary>
    /// <param name="strsql">The STRSQL.</param>
    public static void Exec(string strSql, MySqlConnection con)
    {
        try
        {
            cmd.Connection = con;
            cmd.CommandText = strSql;
            int count = cmd.ExecuteNonQuery();
        }
        catch (MySqlException o)
        {
            MessageBox.Show(o.Message.ToString(), "数据库错误警告");
        }
        finally
        {
            Clear(con);
        }
    }

    /// <summary>
    /// Execute the SQL and get count.执行sql语句并返回影响的行数
    /// </summary>
    /// <param name="strSql">The STR SQL.</param>
    /// <returns></returns>
    public static int ExecAndGetCount(string strSql, MySqlConnection con)
    {
        int count = 0;
        try
        {
            cmd.Connection = con;
            cmd.CommandText = strSql;
            string result = cmd.ExecuteScalar().ToString();
            count = Convert.ToInt32(result);
        }
        catch (MySqlException o)
        {
            MessageBox.Show(o.Message.ToString(), "数据库错误警告");
        }
        finally
        {
            Clear(con);
        }
        return count;
    }
    /// <summary>
    /// Execute the SQL and get DataReader.执行sql语句并返回DateReader对象
    /// </summary>
    /// <param name="strSql">The STR SQL.</param>
    /// <returns></returns>
    public static MySqlDataReader ExecAndGetOdr(string strSql, MySqlConnection con)
    {
        MySqlDataReader Odr = null;
        try
        {
            cmd.Connection = con;
            cmd.CommandText = strSql;
            Odr = cmd.ExecuteReader();
        }
        catch (MySqlException o)
        {
            MessageBox.Show(o.Message.ToString(), "数据库错误警告");
        }
        return Odr;
    }
    /// <summary>
    /// Execute the SQL and get DataSet.执行Sql语句返回DataSet对象
    /// </summary>
    /// <param name="StrSql">The STR SQL.</param>
    /// <param name="TableName">Name of the table.</param>
    /// <returns></returns>
    public static DataSet ExecAndGetDs(string StrSql, string TableName, MySqlConnection con)
    {
        DataSet ds = new DataSet();
        try
        {
            //string strcmd = "SELECT GUNARK_TYPE,GUNARK_NAME,GUNARK_ID from gunark_gunark;";
            MySqlCommand cmd = new MySqlCommand(StrSql, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            // DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集
            MySqlDataAdapter Oda = new MySqlDataAdapter(StrSql, con);

            Oda.Fill(ds, TableName);
        }
        catch (MySqlException o)
        {
            MessageBox.Show(o.Message.ToString(), "数据库错误警告");
        }
        finally
        {
            Clear(con);
        }
        return ds;
    }
    /// <summary>
    /// Execute the SQL in transation.应用事务机制，批处理sql语句
    /// </summary>
    /// <param name="strSql">The STR SQL.</param>
    /// <returns></returns>
    public static bool ExecInTransation(string[] strSql, MySqlConnection con)
    {
        bool flag = false;
        cmd.Connection = con;
        MySqlTransaction myTransation;
        myTransation = con.BeginTransaction();
        try
        {
            for (int i = 0; i < strSql.Length; i++)
            {
                cmd.Transaction = myTransation;
                cmd.CommandText = strSql[i];
                cmd.ExecuteReader();
            }
            myTransation.Commit();
            flag = true;
        }
        catch (Exception e)
        {
            myTransation.Rollback();
            flag = false;
            MessageBox.Show(e.Message.ToString(), "数据库错误警告");
        }
        finally
        {
            Clear(con);
        }
        return flag;
    }

}

