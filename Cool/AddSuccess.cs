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
using System.Threading;

namespace Cool
{
    public partial class AddSuccess : Form
    {

        string user1 = Convert12.ByteArrToStr(ConnectionClient.CheckuserGroup0);
        string user2 = Convert12.ByteArrToStr(ConnectionClient.CheckuserGroup1);
        GunProtocol GunPro = new GunProtocol();
        DBsotorage DBstor = new DBsotorage();
        DBgunStatus DbSt = new DBgunStatus(); 
        string unitid;
        string gunarkid;

        string[] gunid = new string[100];
        string[] gunpositionid = new string[100];
        byte[] gunpositionnum = new byte[100];
        byte[] magazinenum = new byte[100];
        int count = 0;
        
        public AddSuccess()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);


            //更改任务状态信息 , 需要判断
            string str3 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con3 = new MySqlConnection(str3);//实例化链接
            con3.Open();//开启连接
            string strcmd3 = "update gunark_task_info set TASK_STATUS =6 where TASK_ID ='" + frmMain.taskid + "'";
            MySqlCommand cmd3 = new MySqlCommand(strcmd3, con3);
            MySqlDataAdapter ada3 = new MySqlDataAdapter(cmd3);
            DataSet ds3 = new DataSet();
            ada3.Fill(ds3);//查询结果填充数据集
            //dataGridView1.DataSource = ds.Tables[0];
            //a = dataGridView1.CurrentRow("TASK_ID").ToString;
            con3.Close();//关闭连接


            //更改枪支状态

            for (int i = 0; i < Task_Execute_Detail.count; i++)
            {
                try
                {
                    string str1 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con1 = new MySqlConnection(str1);//实例化链接
                    con1.Open();//开启连接
                    string strcmd1 = "update gun_info set GUN_STATUS =1,IN_TIME =  now() ,OUT_TIME = NULL where GUN_INFO_ID ='" + Task_Execute_Detail.gunid[i] + "'";
                    MySqlCommand cmd1 = new MySqlCommand(strcmd1, con1);
                    MySqlDataAdapter ada1 = new MySqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    ada1.Fill(ds1);//查询结果填充数据集
                    //dataGridView1.DataSource = ds.Tables[0];
                    //a = dataGridView1.CurrentRow("TASK_ID").ToString;
                    con1.Close();//关闭连接
                }
                catch (Exception ee)
                { continue; }
            }

            //更改枪位状态

            for (int i = 0; i < Task_Execute_Detail.count; i++)
            {
                try
                {
                    string str2 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con2 = new MySqlConnection(str2);//实例化链接
                    con2.Open();//开启连接
                    string strcmd2 = "update gunark_position_info set GUN_POSITION_STATUS =3 where GUN_POSITION_INFO_ID ='" + Task_Execute_Detail.gunpositionid[i] + "'";
                    MySqlCommand cmd2 = new MySqlCommand(strcmd2, con2);
                    MySqlDataAdapter ada2 = new MySqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    ada2.Fill(ds2);//查询结果填充数据集
                    //dataGridView1.DataSource = ds.Tables[0];
                    //a = dataGridView1.CurrentRow("TASK_ID").ToString;
                    con2.Close();//关闭连接
                }
                catch(Exception eee)
                { continue; }

            }

            //更新现存子弹数量

            for (int i = 0; i < Task_Execute_Detail.count; i++)
            {
                try
                {
                    string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con = new MySqlConnection(str);//实例化链接
                    con.Open();//开启连接
                    string strcmd = "UPDATE gunark_magazine_info set STOCK_QTY = STOCK_QTY+" + Task_Execute_Detail.qty1[i] + " WHERE MAGAZINE_INFO_ID ='" + Task_Execute_Detail.mgz_id1[i] + "'";
                    MySqlCommand cmd = new MySqlCommand(strcmd, con);
                    MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ada.Fill(ds);//查询结果填充数据集
                    //dataGridView1.DataSource = ds.Tables[0];
                    //a = dataGridView1.CurrentRow("TASK_ID").ToString;
                    con.Close();//关闭连接
                }
                catch (Exception eee)
                {
                    continue;
                }

            }

            
            //日志
            DateTime now = DateTime.Now;
            string finger_user1 = Utils.getUserById(user1);
            string finger_user2 = Utils.getUserById(user2);
            int qty = 0;
            for (int i = 0; i < Task_Execute_Detail.qty1.Length; i++)
            {
                qty = qty + Task_Execute_Detail.qty1[i];
            }

