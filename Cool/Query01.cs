using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Cool
{
    public partial class Query01 : Form
    {
        public Query01()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //获取搜索框的qurey请求
            String query = textBox1.Text.Trim();
            if (query == "")
            {
                MessageBox.Show("请输入枪支编号！！！");
            }
            else
            {
                //将连接数据库的方法封装到DatabaseManger类中，该类设置为静态类，可直接用类名调用其方法。
                //链接数据库
                MySqlConnection con = null;
                con = DatabaseManager.GetCon(con);
                con.Open();
                //SQL语句
                //String strsql = "select GUN_NUMBER, GUNARK_ID, UNIT_ID, GUN_TYPE, GUN_STATUS, GUN_BULLET_LOCATION, IN_TIME, OUT_TIME from gun_info where GUN_NUMBER like '%" + query + "%'";
                String strsql = "select gi.GUN_NUMBER, gi.UNIT_ID, gg.GUNARK_NAME, gi.GUN_TYPE, gi.GUN_STATUS, gi.GUN_BULLET_LOCATION, gi.IN_TIME, gi.OUT_TIME from gun_info gi left join gunark_gunark gg on gi.GUNARK_ID = gg.GUNARK_ID where GUN_NUMBER like '%" + query + "%'";
                //查询数据库返回的DataSet
                DataSet ds = DatabaseManager.ExecAndGetDs(strsql, "gun_info",con);
                //关闭数据库连接
                DatabaseManager.Clear(con);
                //处理结果集
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("抱歉，编号为【" + query + "】的枪支不存在！！！");
                }
                else
                {

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
                        if (ds.Tables[0].Rows[i][4].Equals("3"))
                        {
                            ds.Tables[0].Rows[i][4] = "未置枪";
                        }
                        else 
                        {
                            ds.Tables[0].Rows[i][4] = "置枪";
                        }
                    }


                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].HeaderText = "枪支编号";
                    dataGridView1.Columns[1].HeaderText = "所属机构";
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].HeaderText = "所属枪柜";
                    dataGridView1.Columns[3].HeaderText = "枪支种类";
                    dataGridView1.Columns[4].HeaderText = "枪支状态";
                    dataGridView1.Columns[5].HeaderText = "所在枪位";
                    dataGridView1.Columns[6].HeaderText = "入库时间";
                    dataGridView1.Columns[7].HeaderText = "出库时间";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Query01_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("宋体",15);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("宋体",15);

            this.queryAll();
        }

        private void queryAll()
        {
            //将连接数据库的方法封装到DatabaseManger类中，该类设置为静态类，可直接用类名调用其方法。
            //链接数据库
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            con.Open();
            //SQL语句
            String strsql = "select gi.GUN_NUMBER, gi.UNIT_ID, gg.GUNARK_NAME, gi.GUN_TYPE, gi.GUN_STATUS, gi.GUN_BULLET_LOCATION, gi.IN_TIME, gi.OUT_TIME from gun_info gi left join gunark_gunark gg on gi.GUNARK_ID = gg.GUNARK_ID";
            //查询数据库返回的DataSet
            DataSet ds = DatabaseManager.ExecAndGetDs(strsql, "gun_info",con);
            //关闭数据库连接
            DatabaseManager.Clear(con);
            //处理结果集

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
                if (ds.Tables[0].Rows[i][4].Equals("3"))
                {
                    ds.Tables[0].Rows[i][4] = "未置枪";
                }
                else if (ds.Tables[0].Rows[i][4].Equals("1"))
                {
                    ds.Tables[0].Rows[i][4] = "置枪";
                }
                else if (ds.Tables[0].Rows[i][4].Equals("5"))
                {
                    ds.Tables[0].Rows[i][4] = "出库";
 
                }
                else if (ds.Tables[0].Rows[i][4].Equals("0"))
                {
                    ds.Tables[0].Rows[i][4] = "封存";

                }
                
            }


            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].HeaderText = "枪支编号";
            dataGridView1.Columns[1].HeaderText = "所属机构";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "所属枪柜";
            dataGridView1.Columns[3].HeaderText = "枪支种类";
            dataGridView1.Columns[4].HeaderText = "枪支状态";
            dataGridView1.Columns[5].HeaderText = "所在枪位";
            dataGridView1.Columns[6].HeaderText = "入库时间";
            dataGridView1.Columns[7].HeaderText = "出库时间";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            queryAll();
            this.textBox1.Text = "";
        }
    }
}
