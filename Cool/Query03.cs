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
    public partial class Query03 : Form
    {
        public static string gunark_query;
        public Query03()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);

            //显示在库枪柜
            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = "SELECT GUNARK_TYPE,GUNARK_NAME,GUNARK_ID from gunark_gunark where GUNARK_STATUS = 1;";
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集
            dataGridView3.DataSource = ds.Tables[0];
            dataGridView3.Columns[1].HeaderText = "枪柜类型";
            dataGridView3.Columns[2].HeaderText = "枪柜名";
            dataGridView3.Columns[3].Visible = false;
            con.Close();//关闭连接

        }
        private void button5_Click(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                if ((bool)dataGridView3.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                MessageBox.Show("请选择枪柜！");
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
                foreach (DataGridViewRow dr in dataGridView3.Rows)   //遍历dgv1
                {
                    DataRow r = dt.NewRow();
                    if (dr.Cells[0].Value != null && (bool)dr.Cells[0].Value == true)  //如果checked,开始复制数据
                    {
                        for (int i = 1; i <= 3; i++)
                        {
                            r[i - 1] = dr.Cells[i].Value;
                        }
                        dt.Rows.Add(r);
                    }
                }

                gunark_query = dt.Rows[0][2].ToString();

                Cooldetail c = new Cooldetail();
                c.ShowDialog();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
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
            if (dataGridView3.Rows.Count > 0)
            {
                DataGridViewCheckBoxCell check = (DataGridViewCheckBoxCell)dataGridView3.Rows[index].Cells[0];
                check.Value = true;
                //MessageBox.Show(e.RowIndex.ToString());
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    check = (DataGridViewCheckBoxCell)row.Cells[0];
                    if (row.Index != e.RowIndex)
                    {
                        check.Value = false;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView3_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = "鼠标左键双击可查看该枪柜详细信息";
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int count = 0;
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                if ((bool)dataGridView3.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                MessageBox.Show("请选择枪柜！");
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
                foreach (DataGridViewRow dr in dataGridView3.Rows)   //遍历dgv1
                {
                    DataRow r = dt.NewRow();
                    if (dr.Cells[0].Value != null && (bool)dr.Cells[0].Value == true)  //如果checked,开始复制数据
                    {
                        for (int i = 1; i <= 3; i++)
                        {
                            r[i - 1] = dr.Cells[i].Value;
                        }
                        dt.Rows.Add(r);
                    }
                }

                gunark_query = dt.Rows[0][2].ToString();

                //Cooldetail c = new Cooldetail();
                //c.ShowDialog();

                Gunark_Details gd = new Gunark_Details(gunark_query);
                gd.FormBorderStyle = FormBorderStyle.None;
                gd.TopLevel = false;
                gd.Dock = System.Windows.Forms.DockStyle.Fill;
                gd.FormBorderStyle = FormBorderStyle.None;
                this.myPanel1.Controls.Add(gd);
                gd.Dock = DockStyle.Fill;
                gd.Show();
            }
        }

        private void Query03_Load(object sender, EventArgs e)
        {

        }

        private void myPanel1_Paint(object sender, PaintEventArgs e)
        {
            this.dataGridView3.DefaultCellStyle.Font = new Font("宋体", 15);
            this.dataGridView3.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15);
        }

    }
}
