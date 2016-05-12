using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Cool
{
    class GunTool
    {
        public static bool IsContinue = true;
        public static bool erroTaskST = false;
        GunProtocol GunPro = new GunProtocol();
        public void checkTask(string unitid, string gunarkid)
        {
            Utils.isAlive = true;
            bool task_satus = GunPro.GunBullet_Status(gunarkid);
            int count = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(5000);

                if (!task_satus)
                {
                    erroTaskST = true;
                    Utils.isAlive = false;
                    //更改任务状态信息 , 需要判断
                    string str3 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con3 = new MySqlConnection(str3);//实例化链接
                    con3.Open();//开启连接
                    string strcmd3 = "update gunark_task_info set TASK_STATUS =10 where TASK_ID ='" + frmMain.taskid + "'";
                    MySqlCommand cmd3 = new MySqlCommand(strcmd3, con3);
                    MySqlDataAdapter ada3 = new MySqlDataAdapter(cmd3);
                    DataSet ds3 = new DataSet();
                    ada3.Fill(ds3);//查询结果填充数据集
                    //dataGridView1.DataSource = ds.Tables[0];
                    //a = dataGridView1.CurrentRow("TASK_ID").ToString;
                    con3.Close();//关闭连接
                    frmMain.webService.setAlarm(unitid, gunarkid, "未正常入柜", DateTime.Now, "未正常入柜");
                    for (int i = 0; i < GunProtocol.error_gun_position_ID.Count; i++)
                    {
                        frmMain.webService.setGunNotOnPosition(unitid, gunarkid, GunProtocol.error_gun_position_ID[i]);
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
        public void OpenDoor(string IpSocket)
        {
            string openDoor = GunPro.OpenDoor();
            string connectionSokKey = IpSocket;
            frmMain.dictConn[connectionSokKey].Send(openDoor);
            System.Threading.Thread.Sleep(1000);
            while (!ConnectionClient.doorStaus) 
            {
                frmMain.dictConn[connectionSokKey].Send(openDoor);
                System.Threading.Thread.Sleep(1000);
                if (ConnectionClient.doorStaus)
                 {
                    break;
                 }
            }
        }
        #region 内存回收
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        /// <summary>
        /// 获取应用内存
        /// </summary>
        /// <returns></returns>
        public static float getMem()
        {
            string procName = Process.GetCurrentProcess().ProcessName;
            using (PerformanceCounter pc = new PerformanceCounter("Process", "Working Set - Private", procName))
            {
                float r = (pc.NextValue() / 1024 / 1024);
                return r;
            }
        }
        #endregion
    }
}
