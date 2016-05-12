using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
namespace Cool
{
    public partial class Login : Form
    {
        public static string user = "";
        public static string user_privieges = "";
        string[] source = null;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            label2.Text = "枪弹库版本号：" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            con.Open();
            string sql_3 = "select ID ,USER_NAME from gunark_login_user";
            DataSet ds = DatabaseManager.ExecAndGetDs(sql_3,"gunark_login_user",con);
            DatabaseManager.Clear(con);

            source = new string[ds.Tables[0].Rows.Count + 1];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                source[i] = ds.Tables[0].Rows[i][1].ToString();
            }
            comboBox1.DataSource = source;
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || textBox2.Text == "")
                this.label1.Text = "用户名或密码不能为空!";
            else
            {
                string name = comboBox1.Text.Trim();
                string password = textBox2.Text.Trim();

                //Md5加密
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                string psw = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(password))).ToLower();
                psw = psw.Replace("-", "");

                MySqlConnection con = null;
                con = DatabaseManager.GetCon(con);
                con.Open();
                string sql = "select USER_PRIVIEGES FROM gunark_user where USER_NAME = '" + name + "' and USER_PWD = '" + psw + "'";
                DataSet ds = DatabaseManager.ExecAndGetDs(sql, "gunark_user",con);
                DatabaseManager.Clear(con);

                if (ds.Tables[0].Rows.Count != 0)
                {
                    frmMain.IsLogin = true;
                    user = comboBox1.Text;
                    user_privieges = ds.Tables[0].Rows[0][0].ToString();
                    this.Close();
                    if (this.checkBox1.Checked == true)
                    {
                        MySqlConnection con1 = null;
                        con1 = DatabaseManager.GetCon(con1);
                        con1.Open();
                        string sql_2 = "insert ignore into gunark_login_user (USER_NAME) values ('" + this.comboBox1.Text + "')";
                        DatabaseManager.Exec(sql_2,con1);
                        DatabaseManager.Clear(con1);
                    }
                }
                else
                {
                    this.label1.Text = "用户名或密码错误！";
                }
            }
        }


        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Equals("******"))
            {
                this.textBox2.Text = "";
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("请输入用户名"))
            {
                comboBox1.Text = "";
            }
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (comboBox1.Text.Equals("请输入用户名"))
            {
                comboBox1.Text = "";
            }
        }
        // 回车键功能实现：
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(button1, null);
            }
        } 
       
    }
}
