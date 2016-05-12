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
    public partial class Retgunsuccess : Form
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

        public Retgunsuccess()
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
                        string strcmd1 = "update gun_info set GUN_STATUS =1 where GUN_INFO_ID ='" + Task_Execute_Detail.gunid[i] + "'";
                        MySqlCommand cmd1 = new MySqlCommand(strcmd1, con1);
                        MySqlDataAdapter ada1 = new MySqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        ada1.Fill(ds1);//查询结果填充数据集
                        //dataGridView1.DataSource = ds.Tables[0];
                        //a = dataGridView1.CurrentRow("TASK_ID").ToString;
                        con1.Close();//关闭连接
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
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
                    catch (Exception ee)
                    {
                        continue;
                    }

                }

                //更新现存子弹数量

                //for (int i = 0; i < Task_Execute_Detail.count; i++)
                //{
                //    try
                //    {
                //        string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                //        MySqlConnection con = new MySqlConnection(str);//实例化链接
                //        con.Open();//开启连接
                //        string strcmd = "UPDATE gunark_magazine_info set STOCK_QTY = STOCK_QTY+" + Task_Execute_Detail.qty1[i] + " WHERE MAGAZINE_INFO_ID ='" + Task_Execute_Detail.mgz_id1[i] + "'";
                //        MySqlCommand cmd = new MySqlCommand(strcmd, con);
                //        MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                //        DataSet ds = new DataSet();
                //        ada.Fill(ds);//查询结果填充数据集
                //        //dataGridView1.DataSource = ds.Tables[0];
                //        //a = dataGridView1.CurrentRow("TASK_ID").ToString;
                //        con.Close();//关闭连接
                //    }
                //    catch (Exception eee)
                //    {
                //        continue;
                //    }

                //}


                if (!Task_execute02.task_status.Equals("超期未还"))
                {
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
                    string strcmd5 = "INSERT into gunark_log(GUNARK_LOG_TIME, GUNARK_LOG_TYPE, GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER,GUNARK_BULLET_NUMBER)VALUES('" + now + "'," + "'归还枪弹'" + ",'" + finger_user1 + "','" + finger_user2 + "','" + Task_Execute_Detail.count + "','" + qty + "');";
                    MySqlCommand cmd5 = new MySqlCommand(strcmd5, con5);
                    MySqlDataAdapter ada5 = new MySqlDataAdapter(cmd5);
                    DataSet ds5 = new DataSet();
                    ada5.Fill(ds5);//查询结果填充数据集
                    //dataGridView1.DataSource = ds.Tables[0];
                    //a = dataGridView1.CurrentRow("TASK_ID").ToString;
                    con5.Close();//关闭连接
                }
                else 
                {
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
                    string strcmd5 = "INSERT into gunark_log(GUNARK_LOG_TIME, GUNARK_LOG_TYPE, GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER,GUNARK_BULLET_NUMBER)VALUES('" + now + "'," + "'归还枪弹'" + ",'" + finger_user1 + "','" + finger_user2 + "','" + Task_Execute_Detail.count + "','" + qty + "');";
                    MySqlCommand cmd5 = new MySqlCommand(strcmd5, con5);
                    MySqlDataAdapter ada5 = new MySqlDataAdapter(cmd5);
                    DataSet ds5 = new DataSet();
                    ada5.Fill(ds5);//查询结果填充数据集
                    //dataGridView1.DataSource = ds.Tables[0];
                    //a = dataGridView1.CurrentRow("TASK_ID").ToString;
                    con5.Close();//关闭连
 
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
            
            //调用开门、枪位方法
            backgroundWorker1.RunWorkerAsync();

            //调用webService接口
            backgroundWorker2.RunWorkerAsync();

            this.Close();

            Utils.clearData();
        }

        private void Retgunsuccess_Load(object sender, EventArgs e)
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
            /// 2016-3-23
            string IpSocket = DBstor.getDBipSocket(gunarkid);
           // string IpSocket = DBstor.getDBipSocket("192.168.1.52");
            //System.Threading.Thread.Sleep(3180);
            new GunTool().OpenDoor(IpSocket);
            if (!Task_execute02.task_status.Equals("未正常归还"))
            {
                byte[] gunNumber = gunpositionnum[0] == 0 ? magazinenum : gunpositionnum;
                System.Threading.Thread.Sleep(1000);
            
                ReturnGunStatus(IpSocket, gunNumber);

            //backgroundWorker3.RunWorkerAsync();
           }
        }

        void OpenDoor(string IpSocket)
        {
            string openDoor = GunPro.OpenDoor();
            string connectionSokKey = IpSocket;
            frmMain.dictConn[connectionSokKey].Send(openDoor);
        }
        void ReturnGunStatus(string IpSocket, byte[] gunNumber)
        {
            string openGun = GunPro.ReturnGun(gunNumber,gunarkid);

            frmMain.dictConn[IpSocket].Send(openGun);
            DbSt.InsGunMark(openGun.Substring(8, 32), gunarkid);
           // System.Threading.Thread.Sleep(2668);
            //while (true)
            //{
            //    System.Threading.Thread.Sleep(1000);
            //    if (ConnectionClient.markStaus)
            //    {
            //        //DbSt.InsGunMark(openGun.Substring(8, 32), gunarkid);
            //        break;
            //    }
            //    else
            //    {
            //        frmMain.dictConn[IpSocket].Send(openGun);
            //    }
            //}
            ConnectionClient.markStaus = false;
           
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //开门
            frmMain.webService.uploadOpenMessage(unitid, gunarkid, Utils.getUserPoliceNumById(user1), Utils.getUserPoliceNumById(user2), DateTime.Now, 1);
            for (int i = 0; i < count; i++)
            {
                //调用WebService接口
                try
                {
                    frmMain.webService.setGunOnPosition(unitid, gunarkid, gunpositionid[i]);
                }
                catch
                { continue; }
            }
            //任务完成
            frmMain.webService.setFinishTask(frmMain.taskid, DateTime.Now, Utils.getUserPoliceNumById(user1), Utils.getUserPoliceNumById(user2));
            //关门
            frmMain.webService.closeDoor(unitid, gunarkid);
            backgroundWorker3.RunWorkerAsync();
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            new GunTool().checkTask(unitid, gunarkid);
            
        }

    }
}
