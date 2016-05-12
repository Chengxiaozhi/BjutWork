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
    public partial class Task_execute : Form
    {
        public static string taskid;
        string sss;

        public Task_execute()
        {
            InitializeComponent();
        }

        private void Task_execute_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //根据计划开始时间判断领取任务是否可以执行；
            DateTime now = DateTime.Now;
            //label1.Text = now.ToString();
            string m;//月
            string d;//日
            string h;
            //取月份
            if (now.ToString().Substring(6, 1).Equals("-"))
            {
                m = now.ToString().Substring(5, 1);

                if (now.ToString().Substring(8, 1).Equals(" "))
                    d = now.ToString().Substring(7, 1);
                else
                    d = now.ToString().Substring(7, 2);
            }
            else
            {
                m = now.ToString().Substring(5, 2);

                if (now.ToString().Substring(9, 1).Equals(" "))
                    d = now.ToString().Substring(8, 1);
                else
                    d = now.ToString().Substring(8, 2);
            }

            h = now.Hour.ToString();
            if (int.Parse(m) < 10 & int.Parse(d) < 10 & int.Parse(h) < 10)
            {
                sss = now.ToString().Substring(0, 5) + "0" + m + "-0" + d + " 0" + h + now.ToString().Substring(10, 6);
                //MessageBox.Show(sss);
            }
            else if (int.Parse(m) < 10 & int.Parse(d) < 10 & int.Parse(h) >= 10)
            {
                sss = now.ToString().Substring(0, 5) + "0" + m + "-0" + d + now.ToString().Substring(8, 9);
                //MessageBox.Show(sss);
            }
            else if (int.Parse(m) < 10 & int.Parse(d) >= 10 & int.Parse(h) < 10)
            {
                sss = now.ToString().Substring(0, 5) + "0" + m + "-" + d + " 0" + h + now.ToString().Substring(11, 6);
            }
            else if (int.Parse(m) < 10 & int.Parse(d) >= 10 & int.Parse(h) >= 10)
            {
                sss = now.ToString().Substring(0, 5) + "0" + m + "-" + d + " " + h + now.ToString().Substring(12, 6);
            }
            else if (int.Parse(m) >= 10 & int.Parse(d) < 10 & int.Parse(h) < 10)
            {
                sss = now.ToString().Substring(0, 8) + "0" + d + " 0" + h + now.ToString().Substring(11, 6);
            }
            else if (int.Parse(m) >= 10 & int.Parse(d) < 10 & int.Parse(h) >= 10)
            {
                sss = now.ToString().Substring(0, 8) + "0" + d + now.ToString().Substring(9, 9);
            }
            else if (int.Parse(m) >= 10 & int.Parse(d) >= 10 & int.Parse(h) < 10)
            {
                sss = now.ToString().Substring(0, 11) + "0" + now.ToString().Substring(11, 7);

            }
            else if (int.Parse(m) >= 10 & int.Parse(d) >= 10 & int.Parse(h) >= 10)
            {
                sss = now.ToString();
            }

            //显示任务
            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = "select TASK_ID,TASK_STATUS,gu.USER_REALNAME,cast(TASK_BIGTYPE as CHAR),TASK_APPLY_USERID,TASK_PLAN_BEGINTIME,TASK_PLAN_FINISHTIME FROM gunark_task_info gti LEFT JOIN gunark_user gu on gti.TASK_APPLY_USERID=gu.USER_POLICENUMB where TASK_BIGTYPE='3'and TASK_STATUS='3' and ('" + sss.ToString() + "' between TASK_PLAN_BEGINTIME and TASK_PLAN_FINISHTIME)";
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][1].Equals("1"))
                {
                    ds.Tables[0].Rows[i][1] = "申请";
                }
                else if (ds.Tables[0].Rows[i][1].Equals("2"))
                {
                    ds.Tables[0].Rows[i][1] = "审核";
                }
                else if (ds.Tables[0].Rows[i][1].Equals("3"))
                {
                    ds.Tables[0].Rows[i][1] = "审批完成";
                }
                else if (ds.Tables[0].Rows[i][1].Equals("4"))
                {
                    ds.Tables[0].Rows[i][1] = "拒绝";

                }
                else if (ds.Tables[0].Rows[i][1].Equals("5"))
                {
                    ds.Tables[0].Rows[i][1] = "进行中";
                }
                else
                {
                    ds.Tables[0].Rows[i][1] = "完成";
                }
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][3].Equals("1"))
                {
                    ds.Tables[0].Rows[i][3] = "申请弹仓";
                }
                else if (ds.Tables[0].Rows[i][3].Equals("2"))
                {
                    ds.Tables[0].Rows[i][3] = "枪弹入柜";
                }
                else if (ds.Tables[0].Rows[i][3].Equals("3"))
                {
                    ds.Tables[0].Rows[i][3] = "申请枪弹";
                }
                else if (ds.Tables[0].Rows[i][3].Equals("4"))
                {
                    ds.Tables[0].Rows[i][3] = "枪支封存";

                }
                else if (ds.Tables[0].Rows[i][3].Equals("5"))
                {
                    ds.Tables[0].Rows[i][3] = "枪支报废";

                }
                else if (ds.Tables[0].Rows[i][3].Equals("6"))
                {
                    ds.Tables[0].Rows[i][3] = "枪支保养";

                }
                else if (ds.Tables[0].Rows[i][3].Equals("7"))
                {
                    ds.Tables[0].Rows[i][3] = "枪弹调拨";
                }
            }

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[2].HeaderText = "任务状态";
            dataGridView1.Columns[3].HeaderText = "申请人";
            dataGridView1.Columns[4].HeaderText = "任务类型";
            dataGridView1.Columns[6].HeaderText = "计划领枪开始时间";
            dataGridView1.Columns[7].HeaderText = "计划领枪结束时间";
            con.Close();//关闭连接
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //根据计划开始时间判断领取任务是否可以执行；
            DateTime now = DateTime.Now;
            //label1.Text = now.ToString();
            string m;//月
            string d;//日
            string h;
            //取月份
            if (now.ToString().Substring(6, 1).Equals("-"))
            {
                m = now.ToString().Substring(5, 1);

                if (now.ToString().Substring(8, 1).Equals(" "))
                    d = now.ToString().Substring(7, 1);
                else
                    d = now.ToString().Substring(7, 2);
            }
            else
            {
                m = now.ToString().Substring(5, 2);

                if (now.ToString().Substring(9, 1).Equals(" "))
                    d = now.ToString().Substring(8, 1);
                else
                    d = now.ToString().Substring(8, 2);
            }

            h = now.Hour.ToString();
            if (int.Parse(m) < 10 & int.Parse(d) < 10 & int.Parse(h) < 10)
            {
                sss = now.ToString().Substring(0, 5) + "0" + m + "-0" + d + " 0" + h + now.ToString().Substring(10, 6);
                //MessageBox.Show(sss);
            }
            else if (int.Parse(m) < 10 & int.Parse(d) < 10 & int.Parse(h) >= 10)
            {
                sss = now.ToString().Substring(0, 5) + "0" + m + "-0" + d + now.ToString().Substring(8, 9);
                //MessageBox.Show(sss);
            }
            else if (int.Parse(m) < 10 & int.Parse(d) >= 10 & int.Parse(h) < 10)
            {
                sss = now.ToString().Substring(0, 5) + "0" + m + "-" + d + " 0" + h + now.ToString().Substring(11, 6);
            }
            else if (int.Parse(m) < 10 & int.Parse(d) >= 10 & int.Parse(h) >= 10)
            {
                sss = now.ToString().Substring(0, 5) + "0" + m + "-" + d + " " + h + now.ToString().Substring(12, 6);
            }
            else if (int.Parse(m) >= 10 & int.Parse(d) < 10 & int.Parse(h) < 10)
            {
                sss = now.ToString().Substring(0, 8) + "0" + d + " 0" + h + now.ToString().Substring(11, 6);
            }
            else if (int.Parse(m) >= 10 & int.Parse(d) < 10 & int.Parse(h) >= 10)
            {
                sss = now.ToString().Substring(0, 8) + "0" + d + now.ToString().Substring(9, 9);
            }
            else if (int.Parse(m) >= 10 & int.Parse(d) >= 10 & int.Parse(h) < 10)
            {
                sss = now.ToString().Substring(0, 11) + "0" + now.ToString().Substring(11, 7);

            }
            else if (int.Parse(m) >= 10 & int.Parse(d) >= 10 & int.Parse(h) >= 10)
            {
                sss = now.ToString();
            }

            //显示任务
            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = "select TASK_ID,TASK_STATUS,gu.USER_REALNAME,cast(TASK_BIGTYPE as CHAR),TASK_APPLY_USERID,TASK_PLAN_BEGINTIME,TASK_PLAN_FINISHTIME FROM gunark_task_info gti LEFT JOIN gunark_user gu on gti.TASK_APPLY_USERID=gu.USER_POLICENUMB where (TASK_BIGTYPE='4' or TASK_BIGTYPE='5' or TASK_BIGTYPE='6' or TASK_BIGTYPE='7') and TASK_STATUS='3' and ('" + sss.ToString() + "' between TASK_PLAN_BEGINTIME and TASK_PLAN_FINISHTIME)";
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][1].Equals("1"))
                {
                    ds.Tables[0].Rows[i][1] = "申请";
                }
                else if (ds.Tables[0].Rows[i][1].Equals("2"))
                {
                    ds.Tables[0].Rows[i][1] = "审核";
                }
                else if (ds.Tables[0].Rows[i][1].Equals("3"))
                {
                    ds.Tables[0].Rows[i][1] = "审批完成";
                }
                else if (ds.Tables[0].Rows[i][1].Equals("4"))
                {
                    ds.Tables[0].Rows[i][1] = "拒绝";

                }
                else if (ds.Tables[0].Rows[i][1].Equals("5"))
                {
                    ds.Tables[0].Rows[i][1] = "进行中";
                }
                else
                {
                    ds.Tables[0].Rows[i][1] = "完成";
                }
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][3].Equals("1"))
                {
                    ds.Tables[0].Rows[i][3] = "申请弹仓";
                }
                else if (ds.Tables[0].Rows[i][3].Equals("2"))
                {
                    ds.Tables[0].Rows[i][3] = "枪弹入柜";
                }
                else if (ds.Tables[0].Rows[i][3].Equals("3"))
                {
                    ds.Tables[0].Rows[i][3] = "申请枪弹";
                }
                else if (ds.Tables[0].Rows[i][3].Equals("4"))
                {
                    ds.Tables[0].Rows[i][3] = "枪支封存";

                }
                else if (ds.Tables[0].Rows[i][3].Equals("5"))
                {
                    ds.Tables[0].Rows[i][3] = "枪支报废";

                }
                else if (ds.Tables[0].Rows[i][3].Equals("6"))
                {
                    ds.Tables[0].Rows[i][3] = "枪支保养";

                }
                else if (ds.Tables[0].Rows[i][3].Equals("7"))
                {
                    ds.Tables[0].Rows[i][3] = "枪弹调拨";
                }
            }

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[2].HeaderText = "任务状态";
            dataGridView1.Columns[3].HeaderText = "申请人";
            dataGridView1.Columns[4].HeaderText = "任务类型";
            dataGridView1.Columns[6].HeaderText = "计划领枪开始时间";
            dataGridView1.Columns[7].HeaderText = "计划领枪结束时间";
            con.Close();//关闭连接
        }

        //整行选中
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int index = 0;
            if (e.RowIndex == -1)
            {
                index = 0;
            }
            else
            {
                index = e.RowIndex;
            }
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewCheckBoxCell check = (DataGridViewCheckBoxCell)dataGridView1.Rows[index].Cells[0];
                check.Value = true;
                //MessageBox.Show(e.RowIndex.ToString());
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    check = (DataGridViewCheckBoxCell)row.Cells[0];
                    if (row.Index != e.RowIndex)
                    {
                        check.Value = false;
                    }
                }
            }
        }

    }
}
