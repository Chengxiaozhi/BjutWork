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
    public partial class Emergencysuccess : Form
    {
        public static string user1 = Convert12.ByteArrToStr(ConnectionClient.CheckuserGroup0);
        public static string user2 = Convert12.ByteArrToStr(ConnectionClient.CheckuserGroup1);
        //新加
        public static string[] emg_storage = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

        public static int emg_j;
        public static string emg_noclosedoor;
        public static DateTime now;
        public static string leader_finger = "";
        public static string emg_finger1 = "";
        public static string emg_finger2 = "";

        GunProtocol GunPro = new GunProtocol();
        DBsotorage DBstor = new DBsotorage();
        DBgunStatus DbSt = new DBgunStatus();
        string unitid;
        string gunarkid;
        string[] emg_gunid = new string[100];
        byte[] emg_gunpositionnum = new byte[100];
        byte[] emg_magazinenum = new byte[100];
        string[] emg_gunpositionid = new string[100];
        int j;
        string uuid = "";
        public Emergencysuccess()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);

            //增加一条紧急任务
            //生成任务ID
            //DatabaseManager.GetCon();
            if (Emergency.gunark_type.Equals("枪柜"))
            {
                string sql = "select replace(uuid(), '-', '') from dual;";
                MySqlConnection con = null;
                con = DatabaseManager.GetCon(con);
                con.Open();
                DataSet ds = DatabaseManager.ExecAndGetDs(sql, "dual",con);
                uuid = ds.Tables[0].Rows[0][0].ToString();
                DatabaseManager.Clear(con);

                MySqlConnection con1 = null;
                con1 = DatabaseManager.GetCon(con1);
                con1.Open();
                string sql_01 = "insert into gunark_emergency_task_info (EMERGENCY_TASK_ID,GUNARK_ID,UNIT_ID,TASK_STATUS,TASK_BIGTYPE,TASK_SMALLTYPE,TASK_BEGIN_TIME,TASK_USER1,TASK_USER2) values ('" + uuid + "','" + Emergency.gunark_id + "','" + Emergency.unit_id + "',5,3,'紧急取枪','" + DateTime.Now + "','" + Utils.getUserPoliceNumById(user1) + "','" + Utils.getUserPoliceNumById(user2) + "')";
                DatabaseManager.Exec(sql_01,con1);
                DatabaseManager.Clear(con1);

                //生成任务detail
                for (int i = 0; i < Emergencygun.j; i++)
                {
                    MySqlConnection con2 = null;
                    con2 = DatabaseManager.GetCon(con2);
                    con2.Open();
                    string sql_02 = "insert into gunark_emergency_task_info_detail (EMERGENCY_TASK_DETAIL_ID,EMERGENCY_TASK_ID,GUN_INFO_ID,GUN_POSITION_INFO_ID,GUNARK_ID,UNIT_ID)" +
                        " values (replace(uuid(), '-', ''),'" + uuid + "','" + Emergencygun.emg_gunid[i] + "','" + Emergencygun.emg_gunpositionid[i] + "','"+Emergency.gunark_id + "','" + Emergency.unit_id +"')";
                    DatabaseManager.Exec(sql_02,con2);
                    DatabaseManager.Clear(con2);
                }

                //更改枪支状态
                for (int i = 0; i < Emergencygun.j; i++)
                {
                    MySqlConnection con3 = null;
                    con3 = DatabaseManager.GetCon(con3);
                    con3.Open();//开启连接
                    string strcmd1 = "update gun_info set GUN_STATUS =3 where GUN_INFO_ID ='" + Emergencygun.emg_gunid[i] + "'";
                    DatabaseManager.Exec(strcmd1,con3);
                    DatabaseManager.Clear(con3);
                }

                //更改枪柜状态
                for (int i = 0; i < Emergencygun.j; i++)
                {
                    string str2 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con2 = new MySqlConnection(str2);//实例化链接
                    con2.Open();//开启连接
                    string strcmd2 = "update gunark_position_info set GUN_POSITION_STATUS =2 where GUN_POSITION_INFO_ID ='" + Emergencygun.emg_gunpositionid[i] + "'";
                    MySqlCommand cmd2 = new MySqlCommand(strcmd2, con2);
                    MySqlDataAdapter ada2 = new MySqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    ada2.Fill(ds2);//查询结果填充数据集
                    con2.Close();//关闭连接
                   // emg_storage[i] = Emergencygun.emg_gunpositionid[i]; 20160509

                }
            }
            else
            {
                MySqlConnection con4 = null;
                con4 = DatabaseManager.GetCon(con4);
                con4.Open();
                string sql = "select replace(uuid(), '-', '') from dual;";
                DataSet ds = DatabaseManager.ExecAndGetDs(sql, "dual",con4);
                uuid = ds.Tables[0].Rows[0][0].ToString();
                DatabaseManager.Clear(con4);

                MySqlConnection con5 = null;
                con5 = DatabaseManager.GetCon(con5);
                con5.Open();
                string sql_01 = "insert into gunark_emergency_task_info (EMERGENCY_TASK_ID,GUNARK_ID,UNIT_ID,TASK_STATUS,TASK_BIGTYPE,TASK_SMALLTYPE,TASK_BEGIN_TIME,TASK_USER1,TASK_USER2) values ('" + uuid + "','" + Emergency.gunark_id + "','" + Emergency.unit_id + "',5,3,'紧急取弹','" + DateTime.Now +"','" + Utils.getUserPoliceNumById(user1) + "','" + Utils.getUserPoliceNumById(user2) +  "')";
                DatabaseManager.Exec(sql_01,con5);
                DatabaseManager.Clear(con5);

                //生成任务detail
                for (int i = 0; i < Emergencygun.j; i++)
                {
                    if (!Emergencygun.mgz_id1[i].Equals(""))
                    {
                        MySqlConnection con6 = null;
                        con6 = DatabaseManager.GetCon(con6);
                        con6.Open();
                        string sql_02 = "insert into gunark_emergency_task_info_detail (EMERGENCY_TASK_DETAIL_ID,EMERGENCY_TASK_ID,MAGAZINE_INFO_ID,APPLY_BULLET_QTY,GUNARK_ID,UNIT_ID,BULLET_TYPE)" +
                            " values (replace(uuid(), '-', ''),'" + uuid + "','" + Emergencygun.mgz_id1[i] + "','" + Emergencygun.qtsl[i] + "','" + Emergency.gunark_id + "','" + Emergency.unit_id + "','" + Emergencygun.emg_bullettype[i] + "')";
                        DatabaseManager.Exec(sql_02, con6);
                        DatabaseManager.Clear(con6);
                    }
                }

                //更新现存子弹数量

                for (int i = 0; i < Emergencygun.j; i++)
                {
                    try
                    {
                        string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                        MySqlConnection con = new MySqlConnection(str);//实例化链接
                        con.Open();//开启连接
                        string strcmd = "UPDATE gunark_magazine_info set STOCK_QTY = STOCK_QTY-" + Emergencygun.qtsl[i] + " WHERE MAGAZINE_INFO_ID ='" + Emergencygun.mgz_id1[i] + "'";
                        MySqlCommand cmd = new MySqlCommand(strcmd, con);
                        MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                        DataSet ds8 = new DataSet();
                        ada.Fill(ds8);//查询结果填充数据集
                        //dataGridView1.DataSource = ds.Tables[0];
                        //a = dataGridView1.CurrentRow("TASK_ID").ToString;
                        con.Close();//关闭连接
                    }
                    catch (Exception eee)
                    {
                        continue;
                    }

                }
            }

            

            emg_j = Emergencygun.j;

            now = DateTime.Now;

            int qty = 0;
            for (int i = 0; i < Emergencygun.qtsl.Length; i++)
            {
                qty = qty + Emergencygun.qtsl[i];
            }

            //日志
            MySqlConnection con7 = null;
            con7 = DatabaseManager.GetCon(con7);
            con7.Open();
            string strcmd4 = "INSERT into gunark_log(GUNARK_LOG_TIME, GUNARK_LOG_TYPE, GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER,GUNARK_BULLET_NUMBER)VALUES('" + now + "'," + "'紧急取枪'" + ",'" + Utils.getUserById(user1) + "','" + Utils.getUserById(user2) + "','" + Emergencygun.j + "','" + qty + "');";
            DatabaseManager.Exec(strcmd4,con7);
            DatabaseManager.Clear(con7);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Emergencygun.j; i++)
            {
                emg_gunid[i] = Emergencygun.emg_gunid[i];
                emg_gunpositionnum[i] = Emergencygun.emg_gunpositionnum[i];
                emg_gunpositionid[i] = Emergencygun.emg_gunpositionid[i];
                emg_magazinenum[i] = Emergencygun.magazinenum[i];
            }
            j = Emergencygun.j;


            //调用开门、开枪位
            backgroundWorker1.RunWorkerAsync();

            //调用webService接口
            //backgroundWorker2.RunWorkerAsync();
            
            this.Close();

            ClearData();
        }

        void OpenDoor(string IpSocket)
        {
            string openDoor = GunPro.OpenDoor();
            string connectionSokKey = IpSocket;
            frmMain.dictConn[connectionSokKey].Send(openDoor);
        }
        void OpenGunStatus(string IpSocket, byte[] gunNumber)
        {
            string openGun = GunPro.OpenGunState(gunNumber,gunarkid);
            frmMain.dictConn[IpSocket].Send(openGun);
            DbSt.InsGunMark(openGun.Substring(8, 32), gunarkid);
            //System.Threading.Thread.Sleep(8668);
            //while (true)
            //{
            //    System.Threading.Thread.Sleep(1000);
            //    if (ConnectionClient.markStaus)
            //    {
            //       // DbSt.InsGunMark(openGun.Substring(8, 32), gunarkid);
            //        break;
            //    }
            //    else
            //    {
            //        frmMain.dictConn[IpSocket].Send(openGun);
            //    }
            //}
            ConnectionClient.markStaus = false;
        }
        private void Emergencysuccess_Load(object sender, EventArgs e)
        {
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            /// 2016-3-23
            /// 
            gunarkid = Emergency.gunark_id;
            string IpSocket = DBstor.getDBipSocket(gunarkid);
           // string IpSocket = DBstor.getDBipSocket("192.168.1.52");
           // System.Threading.Thread.Sleep(2180);
            new GunTool().OpenDoor(IpSocket);
            System.Threading.Thread.Sleep(1000);
            byte[] gunNumber = emg_gunpositionnum[0] == 0 ? emg_magazinenum : emg_gunpositionnum;

            OpenGunStatus(IpSocket, gunNumber);

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (frmMain.online)
            {
                ////开门,开始任务
                try
                {
                    frmMain.webService.uploadOpenMessage(Emergency.unit_id, Emergency.gunark_id, Utils.getUserPoliceNumById(user1), Utils.getUserPoliceNumById(user2), DateTime.Now, 1);
                }
                catch (Exception e0)
                { MessageBox.Show(e0.Message.ToString()); }
                //任务开始接口
                //
                //调用枪位接口
                for (int i = 0; i < j; i++)
                {
                    try
                    {
                        frmMain.webService.setGunNotOnPosition(Emergency.unit_id, Emergency.gunark_id, emg_gunpositionid[i]);
                    }
                    catch
                    { continue; }
                }
                //关门
                try
                {
                    frmMain.webService.closeDoor(Emergency.unit_id, Emergency.gunark_id);
                }
                catch (Exception e0)
                { MessageBox.Show(e0.Message.ToString()); }
                //任务完成接口

                //调用接口成功
                updateCall(uuid);
            }
        }
        private void updateCall(string emergency_id)
        {
            MySqlConnection con5 = null;
            con5 = DatabaseManager.GetCon(con5);
            con5.Open();
            string sql = "update gunark_emergency_task_info_detail set IS_CALL = 'yes' where EMERGENCY_TASK_ID = '"+emergency_id+"'";
            DatabaseManager.Exec(sql,con5);
            DatabaseManager.Clear(con5);
        }
        private void ClearData()
        {
            for (int i = 0; i < Emergencygun.j; i++)
            {
                Emergencygun.emg_gunid[i] = "";
                Emergencygun.emg_gunpositionnum[i] = 0;
                Emergencygun.emg_gunpositionid[i] = "";
                Emergencygun.magazinenum[i] = 0;
            }
            Emergencygun.j = 0;
        }
    }
}
