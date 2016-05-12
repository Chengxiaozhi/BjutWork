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
    public partial class ReturnEmergencyGun : Form
    {
        string sss;
        public static string emergency_task_id;
        public static string emergency_task_status;
        public ReturnEmergencyGun()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            frmMain.isGet = 10;
            Display();
        }

        private void Display()
        {
            //显示任务
            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = "select gg.GUNARK_NAME,geti.TASK_STATUS,geti.TASK_BEGIN_TIME,geti.EMERGENCY_TASK_ID from gunark_emergency_task_info geti left join gunark_gunark gg on gg.GUNARK_ID = geti.GUNARK_ID where (TASK_STATUS='5' or TASK_STATUS='10')";
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][1].Equals("5"))
                {
                    ds.Tables[0].Rows[i][1] = "进行中";
                }
                else if (ds.Tables[0].Rows[i][1].Equals("6"))
                {
                    ds.Tables[0].Rows[i][1] = "完成";
                }
                else if (ds.Tables[0].Rows[i][1].Equals("10"))
                {
                    ds.Tables[0].Rows[i][1] = "未正常还枪";
                }
            }

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[1].HeaderText = "枪柜名";
            dataGridView1.Columns[2].HeaderText = "任务状态";
            dataGridView1.Columns[3].HeaderText = "紧急取枪时间";
            dataGridView1.Columns[4].Visible = false;
            
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
                    for (int i = 1; i <= 4; i++)
                    {
                        r[i - 1] = dr.Cells[i].Value;
                    }
                    dt.Rows.Add(r);
                }
            }
           
            emergency_task_id = dt.Rows[0][3].ToString();
            emergency_task_status = dt.Rows[0][1].ToString();
            //MessageBox.Show(emergency_task_id);
            ReturnEmergencyGunDetail regd = new ReturnEmergencyGunDetail();
            regd.ShowDialog();
            Display();
            //this.Close();
        }

        private void ReturnEmergencyGun_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DefaultCellStyle.Font = new Font("宋体", 15);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15);
        }
    }
}
