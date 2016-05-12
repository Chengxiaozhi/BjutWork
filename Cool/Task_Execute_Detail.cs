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
    public partial class Task_Execute_Detail : Form
    {
        public static string[] gunid = new string[100];//获取gunid
        public static string[] gunpositionid = new string[100];//获取枪位id
        public static byte[] gunpositionnum = new byte[100];//获取枪位号
        public static int count;

        public static byte[] magazinenum = new byte[100];//获取弹仓号
        public static string[] mgz_id1 = new string[100];
        public static int[] qty1 = new int[100];

        public static string gunark_name;
        public Task_Execute_Detail()
        {
            InitializeComponent();

            Display();
        }

        private void Display()
        {
            //显示所选任务detail
            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = "select gti.TASK_ID, gg.GUNARK_NAME, gpi.GUN_POSITION_NUMBER, gpi.GUN_TYPE, gtid.GUN_INFO_ID, gtid.GUN_POSITION_INFO_ID, gti.TASK_PLAN_BEGINTIME, gu.USER_REALNAME, cast(gti.TASK_BIGTYPE as CHAR), gu.USER_REALNAME, gmi.MAGAZINE_NUMBER,gtid.BULLET_TYPE, cast(gtid.APPLY_BULLET_QTY as CHAR),gtid.MAGAZINE_INFO_ID from gunark_task_info gti left join gunark_task_info_detail gtid on gtid.TASK_ID = gti.TASK_ID left join gunark_position_info gpi on gpi.GUN_POSITION_INFO_ID = gtid.GUN_POSITION_INFO_ID left join gunark_gunark gg on gg.GUNARK_ID = gtid.GUNARK_ID left join gunark_magazine_info gmi on gmi.MAGAZINE_INFO_ID = gtid.MAGAZINE_INFO_ID left join gunark_user gu on gtid.GUN_DUTY_USER=gu.USER_POLICENUMB where gtid.TASK_ID ='" + frmMain.taskid + "'";

            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集

            gunark_name = ds.Tables[0].Rows[0][1].ToString();
            //label1.Text = ds.Tables[0].Rows.Count.ToString();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][11].Equals("1"))
                {
                    ds.Tables[0].Rows[i][11] = "54式手枪弹";
                }
                else if (ds.Tables[0].Rows[i][11].Equals("2"))
                {
                    ds.Tables[0].Rows[i][11] = "64式手枪弹";
                }
                else if (ds.Tables[0].Rows[i][11].Equals("3"))
                {
                    ds.Tables[0].Rows[i][11] = "77式手枪弹";
                }
                else if (ds.Tables[0].Rows[i][11].Equals("4"))
                {
                    ds.Tables[0].Rows[i][11] = "79式冲锋枪弹";
                }
                else if (ds.Tables[0].Rows[i][11].Equals("5"))
                {
                    ds.Tables[0].Rows[i][11] = "97式防暴枪弹";
                }
                else
                {
                    ds.Tables[0].Rows[i][11] = "";
                }
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][3].Equals("1"))
                {
                    ds.Tables[0].Rows[i][3] = "54式手枪";
                }
                else if (ds.Tables[0].Rows[i][3].Equals("2"))
                {
                    ds.Tables[0].Rows[i][3] = "64式手枪";
                }
                else if (ds.Tables[0].Rows[i][3].Equals("3"))
                {
                    ds.Tables[0].Rows[i][3] = "77式手枪";
                }
                else if (ds.Tables[0].Rows[i][3].Equals("4"))
                {
                    ds.Tables[0].Rows[i][3] = "79式冲锋枪";

                }
                else if (ds.Tables[0].Rows[i][3].Equals("5"))
                {
                    ds.Tables[0].Rows[i][3] = "97式防暴枪";
                }
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][7].Equals("1"))
                {
                    ds.Tables[0].Rows[i][7] = "申请弹仓";
                }
                else if (ds.Tables[0].Rows[i][7].Equals("2"))
                {
                    ds.Tables[0].Rows[i][7] = "枪弹入柜";
                }
                else if (ds.Tables[0].Rows[i][7].Equals("3"))
                {
                    ds.Tables[0].Rows[i][7] = "申请枪弹";
                }
                else if (ds.Tables[0].Rows[i][7].Equals("4"))
                {
                    ds.Tables[0].Rows[i][7] = "枪支封存";

                }
                else if (ds.Tables[0].Rows[i][7].Equals("5"))
                {
                    ds.Tables[0].Rows[i][7] = "枪支报废";
                }
            }

            dataGridView1.DataSource = ds.Tables[0];


            dataGridView1.Columns[0].Visible = false;
            //dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[1].HeaderText = "枪柜名";
            dataGridView1.Columns[2].HeaderText = "枪位号";
            dataGridView1.Columns[3].HeaderText = "枪支类型";
            //dataGridView1.Columns[4].HeaderText = "弹仓号";
            //dataGridView1.Columns[5].HeaderText = "子弹类型";
            dataGridView1.Columns[8].HeaderText = "任务类型";
            dataGridView1.Columns[6].HeaderText = "计划领枪开始时间";
            dataGridView1.Columns[7].HeaderText = "用枪人";
            dataGridView1.Columns[10].HeaderText = "弹仓号";
            dataGridView1.Columns[11].HeaderText = "子弹类型";
            dataGridView1.Columns[12].HeaderText = "子弹数量";
            //this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //a = dataGridView1.CurrentRow("TASK_ID").ToString;
            con.Close();//关闭连接
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            count = dataGridView1.Rows.Count;

            if (count == 0)
            {

            }
            else
            {
                //获取gunid

                for (int i = 0; i < count; i++)
                {
                    string rows = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    gunid[i] = rows;
                }
                //获取gunpositionid

                for (int i = 0; i < count; i++)
                {

                    string rows = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    gunpositionid[i] = rows;
                }
                //获取gunpositionnum
                for (int ii = 0; ii < count; ii++)
                {
                    try
                    {
                        string gpn;
                        gpn = dataGridView1.Rows[ii].Cells[2].Value.ToString();

                        byte rows = (byte)int.Parse(gpn);
                        gunpositionnum[ii] = rows;
                    }
                    catch (Exception eee)
                    {
                        continue;
                    }
                }
                //获取弹仓id
                for (int i = 0; i < count; i++)
                {
                    string rows = dataGridView1.Rows[i].Cells[13].Value.ToString();
                    mgz_id1[i] = rows;
                }

                //获取子弹数量
                for (int i = 0; i < count; i++)
                {
                    string rows = dataGridView1.Rows[i].Cells[12].Value.ToString();
                    qty1[i] = int.Parse(rows);
                }

                //获取magazinenum
                for (int iii = 0; iii < dataGridView1.Rows.Count; iii++)
                {
                    try
                    {
                        string gpn;
                        gpn = dataGridView1.Rows[iii].Cells[10].Value.ToString();
                        byte rows = (byte)int.Parse(gpn);
                        magazinenum[iii] = rows;
                    }
                    catch (Exception ee)
                    {
                        continue;
                    }
                }

            }
            Finger_Check fc = new Finger_Check();
            fc.ShowDialog();
            this.Close();
            Display();
 
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Task_Execute_Detail_Load(object sender, EventArgs e)
        {

        }
    }
}
