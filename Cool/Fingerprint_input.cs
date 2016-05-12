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
    public partial class Fingerprint_input : Form
    {
        public static int user_number;
        DataSet ds = new DataSet();
        public Fingerprint_input()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
        }

        private void Fingerprint_input_Load(object sender, EventArgs e)
        {
            //显示任务
            Dispaly();
            this.dataGridView1.DefaultCellStyle.Font = new Font("宋体", 15);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15);
        }

        private void Dispaly()
        {
            ds = new DataSet();
            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = "SELECT USER_REALNAME,USER_SEX,USER_PRIVIEGES,USER_POLICENUMB,UNITINFO_CODE,ID,IsInputFinger_1,IsInputFinger_2,IsInputFinger_3 FROM gunark_user_finger";
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            
            ada.Fill(ds);//查询结果填充数据集
            con.Close();//关闭连接

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][1].Equals("0"))
                {
                    ds.Tables[0].Rows[i][1] = "男";
                }
                else if (ds.Tables[0].Rows[i][1].Equals("1"))
                {
                    ds.Tables[0].Rows[i][1] = "女";
                }
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][2].Equals("1"))
                {
                    ds.Tables[0].Rows[i][2] = "普通用户";
                }
                else if (ds.Tables[0].Rows[i][2].Equals("2"))
                {
                    ds.Tables[0].Rows[i][2] = "系统管理员";
                }
                else if (ds.Tables[0].Rows[i][2].Equals("3"))
                {
                    ds.Tables[0].Rows[i][2] = "枪柜管理员";
                }
                else if (ds.Tables[0].Rows[i][2].Equals("4"))
                {
                    ds.Tables[0].Rows[i][2] = "弹柜管理员";
                }
                else if (ds.Tables[0].Rows[i][2].Equals("5"))
                {
                    ds.Tables[0].Rows[i][2] = "审核员";
                }
                else if (ds.Tables[0].Rows[i][2].Equals("6"))
                {
                    ds.Tables[0].Rows[i][2] = "审批员";
                }
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string remark = ds.Tables[0].Rows[i][6].ToString() + ds.Tables[0].Rows[i][7].ToString() + ds.Tables[0].Rows[i][8].ToString();
                ds.Tables[0].Rows[i][8] = remark;
            }

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[1].HeaderText = "";
            dataGridView1.Columns[1].HeaderText = "姓名";
            dataGridView1.Columns[2].HeaderText = "性别";
            dataGridView1.Columns[3].HeaderText = "用户权限";
            dataGridView1.Columns[4].HeaderText = "警号";
            dataGridView1.Columns[5].HeaderText = "组织机构";
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[9].HeaderText = "备注(已录入手指)";
            dataGridView1.Columns[8].Visible = false;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    int count = 0;
        //    for (int i = 0; i < dataGridView1.RowCount; i++)
        //    {
        //        if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
        //        {
        //            count++;
        //            user_number = (int)ds.Tables[0].Rows[i][5];
        //        }
        //    }
        //    if (count == 0)
        //    {
        //        MessageBox.Show("请选择人员！");

        //    }
        //    else
        //    {
        //        DataTable dt = new DataTable();  //建立一个准备存储数据的新dt
        //        dt.Columns.Add("c1");
        //        dt.Columns.Add("c2");
        //        dt.Columns.Add("c3");
        //        dt.Columns.Add("c4");
        //        dt.Columns.Add("c5");
        //        dt.Columns.Add("c6");
        //        dt.Columns.Add("c7");
        //        dt.Columns.Add("c8");
        //        dt.Columns.Add("c9");
        //        dt.Columns.Add("c10");
        //        dt.Columns.Add("c11");
        //        dt.Columns.Add("c12");
        //        foreach (DataGridViewRow dr in dataGridView1.Rows)   //遍历dgv1
        //        {
        //            DataRow r = dt.NewRow();
        //            if (dr.Cells[0].Value != null && (bool)dr.Cells[0].Value == true)  //如果checked,开始复制数据
        //            {
        //                for (int i = 1; i <= 5; i++)
        //                {
        //                    r[i - 1] = dr.Cells[i].Value;
        //                }
        //                dt.Rows.Add(r);

        //            }

        //        }

        //        Form2 f = new Form2();
        //        f.ShowDialog();
        //        Dispaly();
        //    }
            
            
        //}
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int count = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    count++;
                    user_number = (int)ds.Tables[0].Rows[i][5];
                }
            }
            if (count == 0)
            {
                //MessageBox.Show("请选择人员！");

            }
            else
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
                        for (int i = 1; i <= 5; i++)
                        {
                            r[i - 1] = dr.Cells[i].Value;
                        }
                        dt.Rows.Add(r);

                    }

                }

                Form2 f = new Form2();
                f.ShowDialog();
            }
            Dispaly();
        }

        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = "鼠标左键双击选中行可选择该人员";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
    }
}
