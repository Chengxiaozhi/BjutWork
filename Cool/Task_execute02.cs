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
    public partial class Task_execute02 : Form
    {
        
        //public static int isGet = 0;//取还枪标志位，0取枪，1还枪，2报废封存调拨，3保养检查
        string sss;
        public static string task_status;
        public Task_execute02()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            frmMain.isGet = 1;
            Display();
        }

        private void Display()
        {
            DateTime now = DateTime.Now;
            //label1.Text = now.ToString();
            string m;//月
            string d;//日
            string h;
            //取月份
            if (now.ToString().Substring(6, 1).Equals("/"))
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
                sss = now.ToString().Substring(0, 4) + "-0" + m + "-0" + d + " 0" + h + now.ToString().Substring(10, 6);
                //MessageBox.Show(sss);
            }
            else if (int.Parse(m) < 10 & int.Parse(d) < 10 & int.Parse(h) >= 10)
            {
                sss = now.ToString().Substring(0, 4) + "-0" + m + "-0" + d + now.ToString().Substring(8, 9);
                //MessageBox.Show(sss);
            }
            else if (int.Parse(m) < 10 & int.Parse(d) >= 10 & int.Parse(h) < 10)
            {
                sss = now.ToString().Substring(0, 4) + "-0" + m + "-" + d + " 0" + h + now.ToString().Substring(11, 6);
            }
            else if (int.Parse(m) < 10 & int.Parse(d) >= 10 & int.Parse(h) >= 10)
            {
                sss = now.ToString().Substring(0, 4) + "-0" + m + "-" + d + " " + h + now.ToString().Substring(12, 6);
            }
            else if (int.Parse(m) >= 10 & int.Parse(d) < 10 & int.Parse(h) < 10)
            {
                sss = now.ToString().Substring(0, 4) + "-" + m + "-0" + d + " 0" + h + now.ToString().Substring(11, 6);
            }
            else if (int.Parse(m) >= 10 & int.Parse(d) < 10 & int.Parse(h) >= 10)
            {
                sss = now.ToString().Substring(0, 4) + "-" + m + "-0" + d + now.ToString().Substring(9, 9);
            }
            else if (int.Parse(m) >= 10 & int.Parse(d) >= 10 & int.Parse(h) < 10)
            {
                //sss = now.ToString().Substring(0, 11) + "0" + now.ToString().Substring(11, 7);
                sss = now.ToString().Substring(0, 4) + "-" + m + "-" + d + " 0" + now.ToString().Substring(11, 7);

            }
            else if (int.Parse(m) >= 10 & int.Parse(d) >= 10 & int.Parse(h) >= 10)
            {
                //sss = now.ToString();
                sss = now.ToString().Substring(0, 4) + "-" + m + "-" + d + now.ToString().Substring(10, 8);
            }

            //更改任务状态
            //MessageBox.Show(sss);
            string str1 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con1 = new MySqlConnection(str1);//实例化链接
            con1.Open();//开启连接
            string strcmd1 = "update gunark_task_info set TASK_STATUS = '7' where (TASK_PLAN_FINISHTIME <'" + sss + ".0' and TASK_STATUS='5')";
            MySqlCommand cmd1 = new MySqlCommand(strcmd1, con1);
            MySqlDataAdapter ada1 = new MySqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            ada1.Fill(ds1);//查询结果填充数据集
            con1.Close();
            //显示任务
            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = "select TASK_ID,TASK_STATUS,gu.USER_REALNAME,cast(TASK_BIGTYPE as CHAR),TASK_APPLY_USERID,TASK_PLAN_BEGINTIME,TASK_PLAN_FINISHTIME FROM gunark_task_info gti LEFT JOIN gunark_user gu on gti.TASK_APPLY_USERID=gu.USER_POLICENUMB where (TASK_BIGTYPE='3' or TASK_BIGTYPE='6') and (TASK_STATUS='5' or TASK_STATUS='7' or TASK_STATUS='10')";
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
                else if (ds.Tables[0].Rows[i][1].Equals("7"))
                {
                    ds.Tables[0].Rows[i][1] = "超期未还";
                }
                else if (ds.Tables[0].Rows[i][1].Equals("10"))
                {
                    ds.Tables[0].Rows[i][1] = "未正常归还";
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
                else if (ds.Tables[0].Rows[i][3].Equals("9"))
                {
                    ds.Tables[0].Rows[i][3] = "枪弹检查";
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
            this.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = "鼠标左键双击选中行可执行该任务";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dt = new DataTable();  //建立一个准备存储数据的新dt
            dt.Columns.Add("c1");
            dt.Columns.Add("c2");
            dt.Columns.Add("c3");
            dt.Columns.Add("c4");
            dt.Columns.Add("c5");
            dt.Columns.Add("c6");
            dt.Columns.Add("c7");
            dt.Columns.Add("c8");
            dt.Columns.Add("c9");
            dt.Columns.Add("c10");
            dt.Columns.Add("c11");
            dt.Columns.Add("c12");
            foreach (DataGridViewRow dr in dataGridView1.Rows)   //遍历dgv1
            {
                DataRow r = dt.NewRow();
                if (dr.Cells[0].Value != null && (bool)dr.Cells[0].Value == true)  //如果checked,开始复制数据
                {
                    for (int i = 1; i <= 7; i++)
                    {
                        r[i - 1] = dr.Cells[i].Value;
                    }
                    dt.Rows.Add(r);
                }
            }
            try
            {
                frmMain.taskid = dt.Rows[0][0].ToString();
                frmMain.taskbigtype = dt.Rows[0][3].ToString();
                task_status = dt.Rows[0][1].ToString();
                //MessageBox.Show(frmMain.taskbigtype);
                Task_Execute_Detail td = new Task_Execute_Detail();
                td.ShowDialog();
                Display();
                //this.Close();
            }
            catch { }
        }

        private void Task_execute02_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DefaultCellStyle.Font = new Font("宋体", 15);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15);
        }
    }
}
