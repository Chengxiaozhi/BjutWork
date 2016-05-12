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
    public partial class ReturnEmergencyGunDetail : Form
    {
        public static int count;
        public static string gunark_name;
        public static string[] gunid = new string[100];//获取gunid
        public static string[] gunpositionid = new string[100];//获取枪位id
        public static byte[] gunpositionnum = new byte[100];//获取枪位号
        public static byte[] magazinenum = new byte[100];//获取弹仓号
        public static string[] mgz_id1 = new string[100];
        public static int[] qty1 = new int[100];//子弹库存
        public static int[] hdsl = new int[100];//还弹数量

        public ReturnEmergencyGunDetail()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            Display();
        }

        private void Display()
        {
            //显示所选任务detail
            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = "select geti.EMERGENCY_TASK_ID, gg.GUNARK_NAME, gpi.GUN_POSITION_NUMBER, gpi.GUN_TYPE, getid.GUN_INFO_ID, getid.GUN_POSITION_INFO_ID, gmi.MAGAZINE_NUMBER,gmi.BULLET_MODEL, cast(getid.APPLY_BULLET_QTY as CHAR),getid.MAGAZINE_INFO_ID from gunark_emergency_task_info geti left join gunark_emergency_task_info_detail getid on getid.EMERGENCY_TASK_ID = geti.EMERGENCY_TASK_ID left join gunark_position_info gpi on gpi.GUN_POSITION_INFO_ID = getid.GUN_POSITION_INFO_ID left join gunark_gunark gg on gg.GUNARK_ID = geti.GUNARK_ID left join gunark_magazine_info gmi on gmi.MAGAZINE_INFO_ID = getid.MAGAZINE_INFO_ID where getid.EMERGENCY_TASK_ID ='" + ReturnEmergencyGun.emergency_task_id + "'";

            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集

            gunark_name = ds.Tables[0].Rows[0][1].ToString();
            //label1.Text = ds.Tables[0].Rows.Count.ToString();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][7].Equals("1"))
                {
                    ds.Tables[0].Rows[i][7] = "54式手枪弹";
                }
                else if (ds.Tables[0].Rows[i][7].Equals("2"))
                {
                    ds.Tables[0].Rows[i][7] = "64式手枪弹";
                }
                else if (ds.Tables[0].Rows[i][7].Equals("3"))
                {
                    ds.Tables[0].Rows[i][7] = "77式手枪弹";
                }
                else if (ds.Tables[0].Rows[i][7].Equals("4"))
                {
                    ds.Tables[0].Rows[i][7] = "79式冲锋枪弹";
                }
                else if (ds.Tables[0].Rows[i][7].Equals("5"))
                {
                    ds.Tables[0].Rows[i][7] = "97式防暴枪弹";
                }
                else
                {
                    ds.Tables[0].Rows[i][7] = "";
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

            dataGridView1.DataSource = ds.Tables[0];


            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "枪柜名";
            dataGridView1.Columns[2].HeaderText = "枪位号";
            dataGridView1.Columns[3].HeaderText = "枪支类型";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].HeaderText = "弹仓号";
            dataGridView1.Columns[7].HeaderText = "子弹类型";
            dataGridView1.Columns[8].HeaderText = "取弹数量";
            dataGridView1.Columns[9].Visible = false;

            DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "acCode";
            acCode.DataPropertyName = "acCode";
            acCode.HeaderText = "还弹数量";
            dataGridView1.Columns.Add(acCode);


            con.Close();//关闭连接
        }

        private void ReturnEmergencyGunDetail_Load(object sender, EventArgs e)
        {
            dataGridView1.Controls.Add(textBox3); 
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
                if (MessageBox.Show(this, "您确定要执行该任务么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    //获取gunid

                    for (int i = 0; i < count; i++)
                    {
                        string rows = dataGridView1.Rows[i].Cells[5].Value.ToString();
                        gunid[i] = rows;
                    }
                    //获取gunpositionid

                    for (int i = 0; i < count; i++)
                    {

                        string rows = dataGridView1.Rows[i].Cells[6].Value.ToString();
                        gunpositionid[i] = rows;
                    }
                    //获取gunpositionnum
                    for (int ii = 0; ii < count; ii++)
                    {
                        try
                        {
                            string gpn;
                            gpn = dataGridView1.Rows[ii].Cells[3].Value.ToString();

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
                        string rows = dataGridView1.Rows[i].Cells[10].Value.ToString();
                        mgz_id1[i] = rows;
                    }

                    //获取子弹数量
                    for (int i = 0; i < count; i++)
                    {
                        string rows = dataGridView1.Rows[i].Cells[9].Value.ToString();
                        if (rows != "")
                        {
                            qty1[i] = int.Parse(rows);
                        }
                        else
                        {
                            continue;
                        }
                        //获取还弹数量
                        try
                        {
                            string rows1 = dataGridView1.Rows[i].Cells[0].Value.ToString();
                            if (rows1 != "")
                            {
                                hdsl[i] = int.Parse(rows1);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("第【" + (i + 1) + "】行，没有填入还弹数量！（若子弹消耗完请在还弹数量处填【0】）", "提示");
                            return;
                        }

                        if (hdsl[i] > qty1[i])
                        {
                            MessageBox.Show("第【" + (i + 1) + "】行，还弹数量不能大于取弹数量！", "提示");
                            return;
                        }

                    }

                    //获取magazinenum
                    for (int iii = 0; iii < dataGridView1.Rows.Count; iii++)
                    {
                        try
                        {
                            string gpn;
                            gpn = dataGridView1.Rows[iii].Cells[7].Value.ToString();
                            byte rows = (byte)int.Parse(gpn);
                            magazinenum[iii] = rows;
                        }
                        catch (Exception ee)
                        {
                            continue;
                        }
                    }
                    Finger_Check fc = new Finger_Check();
                    fc.ShowDialog();
                    this.Close();
                    Display();
                }
                else
                {
                    return;
                }
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            this.textBox3.Visible = false;
            this.textBox3.Width = 0;
            try
            {
                //前提是你的datagridview1一定要有数据呀
                if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText == "还弹数量")
                {
                    //以下是把把textBox3的位置与datagridview1的单元格的位置一样,不过长宽均比单元格的短2
                    this.textBox3.Left = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, true).Left + 20;
                    this.textBox3.Top = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, true).Top + 10;
                    this.textBox3.Width = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, true).Width - 40;
                    this.textBox3.Height = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, true).Height - 2;

                    string str = Convert.ToString(this.dataGridView1.CurrentCell.Value); this.textBox3.Text = str;//将单元格的值赋给文本框
                    this.textBox3.Visible = true;
                }
            }
            catch
            {
            }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            this.dataGridView1.CurrentCell.Value = this.textBox3.Text;//如果文本框输入内容后,把其值赋给其对应的单元格
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            } 
        }

    }
}
