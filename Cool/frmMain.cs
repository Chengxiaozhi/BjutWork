using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;//Endpoint
using System.Net.Sockets;//包含套接字
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
using System.Net.NetworkInformation;
namespace Cool
{
    public partial class frmMain : Form
    {
        //是否登录标志位
        public static bool IsLogin = false;
        //public int count = 0;
        //检测与远程服务器联网状态
        public static bool online = false; //是否在线
        private float mem = 0;
        Ping ping = new Ping();
        PingReply pingReply = null;

        //服务器Ip

        string ServiceIp;

        //柜子联网状态
        public static string [] Gunark_Online_State = new string[100];
        string[] gunark_ip = new string[100];

        //Timer time1 = new Timer();
        public static string gunark_query;
        public static string taskid;
        public static string taskbigtype;
        public static bool OpenDoorState = false;
        private int count = 0;
        public static int isGet = 0;//取还枪标志位，0取枪，1还枪，2报废封存调拨，3保养检查, 4枪支入柜
        //底层相关
        bool isWatch = true;
        public delegate void MyInvoke(string str);
        DBsotorage DBstor = new DBsotorage();
        GunProtocol GunPro = new GunProtocol();
        PrinterProtocol PtPro = new PrinterProtocol();
        DBprinter DbPt = new DBprinter();
        Convert12 Cver12 = new Convert12();
        Socket sokWatch = null;//负责监听（枪弹柜的） 客户端段 连接请求的  套接字
        Socket sokWatchPt = null;
        Socket sokMsg = null;
        public static Socket sokPtClient = null;//负责链接指纹仪服务器的套接字
        Thread threadWatch = null;//负责 调用套接字， 执行 监听（枪弹柜的）请求的线程
        Thread thrdWatPrinter = null; //负责 调用套接字， 执行 监听（指纹仪的）请求的线程
        Thread thrWatSever1 = null;// 作为（指纹仪）的客户端，接收指纹仪的消息的线程
        Thread thrWatSocket;// 负责检测socket断开的线程：2016/1/12
        public static Dictionary<string, ConnectionClient> dictConn = new Dictionary<string, ConnectionClient>();
        //底层定义结束
        //调用远程WebService接口实例化,第二个参数为服务器url地址，通过查找表gunark_config中的URL字段获取
        public static WebServices.GunServicesClient webService;

        public static Dictionary<string, string> userFinger = new Dictionary<string, string>();//值班员指纹
        public static Dictionary<string, string> managerFinger = new Dictionary<string, string>();//管理员指纹
        public static Dictionary<string, string> leaderFinger = new Dictionary<string, string>();//领导指纹
        private bool isSetSuccess = false;
        private string localip = GetLocalIP();
        private string printIp = "";
        public frmMain()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;//关闭跨线程修改控件检查

        }
        private void frmMain_Load(object sender, EventArgs e)
        {
           

            this.IsMdiContainer = true;//设置父窗体是容器
            printIp = getPrintIp();
            //获取服务器Ip

            string str7 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con7 = new MySqlConnection(str7);//实例化链接
            con7.Open();//开启连接
            string strcmd7 = "SELECT IP from gunark_config;";
            MySqlCommand cmd7 = new MySqlCommand(strcmd7, con7);
            MySqlDataAdapter ada7 = new MySqlDataAdapter(cmd7);
            DataSet ds7 = new DataSet();
            ada7.Fill(ds7);//查询结果填充数据集
            con7.Close();

            ServiceIp = ds7.Tables[0].Rows[0][0].ToString();

            //获取远程service链接
            webService = new WebServices.GunServicesClient("GunServicesImplPort", getServiceIp(ServiceIp));

            //开启服务
            StartGunSever();
            StartptSever();
            toPtServer_Click();

            //显示当前系统时间
            timer1.Start();

            //检测与远程服务器以及枪柜的联网状态
            backgroundWorker1.RunWorkerAsync();

            //程序启动默认加载主页
            HomePage hp = new HomePage();
            this.panel1.Controls.Clear();
            hp.FormBorderStyle = FormBorderStyle.None;
            hp.TopLevel = false;
            hp.Dock = System.Windows.Forms.DockStyle.Fill;
            hp.FormBorderStyle = FormBorderStyle.None;
            this.panel1.Controls.Add(hp);
            hp.Show();
            
        }

        //private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    StartGunSever();
        //    StartptSever();
        //    toPtServer_Click();
        //}
        /// <summary>
        /// 读取指纹仪IP
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
         private String getPrintIp()
        {
            String url = "";
            //将连接数据库的方法封装到DatabaseManger类中，该类设置为静态类，可直接用类名调用其方法。
            //链接数据库
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            con.Open();
            //SQL语句
            String strsql = "select gc.PRINTIP from gunark_config gc ";
            //查询数据库返回的DataSet
            DataSet ds = DatabaseManager.ExecAndGetDs(strsql, "gunark_config",con);
            //关闭数据库连接
            DatabaseManager.Clear(con);
            url = ds.Tables[0].Rows[0][0].ToString();
            return url;
        }
        //读取数据库表gunark_config中的ip字段
        public static String getServiceIp(String ip)
        {
            String url = "";
            //将连接数据库的方法封装到DatabaseManger类中，该类设置为静态类，可直接用类名调用其方法。
            //链接数据库
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            con.Open();
            //SQL语句
            String strsql = "select gc.URL from gunark_config gc where gc.IP = '" + ip + "'";
            //查询数据库返回的DataSet
            DataSet ds = DatabaseManager.ExecAndGetDs(strsql, "gunark_config",con);
            //关闭数据库连接
            DatabaseManager.Clear(con);
            url = ds.Tables[0].Rows[0][0].ToString();
            return url;
        }

       
        //显示当前系统时间
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            count++;
            if (count == 60)
            {
                 count = 0;
                 mem = GunTool.getMem();
            }
           
