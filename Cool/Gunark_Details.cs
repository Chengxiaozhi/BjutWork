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
    public partial class Gunark_Details : Form
    {
        //枪柜ID
        string g_id;
        string g_alarm;
        string g_doorStaus;
        List<byte> gunNumber = null;
        DataSet ds1 = new DataSet();
        DataSet ds = new DataSet();
        char[] GunStatus = null;
        string connectionSokKey = "";
        GunProtocol GunPro = new GunProtocol();
        Convert12 Cver12 = new Convert12();
        DBsotorage DBstor = new DBsotorage();
        string gunark_model = "";
        private bool isAlive = true;
        public Gunark_Details(string gunark_id)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            this.g_id = gunark_id;
        }

        public Gunark_Details(string[] status)
        {
            InitializeComponent();
            this.g_id = status[7];
            this.g_alarm = status[6];
            this.g_doorStaus = status[5];
            this.label6.Text = "主页->枪弹柜详情";
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
        }

        private void Gunark_Details_Load(object sender, EventArgs e)
        {
            //获取实时枪位
            backgroundWorker1.RunWorkerAsync();
            //显示枪柜信息
            this.WindowState = FormWindowState.Maximized;
            string str7 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con7 = new MySqlConnection(str7);//实例化链接
            con7.Open();//开启连接
            string strcmd7 = "SELECT GUNARK_NAME,GUNARK_TYPE,cast(GUNARK_STATUS as CHAR),GUNARK_IP FROM gunark_gunark WHERE GUNARK_ID = '" + g_id + "'";
            MySqlCommand cmd7 = new MySqlCommand(strcmd7, con7);
            MySqlDataAdapter ada7 = new MySqlDataAdapter(cmd7);
            DataSet ds7 = new DataSet();
            ada7.Fill(ds7);//查询结果填充数据集
            con7.Close();
            
            string gunark_name = ds7.Tables[0].Rows[0][0].ToString();//枪弹柜名称
            gunark_model = ds7.Tables[0].Rows[0][1].ToString();//枪弹柜类型
            string gunark_status = ds7.Tables[0].Rows[0][2].ToString();//枪弹柜状态
            string gunark_ip = ds7.Tables[0].Rows[0][3].ToString();//枪弹柜IP
            if (gunark_status.Equals("1"))
                gunark_status = "枪柜在用";
            else if (gunark_status.Equals("2"))
                gunark_status = "枪柜已注销";
            label3.Text = gunark_name;
            label5.Text = gunark_status;
            label11.Text = "已经联网";
            label13.Text = "外部电源";
            
            if (g_alarm != "")
            {
                label17.Text = g_alarm;
            }
            if (g_doorStaus != null)
            {
                label15.Text = g_doorStaus;
            }
            
        }

        /// <summary>
        /// 查询柜子的ip
        /// </summary>
        /// <param name="gunark_id"></param>
        /// <returns></returns>
        public char[] GetGunSt(string IpSocket)
        {
            char[] GunStatus = null;
            while (isAlive)
            {
                System.Threading.Thread.Sleep(3000);
                string check = GunPro.CheckStatus();
                string connectionSokKey = IpSocket;
                try
                {
                    frmMain.dictConn[connectionSokKey].Send(check);
                    System.Threading.Thread.Sleep(2000);
                }
                catch
                {
                    continue;
                }
                if (ConnectionClient.gunCheckSt != "")
                {
                    GunStatus = ConnectionClient.gunCheckSt.ToCharArray();
                    ConnectionClient.gunCheckSt = "";
                    break;
                }
            }
            return GunStatus;
        }

        private void listView1_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            //e.Item.ToolTipText = (e.Item.Index + 1).ToString();
            //ToolTip t = new ToolTip();
            //t.ToolTipIcon = ToolTipIcon.Info;
            //t.IsBalloon = true;
            //t.ShowAlways = false;
            ////t.UseAnimation = true;
            ////t.UseFading = true;
            //if (ds.Tables[0].Rows[e.Item.Index][2].ToString().IndexOf("枪") != -1)
            //{
            //    t.ToolTipTitle = "枪支信息";
            //    t.SetToolTip(this.listView1, "枪支类型：" + ds.Tables[0].Rows[e.Item.Index][2] + "  " + "枪支编号：" + ds.Tables[0].Rows[e.Item.Index][3]);
            //}
            //else
            //{
            //    t.ToolTipTitle = "子弹信息";
            //    t.SetToolTip(this.listView1, "子弹类型：" + ds.Tables[0].Rows[e.Item.Index][3]);
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.isAlive = false;
            this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            connectionSokKey = DBstor.getDBipSocket(g_id);
            ///
            //返回实时在位的枪支信息
            ///
            GunStatus = GetGunSt(connectionSokKey);
            byte[] data2 = new byte[24];
            for (int n = 0; n < 24; n++)
            {
                data2[n] = (byte)Cver12.ASCIItoHEX((byte)GunStatus[n]);
            }
            string[] data3 = Cver12.DECtoBIN(data2);
            string data4 = "";
            for (int n = 0; n < data3.Length; n++)
            {
                data4 = data4 + data3[n];

            }
            char[] data5 = data4.ToCharArray();
            gunNumber = new List<byte>();
            for (int j = 0; j < data5.Length; j++)
            {
                if (data5[j] == '1')
                {
                    byte gunbulletnumber = (byte)(j + 1);
                    gunNumber.Add(gunbulletnumber);
                }
            }

            while (true)
            {
                this.pictureBox4.Visible = false;
                if (gunark_model.Equals("枪柜"))
                {
                    //显示所选库的在位枪支
                    string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con = new MySqlConnection(str);//实例化链接
                    con.Open();//开启连接
                    string strcmd = "SELECT GUN_POSITION_NUMBER,GUN_POSITION_STATUS,gpi.GUN_TYPE,gi.GUN_NUMBER FROM gunark_position_info gpi LEFT JOIN gun_info gi on gi.GUN_INFO_ID = gpi.GUN_INFO_ID WHERE gpi.GUN_POSITION_STATUS != 0 and gpi.GUNARK_ID = '" + g_id + "' order by 0 + GUN_POSITION_NUMBER";
                    MySqlCommand cmd = new MySqlCommand(strcmd, con);
                    MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    ada.Fill(ds);//查询结果填充数据集
                    con.Close();

                    int count = ds.Tables[0].Rows.Count;//枪位数

                    byte[] gun_pos_num = new byte[count];
                    for (int i = 0; i < count; i++)
                    {
                        gun_pos_num[i] = Convert.ToByte(ds.Tables[0].Rows[i][0].ToString());
                    }

                    this.listView1.View = View.LargeIcon;

                    this.listView1.LargeImageList = this.imageList1;

                    this.listView1.BeginUpdate();

                    for (int i = 0; i < count; i++)
                    {
                        ListViewItem lvi = new ListViewItem();
                        if (gunNumber.Contains(gun_pos_num[i]))
                        {
                            lvi.ImageIndex = 1;
                        }
                        else
                            lvi.ImageIndex = 0;
                        // 枪号找对应枪型
                        lvi.Text = (int)gun_pos_num[i] + "\n" + ds.Tables[0].Rows[i][2] + "";
                        this.listView1.Items.Add(lvi);
                    }

                    this.listView1.EndUpdate();
                }
                else
                {
                    label1.Text = "弹仓状态信息:";
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = false;
                    label18.Visible = false;
                    label19.Visible = false;

                    //显示所选库的弹仓情况
                    string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con = new MySqlConnection(str);//实例化链接
                    con.Open();//开启连接
                    string strcmd = "SELECT MAGAZINE_NUMBER,STOCK_QTY,CAPACITY_QTY,BULLET_MODEL FROM gunark_magazine_info where GUNARK_ID = '" + g_id + "' order by 0 + MAGAZINE_NUMBER";
                    MySqlCommand cmd = new MySqlCommand(strcmd, con);
                    MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    ada.Fill(ds);//查询结果填充数据集

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i][3].Equals("1"))
                        {
                            ds.Tables[0].Rows[i][3] = "64式子弹";
                        }
                        else if (ds.Tables[0].Rows[i][3].Equals("2"))
                        {
                            ds.Tables[0].Rows[i][3] = "51式子弹";
                        }
                        else if (ds.Tables[0].Rows[i][3].Equals("3"))
                        {
                            ds.Tables[0].Rows[i][3] = "51式空爆弹";
                        }
                        else if (ds.Tables[0].Rows[i][3].Equals("4"))
                        {
                            ds.Tables[0].Rows[i][3] = "97式动能弹";

                        }
                        else
                        {
                            ds.Tables[0].Rows[i][3] = "97式杀伤弹";
                        }
                    }

                    int count = ds.Tables[0].Rows.Count;//弹仓数

                    this.listView1.View = View.Details;

                    this.listView1.Columns.Add("弹仓号", 120, HorizontalAlignment.Left); //一步添加 
                    this.listView1.Columns.Add("子弹类型", 120, HorizontalAlignment.Left); //一步添加 
                    this.listView1.Columns.Add("弹仓容量", 120, HorizontalAlignment.Left); //一步添加 
                    this.listView1.Columns.Add("实际数量", 120, HorizontalAlignment.Left); //一步添加 

                    listView1.SmallImageList = imageList1; //这里设置listView的SmallImageList ,用imgList将其撑大 

                    this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 

                    for (int i = 0; i < count; i++)
                    {
                        ListViewItem lvi = new ListViewItem();

                        lvi.ImageIndex = 2;//通过与imageList绑定，显示imageList中第i项图标 

                        lvi.Text = ds.Tables[0].Rows[i][0].ToString();

                        lvi.SubItems.Add(ds.Tables[0].Rows[i][3].ToString());
                        lvi.SubItems.Add(ds.Tables[0].Rows[i][1].ToString());
                        lvi.SubItems.Add(ds.Tables[0].Rows[i][2].ToString());

                        this.listView1.Items.Add(lvi);
                    }
                    this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。 
                }
                break;
            }
        }
    }
}
