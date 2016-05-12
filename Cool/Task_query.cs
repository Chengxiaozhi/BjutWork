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
    public partial class Task_query : Form
    {
        public static string task_query;
        int index = -1;
        public Task_query()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void Task_query_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            this.dataGridView1.DefaultCellStyle.Font = new Font("宋体", 15);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("宋体",15);
            //dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            ////dt1.CustomFormat = "dd'/'MM'/'yyyy hh':'mm tt";
            //dateTimePicker2.CustomFormat = "yyyy'年'MM'月'";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string timeChange(string now)
        {
            string sss = "";
            string m;//月
            string d;//日
           

            //取月、日
            if (now.Substring(6, 1).Equals("/"))
            {
                m = now.ToString().Substring(5, 1);
                if (now.Length == 8)
                    d = now.ToString().Substring(7, 1);
                else
                    d = now.ToString().Substring(7, 2);
            }
            else
            {
                m = now.ToString().Substring(5, 2);
                if (now.Length == 9)
                    d = now.ToString().Substring(8, 1);
                else
                {
                    d = now.ToString().Substring(8, 2);
                }
            }

            if (int.Parse(m) < 10 & int.Parse(d) < 10)
            {
                sss = now.ToString().Substring(0, 4) + "-0" + m + "-0" + d;
                //MessageBox.Show(sss);
            }

            else if (int.Parse(m) < 10 & int.Parse(d) >= 10)
            {
                sss = now.ToString().Substring(0, 4) + "-0" + m + "-" + d;
            }

            else if (int.Parse(m) >= 10 & int.Parse(d) < 10)
            {
                sss = now.ToString().Substring(0, 4) + "-" + m + "-0" + d;
            }



            else if (int.Parse(m) >= 10 & int.Parse(d) >= 10)
            {
                sss = now.ToString().Substring(0, 4) + "-" + m + "-" + d; ;
            }

            return sss;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string a = timeChange(dateTimePicker1.Value.ToShortDateString());
            string b = timeChange(dateTimePicker2.Value.ToShortDateString());
            string aaa = "";
            //如果选中
            if (comboBox1.SelectedIndex > -1)
            {
                aaa = comboBox1.SelectedItem.ToString();//方法2
            }
            //MessageBox.Show(aaa);

            switch (aaa)
            {
                case "枪弹入库":
                    string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con = new MySqlConnection(str);//实例化链接
                    con.Open();//开启连接
                    string strcmd = "SELECT GUNARK_ID,TASK_STATUS,cast(TASK_BIGTYPE as CHAR),TASK_PLAN_BEGINTIME,TASK_PLAN_FINISHTIME,TASK_ID FROM gunark_task_info where TASK_BIGTYPE ='2' and (TASK_PLAN_BEGINTIME between '" + a + " 00:00:00' and '" + b + " 23:59:59')";

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
                        else if (ds.Tables[0].Rows[i][1].Equals("6"))
                        {
                            ds.Tables[0].Rows[i][1] = "完成";
                        }
                        else if (ds.Tables[0].Rows[i][1].Equals("10"))
                        {
                            ds.Tables[0].Rows[i][1] = "未正常入柜";
                        }
                    }

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i][2].Equals("1"))
                        {
                            ds.Tables[0].Rows[i][2] = "申请弹仓";
                        }
                        else if (ds.Tables[0].Rows[i][2].Equals("2"))
                        {
                            ds.Tables[0].Rows[i][2] = "枪弹入柜";
                        }
                        else if (ds.Tables[0].Rows[i][2].Equals("3"))
                        {
                            ds.Tables[0].Rows[i][2] = "申请枪弹";
                        }
                        else if (ds.Tables[0].Rows[i][2].Equals("4"))
                        {
                            ds.Tables[0].Rows[i][2] = "枪支封存";

                        }
                        else if (ds.Tables[0].Rows[i][2].Equals("5"))
                        {
                            ds.Tables[0].Rows[i][2] = "枪支报废";

                        }
                        else if (ds.Tables[0].Rows[i][2].Equals("6"))
                        {
                            ds.Tables[0].Rows[i][2] = "枪支保养";

                        }
                        else if (ds.Tables[0].Rows[i][2].Equals("7"))
                        {
                            ds.Tables[0].Rows[i][2] = "枪弹调拨";
                        }
                        else if (ds.Tables[0].Rows[i][2].Equals("9"))
                        {
                            ds.Tables[0].Rows[i][2] = "枪弹检查";
                        }
                    }

                    dataGridView1.DataSource = ds.Tables[0];

                    dataGridView1.Columns[4].HeaderText = "任务结束时间";

                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[0].HeaderText = "枪柜号";
                    dataGridView1.Columns[1].HeaderText = "任务状态";
                    dataGridView1.Columns[2].HeaderText = "任务类型";
                    dataGridView1.Columns[3].HeaderText = "任务开始时间";
                    con.Close();//关闭连接

                    break;
                case "领取枪弹":

                    string str1 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con1 = new MySqlConnection(str1);//实例化链接
                    con1.Open();//开启连接
                    string strcmd1 = "SELECT GUNARK_ID,TASK_STATUS,cast(TASK_BIGTYPE as CHAR),TASK_PLAN_BEGINTIME,TASK_PLAN_FINISHTIME,TASK_ID FROM gunark_task_info where TASK_BIGTYPE ='3' and (TASK_PLAN_BEGINTIME between '" + a + " 00:00:00' and '" + b + " 23:59:59')";


                    MySqlCommand cmd1 = new MySqlCommand(strcmd1, con1);
                    MySqlDataAdapter ada1 = new MySqlDataAdapter(cmd1);

                    DataSet ds1 = new DataSet();
                    ada1.Fill(ds1);//查询结果填充数据集

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        if (ds1.Tables[0].Rows[i][1].Equals("1"))
                        {
                            ds1.Tables[0].Rows[i][1] = "申请";
                        }
                        else if (ds1.Tables[0].Rows[i][1].Equals("2"))
                        {
                            ds1.Tables[0].Rows[i][1] = "审核";
                        }
                        else if (ds1.Tables[0].Rows[i][1].Equals("3"))
                        {
                            ds1.Tables[0].Rows[i][1] = "审批完成";
                        }
                        else if (ds1.Tables[0].Rows[i][1].Equals("4"))
                        {
                            ds1.Tables[0].Rows[i][1] = "拒绝";

                        }
                        else if (ds1.Tables[0].Rows[i][1].Equals("5"))
                        {
                            ds1.Tables[0].Rows[i][1] = "进行中";
                        }
                        else
                        {
                            ds1.Tables[0].Rows[i][1] = "完成";
                        }
                    }

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        if (ds1.Tables[0].Rows[i][2].Equals("1"))
                        {
                            ds1.Tables[0].Rows[i][2] = "申请弹仓";
                        }
                        else if (ds1.Tables[0].Rows[i][2].Equals("2"))
                        {
                            ds1.Tables[0].Rows[i][2] = "枪弹入柜";
                        }
                        else if (ds1.Tables[0].Rows[i][2].Equals("3"))
                        {
                            ds1.Tables[0].Rows[i][2] = "申请枪弹";
                        }
                        else if (ds1.Tables[0].Rows[i][2].Equals("4"))
                        {
                            ds1.Tables[0].Rows[i][2] = "枪支封存";

                        }
                        else if (ds1.Tables[0].Rows[i][2].Equals("5"))
                        {
                            ds1.Tables[0].Rows[i][2] = "枪支报废";

                        }
                        else if (ds1.Tables[0].Rows[i][2].Equals("6"))
                        {
                            ds1.Tables[0].Rows[i][2] = "枪支保养";

                        }
                        else if (ds1.Tables[0].Rows[i][2].Equals("7"))
                        {
                            ds1.Tables[0].Rows[i][2] = "枪弹调拨";
                        }
                    }

                    dataGridView1.DataSource = ds1.Tables[0];
                    con1.Close();//关闭连接

                    dataGridView1.Columns[0].HeaderText = "枪柜号";
                    dataGridView1.Columns[1].HeaderText = "任务状态";
                    dataGridView1.Columns[2].HeaderText = "任务类型";
                    dataGridView1.Columns[3].HeaderText = "任务开始时间";
                    dataGridView1.Columns[4].HeaderText = "任务结束时间";

                    dataGridView1.Columns[5].Visible = false;
                    break;
                case "枪弹保养":

                    string str2 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con2 = new MySqlConnection(str2);//实例化链接
                    con2.Open();//开启连接
                    string strcmd2 = "SELECT GUNARK_ID,TASK_STATUS,cast(TASK_BIGTYPE as CHAR),TASK_PLAN_BEGINTIME,TASK_PLAN_FINISHTIME,TASK_ID FROM gunark_task_info where TASK_BIGTYPE ='6' and (TASK_PLAN_BEGINTIME between '" + a + " 00:00:00' and '" + b + " 23:59:59')";



                    MySqlCommand cmd2 = new MySqlCommand(strcmd2, con2);
                    MySqlDataAdapter ada2 = new MySqlDataAdapter(cmd2);

                    DataSet ds2 = new DataSet();
                    ada2.Fill(ds2);//查询结果填充数据集

                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        if (ds2.Tables[0].Rows[i][1].Equals("1"))
                        {
                            ds2.Tables[0].Rows[i][1] = "申请";
                        }
                        else if (ds2.Tables[0].Rows[i][1].Equals("2"))
                        {
                            ds2.Tables[0].Rows[i][1] = "审核";
                        }
                        else if (ds2.Tables[0].Rows[i][1].Equals("3"))
                        {
                            ds2.Tables[0].Rows[i][1] = "审批完成";
                        }
                        else if (ds2.Tables[0].Rows[i][1].Equals("4"))
                        {
                            ds2.Tables[0].Rows[i][1] = "拒绝";

                        }
                        else if (ds2.Tables[0].Rows[i][1].Equals("5"))
                        {
                            ds2.Tables[0].Rows[i][1] = "进行中";
                        }
                        else
                        {
                            ds2.Tables[0].Rows[i][1] = "完成";
                        }
                    }

                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        if (ds2.Tables[0].Rows[i][2].Equals("1"))
                        {
                            ds2.Tables[0].Rows[i][2] = "申请弹仓";
                        }
                        else if (ds2.Tables[0].Rows[i][2].Equals("2"))
                        {
                            ds2.Tables[0].Rows[i][2] = "枪弹入柜";
                        }
                        else if (ds2.Tables[0].Rows[i][2].Equals("3"))
                        {
                            ds2.Tables[0].Rows[i][2] = "申请枪弹";
                        }
                        else if (ds2.Tables[0].Rows[i][2].Equals("4"))
                        {
                            ds2.Tables[0].Rows[i][2] = "枪支封存";

                        }
                        else if (ds2.Tables[0].Rows[i][2].Equals("5"))
                        {
                            ds2.Tables[0].Rows[i][2] = "枪支报废";

                        }
                        else if (ds2.Tables[0].Rows[i][2].Equals("6"))
                        {
                            ds2.Tables[0].Rows[i][2] = "枪支保养";

                        }
                        else if (ds2.Tables[0].Rows[i][2].Equals("7"))
                        {
                            ds2.Tables[0].Rows[i][2] = "枪弹调拨";
                        }
                    }

                    dataGridView1.DataSource = ds2.Tables[0];
                    con2.Close();//关闭连接
                    dataGridView1.Columns[0].HeaderText = "枪柜号";
                    dataGridView1.Columns[1].HeaderText = "任务状态";
                    dataGridView1.Columns[2].HeaderText = "任务类型";
                    dataGridView1.Columns[3].HeaderText = "任务开始时间";
                    dataGridView1.Columns[4].HeaderText = "任务结束时间";

                    dataGridView1.Columns[5].Visible = false;
                    break;
                case "枪弹调拨":

                    string str3 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con3 = new MySqlConnection(str3);//实例化链接
                    con3.Open();//开启连接
                    string strcmd3 = "SELECT GUNARK_ID,TASK_STATUS,cast(TASK_BIGTYPE as CHAR),TASK_PLAN_BEGINTIME,TASK_PLAN_FINISHTIME,TASK_ID FROM gunark_task_info where TASK_BIGTYPE ='7' and (TASK_PLAN_BEGINTIME between '" + a + " 00:00:00' and '" + b + " 23:59:59')";

                    MySqlCommand cmd3 = new MySqlCommand(strcmd3, con3);
                    MySqlDataAdapter ada3 = new MySqlDataAdapter(cmd3);

                    DataSet ds3 = new DataSet();
                    ada3.Fill(ds3);//查询结果填充数据集

                    for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                    {
                        if (ds3.Tables[0].Rows[i][1].Equals("1"))
                        {
                            ds3.Tables[0].Rows[i][1] = "申请";
                        }
                        else if (ds3.Tables[0].Rows[i][1].Equals("2"))
                        {
                            ds3.Tables[0].Rows[i][1] = "审核";
                        }
                        else if (ds3.Tables[0].Rows[i][1].Equals("3"))
                        {
                            ds3.Tables[0].Rows[i][1] = "审批完成";
                        }
                        else if (ds3.Tables[0].Rows[i][1].Equals("4"))
                        {
                            ds3.Tables[0].Rows[i][1] = "拒绝";

                        }
                        else if (ds3.Tables[0].Rows[i][1].Equals("5"))
                        {
                            ds3.Tables[0].Rows[i][1] = "进行中";
                        }
                        else
                        {
                            ds3.Tables[0].Rows[i][1] = "完成";
                        }
                    }

                    for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                    {
                        if (ds3.Tables[0].Rows[i][2].Equals("1"))
                        {
                            ds3.Tables[0].Rows[i][2] = "申请弹仓";
                        }
                        else if (ds3.Tables[0].Rows[i][2].Equals("2"))
                        {
                            ds3.Tables[0].Rows[i][2] = "枪弹入柜";
                        }
                        else if (ds3.Tables[0].Rows[i][2].Equals("3"))
                        {
                            ds3.Tables[0].Rows[i][2] = "申请枪弹";
                        }
                        else if (ds3.Tables[0].Rows[i][2].Equals("4"))
                        {
                            ds3.Tables[0].Rows[i][2] = "枪支封存";

                        }
                        else if (ds3.Tables[0].Rows[i][2].Equals("5"))
                        {
                            ds3.Tables[0].Rows[i][2] = "枪支报废";

                        }
                        else if (ds3.Tables[0].Rows[i][2].Equals("6"))
                        {
                            ds3.Tables[0].Rows[i][2] = "枪支保养";

                        }
                        else if (ds3.Tables[0].Rows[i][2].Equals("7"))
                        {
                            ds3.Tables[0].Rows[i][2] = "枪弹调拨";
                        }
                    }

                    dataGridView1.DataSource = ds3.Tables[0];
                    con3.Close();//关闭连接
                    dataGridView1.Columns[0].HeaderText = "枪柜号";
                    dataGridView1.Columns[1].HeaderText = "任务状态";
                    dataGridView1.Columns[2].HeaderText = "任务类型";
                    dataGridView1.Columns[3].HeaderText = "任务开始时间";
                    dataGridView1.Columns[4].HeaderText = "任务结束时间";

                    dataGridView1.Columns[5].Visible = false;

                    break;
                case "枪弹封存":

                    string str4 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con4 = new MySqlConnection(str4);//实例化链接
                    con4.Open();//开启连接
                    string strcmd4 = "SELECT GUNARK_ID,TASK_STATUS,cast(TASK_BIGTYPE as CHAR),TASK_PLAN_BEGINTIME,TASK_PLAN_FINISHTIME,TASK_ID FROM gunark_task_info where TASK_BIGTYPE ='4' and (TASK_PLAN_BEGINTIME between '" + a + " 00:00:00' and '" + b + " 23:59:59')";

                    MySqlCommand cmd4 = new MySqlCommand(strcmd4, con4);
                    MySqlDataAdapter ada4 = new MySqlDataAdapter(cmd4);

                    DataSet ds4 = new DataSet();
                    ada4.Fill(ds4);//查询结果填充数据集

                    for (int i = 0; i < ds4.Tables[0].Rows.Count; i++)
                    {
                        if (ds4.Tables[0].Rows[i][1].Equals("1"))
                        {
                            ds4.Tables[0].Rows[i][1] = "申请";
                        }
                        else if (ds4.Tables[0].Rows[i][1].Equals("2"))
                        {
                            ds4.Tables[0].Rows[i][1] = "审核";
                        }
                        else if (ds4.Tables[0].Rows[i][1].Equals("3"))
                        {
                            ds4.Tables[0].Rows[i][1] = "审批完成";
                        }
                        else if (ds4.Tables[0].Rows[i][1].Equals("4"))
                        {
                            ds4.Tables[0].Rows[i][1] = "拒绝";

                        }
                        else if (ds4.Tables[0].Rows[i][1].Equals("5"))
                        {
                            ds4.Tables[0].Rows[i][1] = "进行中";
                        }
                        else
                        {
                            ds4.Tables[0].Rows[i][1] = "完成";
                        }
                    }

                    for (int i = 0; i < ds4.Tables[0].Rows.Count; i++)
                    {
                        if (ds4.Tables[0].Rows[i][2].Equals("1"))
                        {
                            ds4.Tables[0].Rows[i][2] = "申请弹仓";
                        }
                        else if (ds4.Tables[0].Rows[i][2].Equals("2"))
                        {
                            ds4.Tables[0].Rows[i][2] = "枪弹入柜";
                        }
                        else if (ds4.Tables[0].Rows[i][2].Equals("3"))
                        {
                            ds4.Tables[0].Rows[i][2] = "申请枪弹";
                        }
                        else if (ds4.Tables[0].Rows[i][2].Equals("4"))
                        {
                            ds4.Tables[0].Rows[i][2] = "枪支封存";

                        }
                        else if (ds4.Tables[0].Rows[i][2].Equals("5"))
                        {
                            ds4.Tables[0].Rows[i][2] = "枪支报废";

                        }
                        else if (ds4.Tables[0].Rows[i][2].Equals("6"))
                        {
                            ds4.Tables[0].Rows[i][2] = "枪支保养";

                        }
                        else if (ds4.Tables[0].Rows[i][2].Equals("7"))
                        {
                            ds4.Tables[0].Rows[i][2] = "枪弹调拨";
                        }
                    }

                    dataGridView1.DataSource = ds4.Tables[0];
                    con4.Close();//关闭连接
                    dataGridView1.Columns[0].HeaderText = "枪柜号";
                    dataGridView1.Columns[1].HeaderText = "任务状态";
                    dataGridView1.Columns[2].HeaderText = "任务类型";
                    dataGridView1.Columns[3].HeaderText = "任务开始时间";
                    dataGridView1.Columns[4].HeaderText = "任务结束时间";

                    dataGridView1.Columns[5].Visible = false;

                    break;

                case "枪弹报废":

                    string str7 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con7 = new MySqlConnection(str7);//实例化链接
                    con7.Open();//开启连接
                    string strcmd7 = "SELECT GUNARK_ID,TASK_STATUS,cast(TASK_BIGTYPE as CHAR),TASK_PLAN_BEGINTIME,TASK_PLAN_FINISHTIME,TASK_ID FROM gunark_task_info where TASK_BIGTYPE ='5' and (TASK_PLAN_BEGINTIME between '" + a + " 00:00:00' and '" + b + " 23:59:59')";

                    MySqlCommand cmd7 = new MySqlCommand(strcmd7, con7);
                    MySqlDataAdapter ada7 = new MySqlDataAdapter(cmd7);

                    DataSet ds7 = new DataSet();
                    ada7.Fill(ds7);//查询结果填充数据集
                    dataGridView1.DataSource = ds7.Tables[0];

                    for (int i = 0; i < ds7.Tables[0].Rows.Count; i++)
                    {
                        if (ds7.Tables[0].Rows[i][1].Equals("1"))
                        {
                            ds7.Tables[0].Rows[i][1] = "申请";
                        }
                        else if (ds7.Tables[0].Rows[i][1].Equals("2"))
                        {
                            ds7.Tables[0].Rows[i][1] = "审核";
                        }
                        else if (ds7.Tables[0].Rows[i][1].Equals("3"))
                        {
                            ds7.Tables[0].Rows[i][1] = "审批完成";
                        }
                        else if (ds7.Tables[0].Rows[i][1].Equals("4"))
                        {
                            ds7.Tables[0].Rows[i][1] = "拒绝";

                        }
                        else if (ds7.Tables[0].Rows[i][1].Equals("5"))
                        {
                            ds7.Tables[0].Rows[i][1] = "进行中";
                        }
                        else
                        {
                            ds7.Tables[0].Rows[i][1] = "完成";
                        }
                    }

                    for (int i = 0; i < ds7.Tables[0].Rows.Count; i++)
                    {
                        if (ds7.Tables[0].Rows[i][2].Equals("1"))
                        {
                            ds7.Tables[0].Rows[i][2] = "申请弹仓";
                        }
                        else if (ds7.Tables[0].Rows[i][2].Equals("2"))
                        {
                            ds7.Tables[0].Rows[i][2] = "枪弹入柜";
                        }
                        else if (ds7.Tables[0].Rows[i][2].Equals("3"))
                        {
                            ds7.Tables[0].Rows[i][2] = "申请枪弹";
                        }
                        else if (ds7.Tables[0].Rows[i][2].Equals("4"))
                        {
                            ds7.Tables[0].Rows[i][2] = "枪支封存";

                        }
                        else if (ds7.Tables[0].Rows[i][2].Equals("5"))
                        {
                            ds7.Tables[0].Rows[i][2] = "枪支报废";

                        }
                        else if (ds7.Tables[0].Rows[i][2].Equals("6"))
                        {
                            ds7.Tables[0].Rows[i][2] = "枪支保养";

                        }
                        else if (ds7.Tables[0].Rows[i][2].Equals("7"))
                        {
                            ds7.Tables[0].Rows[i][2] = "枪弹调拨";
                        }
                    }

                    con7.Close();//关闭连接
                    dataGridView1.Columns[0].HeaderText = "枪柜号";
                    dataGridView1.Columns[1].HeaderText = "任务状态";
                    dataGridView1.Columns[2].HeaderText = "任务类型";
                    dataGridView1.Columns[3].HeaderText = "任务开始时间";
                    dataGridView1.Columns[4].HeaderText = "任务结束时间";

                    dataGridView1.Columns[5].Visible = false;

                    break;

                case "全选":

                    string str8 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con8 = new MySqlConnection(str8);//实例化链接
                    con8.Open();//开启连接
                    string strcmd8 = "SELECT GUNARK_ID,TASK_STATUS,cast(TASK_BIGTYPE as CHAR),TASK_PLAN_BEGINTIME,TASK_PLAN_FINISHTIME,TASK_ID FROM gunark_task_info where (TASK_PLAN_BEGINTIME between '" + a + " 00:00:00' and '" + b + " 23:59:59')";


                    MySqlCommand cmd8 = new MySqlCommand(strcmd8, con8);
                    MySqlDataAdapter ada8 = new MySqlDataAdapter(cmd8);

                    DataSet ds8 = new DataSet();
                    ada8.Fill(ds8);//查询结果填充数据集

                    for (int i = 0; i < ds8.Tables[0].Rows.Count; i++)
                    {
                        if (ds8.Tables[0].Rows[i][1].Equals("1"))
                        {
                            ds8.Tables[0].Rows[i][1] = "申请";
                        }
                        else if (ds8.Tables[0].Rows[i][1].Equals("2"))
                        {
                            ds8.Tables[0].Rows[i][1] = "审核";
                        }
                        else if (ds8.Tables[0].Rows[i][1].Equals("3"))
                        {
                            ds8.Tables[0].Rows[i][1] = "审批完成";
                        }
                        else if (ds8.Tables[0].Rows[i][1].Equals("4"))
                        {
                            ds8.Tables[0].Rows[i][1] = "拒绝";

                        }
                        else if (ds8.Tables[0].Rows[i][1].Equals("5"))
                        {
                            ds8.Tables[0].Rows[i][1] = "进行中";
                        }
                        else
                        {
                            ds8.Tables[0].Rows[i][1] = "完成";
                        }
                    }

                    for (int i = 0; i < ds8.Tables[0].Rows.Count; i++)
                    {
                        if (ds8.Tables[0].Rows[i][2].Equals("1"))
                        {
                            ds8.Tables[0].Rows[i][2] = "申请弹仓";
                        }
                        else if (ds8.Tables[0].Rows[i][2].Equals("2"))
                        {
                            ds8.Tables[0].Rows[i][2] = "枪弹入柜";
                        }
                        else if (ds8.Tables[0].Rows[i][2].Equals("3"))
                        {
                            ds8.Tables[0].Rows[i][2] = "申请枪弹";
                        }
                        else if (ds8.Tables[0].Rows[i][2].Equals("4"))
                        {
                            ds8.Tables[0].Rows[i][2] = "枪支封存";

                        }
                        else if (ds8.Tables[0].Rows[i][2].Equals("5"))
                        {
                            ds8.Tables[0].Rows[i][2] = "枪支报废";

                        }
                        else if (ds8.Tables[0].Rows[i][2].Equals("6"))
                        {
                            ds8.Tables[0].Rows[i][2] = "枪支保养";

                        }
                        else if (ds8.Tables[0].Rows[i][2].Equals("7"))
                        {
                            ds8.Tables[0].Rows[i][2] = "枪弹调拨";
                        }
                        else if (ds8.Tables[0].Rows[i][2].Equals("10"))
                        {
                            ds8.Tables[0].Rows[i][2] = "枪支解封";
                        }
                        else if (ds8.Tables[0].Rows[i][2].Equals("9"))
                        {
                            ds8.Tables[0].Rows[i][2] = "枪弹检查";
                        }
                    }

                    dataGridView1.DataSource = ds8.Tables[0];
                    con8.Close();//关闭连接
                    dataGridView1.Columns[0].HeaderText = "枪柜号";
                    dataGridView1.Columns[1].HeaderText = "任务状态";
                    dataGridView1.Columns[2].HeaderText = "任务类型";
                    dataGridView1.Columns[3].HeaderText = "任务开始时间";
                    dataGridView1.Columns[4].HeaderText = "任务结束时间";
                    dataGridView1.Columns[5].Visible = false;
                    break;
                //case "枪支保养":

                //    string str5 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                //    MySqlConnection con5 = new MySqlConnection(str5);//实例化链接
                //    con5.Open();//开启连接
                //    string strcmd5 = "select GUNARK_LOG_TIME,GUNARK_LOG_TYPE,GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER from gunark_log where GUNARK_LOG_TYPE ='枪支保养' and (GUNARK_LOG_TIME between '" + dateTimePicker2.Value.ToShortDateString() + " 00:00:00' and '" + dateTimePicker2.Value.ToShortDateString() + " 23:59:59')";

                //    MySqlCommand cmd5 = new MySqlCommand(strcmd5, con5);
                //    MySqlDataAdapter ada5 = new MySqlDataAdapter(cmd5);

                //    DataSet ds5 = new DataSet();
                //    ada5.Fill(ds5);//查询结果填充数据集
                //    dataGridView1.DataSource = ds5.Tables[0];
                //    con5.Close();//关闭连接
                //    dataGridView1.Columns[0].HeaderText = "日志时间";
                //    dataGridView1.Columns[1].HeaderText = "日志类型";
                //    dataGridView1.Columns[2].HeaderText = "操作管理员1";
                //    dataGridView1.Columns[3].HeaderText = "操作管理员2";
                //    dataGridView1.Columns[4].HeaderText = "保养数量";

                //    break;
                //case "紧急取枪":

                //    string str6 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                //    MySqlConnection con6 = new MySqlConnection(str6);//实例化链接
                //    con6.Open();//开启连接
                //    string strcmd6 = "select GUNARK_LOG_TIME,GUNARK_LOG_TYPE,GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER from gunark_log where GUNARK_LOG_TYPE ='紧急取枪' and (GUNARK_LOG_TIME between '" + dateTimePicker2.Value.ToShortDateString() + " 00:00:00' and '" + dateTimePicker2.Value.ToShortDateString() + " 23:59:59')";

                //    MySqlCommand cmd6 = new MySqlCommand(strcmd6, con6);
                //    MySqlDataAdapter ada6 = new MySqlDataAdapter(cmd6);

                //    DataSet ds6 = new DataSet();
                //    ada6.Fill(ds6);//查询结果填充数据集
                //    dataGridView1.DataSource = ds6.Tables[0];
                //    con6.Close();//关闭连接
                //    dataGridView1.Columns[0].HeaderText = "日志时间";
                //    dataGridView1.Columns[1].HeaderText = "日志类型";
                //    dataGridView1.Columns[2].HeaderText = "操作管理员1";
                //    dataGridView1.Columns[3].HeaderText = "操作管理员2";
                //    dataGridView1.Columns[4].HeaderText = "紧急取枪数量";


                //    break;

            }
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //int index = 0;
            //if (e.RowIndex == -1)
            //{
            //    index = 0;
            //}
            //else
            //{
            //    index = e.RowIndex;
            //}
            //if (dataGridView1.Rows.Count > 0)
            //{
            //    DataGridViewCheckBoxCell check = (DataGridViewCheckBoxCell)dataGridView1.Rows[index].Cells[0];
            //    check.Value = true;
            //    //MessageBox.Show(e.RowIndex.ToString());
            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        check = (DataGridViewCheckBoxCell)row.Cells[0];
            //        if (row.Index != e.RowIndex)
            //        {
            //            check.Value = false;
            //        }
            //    }
            //}
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }



        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = "双击鼠标左键可查看枪弹柜详细信息";                                                                                                                                                                                                                
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
