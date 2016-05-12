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
    public partial class Task_Detail : Form
    {

        DataSet ds;
        List<string> result = new List<string>();
        public Task_Detail()
        {
            InitializeComponent();
        }

        private void Cooldetail_Load(object sender, EventArgs e)
        {

        }

        private void Taskdetail_Load(object sender, EventArgs e)
        {
            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            //string strcmd = "SELECT * FROM gunark_task_info_detail WHERE TASK_ID = '" + Task_query.task_query + "'";
            string strcmd = "SELECT gi.GUN_NUMBER,gi.GUN_TYPE, gu.USER_REALNAME, gpi.GUN_POSITION_NUMBER, gmi.MAGAZINE_NUMBER, gtid.BULLET_TYPE, gtid.DEPLETION_BULLET_QTY, gtid.APPLY_BULLET_QTY, gtid.RETURN_GUNBULLET_TIME, gtid.TAKE_GUNBULLET_TIME, gtid.RETURN_GUNBULLET_USER, TAKE_GUNBULLET_USER FROM gun_info gi left join gunark_task_info_detail gtid on gtid.GUN_INFO_ID = gi.GUN_INFO_ID left join gunark_position_info gpi on gtid.GUN_POSITION_INFO_ID = gpi.GUN_POSITION_INFO_ID left join gunark_magazine_info gmi on gtid.MAGAZINE_INFO_ID = gmi.MAGAZINE_INFO_ID left join gunark_user gu on gu.USER_POLICENUMB = gtid.GUN_DUTY_USER WHERE TASK_ID = '" + Task_query.task_query + "'";
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集
            con.Close();//关闭连接

            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                result.Add(ds.Tables[0].Rows[i][0].ToString());
            }

            comboBox1.DataSource = result;

            if (ds.Tables[0].Rows.Count > 1)
            {
                label2.Text = "*请根据枪支编号查询具体任务详情！";
            }
            else {
                label2.Text = "";
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(CellValue);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }
        //一条任务包含多个枪时，通过枪支编号显示任务详情
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pos = comboBox1.SelectedIndex;
            label4.Text = ds.Tables[0].Rows[pos][1].ToString();
            label6.Text = ds.Tables[0].Rows[pos][2].ToString();
            label8.Text = ds.Tables[0].Rows[pos][3].ToString();
            label10.Text = ds.Tables[0].Rows[pos][4].ToString();
            label12.Text = ds.Tables[0].Rows[pos][5].ToString();
            label14.Text = ds.Tables[0].Rows[pos][6].ToString();
            label16.Text = ds.Tables[0].Rows[pos][7].ToString();
            label18.Text = ds.Tables[0].Rows[pos][8].ToString();
            label20.Text = ds.Tables[0].Rows[pos][9].ToString();
            label22.Text = ds.Tables[0].Rows[pos][10].ToString();
            label24.Text = ds.Tables[0].Rows[pos][11].ToString();
        }


    }
}
