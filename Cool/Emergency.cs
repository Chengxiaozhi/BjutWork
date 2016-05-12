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
    public partial class Emergency : Form
    {
        public static string gunark_id;
        public static string unit_id;
        public static string gunark_name;
        public static string gunark_type;
        public Emergency()
        {

            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            //显示在库枪柜
            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = "SELECT GUNARK_TYPE,GUNARK_NAME,GUNARK_ID,UNITINFO_CODE from gunark_gunark where GUNARK_STATUS = 1;";
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[1].HeaderText = "枪柜类型";
            dataGridView1.Columns[2].HeaderText = "枪柜名";
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            con.Close();//关闭连接

            
        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Emergency_Load(object sender, EventArgs e)
        {

        }
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

        

        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = "双击鼠标左键可选择枪柜";
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
                    for (int i = 1; i <= 4; i++)
                    {
                        r[i - 1] = dr.Cells[i].Value;
                    }
                    dt.Rows.Add(r);

                }

            }
            gunark_type = dt.Rows[0][0].ToString();
            gunark_name = dt.Rows[0][1].ToString();
            gunark_id = dt.Rows[0][2].ToString();
            unit_id = dt.Rows[0][3].ToString();

            //label2.Text = gunark_id;
            //Emergencygun eg = new Emergencygun();
            //eg.ShowDialog();
            //this.Close();
            //this.myPanel1.Controls.Clear();
            Emergencygun ec = new Emergencygun();
            ec.FormBorderStyle = FormBorderStyle.None;
            ec.TopLevel = false;
            ec.Dock = System.Windows.Forms.DockStyle.Fill;
            ec.FormBorderStyle = FormBorderStyle.None;
            this.myPanel1.Controls.Add(ec);
            ec.Dock = DockStyle.Fill;
            ec.Show();
        }

        private void myPanel1_Paint(object sender, PaintEventArgs e)
        {
            this.dataGridView1.DefaultCellStyle.Font = new Font("宋体", 15);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15);
        }

       

       
    }
}
