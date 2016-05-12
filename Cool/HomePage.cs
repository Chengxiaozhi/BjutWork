using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;

namespace Cool
{
    public partial class HomePage : Form
    {
        public static string unitid;
        public static int alarmcount = 0;
      
        DBsotorage DBstor = new DBsotorage();
        GunProtocol GunPro = new GunProtocol();
        Convert12 Cver12 = new Convert12();
        DataSet ds = new DataSet();
        List<byte> gunNumber = null;
        //柜子数量
        int count = 0;
        MyControl.myControl[] myControls = null;
        string[,] args = null;
        string[] gunark_ipSocket = null;
        string[] gunark_name = null;
        public HomePage()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer|ControlStyles.ResizeRedraw|ControlStyles.SupportsTransparentBackColor, true);
            flowLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(flowLayoutPanel1,true,null);
        }
        /// <summary>
        /// 创建柜子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomePage_Load(object sender, EventArgs e)
        {
            //同步柜子ip
            this.updateIP();

            //获取柜子状态
            string sql = "SELECT gg.GUNARK_NAME,gg.GUNARK_TYPE,gg.GUNARK_ID,gg.GUNARK_IP, ggs.GUNARK_SOCKET from gunark_gunark gg left join gunark_gunark_storage ggs on gg.GUNARK_ID = ggs.GUNARK_ID where gg.GUNARK_STATUS = 1 order by gg.GUNARK_ENTERTIME";
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            con.Open();
            ds = DatabaseManager.ExecAndGetDs(sql,"gunark_gunark",con);
            DatabaseManager.Clear(con);

            count = ds.Tables[0].Rows.Count;

            myControls = new MyControl.myControl[count];
            
            gunark_ipSocket = new string[count];
            gunark_name = new string[count];
            for (int i = 0; i < count; i++)
            {
                gunark_ipSocket[i] = ds.Tables[0].Rows[i][3].ToString();
                gunark_name[i] = ds.Tables[0].Rows[i][0].ToString(); 
            }
            //创建自定义控件
            for (int i = 0; i < count; i++)
            {
                //调用底层代码获取枪柜状态信息，参数列表：args【0-6】分别为args【枪柜名称，枪柜编号，封存状态，联网状态，电源状态，开门状态，报警，枪柜ID】
                string[] state = { ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), "封存", "", "", "", "", ds.Tables[0].Rows[i][2].ToString() };
                myControls[i] = new MyControl.myControl(state);
                myControls[i].Dock = DockStyle.Fill;
                myControls[i].BackColor = TransparencyKey;
                myControls[i].BorderStyle = BorderStyle.FixedSingle;
                myControls[i].Anchor = AnchorStyles.None;
                myControls[i].BackColor = Color.Transparent;
                myControls[i].ResumeLayout(false);
                myControls[i].DoubleClick += new EventHandler(UC_DoubleClick);
                flowLayoutPanel1.Controls.Add(myControls[i]);
            }

            string str1 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con1 = new MySqlConnection(str1);//实例化链接
            con1.Open();//开启连接
            string strcmd1 = "SELECT UNITINFO_CODE, GUNARK_ID FROM gunark_gunark";
            MySqlCommand cmd1 = new MySqlCommand(strcmd1, con1);
            MySqlDataAdapter ada1 = new MySqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            ada1.Fill(ds1);//查询结果填充数据集
            con1.Close();
            if(ds1.Tables[0].Rows.Count != 0)
                unitid = ds1.Tables[0].Rows[0][0].ToString();

            //自刷新柜子状态
            backgroundWorker2.RunWorkerAsync();

        }

        //同步柜子ip
        private void updateIP()
        {
            MySqlConnection con5 = null;
            con5 = DatabaseManager.GetCon(con5);
            con5.Open();
            string sql = "update gunark_gunark set GUNARK_IP = GUNARK_PICURL where GUNARK_NUMOFBULLET = '1'";
            DatabaseManager.Exec(sql,con5);
            DatabaseManager.Clear(con5);
        }

        /// <summary>
        /// 获取柜子参数后台进程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

            while (GunTool.IsContinue)
            {
              
                args = new string[count, 8];
                for (int i = 0; i < count; i++)
                {
                   // myControls[i].Enabled = false;
                    string IpSocket = DBstor.getDBip(gunark_ipSocket[i]);
                    string require = GunPro.Require();

                    string connectionSokKey = IpSocket;
                    try
                    {
                        frmMain.dictConn[connectionSokKey].Send(require);
                        System.Threading.Thread.Sleep(2000);
                    }
                    catch
                    {
                        continue;
                    }
                    if (ConnectionClient.gunStatus != "")
                    {
                        char[] GunStatus = null;
                        char[] doorStatus = null;
                        try
                        {
                           // GunStatus = ConnectionClient.gunStatus.Substring(0, 12).ToCharArray();
                            doorStatus = ConnectionClient.gunStatus.Substring(24, 2).ToCharArray();
                        }
                        catch
                        {
                            continue;
                        }
                        ConnectionClient.gunStatus = "";
                        //将柜子返回的信息转换为相应状态

                        int cc1 = Cver12.StrToInt(doorStatus[0], doorStatus[1]);
                        string cc2 = Convert.ToString(cc1, 2);
                        char[] alarmCC = cc2.ToCharArray();

                        if (alarmCC[7] == '1')
                            this.args[i, 0] = "已经打开";
                        else
                            this.args[i, 0] = "门已关闭";

                        if (alarmCC[6] == '1')
                        {

                            if (alarmcount == 0)
                            {
                                DateTime now = DateTime.Now;
                                //WebReference.gunServices client = new Bullet.WebReference.gunServices();
                                string alarm_id = frmMain.webService.setAlarm(gunark_name[i], unitid, "非法开门", now, "非法开门");
                                //rizhi
                                string str4 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                                MySqlConnection con4 = new MySqlConnection(str4);//实例化链接
                                con4.Open();//开启连接
                                string strcmd4 = "insert into gunark_log (GUNARK_LOG_TIME,GUNARK_LOG_TYPE,GUNARK_ALARM,GUNARK_STATUS,GUNARK_ALARM_ID1,GUNARK_ID)values('" + now + "'," + "'日常报警'" + "," + "'非法开门'" + ",0,'" + alarm_id + "','" + gunark_ipSocket[i] + "')";
                                MySqlCommand cmd4 = new MySqlCommand(strcmd4, con4);
                                MySqlDataAdapter ada4 = new MySqlDataAdapter(cmd4);
                                DataSet ds4 = new DataSet();
                                ada4.Fill(ds4);//查询结果填充数据集
                                //dataGridView1.DataSource = ds.Tables[0];
                                //a = dataGridView1.CurrentRow("TASK_ID").ToString;
                                con4.Close();//关闭连接
                                //MessageBox.Show("请注意库室中的"+gunark_name[i]+"柜子有非法开门操作");
                                alarmcount++;
                            }
                            this.args[i, 1] = "非法开门";
                            
                        }
                        if (alarmCC[5] == '1')
                            this.args[i, 2] = "开门超时报警";
                        if (alarmCC[4] == '1')
                            this.args[i, 3] = "超温报警";
                        if (alarmCC[3] == '1')
                            this.args[i, 4] = "应急锁报警";
                        if (alarmCC[2] == '1')
                            this.args[i, 5] = "已联机";
                        else
                            this.args[i, 5] = "未联机";
                        if (alarmCC[0] == '1')
                            this.args[i, 6] = "正常供电";
                        else
                            this.args[i, 6] = "内部电源";
                        //Thread.Sleep(500);
                       
                        //设置自定义控件参数
                        myControls[i].setDoor_status(this.args[i,0]);
                        myControls[i].setConnect_status(this.args[i, 5]);
                        myControls[i].setAlerm_status(args[i, 1] + args[i, 2] + args[i, 3] + args[i, 4]);
                        myControls[i].setPower_status(this.args[i,6]);
                        //6frmMain.webService.setAlarm();
                        /////
                        ////返回实时在位的枪支信息
                        /////
                        //GunStatus = GetGunSt(connectionSokKey);
                        //byte[] data2 = new byte[24];
                        //for (int n = 0; n < 24; n++)
                        //{
                        //    data2[n] = (byte)Cver12.ASCIItoHEX((byte)GunStatus[n]);
                        //}
                        //string[] data3 = Cver12.DECtoBIN(data2);
                        //string data4 = "";
                        //for (int n = 0; n < data3.Length; n++)
                        //{
                        //    data4 = data4 + data3[n];

                        //}
                        //char[] data5 = data4.ToCharArray();
                        //gunNumber = new List<byte>();
                        //for (int j = 0; j < data5.Length; j++)
                        //{
                        //    if (data5[j] == '1')
                        //    {
                        //        byte gunbulletnumber = (byte)(j + 1);
                        //        gunNumber.Add(gunbulletnumber);
                        //    }
                        //}

                       // myControls[i].Enabled = true;
                        Thread.Sleep(5000);
        
                       // ConnectionClient.gunStatus = "";
                    }
                }
                //Console.WriteLine("输出：" + args[0, 0] + args[1, 0] + args[2, 0] + args[3, 0] + args[4, 0] + args[5, 0]);
            }
        }
        /// <summary>
        /// 查询柜子的ip
        /// </summary>
        /// <param name="gunark_id"></param>
        /// <returns></returns>
        //public char[] GetGunSt(string IpSocket)
        //{
        //    char[] GunStatus = null;
        //    while (true)
        //    {
        //        System.Threading.Thread.Sleep(3000);
        //        string check = GunPro.CheckStatus();
                
                

        //        string connectionSokKey = IpSocket;
        //        try
        //        {
        //            frmMain.dictConn[connectionSokKey].Send(check);
        //            System.Threading.Thread.Sleep(2000);
        //        }
        //        catch
        //        {
        //            continue;
        //        }
        //        if (ConnectionClient.gunCheckSt != "")
        //        {
        //            GunStatus = ConnectionClient.gunCheckSt.ToCharArray();
        //            ConnectionClient.gunCheckSt = "";
        //            break;
        //        }
        //    }
        //    return GunStatus;
        //}
        /// <summary>
        /// 柜子双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UC_DoubleClick(object sender,EventArgs e)
        {
            //if (gunNumber == null)
            //{
            //    MessageBox.Show("正在刷新，请稍候再试！！");
            //}
            //else
            //{
            //    MyControl.myControl uc = (MyControl.myControl)sender;
            //    Gunark_Details gd = new Gunark_Details(uc.Gunark_Args);
            //    //gunNumber = null;
            //    gd.FormBorderStyle = FormBorderStyle.None;
            //    gd.TopLevel = false;
            //    gd.Dock = System.Windows.Forms.DockStyle.Fill;
            //    gd.FormBorderStyle = FormBorderStyle.None;
            //    this.myPanel1.Controls.Add(gd);
            //    gd.Dock = DockStyle.Fill;
            //    gd.Show();
            //}
            MyControl.myControl uc = (MyControl.myControl)sender;
            Gunark_Details gd = new Gunark_Details(uc.Gunark_Args);
            //gunNumber = null;
            gd.FormBorderStyle = FormBorderStyle.None;
            gd.TopLevel = false;
            gd.Dock = System.Windows.Forms.DockStyle.Fill;
            gd.FormBorderStyle = FormBorderStyle.None;
            this.myPanel1.Controls.Add(gd);
            gd.Dock = DockStyle.Fill;
            gd.Show();
        }
        /// <summary>
        /// 调用webservice接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //frmMain.webService.setAlarm(Homepage.gunarkid, Homepage.unitid, alarmtype, now, true, "应急锁报警");

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void myPanel1_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
