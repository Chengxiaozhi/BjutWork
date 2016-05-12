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
    public partial class Setting_GunID : Form
    {
        private string[] source = null;
        private GunProtocol GunPro = new GunProtocol();
        //要修改的柜子的ip
        private string gun_ip;
        Convert12 con12 = new Convert12();
        FTPhelp FTP = new FTPhelp();
        Modify_File EditFile = new Modify_File();
        public Setting_GunID()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
        }

        private void GunIp_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        /// <summary>
        /// 选择枪柜后填充其余文本框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void gunark_name_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            con.Open();
            string sql = "select GUNARK_NAME,GUNARK_ID,GUNARK_IP,GUNARK_SUBNET,GUNARK_GATEWAY FROM gunark_gunark where GUNARK_NAME = '" + gunark_name.Text + "' AND GUNARK_STATUS = 1";
            DataSet ds = DatabaseManager.ExecAndGetDs(sql, "gunark_gunark", con);
            DatabaseManager.Clear(con);

            gun_ip = ds.Tables[0].Rows[0][2].ToString();
            gunark_num.Text = ds.Tables[0].Rows[0][1].ToString();
            gunark_ip.Text = ds.Tables[0].Rows[0][2].ToString();
            gunark_subway.Text = ds.Tables[0].Rows[0][3].ToString();
            gunark_gateway.Text = ds.Tables[0].Rows[0][4].ToString();
        }
        /// <summary>
        /// 点击确定后修改IP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Alter_Click(object sender, EventArgs e)
        {
            //检查输入的IP格式是否正确
            if (!IsIpaddress(gunark_ip.Text.Trim()))
            {
                MessageBox.Show("ip格式不正确！"); return;
            }
            if (!IsIpaddress(gunark_subway.Text.Trim()))
            {
                MessageBox.Show("子网掩码格式不正确！"); return;
            }
            if (!IsIpaddress(gunark_gateway.Text.Trim()))
            {
                MessageBox.Show("网关格式不正确！"); return;
            }

            string connectionSokKey = frmMain.dictConn.Keys.Last();
            if (!string.IsNullOrEmpty(connectionSokKey))
            {
                if (MessageBox.Show(this, "您确定要修改该枪柜的IP么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    //从字典集合中根据键获得 负责与该客户端通信的套接字，并调用send方法发送数据过去
                    try
                    {
                        string ftpPath = GetftpPath(connectionSokKey.Substring(0, connectionSokKey.Length - 5));
                        //下载文件
                        DownConfig(ftpPath);
                        string path = AppDomain.CurrentDomain.BaseDirectory;
                        //修改文件
                        Modify_File.EditFile(3, "MIP=" + gunark_ip.Text + ";", @path + "Config.txt");
                        //上传文件
                        Upload(ftpPath);
                        MessageBox.Show(this, "设置成功，请连续重启两次枪柜！", "提示");
                        string openDoor = GunPro.OpenDoor();

                        frmMain.dictConn[connectionSokKey].Send(openDoor);
                        //更改数据库中的枪柜IP
                        string sql_01 = "INSERT IGNORE INTO gunark_gunark_storage(GUNARK_IP)VALUES ('" + gunark_ip.Text + "')";
                        MySqlConnection con = null;
                        con = DatabaseManager.GetCon(con);
                        con.Open();
                        DatabaseManager.Exec(sql_01, con);
                        DatabaseManager.Clear(con);
                        String sql_02 = "UPDATE gunark_gunark_storage set GUNARK_ID = '" + gunark_num.Text + "' WHERE GUNARK_IP = '" + gunark_ip.Text + "'";
                        MySqlConnection con1 = null;
                        con1 = DatabaseManager.GetCon(con1);
                        con1.Open();
                        DatabaseManager.Exec(sql_02, con1);
                        DatabaseManager.Clear(con1);
                        new DBgunStatus().SetGunID(gunark_num.Text);
                        this.Alter.Enabled = false;
                    }
                    catch
                    {
                        if (MessageBox.Show("设置失败，请重启系统后重试！，是否马上重启软件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Application.Exit();
                            System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要发送的枪柜！");
            }
        }

        /// <summary>
        /// 
        ///选择枪柜下拉条填充
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            con.Open();
            string sql = "select GUNARK_NAME,GUNARK_ID,GUNARK_IP,GUNARK_SUBNET,GUNARK_GATEWAY FROM gunark_gunark where GUNARK_STATUS = 1 order by GUNARK_ENTERTIME";
            DataSet ds = DatabaseManager.ExecAndGetDs(sql, "gunark_gunark", con);
            DatabaseManager.Clear(con);

            source = new string[ds.Tables[1].Rows.Count + 1];

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                source[i] = ds.Tables[1].Rows[i][0].ToString();
            }
            gunark_name.DataSource = source;
            //gunark_name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //gunark_name.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        /// <summary>
        /// 判断是否是正确的ip地址
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        protected bool IsIpaddress(string ipaddress)
        {
            string[] nums = ipaddress.Split('.');
            if (nums.Length != 4) return false;
            try
            {
                foreach (string num in nums)
                {
                    if (Convert.ToInt32(num) < 0 || Convert.ToInt32(num) > 255) return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 下载配置文件
        /// </summary>
        /// <param name="ftpPath"></param>
        void DownConfig(string ftpPath)
        {
            // public void Download(string userId, string pwd, string ftpPath, string filePath, string fileName)
            FTP.Download("", "", ftpPath, System.Environment.CurrentDirectory, "Config.txt");
        }
        /// <summary>
        /// 更新柜子配置文件
        /// </summary>
        /// <param name="ftpPath"> ftp路径</param>
        void Upload(string ftpPath)
        {
            // public void Upload(string userId, string pwd, string filename, string ftpPath)
            FTP.Upload("", "", "Config.txt", ftpPath);
        }
        /// <summary>
        /// 得到FTp路径 ftp://192.168.1.61/"
        /// </summary>
        /// <param name="gunIp">柜子IP</param>
        /// <returns></returns>
        string GetftpPath(string gunIp)
        {
            string ftpPathHeald = "ftp://";
            string ftpPath = ftpPathHeald + gunIp + "/";
            return ftpPath;
        }
    }
}
