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
    public partial class ReturnEmergencyGunSuccess : Form
    {
        string user1 = Convert12.ByteArrToStr(ConnectionClient.CheckuserGroup0);
        string user2 = Convert12.ByteArrToStr(ConnectionClient.CheckuserGroup1);
        GunProtocol GunPro = new GunProtocol();
        DBsotorage DBstor = new DBsotorage();
        DBgunStatus DbSt = new DBgunStatus();
        string unitid;
        string gunarkid;
        string[] gunid = new string[100];
        byte[] gunpositionnum = new byte[100];
        byte[] magazinenum = new byte[100];
        string[] gunpositionid = new string[100];
        int count = 0;
        string taskID="";
        private bool task_satus = true;
        public ReturnEmergencyGunSuccess()
        {
            taskID = ReturnEmergencyGun.emergency_task_id;
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);

            //更改任务状态信息 , 需要判断
            string str3 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con3 = new MySqlConnection(str3);//实例化链接
            con3.Open();//开启连接
            string strcmd3 = "update gunark_emergency_task_info set TASK_STATUS =6 where EMERGENCY_TASK_ID ='" + taskID + "'";
            MySqlCommand cmd3 = new MySqlCommand(strcmd3, con3);
            MySqlDataAdapter ada3 = new MySqlDataAdapter(cmd3);
            DataSet ds3 = new DataSet();
            ada3.Fill(ds3);//查询结果填充数据集
            //dataGridView1.DataSource = ds.Tables[0];
            //a = dataGridView1.CurrentRow("TASK_ID").ToString;
            con3.Close();//关闭连接

            string str9 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con9 = new MySqlConnection(str9);//实例化链接
            con9.Open();//开启连接
            string strcmd9 = "update gunark_emergency_task_info set TASK_END_TIME = '"+ DateTime.Now +"' where EMERGENCY_TASK_ID ='" + taskID + "'";
            MySqlCommand cmd9 = new MySqlCommand(strcmd9, con9);
            MySqlDataAdapter ada9 = new MySqlDataAdapter(cmd9);
            DataSet ds9 = new DataSet();
            ada9.Fill(ds9);//查询结果填充数据集
            //dataGridView1.DataSource = ds.Tables[0];
            //a = dataGridView1.CurrentRow("TASK_ID").ToString;
            con9.Close();//关闭连接

            string str4 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con4 = new MySqlConnection(str4);//实例化链接
            con4.Open();//开启连接
            string strcmd4 = "SELECT IS_CALL FROM gunark_emergency_task_info WHERE EMERGENCY_TASK_ID ='" + taskID + "'";
            MySqlCommand cmd4 = new MySqlCommand(strcmd4, con4);
            MySqlDataAdapter ada4 = new MySqlDataAdapter(cmd4);
            DataSet ds4 = new DataSet();
            ada4.Fill(ds4);//查询结果填充数据集
         

            if (ds4.Tables[0].Rows[0][0].ToString().Equals("no"))
            {
                //更改IS_CALL
                string str7 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                MySqlConnection con7 = new MySqlConnection(str7);//实例化链接
                con7.Open();//开启连接
                string strcmd7 = "update gunark_emergency_task_info set IS_CALL ='noo' where EMERGENCY_TASK_ID ='" + taskID + "'";
                MySqlCommand cmd7 = new MySqlCommand(strcmd7, con7);
                MySqlDataAdapter ada7 = new MySqlDataAdapter(cmd7);
                DataSet ds7 = new DataSet();
                ada7.Fill(ds7);//查询结果填充数据集
                con7.Close();
            }
            //更改枪支状态

            for (int i = 0; i < ReturnEmergencyGunDetail.count; i++)
            {
                try
                {
                    string str1 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con1 = new MySqlConnection(str1);//实例化链接
                    con1.Open();//开启连接
                    string strcmd1 = "update gun_info set GUN_STATUS =1 where GUN_INFO_ID ='" + ReturnEmergencyGunDetail.gunid[i] + "'";
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

            for (int i = 0; i < ReturnEmergencyGunDetail.count; i++)
            {
                try
                {
                    string str2 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con2 = new MySqlConnection(str2);//实例化链接
                    con2.Open();//开启连接
                    string strcmd2 = "update gunark_position_info set GUN_POSITION_STATUS =3 where GUN_POSITION_INFO_ID ='" + ReturnEmergencyGunDetail.gunpositionid[i] + "'";
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

            for (int i = 0; i < ReturnEmergencyGunDetail.count; i++)
            {
                try
                {
                    string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con = new MySqlConnection(str);//实例化链接
                    con.Open();//开启连接
                    string strcmd = "UPDATE gunark_magazine_info set STOCK_QTY = STOCK_QTY+" + ReturnEmergencyGunDetail.hdsl[i] + " WHERE MAGAZINE_INFO_ID ='" + ReturnEmergencyGunDetail.mgz_id1[i] + "'";
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
            for (int i = 0; i < ReturnEmergencyGunDetail.hdsl.Length; i++)
            {
                qty = qty + ReturnEmergencyGunDetail.hdsl[i];
            }

            string str5 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con5 = new MySqlConnection(str5);//实例化链接
            con5.Open();//开启连接
            string strcmd5 = "INSERT into gunark_log(GUNARK_LOG_TIME, GUNARK_LOG_TYPE, GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER,GUNARK_BULLET_NUMBER)VALUES('" + now + "'," + "'归还枪弹'" + ",'" + finger_user1 + "','" + finger_user2 + "','" + ReturnEmergencyGunDetail.count + "','" + qty + "');";
            MySqlCommand cmd5 = new MySqlCommand(strcmd5, con5);
            MySqlDataAdapter ada5 = new MySqlDataAdapter(cmd5);
            DataSet ds5 = new DataSet();
            ada5.Fill(ds5);//查询结果填充数据集
            //dataGridView1.DataSource = ds.Tables[0];
            //a = dataGridView1.CurrentRow("TASK_ID").ToString;
            con5.Close();//关闭连接
        }

        private void ReturnEmergencyGunSuccess_Load(object sender, EventArgs e)
        {
            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = "SELECT UNIT_ID, GUNARK_ID FROM gunark_emergency_task_info where EMERGENCY_TASK_ID ='" + taskID + "'";
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集
            con.Close();
            unitid = ds.Tables[0].Rows[0][0].ToString();
            gunarkid = ds.Tables[0].Rows[0][1].ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < ReturnEmergencyGunDetail.count; i++)
            {
                gunid[i] = ReturnEmergencyGunDetail.gunid[i];
                gunpositionnum[i] = ReturnEmergencyGunDetail.gunpositionnum[i];
                gunpositionid[i] = ReturnEmergencyGunDetail.gunpositionid[i];
                magazinenum[i] = ReturnEmergencyGunDetail.magazinenum[i];
            }
            count = ReturnEmergencyGunDetail.count;
            //taskID = ReturnEmergencyGun.emergency_task_id;
            //调用开门、枪位方法
            backgroundWorker1.RunWorkerAsync();

            //调用webService接口
            backgroundWorker2.RunWorkerAsync();

            this.Close();

            ClearData();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            /// 2016-3-23
            string IpSocket = DBstor.getDBipSocket(gunarkid);
            //System.Threading.Thread.Sleep(2668);
            //string IpSocket = DBstor.getDBipSocket("192.168.1.52");
            new GunTool().OpenDoor(IpSocket);
            //开门接口
            byte[] gunNumber = gunpositionnum[0] == 0 ? magazinenum : gunpositionnum;
            System.Threading.Thread.Sleep(1000);
            if(!ReturnEmergencyGun.emergency_task_status.Equals("未正常还枪"))
            {
                ReturnGunStatus(IpSocket, gunNumber);
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
            //System.Threading.Thread.Sleep(1668);
            //while (true)
            //{
            //    System.Threading.Thread.Sleep(1000);
            //    if (ConnectionClient.markStaus)
            //    {
            //        // DbSt.InsGunMark(openGun.Substring(8, 32), gunarkid);
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
            //调用WebService枪位接口
            for (int i = 0; i < count; i++)
            {
                try
                {
                    frmMain.webService.setGunOnPosition(unitid, gunarkid, gunpositionid[i]);
                }
                catch
                { continue; }
            }
            //关门接口
            //
            //任务完成接口
            //
            backgroundWorker3.RunWorkerAsync();
        }

        private void ClearData()
        {
            for (int i = 0; i < ReturnEmergencyGunDetail.count; i++)
            {
                ReturnEmergencyGunDetail.gunid[i] = "";
                ReturnEmergencyGunDetail.gunpositionnum[i] = 0;
                ReturnEmergencyGunDetail.gunpositionid[i] = "";
                ReturnEmergencyGunDetail.magazinenum[i] = 0;
            }
            ReturnEmergencyGunDetail.count = 0;
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            int count = 0;
            Utils.isAlive = true;
            task_satus = GunPro.GunBullet_Status(gunarkid);
            BackgroundWorker bw = sender as BackgroundWorker;
            while (true)
            {
           
                System.Threading.Thread.Sleep(3000);
                
                if (!task_satus)
                {
                    bw.ReportProgress(1);
                    Utils.isAlive = false;
                    //更改任务状态信息 , 需要判断
                    string str3 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con3 = new MySqlConnection(str3);//实例化链接
                    con3.Open();//开启连接
                    string strcmd3 = "update gunark_emergency_task_info set TASK_STATUS = 10 where EMERGENCY_TASK_ID ='" + taskID + "'";
                    MySqlCommand cmd3 = new MySqlCommand(strcmd3, con3);
                    MySqlDataAdapter ada3 = new MySqlDataAdapter(cmd3);
                    DataSet ds3 = new DataSet();
                    ada3.Fill(ds3);//查询结果填充数据集
                    //dataGridView1.DataSource = ds.Tables[0];
                    //a = dataGridView1.CurrentRow("TASK_ID").ToString;
                    con3.Close();//关闭连接

                    frmMain.webService.setAlarm(unitid, gunarkid, "未正常还枪", DateTime.Now, "未正常还枪");
                    for (int i = 0; i < GunProtocol.error_gun_position_ID.Count; i++)
                    {
                        try
                        {
                            frmMain.webService.setGunNotOnPosition(unitid, gunarkid, GunProtocol.error_gun_position_ID[i]);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    MessageBox.Show("库室中的柜子" + gunarkid + "入柜异常，请重新操作", "枪弹库警告:", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    break;

                 
                }

                else
                {
                    count++;
                }
                if (count == 2)
                {
                    Utils.isAlive = false;
                    break;
                }
            }
           
        }
        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!task_satus)
            {
               // Popup.Controls.Frm_Popup.Instance().Show();
            }
        }

        private void backgroundWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           
               // Popup.Controls.Frm_Popup.Instance().Show();
            
        }
    }
}