            string str5 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con5 = new MySqlConnection(str5);//实例化链接
            con5.Open();//开启连接
            string strcmd5 = "INSERT into gunark_log(GUNARK_LOG_TIME, GUNARK_LOG_TYPE, GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER,GUNARK_BULLET_NUMBER)VALUES('" + now + "'," + "'枪弹入库'" + ",'" + finger_user1 + "','" + finger_user2 + "','" + Task_Execute_Detail.count + "','" + qty + "');";
            MySqlCommand cmd5 = new MySqlCommand(strcmd5, con5);
            MySqlDataAdapter ada5 = new MySqlDataAdapter(cmd5);
            DataSet ds5 = new DataSet();
            ada5.Fill(ds5);//查询结果填充数据集
            //dataGridView1.DataSource = ds.Tables[0];
            //a = dataGridView1.CurrentRow("TASK_ID").ToString;
            con5.Close();//关闭连接
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //接收参数            
            for (int i = 0; i < Task_Execute_Detail.count; i++)
            {
                gunid[i] = Task_Execute_Detail.gunid[i];
                gunpositionid[i] = Task_Execute_Detail.gunpositionid[i];
                gunpositionnum[i] = Task_Execute_Detail.gunpositionnum[i];
                magazinenum[i] = Task_Execute_Detail.magazinenum[i];
            }
            count = Task_Execute_Detail.count;

            //调用webService接口
            backgroundWorker1.RunWorkerAsync();

            backgroundWorker3.RunWorkerAsync();
            //调用开门、开枪位方法
            backgroundWorker2.RunWorkerAsync();
           
            this.Close();

            Utils.clearData();
        }

        
        private void AddSuccess_Load(object sender, EventArgs e)
        {
            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = "SELECT UNIT_ID, GUNARK_ID FROM gunark_task_info where TASK_ID ='" + frmMain.taskid + "'";
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集
            con.Close();
            unitid = ds.Tables[0].Rows[0][0].ToString();
            gunarkid = ds.Tables[0].Rows[0][1].ToString();

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //开门接口
            frmMain.webService.uploadOpenMessage(unitid, gunarkid, Utils.getUserPoliceNumById(user1), Utils.getUserPoliceNumById(user2), DateTime.Now, 1);
            //任务开始
            frmMain.webService.setDoingTask(frmMain.taskid, DateTime.Now, Utils.getUserPoliceNumById(user1), Utils.getUserPoliceNumById(user2));
            //枪位接口
            for (int i = 0; i < count; i++)
            {
                try
                {
                    //调用WebService接口
                    frmMain.webService.setGunInStore(gunid[i]);
                    frmMain.webService.setGunOnPosition(unitid, gunarkid, gunpositionid[i]);
                }
                catch (Exception eeee)
                { continue; }
            }
            frmMain.webService.closeDoor(unitid, gunarkid);
            frmMain.webService.setFinishTask(frmMain.taskid, DateTime.Now, Utils.getUserPoliceNumById(user1), Utils.getUserPoliceNumById(user2));
            
            //IsCallWebService = true;
            ////清除Task_Execute_Detail.gunid、Task_Execute_Detail.gunpositionid、Task_Execute_Detail.count
            ////Utils.clearData();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //string ip = DBstor.getDBip(gunarkid);
            string IpSocket = DBstor.getDBipSocket(gunarkid);
            //string IpSocket = DBstor.getDBipSocket("192.168.1.52");
            
            new GunTool().OpenDoor(IpSocket);
           
            if (!Task_execute03.task_status.Equals("未正常入柜"))//!Task_execute03.task_status.Equals("未正常入柜")
            {
                byte[] gunNumber = gunpositionnum[0] == 0 ? magazinenum : gunpositionnum;
                System.Threading.Thread.Sleep(1180);
                AddGunStatus(IpSocket, gunNumber);
            }
        
        }

        void OpenDoor(string IpSocket)
        {
            string openDoor = GunPro.OpenDoor();
            string connectionSokKey = IpSocket;
            frmMain.dictConn[connectionSokKey].Send(openDoor);
        }

        void AddGunStatus(string IpSocket, byte[] gunNumber)
        {
            string addGun = GunPro.GunEnable(gunNumber, gunarkid);
            frmMain.dictConn[IpSocket].Send(addGun);
            string addgunStatus = GunPro.SetToZero(gunNumber, gunarkid);
            DbSt.InsGunEnable(addGun.Substring(8, 32), gunarkid);
            DbSt.InsGunMark(addgunStatus.Substring(8, 32), gunarkid);
            while (true)
            {
                System.Threading.Thread.Sleep(1000);

                if (ConnectionClient.enableStaus)
                {
                    //System.Threading.Thread.Sleep(2668);
                   
                    frmMain.dictConn[IpSocket].Send(addgunStatus);
                    //System.Threading.Thread.Sleep(1668);
                    ConnectionClient.enableStaus = false;

                }
                else
                {
                    frmMain.dictConn[IpSocket].Send(addGun);   
                }
                if (ConnectionClient.markStaus)
                {
                   // DbSt.InsGunEnable(addGun.Substring(8, 32), gunarkid);
                   // DbSt.InsGunMark(addgunStatus.Substring(8, 32), gunarkid);
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                    frmMain.dictConn[IpSocket].Send(addgunStatus);  
                }
            }
            ConnectionClient.markStaus = false;
          

        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            
           new GunTool().checkTask(unitid, gunarkid);
  
        }

        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (GunTool.erroTaskST)
            {

               // Popup.Controls.Frm_Popup.Instance().Show();
            }
        }
    }
}