            if (mem > 200)
            {
                GunTool.ClearMemory();
                Console.WriteLine("--timex clr mm clear finish " );
            }
            timer1.Interval = 1000;
            TimeSpan ts = new TimeSpan(Environment.TickCount * TimeSpan.TicksPerMillisecond);
            string TempStr = string.Format("系统已经运行：{0:d2}天 {1:d2}时 {2:d2}分 {3:d2}秒", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
            DateTime dt = DateTime.Now;
            string TempStr2 = DateTime.Now.ToString("  系统当前时间：yyyy/MM/dd HH:mm:ss");
            //this.toolStripStatusLabel2.Text = TempStr + "   " + TempStr2;
            this.toolStripStatusLabel2.Text =TempStr2;

            //显示当前登录用户
            if (Login.user == "")
            {
                this.toolStripStatusLabel1.Text = "当前没有用户登录";
            }
            else
            {
                this.toolStripStatusLabel1.Text = "当前登录用户：" + Login.user;
            }

            //远程服务器联网状态
            if (online)
            {
                this.WebService_Status.Text = "【已连接远程服务器】";
            }
            else
            {
                this.WebService_Status.Text = "【连接远程服务器失败】";
            }
            //if (count == 600)
            //{
            //    Application.Exit();
            //    System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                
            //}
        }
        private void 任务查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            if (!IsLogin)
            {
                //MessageBox.Show("您还未登录系统，请登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (MessageBox.Show(this, "您还未登录系统，请登录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            else
            {
                this.panel1.Controls.Clear();
                Task_query tq = new Task_query();
                tq.FormBorderStyle = FormBorderStyle.None;
                tq.TopLevel = false;
                tq.Dock = System.Windows.Forms.DockStyle.Fill;
                tq.FormBorderStyle = FormBorderStyle.None;
                this.panel1.Controls.Add(tq);
                tq.Dock = DockStyle.Fill;
                tq.Show();
            }
        }

        private void 退出系统ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void 信息查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 主页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomePage hp = new HomePage();
        
