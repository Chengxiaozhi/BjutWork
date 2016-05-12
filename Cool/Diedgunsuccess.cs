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
    public partial class Diedgunsuccess : Form
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

        public Diedgunsuccess()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);


            //更改任务状态   6 需要判断
            string str3 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con3 = new MySqlConnection(str3);//实例化链接
            con3.Open();//开启连接
            string strcmd3 = "update gunark_task_info set TASK_STATUS =6 where TASK_ID ='" + frmMain.taskid + "'";
            MySqlCommand cmd3 = new MySqlCommand(strcmd3, con3);
            MySqlDataAdapter ada3 = new MySqlDataAdapter(cmd3);
            DataSet ds3 = new DataSet();
            ada3.Fill(ds3);//查询结果填充数据集
            con3.Close();//关闭连接

            //if (frmMain.taskbigtype.Equals("枪弹检查"))
            //{
            //    //更改任务状态   6 需要判断
            //    string str4 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            //    MySqlConnection con4 = new MySqlConnection(str4);//实例化链接
            //    con4.Open();//开启连接
            //    string strcmd4 = "update gunark_task_info set TASK_STATUS =5 where TASK_ID ='" + frmMain.taskid + "'";
            //    MySqlCommand cmd4 = new MySqlCommand(strcmd4, con4);
            //    MySqlDataAdapter ada4 = new MySqlDataAdapter(cmd4);
            //    DataSet ds4 = new DataSet();
            //    ada4.Fill(ds4);//查询结果填充数据集
            //    con4.Close();
            //}

            if (frmMain.taskbigtype.Equals("枪支封存"))
            {
                //更改枪支状态
                for (int i = 0; i < Task_Execute_Detail.count; i++)
                {
                    try
                    {
                        string str1 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                        MySqlConnection con1 = new MySqlConnection(str1);//实例化链接
                        con1.Open();//开启连接
                        string strcmd1 = "update gun_info set GUN_STATUS =0,OUT_TIME = now() where GUN_INFO_ID ='" + Task_Execute_Detail.gunid[i] + "'";
                        MySqlCommand cmd1 = new MySqlCommand(strcmd1, con1);
                        MySqlDataAdapter ada1 = new MySqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        ada1.Fill(ds1);//查询结果填充数据集

                        con1.Close();//关闭连接
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
                
            }
            else if (frmMain.taskbigtype.Equals("枪支解封"))
            {
                //更改枪支状态
                for (int i = 0; i < Task_Execute_Detail.count; i++)
                {
                    try
                    {
                        string str1 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                        MySqlConnection con1 = new MySqlConnection(str1);//实例化链接
                        con1.Open();//开启连接
                        string strcmd1 = "update gun_info set GUN_STATUS =1,OUT_TIME = now() where GUN_INFO_ID ='" + Task_Execute_Detail.gunid[i] + "'";
                        MySqlCommand cmd1 = new MySqlCommand(strcmd1, con1);
                        MySqlDataAdapter ada1 = new MySqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        ada1.Fill(ds1);//查询结果填充数据集

                        con1.Close();//关闭连接
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
            }
            else
            {
                if (Task_Execute_Detail.gunpositionnum[0] != 0)
                {
                    //更改枪支状态
                    for (int i = 0; i < Task_Execute_Detail.count; i++)
                    {
                        try
                        {
                            string str1 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                            MySqlConnection con1 = new MySqlConnection(str1);//实例化链接
                            con1.Open();//开启连接
                            string strcmd1 = "update gun_info set GUN_STATUS =5,OUT_TIME = now() where GUN_INFO_ID ='" + Task_Execute_Detail.gunid[i] + "'";
                            MySqlCommand cmd1 = new MySqlCommand(strcmd1, con1);
                            MySqlDataAdapter ada1 = new MySqlDataAdapter(cmd1);
                            DataSet ds1 = new DataSet();
                            ada1.Fill(ds1);//查询结果填充数据集

                            con1.Close();//关闭连接
                        }
                        catch (Exception e)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    //更新现存子弹数量

                    for (int i = 0; i < Task_Execute_Detail.count; i++)
                    {
                        try
                        {
                            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                            MySqlConnection con = new MySqlConnection(str);//实例化链接
                            con.Open();//开启连接
                            string strcmd = "UPDATE gunark_magazine_info set STOCK_QTY = STOCK_QTY-" + Task_Execute_Detail.qty1[i] + " WHERE MAGAZINE_INFO_ID ='" + Task_Execute_Detail.mgz_id1[i] + "'";
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
 
                }

            }

            //更改枪柜状态
            for (int i = 0; i < Task_Execute_Detail.count; i++)
            {
                try
                {
                    string str2 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con2 = new MySqlConnection(str2);//实例化链接
                    con2.Open();//开启连接
                    string strcmd2 = "update gunark_position_info set GUN_POSITION_STATUS =0 where GUN_POSITION_INFO_ID ='" + Task_Execute_Detail.gunpositionid[i] + "'";
                    MySqlCommand cmd2 = new MySqlCommand(strcmd2, con2);
                    MySqlDataAdapter ada2 = new MySqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    ada2.Fill(ds2);//查询结果填充数据集

                    con2.Close();//关闭连接
                }
                catch (Exception ee)
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
            if (frmMain.taskbigtype.Equals("枪支报废"))
            {
                string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                MySqlConnection con = new MySqlConnection(str);//实例化链接
                con.Open();//开启连接
                string strcmd = "INSERT into gunark_log(GUNARK_LOG_TIME, GUNARK_LOG_TYPE, GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER,GUNARK_BULLET_NUMBER)VALUES('" + now + "'," + "'枪弹报废'" + ",'" + finger_user1 + "','" + finger_user2 + "','" + Task_Execute_Detail.count + "','" + qty + "');";
                MySqlCommand cmd = new MySqlCommand(strcmd, con);
                MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ada.Fill(ds);//查询结果填充数据集
                //dataGridView1.DataSource = ds.Tables[0];
                //a = dataGridView1.CurrentRow("TASK_ID").ToString;
                con.Close();//关闭连接
            }
            else if (frmMain.taskbigtype.Equals("枪支封存"))
            {
                string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                MySqlConnection con = new MySqlConnection(str);//实例化链接
                con.Open();//开启连接
                string strcmd = "INSERT into gunark_log(GUNARK_LOG_TIME, GUNARK_LOG_TYPE, GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER,GUNARK_BULLET_NUMBER)VALUES('" + now + "'," + "'枪弹封存'" + ",'" + finger_user1 + "','" + finger_user2 + "','" + Task_Execute_Detail.count + "','" + qty + "');";
                MySqlCommand cmd = new MySqlCommand(strcmd, con);
                MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ada.Fill(ds);//查询结果填充数据集
                //dataGridView1.DataSource = ds.Tables[0];
                //a = dataGridView1.CurrentRow("TASK_ID").ToString;
                con.Close();//关闭连接
            }
            else if (frmMain.taskbigtype.Equals("枪弹调拨"))
            {
                string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                MySqlConnection con = new MySqlConnection(str);//实例化链接
                con.Open();//开启连接
                string strcmd = "INSERT into gunark_log(GUNARK_LOG_TIME, GUNARK_LOG_TYPE, GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER,GUNARK_BULLET_NUMBER)VALUES('" + now + "'," + "'枪弹调拨'" + ",'" + finger_user1 + "','" + finger_user2 + "','" + Task_Execute_Detail.count + "','" + qty + "');";
                MySqlCommand cmd = new MySqlCommand(strcmd, con);
                MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ada.Fill(ds);//查询结果填充数据集
                //dataGridView1.DataSource = ds.Tables[0];
                //a = dataGridView1.CurrentRow("TASK_ID").ToString;
                con.Close();//关闭连接
            }
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

            if (!frmMain.taskbigtype.Equals("枪支封存") && !frmMain.taskbigtype.Equals("枪支解封"))
            {
                //调用开门，任务状态接口
                backgroundWorker1.RunWorkerAsync();
                //任务完成
                //调用枪位接口
                backgroundWorker2.RunWorkerAsync();
            }
            else
            {
                backgroundWorker3.RunWorkerAsync();
            }
            
            this.Close();

            Utils.clearData();
        }

        private void Diedgunsuccess_Load(object sender, EventArgs e)
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
        void OpenDoor(string IpSocket)
        {
            string openDoor = GunPro.OpenDoor();
            string connectionSokKey = IpSocket;
            frmMain.dictConn[connectionSokKey].Send(openDoor);
        }
        void OpenBulletStatus(string IpSocket, byte[] gunNumber)
        {
            
            string openGun = GunPro.OpenBulletState(gunNumber, gunarkid);
            frmMain.dictConn[IpSocket].Send(openGun);
            // System.Threading.Thread.Sleep(1668);
            //string preEnable = "00000000000000000000000000000000";
            //DbSt.InsGunMark(openGun.Substring(8, 32), gunarkid);
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                if (ConnectionClient.markStaus)
                {
                    ConnectionClient.markStaus = false;
                    //System.Threading.Thread.Sleep(2668);

                    break;

                    // System.Threading.Thread.Sleep(1668);

                }
                else
                {

                    frmMain.dictConn[IpSocket].Send(openGun);

                }
            }
               
             
        }
        void OpenGunStatus(string IpSocket, byte[] gunNumber)
        {
            string cancelGun = "";
            string openGun = GunPro.OpenGunState(gunNumber,gunarkid);
            frmMain.dictConn[IpSocket].Send(openGun);
           // System.Threading.Thread.Sleep(1668);
        
                cancelGun = GunPro.cancel_Enable(gunNumber, gunarkid);

                DbSt.InsGunEnable(cancelGun.Substring(8, 32), gunarkid);
         
            DbSt.InsGunMark(openGun.Substring(8, 32), gunarkid);
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                if (ConnectionClient.markStaus)
                {
                    ConnectionClient.markStaus = false;
                    //System.Threading.Thread.Sleep(2668);
                   
                        frmMain.dictConn[IpSocket].Send(cancelGun);
                  
                    // System.Threading.Thread.Sleep(1668);

                }
                else
                {
                   
                     frmMain.dictConn[IpSocket].Send(openGun);
                    
                }
                if (ConnectionClient.enableStaus)
                {
                   // DbSt.InsGunEnable(cancelGun.Substring(8, 32), gunarkid);
                   // DbSt.InsGunMark(openGun.Substring(8, 32), gunarkid);
                    break;
                }
                else
                {
                    
                        frmMain.dictConn[IpSocket].Send(cancelGun);
                    
                }
            }
            ConnectionClient.enableStaus = false;
           
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //开柜子，开枪位
            string IpSocket = DBstor.getDBipSocket(gunarkid);
            //string IpSocket = DBstor.getDBipSocket("192.168.1.52");
          //  System.Threading.Thread.Sleep(2668);
            new GunTool().OpenDoor(IpSocket);
         
            byte[] gunNumber = gunpositionnum[0] == 0 ? magazinenum : gunpositionnum;
            System.Threading.Thread.Sleep(2180);
            if (Task_Execute_Detail.gunpositionnum[0] != 0)
            {
                OpenGunStatus(IpSocket, gunNumber);
            }
            else 
            {
                OpenBulletStatus(IpSocket, gunNumber);
            }
            

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //开门
            frmMain.webService.uploadOpenMessage(unitid, gunarkid, Utils.getUserPoliceNumById(user1), Utils.getUserPoliceNumById(user2), DateTime.Now, 1);
            //开始任务
            frmMain.webService.setDoingTask(frmMain.taskid, DateTime.Now, Utils.getUserPoliceNumById(user1), Utils.getUserPoliceNumById(user2));
           
                //枪位
                for (int i = 0; i < count; i++)
                {
                    frmMain.webService.setGunBeAllot(gunid[i]);
                    frmMain.webService.setGunPosNotOnGunark(unitid, gunarkid, gunpositionid[i]);
                }
            
            //任务完成
            frmMain.webService.setFinishTask(frmMain.taskid, DateTime.Now, Utils.getUserPoliceNumById(user1), Utils.getUserPoliceNumById(user2));
            //关门
            frmMain.webService.closeDoor(unitid, gunarkid);

        }
        //枪支封存接口
        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            if (frmMain.taskbigtype.Equals("枪支封存"))
            {
                //枪位
                for (int i = 0; i < count; i++)
                {
                    frmMain.webService.setGunBeSeal(gunid[i]);
                }
            }
            else
            {
                //枪位
                for (int i = 0; i < count; i++)
                {
                    frmMain.webService.setGunOnPosition(unitid, gunarkid, gunid[i]);
                }
            }
        }
    }
}
