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

namespace Cool
{
    public partial class Cooldetail : Form
    {
        public Cooldetail()
        {
            InitializeComponent();
        }

        private void Cooldetail_Load(object sender, EventArgs e)
        {

            string str7 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con7 = new MySqlConnection(str7);//实例化链接
            con7.Open();//开启连接
            string strcmd7 = "SELECT GUNARK_NAME,GUNARK_TYPE,cast(GUNARK_STATUS as CHAR),GUNARK_IP FROM gunark_gunark WHERE GUNARK_ID = '" + Query03.gunark_query + "'";
            MySqlCommand cmd7 = new MySqlCommand(strcmd7, con7);
            MySqlDataAdapter ada7 = new MySqlDataAdapter(cmd7);
            DataSet ds7 = new DataSet();
            ada7.Fill(ds7);//查询结果填充数据集
            con7.Close();

            string gunark_name = ds7.Tables[0].Rows[0][0].ToString();//枪弹柜名称
            string gunark_model = ds7.Tables[0].Rows[0][1].ToString();//枪弹柜类型
            string gunark_status = ds7.Tables[0].Rows[0][2].ToString();//枪弹柜状态
            string gunark_ip = ds7.Tables[0].Rows[0][3].ToString();//枪弹柜IP
            if (gunark_status.Equals("1"))
                gunark_status = "枪柜在用";
            else if (gunark_status.Equals("2"))
                gunark_status = "枪柜已注销";
            label3.Text = gunark_name;
            label5.Text = gunark_status;
            label7.Text = gunark_model;
            label9.Text = gunark_ip;

            //显示所选库的在位枪支
            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = "SELECT GUN_POSITION_NUMBER,GUN_POSITION_STATUS,gpi.GUN_TYPE,gi.GUN_NUMBER FROM gunark_position_info gpi LEFT JOIN gun_info gi on gi.GUN_INFO_ID = gpi.GUN_INFO_ID WHERE gpi.GUNARK_ID = '" + Query03.gunark_query + "'";
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][2].Equals("1"))
                {
                    ds.Tables[0].Rows[i][2] = "54式手枪";
                }
                else if (ds.Tables[0].Rows[i][2].Equals("2"))
                {
                    ds.Tables[0].Rows[i][2] = "64式手枪";
                }
                else if (ds.Tables[0].Rows[i][2].Equals("3"))
                {
                    ds.Tables[0].Rows[i][2] = "77式手枪";
                }
                else if (ds.Tables[0].Rows[i][2].Equals("4"))
                {
                    ds.Tables[0].Rows[i][2] = "79式冲锋枪";

                }
                else if (ds.Tables[0].Rows[i][2].Equals("5"))
                {
                    ds.Tables[0].Rows[i][2] = "97式防暴枪";
                }
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][1].Equals("3"))
                {
                    ds.Tables[0].Rows[i][1] = "置枪";
                }
                else
                {
                    ds.Tables[0].Rows[i][1] = "未置枪";
                }
                

            }

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].HeaderText = "枪位号";
            dataGridView1.Columns[1].HeaderText = "枪位状态";
            dataGridView1.Columns[2].HeaderText = "枪支类型";
            dataGridView1.Columns[3].HeaderText = "枪支编号";
            con.Close();//关闭连接
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
