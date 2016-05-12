using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Cool
{
    public partial class Form1 : Form
    {
        public static string user = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
                MessageBox.Show("用户名或密码不能为空");
            else
            {
                string name = textBox1.Text.Trim();
                string password = textBox2.Text.Trim();

                string str7 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                MySqlConnection con7 = new MySqlConnection(str7);//实例化链接
                con7.Open();//开启连接
                string strcmd7 = "select * FROM gunark_user where USER_NAME = '" + name + "' and USER_PWD = '" + password + "'";
                MySqlCommand cmd7 = new MySqlCommand(strcmd7, con7);

                MySqlDataReader read = cmd7.ExecuteReader();
                read.Read();

                //MySqlDataAdapter ada7 = new MySqlDataAdapter(cmd7);
                //DataSet ds7 = new DataSet();
                //ada7.Fill(ds7);//查询结果填充数据集
                if (read.HasRows)
                {
                    con7.Close();
                    user = textBox1.Text;
                    Homepage hp = new Homepage();
                    hp.Show();
                    this.Visible = false;
                }
                else
                {
                    con7.Close();
                    MessageBox.Show("用户名或密码错误");
                }

                
            }
        }
    }
}
