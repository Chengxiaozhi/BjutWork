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
    public partial class Query02 : Form
    {
        public Query02()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //获取搜索框的qurey请求
            String query = textBox2.Text.Trim();
            if (query == "")
            {
                MessageBox.Show("请输入姓名！！！");
            }
            else
            {
                //链接数据库
                MySqlConnection con = null;
                con = DatabaseManager.GetCon(con);
                con.Open();
                //SQL语句
                String strsql = "select USER_REALNAME, USER_SEX, CAST(USER_STATE AS CHAR),USER_ADDRESS, USER_OFFICETELEP, USER_MOBILTELEP, UNITINFO_CODE from gunark_user where USER_REALNAME like '%" + query + "%'";
                //查询数据库返回的DataSet
                DataSet ds = DatabaseManager.ExecAndGetDs(strsql, "gunark_user",con);
                //关闭数据库连接
                DatabaseManager.Clear(con);
                //处理结果集
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("抱歉，【" + query + "】不存在！！！");
                }
                else
                {
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
                        if (ds.Tables[0].Rows[i][2].Equals("1"))
                        {
                            ds.Tables[0].Rows[i][2] = "启用";
                        }
                        else if (ds.Tables[0].Rows[i][2].Equals("0"))
                        {
                            ds.Tables[0].Rows[i][2] = "锁定";
                        }
                    }
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].HeaderText = "姓名";
                    dataGridView1.Columns[1].HeaderText = "性别";
                    dataGridView1.Columns[2].HeaderText = "用户状态";
                    dataGridView1.Columns[3].HeaderText = "住址";
                    dataGridView1.Columns[4].HeaderText = "办公室电话";
                    dataGridView1.Columns[5].HeaderText = "移动电话";
                    //dataGridView1.Columns[6].HeaderText = "组织机构";
                    dataGridView1.Columns[6].Visible = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Query02_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DefaultCellStyle.Font = new Font("宋体", 15);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15);
            queryAll();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            queryAll();
            this.textBox2.Text = "";
        }
        private void queryAll()
        {
            //链接数据库
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            con.Open();
            //SQL语句
            String strsql = "select USER_REALNAME, USER_SEX, CAST(USER_STATE AS CHAR),USER_ADDRESS, USER_OFFICETELEP, USER_MOBILTELEP, UNITINFO_CODE from gunark_user";
            //查询数据库返回的DataSet
            DataSet ds = DatabaseManager.ExecAndGetDs(strsql, "gunark_user",con);
            //关闭数据库连接
            DatabaseManager.Clear(con);
            //处理结果集

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
                if (ds.Tables[0].Rows[i][2].Equals("1"))
                {
                    ds.Tables[0].Rows[i][2] = "启用";
                }
                else if (ds.Tables[0].Rows[i][2].Equals("0"))
                {
                    ds.Tables[0].Rows[i][2] = "锁定";
                }
            }
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].HeaderText = "姓名";
            dataGridView1.Columns[1].HeaderText = "性别";
            dataGridView1.Columns[2].HeaderText = "用户状态";
            dataGridView1.Columns[3].HeaderText = "住址";
            dataGridView1.Columns[4].HeaderText = "办公室电话";
            dataGridView1.Columns[5].HeaderText = "移动电话";
            //dataGridView1.Columns[6].HeaderText = "组织机构";
            dataGridView1.Columns[6].Visible = false;
        }
    }
}
