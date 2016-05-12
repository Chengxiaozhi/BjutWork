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
    public partial class Emergencygun : Form
    {
        //List<string> list = new List<string>();//获取gunid的数目
        public static string[] emg_gunid = new string[100];//获取gunid
        public static string[] emg_gunpositionid = new string[100];//获取枪位id
        public static byte[] emg_gunpositionnum = new byte[100];

        public static byte[] magazinenum = new byte[100];//获取弹仓号
        public static string[] mgz_id1 = new string[100];
        public static int[] qty1 = new int[100];
        public static int[] qtsl = new int[100];
        public static string[] emg_bullettype = new string[100];
        public static string[] total_gunid = new string[300];
        public static string[] total_gunpositionid = new string[300];
        public static int total_j;//总数
        public static int j;//选中数

        public Emergencygun()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            if (Emergency.gunark_type.Equals("枪柜"))
            {
                //显示所选库的在位枪支
                string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                MySqlConnection con = new MySqlConnection(str);//实例化链接
                con.Open();//开启连接
                //string strcmd = "SELECT GUN_INFO_ID,GUN_TYPE,GUN_STATUS, FROM gun_info WHERE GUN_STATUS = '1' and GUNARK_ID = '" + Emergency.gunark_id + "'";
                string strcmd = "SELECT gi.GUN_INFO_ID,gi.GUN_TYPE,gi.GUN_STATUS,gpi.GUN_POSITION_INFO_ID,gpi.GUN_POSITION_NUMBER FROM gun_info gi left join gunark_position_info gpi on gi.GUN_INFO_ID = gpi.GUN_INFO_ID WHERE gi.GUN_STATUS = '1' and gi.GUNARK_ID = '" + Emergency.gunark_id + "'";
                MySqlCommand cmd = new MySqlCommand(strcmd, con);
                MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ada.Fill(ds);//查询结果填充数据集

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][1].Equals("1"))
                    {
                        ds.Tables[0].Rows[i][1] = "54式手枪";
                    }
                    else if (ds.Tables[0].Rows[i][1].Equals("2"))
                    {
                        ds.Tables[0].Rows[i][1] = "64式手枪";
                    }
                    else if (ds.Tables[0].Rows[i][1].Equals("3"))
                    {
                        ds.Tables[0].Rows[i][1] = "77式手枪";
                    }
                    else if (ds.Tables[0].Rows[i][1].Equals("4"))
                    {
                        ds.Tables[0].Rows[i][1] = "79式冲锋枪";

                    }
                    else if (ds.Tables[0].Rows[i][1].Equals("5"))
                    {
                        ds.Tables[0].Rows[i][1] = "97式防暴枪";
                    }
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][2].Equals("1"))
                    {
                        ds.Tables[0].Rows[i][2] = "置枪";
                    }
                    else
                    {
                        ds.Tables[0].Rows[i][2] = "未置枪";
                    }
                }

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[5].HeaderText = "枪位号";
                dataGridView1.Columns[2].HeaderText = "枪支类型";
                dataGridView1.Columns[3].HeaderText = "枪支状态";

                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                con.Close();//关闭连接
            }
            else
            {
                string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                MySqlConnection con = new MySqlConnection(str);//实例化链接
                con.Open();//开启连接
                string strcmd = "SELECT MAGAZINE_NUMBER, BULLET_MODEL, STOCK_QTY, MAGAZINE_INFO_ID from gunark_magazine_info where GUNARK_ID = '" + Emergency.gunark_id + "'";
                MySqlCommand cmd = new MySqlCommand(strcmd, con);
                MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                DataSet ds1 = new DataSet();
                ada.Fill(ds1);//查询结果填充数据集

                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    if (ds1.Tables[0].Rows[i][1].Equals("1"))
                    {
                        ds1.Tables[0].Rows[i][1] = "64式子弹";
                    }
                    else if (ds1.Tables[0].Rows[i][1].Equals("2"))
                    {
                        ds1.Tables[0].Rows[i][1] = "51式子弹";
                    }
                    else if (ds1.Tables[0].Rows[i][1].Equals("3"))
                    {
                        ds1.Tables[0].Rows[i][1] = "51式空爆弹";
                    }
                    else if (ds1.Tables[0].Rows[i][1].Equals("4"))
                    {
                        ds1.Tables[0].Rows[i][1] = "97式动能弹";

                    }
                    else
                    {
                        ds1.Tables[0].Rows[i][1] = "97式杀伤弹";
                    }
                }


                dataGridView1.DataSource = ds1.Tables[0];

                //dataGridView1.Columns[1].Visible = false;
                //dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[1].HeaderText = "弹仓号";
                dataGridView1.Columns[2].HeaderText = "子弹类型";
                dataGridView1.Columns[3].HeaderText = "子弹剩余量";
                dataGridView1.Columns[4].Visible = false;

                DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                acCode.Name = "acCode";
                acCode.DataPropertyName = "acCode";
                acCode.HeaderText = "取弹数量";
                dataGridView1.Columns.Add(acCode);

                con.Close();//关闭连接

            }
        }
        private void Emergencygun_Load(object sender, EventArgs e)
        {
            dataGridView1.Controls.Add(textBox1); 
            this.WindowState = FormWindowState.Maximized;
            this.dataGridView1.DefaultCellStyle.Font = new Font("宋体", 15);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15);
         

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = 0;
            //MessageBox.Show(e.RowIndex.ToString());
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
                if (check.Value == null || false.Equals(check.Value))
                    check.Value = true;
                else
                    check.Value = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //获取选中的行数
            int count = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                {
                    count++;
                }
            }

            Task_Execute_Detail.count = count;

            if (count == 0)
            {
                MessageBox.Show("未选择枪支！", "提示");
            }
            else
            {
                if (MessageBox.Show(this, "您确定要执行该任务么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    if (Emergency.gunark_type.Equals("枪柜"))
                    {
                        for (int i = 0, n = 0, k = 0, m = 0; i < dataGridView1.RowCount; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                            {
                                j++;
                                //获取gunid
                                emg_gunid[n] = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                n++;
                                //获取gunpositionid
                                emg_gunpositionid[k] = dataGridView1.Rows[i].Cells[4].Value.ToString();
                                k++;
                                //获取gunpositionnum
                                string gpn;
                                gpn = dataGridView1.Rows[i].Cells[5].Value.ToString();
                                try
                                {
                                    byte rows = (byte)int.Parse(gpn);
                                    emg_gunpositionnum[m] = rows;
                                    m++;
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    else
                    {
                        ///弹
                        ///
                        for (int i = 0, n = 0, k = 0, m = 0; i < dataGridView1.RowCount; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                            {
                                j++;
                                //获取qty1
                                qty1[n] = (int)dataGridView1.Rows[i].Cells[4].Value;
                                //获取取弹数量
                                try
                                {
                                    qtsl[n] = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value.ToString());
                                }
                                catch
                                {
                                    MessageBox.Show("第【"+ (i+1) +"】行没有填入取弹数量！", "提示");
                                    return;
                                }
                                if (qtsl[n] > qty1[n])
                                {
                                    MessageBox.Show("第【" + (i + 1) + "】行，取弹数量不能大于子弹剩余数量！", "提示");
                                    return;
                                }
                                n++;
                                //获取mgz_id1
                                mgz_id1[k] = dataGridView1.Rows[i].Cells[5].Value.ToString();
                                k++;
                                //获取magazinenum
                                string gpn;
                                gpn = dataGridView1.Rows[i].Cells[2].Value.ToString();
                                try
                                {
                                    byte rows = (byte)int.Parse(gpn);
                                    magazinenum[m] = rows;
                                    m++;
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                        //获取子弹类型
                        for (int iii = 0; iii < dataGridView1.Rows.Count; iii++)
                        {
                            string gpn = dataGridView1.Rows[iii].Cells[2].Value.ToString();
                            string gpn1 = "";
                            if (gpn.Equals("64式子弹"))
                            {
                                gpn1 = "1";

                            }
                            else if (gpn.Equals("51式子弹"))
                            {
                                gpn1 = "2";

                            }
                            else if (gpn.Equals("51式空爆弹"))
                            {
                                gpn1 = "3";

                            }
                            else if (gpn.Equals("97式动能弹"))
                            {
                                gpn1 = "4";

                            }
                            else if (gpn.Equals("97式杀伤弹"))
                            {
                                gpn1 = "5";

                            }

                            emg_bullettype[iii] = gpn1;
                        }
                    }
                }
                else
                {
                    return;
                }

                //将gunid 和 gunpositionid复制到相应的total中            
                emg_gunid.CopyTo(total_gunid, total_j);
                emg_gunpositionid.CopyTo(total_gunpositionid, total_j);
                //计总数
                total_j = total_j + j;

                frmMain.isGet = 9;

                Finger_Check fc = new Finger_Check();
                fc.ShowDialog();
                this.Close();
               
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.ToolTipIcon = ToolTipIcon.Info;
            t.ToolTipTitle = "提示";
            t.UseAnimation = true;
            t.IsBalloon = true;
            t.SetToolTip(this.button1,"点击确定按钮执行紧急任务");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = true;
                    }
                }
            }
            else
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue)
                        {
                            dataGridView1.Rows[i].Cells[0].Value = false;
                        }
                        else
                            dataGridView1.Rows[i].Cells[0].Value = true;
                    }
                }
            }
            
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            this.textBox1.Visible = false;
            this.textBox1.Width = 0;
            try
            {
                //前提是你的datagridview1一定要有数据呀
                if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText == "取弹数量")
                {
                    //以下是把把textbox1的位置与datagridview1的单元格的位置一样,不过长宽均比单元格的短2
                    this.textBox1.Left = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, true).Left+20;
                    this.textBox1.Top = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, true).Top+10;
                    this.textBox1.Width = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, true).Width - 40;
                    this.textBox1.Height = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, true).Height - 2;

                    string str = Convert.ToString(this.dataGridView1.CurrentCell.Value); this.textBox1.Text = str;//将单元格的值赋给文本框
                    this.textBox1.Visible = true;
                }
            }
            catch
            {
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            this.dataGridView1.CurrentCell.Value = this.textBox1.Text;//如果文本框输入内容后,把其值赋给其对应的单元格
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            } 
        }
       
    }
}