            this.panel1.Controls.Clear();
            hp.FormBorderStyle = FormBorderStyle.None;
            hp.TopLevel = false;
            hp.Dock = System.Windows.Forms.DockStyle.Fill;
            hp.FormBorderStyle = FormBorderStyle.None;
            this.panel1.Controls.Add(hp);
            hp.Show();
            
        }

        private void 枪支信息查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            if (!IsLogin)
            {
                //MessageBox.Show("您还未登录系统，请登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (MessageBox.Show(this, "您还未登录系统，请登录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            else
            {
                //此处判断条件修改为包含“管理员”，最好的策略应是按照用户的 USER_PRIVIEGES 字段判断
                if (Login.user_privieges != "1")
                {
                    this.panel1.Controls.Clear();
                    Query01 q1 = new Query01();
                    q1.FormBorderStyle = FormBorderStyle.None;
                    q1.TopLevel = false;
                    q1.Dock = System.Windows.Forms.DockStyle.Fill;
                    q1.FormBorderStyle = FormBorderStyle.None;
                    this.panel1.Controls.Add(q1);
                    q1.Show();
                }
                else
                {
                    MessageBox.Show("您没有该模块的访问权限！");
                }
            }
        }

        private void 人员信息查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            if (!IsLogin)
            {
                //MessageBox.Show("您还未登录系统，请登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (MessageBox.Show(this, "您还未登录系统，请登录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            else
            {
                if (Login.user_privieges != "1")
                {
                    this.panel1.Controls.Clear();
                    Query02 q2 = new Query02();
                    q2.FormBorderStyle = FormBorderStyle.None;
                    q2.TopLevel = false;
                    q2.Dock = System.Windows.Forms.DockStyle.Fill;
                    q2.FormBorderStyle = FormBorderStyle.None;
                    this.panel1.Controls.Add(q2);
                    q2.Show();
                }
                else
                {
                    MessageBox.Show("您没有该模块的访问权限！");
                }
            }
        }

        private void 枪柜信息查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            if (!IsLogin)
            {
                //MessageBox.Show("您还未登录系统，请登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (MessageBox.Show(this, "您还未登录系统，请登录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            else
            {
                if (Login.user_privieges != "1")
                {
                    this.panel1.Controls.Clear();
                    Query03 q3 = new Query03();
                    q3.FormBorderStyle = FormBorderStyle.None;
                    q3.TopLevel = false;
                    q3.Dock = System.Windows.Forms.DockStyle.Fill;
                    q3.FormBorderStyle = FormBorderStyle.None;
                    this.panel1.Controls.Add(q3);
                    q3.Show();
                }
                else
                {
                    MessageBox.Show("您没有该模块的访问权限！");
                }
            }
        }
        private void 领取枪弹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            if (!IsLogin)
            {
                //MessageBox.Show("您还未登录系统，请登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (MessageBox.Show(this, "您还未登录系统，请登录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            else {
                Task_execute01 te = new Task_execute01();
                this.panel1.Controls.Clear();
                te.FormBorderStyle = FormBorderStyle.None;
                te.TopLevel = false;
                te.Dock = System.Windows.Forms.DockStyle.Fill;
                te.FormBorderStyle = FormBorderStyle.None;
                this.panel1.Controls.Add(te);
                te.Show();

            }

            
        }

        private void 归还枪弹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            if (!IsLogin)
            {
                //MessageBox.Show("您还未登录系统，请登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (MessageBox.Show(this, "您还未登录系统，请登录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            else
            {
                Task_execute02 te = new Task_execute02();
                this.panel1.Controls.Clear();
                te.FormBorderStyle = FormBorderStyle.None;
                te.TopLevel = false;
                te.Dock = System.Windows.Forms.DockStyle.Fill;
                te.FormBorderStyle = FormBorderStyle.None;
                this.panel1.Controls.Add(te);
                te.Show();
            }
            
        }

        private void 其他任务ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            if (!IsLogin)
            {
                //MessageBox.Show("您还未登录系统，请登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (MessageBox.Show(this, "您还未登录系统，请登录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            else
            {
                Task_execute03 te = new Task_execute03();
                this.panel1.Controls.Clear();
                te.FormBorderStyle = FormBorderStyle.None;
                te.TopLevel = false;
                te.Dock = System.Windows.Forms.DockStyle.Fill;
                te.FormBorderStyle = FormBorderStyle.None;
                this.panel1.Controls.Add(te);
                te.Show();
            }
            
        }

        private void 消除报警ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            if (!IsLogin)
            {
                //MessageBox.Show("您还未登录系统，请登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (MessageBox.Show(this, "您还未登录系统，请登录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            else
            {
                isGet = 11;
                Finger_Check f = new Finger_Check();
                f.ShowDialog();

                if (Finger_Check.isPassAlram)
                {
                    Finger_Check.isPassAlram = false;
                    Managenment_clear_alert mca = new Managenment_clear_alert();
                    this.panel1.Controls.Clear();
                    mca.FormBorderStyle = FormBorderStyle.None;
                    mca.TopLevel = false;
                    mca.Dock = System.Windows.Forms.DockStyle.Fill;
                    mca.FormBorderStyle = FormBorderStyle.None;
                    this.panel1.Controls.Add(mca);
                    mca.Show();
                }
            }
        }

        private void 日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            if (!IsLogin)
            {
                //MessageBox.Show("您还未登录系统，请登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (MessageBox.Show(this, "您还未登录系统，请登录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            else
            {
                Finger_Check.isPassAlram = false;
                Management_log ml = new Management_log();
                this.panel1.Controls.Clear();
                ml.FormBorderStyle = FormBorderStyle.None;
                ml.TopLevel = false;
                ml.Dock = System.Windows.Forms.DockStyle.Fill;
                ml.FormBorderStyle = FormBorderStyle.None;
                this.panel1.Controls.Add(ml);
                ml.Show();
            }
            
        }

        private void 紧急任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void 紧急取枪弹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            if (!IsLogin)
            {
                //MessageBox.Show("您还未登录系统，请登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (MessageBox.Show(this, "您还未登录系统，请登录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            else
            {
                Emergency em = new Emergency();
                this.panel1.Controls.Clear();
                em.FormBorderStyle = FormBorderStyle.None;
                em.TopLevel = false;
                em.Dock = System.Windows.Forms.DockStyle.Fill;
                em.FormBorderStyle = FormBorderStyle.None;
                this.panel1.Controls.Add(em);
                em.Show();
            }
        }

        private void 紧急还枪弹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            if (!IsLogin)
            {
                //MessageBox.Show("您还未登录系统，请登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (MessageBox.Show(this, "您还未登录系统，请登录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            else
            {
                ReturnEmergencyGun em = new ReturnEmergencyGun();
                this.panel1.Controls.Clear();
                em.FormBorderStyle = FormBorderStyle.None;
                em.TopLevel = false;
                em.Dock = System.Windows.Forms.DockStyle.Fill;
                em.FormBorderStyle = FormBorderStyle.None;
                this.panel1.Controls.Add(em);
                em.Show();
            }
        }


        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            DialogResult res = MessageBox.Show("请确认是否退出程序？",
                "提示",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                Application.Exit();
                frmMain.IsLogin = false;
            }
        }

        private void 重新登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            if (MessageBox.Show(this, "您确定要重新登录么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
            {
                Login login = new Login();
                login.ShowDialog();
            }
        }
        //底层代码
        private void StartGunSever()
        {
            //实例化 套接字 （ip4寻址协议，流式传输，TCP协议）
            sokWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //创建 ip对象
            IPAddress address = IPAddress.Parse(localip);
            //创建网络节点对象 包含 ip和port
            IPEndPoint endpoint = new IPEndPoint(address, int.Parse("8000"));
            //将 监听套接字  绑定到 对应的IP和端口
            sokWatch.Bind(endpoint);
            
            //设置 监听队列 长度为100(同时能够处理 100个连接请求)
            sokWatch.Listen(100);
            threadWatch = new Thread(StartWatch);
            threadWatch.IsBackground = true;
            threadWatch.Start();

            // judgeClient = new Thread(StartJgCt);
            // judgeClient.IsBackground = true;
            // judgeClient.Start();
            StorageSever_state.Text = "【启动枪柜服务器成功】";//2016.1.18新增加
            //txtShow.AppendText("启动枪柜服务器成功......\r\n");


        }
        private void StartptSever()
        {


            //实例化 套接字 （ip4寻址协议，流式传输，TCP协议）
            sokWatchPt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //创建 ip对象
            IPAddress address = IPAddress.Parse(localip);
            //创建网络节点对象 包含 ip和port
            IPEndPoint endpoint = new IPEndPoint(address, int.Parse("6402"));
            //将 监听套接字  绑定到 对应的IP和端口
            sokWatchPt.Bind(endpoint);
            //设置 监听队列 长度为100(同时能够处理 100个连接请求)
            sokWatchPt.Listen(10);
            thrdWatPrinter = new Thread(StartWatchPt);

            thrdWatPrinter.IsBackground = true;
            thrdWatPrinter.Start();
            prclient_state.Text = "【指纹客户端已连接】";
            //txtShow.AppendText("启动指纹服务器成功......\r\n");
        }

        #region 1.被线程调用 监听连接端口
        /// <summary>
        /// 被线程调用 监听连接端口
        /// </summary>
        void StartWatch()
        {
            while (isWatch)
            {//threadWatch.SetApartmentState(ApartmentState.STA);
                //监听 客户端 连接请求，但是，Accept会阻断当前线程
                sokMsg = sokWatch.Accept();//监听到请求，立即创建负责与该客户端套接字通信的套接字

                ConnectionClient connection = new ConnectionClient(sokMsg, ShowMsg, RemoveClientConnection);
                // AcceptThread(sokMsg);
                //将负责与当前连接请求客户端 通信的套接字所在的连接通信类 对象 装入集合
                dictConn.Add(sokMsg.RemoteEndPoint.ToString(), connection);
                //string IPport = sokMsg.RemoteEndPoint.ToString();
                //string ip = IPport.Substring(0, IPport.Length - 5);
                //DBstor.InsertDBip(ip);
                //DBstor.InsertDBipSocket(sokMsg.RemoteEndPoint.ToString(), ip);
                string IPport = sokMsg.RemoteEndPoint.ToString();
                string ip = IPport.Substring(0, IPport.Length - 5);
                DBstor.InsertDBip(ip);
                DBstor.UpdateDBipSocket(sokMsg.RemoteEndPoint.ToString(), ip);
                SetXinTiao();//设置心跳参数
                //将 通信套接字 加入 集合，并以通信套接字的远程IpPort作为键
                //dictSocket.Add(sokMsg.RemoteEndPoint.ToString(), sokMsg);
                //将 通信套接字的 客户端IP端口保存在下拉框里
                cboClient.Items.Add(sokMsg.RemoteEndPoint.ToString());
                ShowMsg(sokMsg.RemoteEndPoint.ToString() + "接收连接成功......");
                // newGun.Text = "sokMsg.RemoteEndPoint.ToString()柜子接入";
                //启动一个新线程，负责监听该客户端发来的数据
                //Thread threadConnection = new Thread(ReciveMsg);
                //threadConnection.IsBackground = true;
                //threadConnection.Start(sokMsg);
                thrWatSocket = new Thread(AcceptThread);
                thrWatSocket.IsBackground = true;
                thrWatSocket.Start();
            }
        }

        void StartWatchPt()
        {
            while (isWatch)
            {
                //threadWatch.SetApartmentState(ApartmentState.STA);
                //监听 客户端 连接请求，但是，Accept会阻断当前线程
                sokMsg = sokWatchPt.Accept();//监听到请求，立即创建负责与该客户端套接字通信的套接字

                ConnectionClient connection = new ConnectionClient(sokMsg, ShowMsg, RemoveClientConnection);
                // AcceptThread(sokMsg);
                //将负责与当前连接请求客户端 通信的套接字所在的连接通信类 对象 装入集合
                dictConn.Add(sokMsg.RemoteEndPoint.ToString(), connection);
                // DBstor.InsertDBipSocket(sokMsg.RemoteEndPoint.ToString(), "192.168.1.52");
                SetXinTiao();//设置心跳参数
                //将 通信套接字 加入 集合，并以通信套接字的远程IpPort作为键
                //dictSocket.Add(sokMsg.RemoteEndPoint.ToString(), sokMsg);
                //将 通信套接字的 客户端IP端口保存在下拉框里
                cboClient.Items.Add(sokMsg.RemoteEndPoint.ToString());
                ShowMsg(sokMsg.RemoteEndPoint.ToString() + "接收连接成功......");
                // newGun.Text = "sokMsg.RemoteEndPoint.ToString()柜子接入";
                //启动一个新线程，负责监听该客户端发来的数据
                //Thread threadConnection = new Thread(ReciveMsg);
                //threadConnection.IsBackground = true;
                //threadConnection.Start(sokMsg);
                thrWatSocket = new Thread(AcceptThread);
                thrWatSocket.IsBackground = true;
                thrWatSocket.Start();
            }
        }
        #endregion
        #region 2.被线程调用 监听客户端是否断开
        /// <summary>
        /// 被线程调用 监听客户端端开没有 2016/1/11新加
        /// </summary>
        public void AcceptThread()
        {
            // Thread.CurrentThread.IsBackground = true;
            // Thread.CurrentThread.IsBackground = true;
            Socket sokclient = sokMsg;
            while (true)
            {

                uint dummy = 0;
                byte[] inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
                BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);
                BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
                BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);
                try
                {
                    sokclient.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
                }
                catch
                {

                    // SendText.Text = ("有客户端连入");
                    //MessageBox.Show("hhhahahhahah");
                }
            }
        }




        private static void SetXinTiao()
        {
            //byte[] inValue = new byte[] { 1, 0, 0, 0, 0x20, 0x4e, 0, 0, 0xd0, 0x07, 0, 0 };// 首次探测时间20 秒, 间隔侦测时间2 秒
            byte[] inValue = new byte[] { 1, 0, 0, 0, 0x88, 0x13, 0, 0, 0xd0, 0x07, 0, 0 };// 首次探测时间5 秒, 间隔侦测时间2 秒
            // theSocket.IOControl(IOControlCode.KeepAliveValues, inValue, null);
        }
        #endregion
        #region 向文本框显示消息
        /// <summary>
        /// 向文本框显示消息
        /// </summary>
        /// <param name="msgStr">消息</param>
        public void ShowMsg(string msgStr)
        {
            //txtShow.Text = ">> " + DateTime.Now.ToString() + msgStr + "\r\n";
            // txtShow.Text = ">> " + sokMsg.RemoteEndPoint.ToString() + msgStr + "\r\n";
            //txtShow.Text = ">> " + sokMsg.RemoteEndPoint.ToString() + "柜子返回的数据为： " + msgStr;
        }
        #endregion
        #region 显示指纹文本框信息
        /// <summary>
        /// 向文本框显示消息
        /// </summary>
        /// <param name="msgStr">消息</param>
        public void ContPtSeSt(string connectState)
        {
            //if (txtShowPt.InvokeRequired)
            //{
            //    MyInvoke _myinvoke = new MyInvoke(ContPtSeSt);
            //    txtShowPt.Invoke(_myinvoke, new object[] { connectState });
            //}
            //else
            //{
            //    txtShowPt.Text = ">> " + "指纹仪返回的信息：" + connectState; 
            //}
            //txtShow.Text = ">> " + DateTime.Now.ToString() + msgStr + "\r\n";
            // txtShow.Text = ">> " + sokMsg.RemoteEndPoint.ToString() + msgStr + "\r\n";
            //txtShowPt.Text = ">> " + "指纹仪返回的信息：" + connectState;

        }
        #endregion

        bool isRec = true;//与客户端通信的套接字 是否 监听消息

       
        #region 2 移除与指定客户端的连接 +void RemoveClientConnection(string key)
        /// <summary>
        /// 移除与指定客户端的连接
        /// </summary>
        /// <param name="key">指定客户端的IP和端口</param>
        public void RemoveClientConnection(string key)
        {
            if (dictConn.ContainsKey(key))
            {
                dictConn.Remove(key);
                cboClient.Items.Remove(key);
            }
        }
        #endregion

        #region  
        /// <summary>
        /// 链接指纹服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toPtServer_Click()
        {
            sokPtClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string ipadd = printIp;
            int port = Convert.ToInt32("6401");
            IPEndPoint severPt = new IPEndPoint(IPAddress.Parse(ipadd), port);
            try
            {
                sokPtClient.Connect(severPt);
             
                //disconnect.Enabled = true;
                prSever_state.Text = "【已连接指纹服务器】     ";
            }
            catch (SocketException ePt)
            {
                prSever_state.Text = "【连接服务器失败】     ";
                MessageBox.Show("连接服务器失败  " + ePt.Message);
                return;
            }
            thrWatSever1 = new Thread(ListenPtSver);
            thrWatSever1.IsBackground = true;
            thrWatSever1.Start();
        }
        void ListenPtSver()
        {
            PrinterClient PtCt = new PrinterClient(sokPtClient, ContPtSeSt);
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
      
            byte[] data = PtPro.TextConnect();
            sokPtClient.Send(data);

        }

        private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void 系统管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            if (!IsLogin)
            {
                //MessageBox.Show("您还未登录系统，请登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (MessageBox.Show(this, "您还未登录系统，请登录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            else
            {
                //isGet = 11;
                //Finger_Check f = new Finger_Check();
                //f.ShowDialog();

                //if (true)
                //{
                    Finger_Check.isPassAlram = false;
                    Setting s = new Setting();
                    this.panel1.Controls.Clear();
                    s.FormBorderStyle = FormBorderStyle.None;
                    s.TopLevel = false;
                    s.Dock = System.Windows.Forms.DockStyle.Fill;
                    s.FormBorderStyle = FormBorderStyle.None;
                    this.panel1.Controls.Add(s);
                    s.Show();
                //}
            }
        }

        private void 人员录入ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            if (!IsLogin)
            {
                //MessageBox.Show("您还未登录系统，请登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (MessageBox.Show(this, "您还未登录系统，请登录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            else
            {
                User_input ui = new User_input();
                this.panel1.Controls.Clear();
                ui.FormBorderStyle = FormBorderStyle.None;
                ui.TopLevel = false;
                ui.Dock = System.Windows.Forms.DockStyle.Fill;
                ui.FormBorderStyle = FormBorderStyle.None;
                this.panel1.Controls.Add(ui);
                ui.Show();
            }
        }

        private void 指纹录入ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
            if (!IsLogin)
            {
                //MessageBox.Show("您还未登录系统，请登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (MessageBox.Show(this, "您还未登录系统，请登录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            else
            {
                Fingerprint_input fi = new Fingerprint_input();
                this.panel1.Controls.Clear();
                fi.FormBorderStyle = FormBorderStyle.None;
                fi.TopLevel = false;
                fi.Dock = System.Windows.Forms.DockStyle.Fill;
                fi.FormBorderStyle = FormBorderStyle.None;
                this.panel1.Controls.Add(fi);
                fi.Show();
            }
        }

        private void menuStrip1_ItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            if (!IsLogin && !e.ClickedItem.Text.Equals("退出系统") && !e.ClickedItem.Text.Equals("主页") && !e.ClickedItem.Text.Equals("任务查询") && !e.ClickedItem.Text.Equals("紧急任务") && !e.ClickedItem.Text.Equals("关于"))
            {
                //MessageBox.Show("您还未登录系统，请登录", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (MessageBox.Show(this, "您还未登录系统，请登录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            if (!e.ClickedItem.Text.Equals("主页"))
            {
                GunTool.IsContinue = false;
            }
            else
            {
                GunTool.IsContinue = true;
            }
        }

        public static string GetLocalIP()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取本机IP出错:" + ex.Message);
                return "";
            }
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //检测联网状态
            while (true)
            {
                try
                {
                    pingReply = ping.Send(ServiceIp);
                }
                catch(Exception m)
                { continue; }

                if (pingReply.Status == IPStatus.Success)
                {
                    //no 取未还 
                    //noo 取还
                    //down 未还已上传
                    //紧急枪弹调用接口
                    string str5 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con5 = new MySqlConnection(str5);//实例化链接
                    con5.Open();//开启连接
                    string strcmd5 = "SELECT * from gunark_emergency_task_info where IS_CALL = 'no' or IS_CALL = 'noo' or IS_CALL = 'down'";
                    MySqlCommand cmd5 = new MySqlCommand(strcmd5, con5);
                    MySqlDataAdapter ada5 = new MySqlDataAdapter(cmd5);
                    DataSet ds5 = new DataSet();
                    ada5.Fill(ds5);//查询结果填充数据集
                    con5.Close();//关闭连接
                    
                    if(ds5.Tables[0].Rows.Count > 0)
                    {
                        string[] taskStatus = new string[60];
                        string unid = ds5.Tables[0].Rows[0][2].ToString();
                        string[] emgid = new string[100];
                        string[] gunarkID = new string[60];
                        DateTime[] task_begin_time = new DateTime[60];
                        
                        string[] task_user1 = new string[60];
                        string[] task_user2 = new string[60];
                        string[] taskID = new string[60];
                        string[] taskdetailID = new string[60];
                        string[] gunID = new string[60];
                        string[] gunpositionID = new string[60];
                        string[] emg_flag = new string[60];
                        int[] stock_qty = new int[60];
                        string[] magazineID = new string[60];
                        string[] bulletType = new string[60];
                        string[] IS_CALL = new string[60];
                        for(int i =0; i<ds5.Tables[0].Rows.Count; i ++)
                        { 
                            gunarkID[i]= ds5.Tables[0].Rows[i][1].ToString();
                            task_begin_time[i] = (DateTime)ds5.Tables[0].Rows[i][7];
                            
                            task_user1[i] = ds5.Tables[0].Rows[i][8].ToString();
                            task_user2[i] = ds5.Tables[0].Rows[i][9].ToString();
                            taskID[i] = ds5.Tables[0].Rows[i][0].ToString();
                            emg_flag[i] = ds5.Tables[0].Rows[i][5].ToString();
                            taskStatus[i] = ds5.Tables[0].Rows[i][3].ToString();
                            IS_CALL[i]= ds5.Tables[0].Rows[i][10].ToString();
                            if (!IS_CALL[i].Equals("down"))
                            {
                                if (emg_flag[i].Equals("紧急取枪"))
                                {
                                    string str6 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                                    MySqlConnection con6 = new MySqlConnection(str6);//实例化链接
                                    con6.Open();//开启连接
                                    string strcmd6 = "SELECT * from gunark_emergency_task_info_detail where EMERGENCY_TASK_ID ='" + taskID[i] + "'";
                                    MySqlCommand cmd6 = new MySqlCommand(strcmd6, con6);
                                    MySqlDataAdapter ada6 = new MySqlDataAdapter(cmd6);
                                    DataSet ds6 = new DataSet();
                                    ada6.Fill(ds6);//查询结果填充数据集
                                    con6.Close();//关闭连接
                                    for (int j = 0; j < ds6.Tables[0].Rows.Count; j++)
                                    {
                                        taskdetailID[j] = ds6.Tables[0].Rows[j][0].ToString();
                                        gunID[j] = ds6.Tables[0].Rows[j][2].ToString();
                                        gunpositionID[j] = ds6.Tables[0].Rows[j][5].ToString();
                                    }
                                    emgid = frmMain.webService.setUrgentGunTask(unid, gunarkID[i], gunID, gunpositionID, task_user1[i], task_begin_time[i], task_user1[i], task_user2[i]);
                                    if (taskStatus[i].Equals("5"))
                                    {
                                        for (int j = 0; j < ds6.Tables[0].Rows.Count; j++)
                                        {
                                            frmMain.webService.setGunNotOnPosition(unid, gunarkID[i], gunpositionID[j]);
                                        }
                                        string str4 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                                        MySqlConnection con4 = new MySqlConnection(str4);//实例化链接
                                        con4.Open();//开启连接
                                        string strcmd4 = "UPDATE gunark_emergency_task_info SET IS_CALL='down' WHERE EMERGENCY_TASK_ID ='" + taskID[i] + "'";
                                        MySqlCommand cmd4 = new MySqlCommand(strcmd4, con4);
                                        MySqlDataAdapter ada4 = new MySqlDataAdapter(cmd4);
                                        DataSet ds4 = new DataSet();
                                        ada4.Fill(ds4);//查询结果填充数据集
                                        con4.Close();//关闭连接

                                        
                                    }
                                    else
                                    {
                                        DateTime[] task_end_time = new DateTime[60];
                                        task_end_time[i] = (DateTime)ds5.Tables[0].Rows[i][6];
                                        frmMain.webService.setFinishTask(emgid[0], task_end_time[i], task_user1[i], task_user2[i]);

                                        string str7 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                                        MySqlConnection con7 = new MySqlConnection(str7);//实例化链接
                                        con7.Open();//开启连接
                                        string strcmd7 = "UPDATE gunark_emergency_task_info SET IS_CALL='yes' WHERE EMERGENCY_TASK_ID ='" + taskID[i] + "'";
                                        MySqlCommand cmd7 = new MySqlCommand(strcmd7, con7);
                                        MySqlDataAdapter ada7 = new MySqlDataAdapter(cmd7);
                                        DataSet ds7 = new DataSet();
                                        ada7.Fill(ds7);//查询结果填充数据集
                                        con7.Close();
                                    }
                                    string str1 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                                    MySqlConnection con1 = new MySqlConnection(str1);//实例化链接
                                    con1.Open();//开启连接
                                    string strcmd1 = "UPDATE gunark_emergency_task_info SET EMERGENCY_TASK_ID='" + emgid[0] + "' WHERE EMERGENCY_TASK_ID ='" + taskID[i] + "'";
                                    MySqlCommand cmd1 = new MySqlCommand(strcmd1, con1);
                                    MySqlDataAdapter ada1 = new MySqlDataAdapter(cmd1);
                                    DataSet ds1 = new DataSet();
                                    ada1.Fill(ds1);//查询结果填充数据集
                                    con1.Close();//关闭连接
                                    string str3 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                                    MySqlConnection con3 = new MySqlConnection(str3);//实例化链接
                                    con3.Open();//开启连接
                                    string strcmd3 = "UPDATE gunark_emergency_task_info_detail SET EMERGENCY_TASK_ID='" + emgid[0] + "' WHERE EMERGENCY_TASK_ID ='" + taskID[i] + "'";
                                    MySqlCommand cmd3 = new MySqlCommand(strcmd3, con3);
                                    MySqlDataAdapter ada3 = new MySqlDataAdapter(cmd3);
                                    DataSet ds3 = new DataSet();
                                    ada3.Fill(ds3);//查询结果填充数据集
                                    con3.Close();//关闭连接
                                }
                                else
                                {
                                    string str6 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                                    MySqlConnection con6 = new MySqlConnection(str6);//实例化链接
                                    con6.Open();//开启连接
                                    string strcmd6 = "SELECT * from gunark_emergency_task_info_detail e LEFT JOIN gunark_magazine_info m on m.MAGAZINE_INFO_ID = e.MAGAZINE_INFO_ID where EMERGENCY_TASK_ID ='" + taskID[i] + "'";
                                    MySqlCommand cmd6 = new MySqlCommand(strcmd6, con6);
                                    MySqlDataAdapter ada6 = new MySqlDataAdapter(cmd6);
                                    DataSet ds6 = new DataSet();
                                    ada6.Fill(ds6);//查询结果填充数据集
                                    con6.Close();//关闭连接
                                    for (int j = 0; j < ds6.Tables[0].Rows.Count; j++)
                                    {
                                        magazineID[j] = ds6.Tables[0].Rows[j][7].ToString();
                                        bulletType[j] = ds6.Tables[0].Rows[j][18].ToString();
                                       // stock_qty[j] = int.Parse(ds6.Tables[0].Rows[j][14].ToString());
                                        stock_qty[j] =  Convert.ToInt32(ds6.Tables[0].Rows[j][14].ToString());
                                    }
                                    emgid = frmMain.webService.setUrgentBulletTask(unid, gunarkID[i], magazineID, bulletType, stock_qty, task_user1[i], task_begin_time[i], task_user1[i], task_user2[i]);
                                    if (taskStatus[i].Equals("6"))
                                    {
                                        DateTime[] task_end_time = new DateTime[60];
                                        task_end_time[i] = (DateTime)ds5.Tables[0].Rows[i][6];
                                        frmMain.webService.setFinishTask(emgid[0], task_end_time[i], task_user1[i], task_user2[i]);

                                        string str7 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                                        MySqlConnection con7 = new MySqlConnection(str7);//实例化链接
                                        con7.Open();//开启连接
                                        string strcmd7 = "UPDATE gunark_emergency_task_info SET IS_CALL='yes' WHERE EMERGENCY_TASK_ID ='" + taskID[i] + "'";
                                        MySqlCommand cmd7 = new MySqlCommand(strcmd7, con7);
                                        MySqlDataAdapter ada7 = new MySqlDataAdapter(cmd7);
                                        DataSet ds7 = new DataSet();
                                        ada7.Fill(ds7);//查询结果填充数据集
                                        con7.Close();
                                    }
                                    else
                                    {
                                        string str4 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                                        MySqlConnection con4 = new MySqlConnection(str4);//实例化链接
                                        con4.Open();//开启连接
                                        string strcmd4 = "UPDATE gunark_emergency_task_info SET IS_CALL='down' WHERE EMERGENCY_TASK_ID ='" + taskID[i] + "'";
                                        MySqlCommand cmd4 = new MySqlCommand(strcmd4, con4);
                                        MySqlDataAdapter ada4 = new MySqlDataAdapter(cmd4);
                                        DataSet ds4 = new DataSet();
                                        ada4.Fill(ds4);//查询结果填充数据集
                                        con4.Close();//关闭连接
 
                                    }
                                    string str1 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                                    MySqlConnection con1 = new MySqlConnection(str1);//实例化链接
                                    con1.Open();//开启连接
                                    string strcmd1 = "UPDATE gunark_emergency_task_info SET EMERGENCY_TASK_ID='" + emgid[0] + "' WHERE EMERGENCY_TASK_ID ='" + taskID[i] + "'";
                                    MySqlCommand cmd1 = new MySqlCommand(strcmd1, con1);
                                    MySqlDataAdapter ada1 = new MySqlDataAdapter(cmd1);
                                    DataSet ds1 = new DataSet();
                                    ada1.Fill(ds1);//查询结果填充数据集
                                    con1.Close();//关闭连接
                                    string str3 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                                    MySqlConnection con3 = new MySqlConnection(str3);//实例化链接
                                    con3.Open();//开启连接
                                    string strcmd3 = "UPDATE gunark_emergency_task_info_detail SET EMERGENCY_TASK_ID='" + emgid[0] + "' WHERE EMERGENCY_TASK_ID ='" + taskID[i] + "'";
                                    MySqlCommand cmd3 = new MySqlCommand(strcmd3, con3);
                                    MySqlDataAdapter ada3 = new MySqlDataAdapter(cmd3);
                                    DataSet ds3 = new DataSet();
                                    ada3.Fill(ds3);//查询结果填充数据集
                                    con3.Close();//关闭连接
                                }
                            }
                            else
                            {
                                if (taskStatus[i].Equals("6"))
                                {
                                    if (emg_flag.Equals("紧急取枪"))
                                    {
                                        DateTime[] task_end_time = new DateTime[60];
                                        task_end_time[i] = (DateTime)ds5.Tables[0].Rows[i][6];
                                        frmMain.webService.setFinishTask(taskID[i], task_end_time[i], task_user1[i], task_user2[i]);
                                        string str2 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                                        MySqlConnection con2 = new MySqlConnection(str2);//实例化链接
                                        con2.Open();//开启连接
                                        string strcmd2 = "SELECT * from gunark_emergency_task_info_detail where EMERGENCY_TASK_ID ='" + taskID[i] + "'";
                                        MySqlCommand cmd2 = new MySqlCommand(strcmd2, con2);
                                        MySqlDataAdapter ada2 = new MySqlDataAdapter(cmd2);
                                        DataSet ds2 = new DataSet();
                                        ada2.Fill(ds2);//查询结果填充数据集
                                        con2.Close();//关闭连接
                                        for (int m = 0; m < ds2.Tables[0].Rows.Count; m++)
                                        {
                                            frmMain.webService.setGunOnPosition(unid, gunarkID[i],ds2.Tables[0].Rows[m][5].ToString());
                                        }
                                    }
                                    else
                                    {
                                        DateTime[] task_end_time = new DateTime[60];
                                        task_end_time[i] = (DateTime)ds5.Tables[0].Rows[i][6];
                                        frmMain.webService.setFinishTask(taskID[i], task_end_time[i], task_user1[i], task_user2[i]);
 
                                    }
                                    string str7 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                                    MySqlConnection con7 = new MySqlConnection(str7);//实例化链接
                                    con7.Open();//开启连接
                                    string strcmd7 = "UPDATE gunark_emergency_task_info SET IS_CALL='yes' WHERE EMERGENCY_TASK_ID ='" + taskID[i] + "'";
                                    MySqlCommand cmd7 = new MySqlCommand(strcmd7, con7);
                                    MySqlDataAdapter ada7 = new MySqlDataAdapter(cmd7);
                                    DataSet ds7 = new DataSet();
                                    ada7.Fill(ds7);//查询结果填充数据集
                                    con7.Close();
                                }
 

                            }
                        }
                        
                            
                       
                    }
                    
                    online = true;
                    
                
                }
                else
                    online = false;
                //遍历menustrip主菜单
                foreach (ToolStripMenuItem ctrl in menuStrip1.Items)
                {
                    if (!online)
                        ctrl.Enabled = false;
                    else
                        ctrl.Enabled = true;
                }
                this.主页ToolStripMenuItem.Enabled = true;
                this.紧急任务ToolStripMenuItem.Enabled = true;
                this.退出系统ToolStripMenuItem1.Enabled = true;

                System.Threading.Thread.Sleep(60000);
            }
        }

        protected override void WndProc(ref Message m)
        {
            //拦截双击标题栏、移动窗体的系统消息  
            if (m.Msg != 0xA3 && m.Msg != 0x0003 && m.WParam != (IntPtr)0xF012)
            {
                base.WndProc(ref m);
            }
        }

        private void 枪弹库管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Content = "版本号：" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\n";
           
            MessageBox.Show(Content, "库室操作系统", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void 任务执行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunTool.IsContinue = false;
        }

        
    }
}
