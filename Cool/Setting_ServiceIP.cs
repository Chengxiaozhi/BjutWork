using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Microsoft.Win32;
namespace Cool
{
    public partial class Setting_ServiceIP : Form
    {
        public Setting_ServiceIP()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //gunark_ip_Selected();
            gunark_sub_Selected();
            gunark_gateway_Selected();
        }

        private void Alter_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(" 确定要修改吗？", "修改IP", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);//这里为确定用户是否点击“确定”按钮
            try
            {
                if (!IsIpaddress(MaskedIP.Text.Trim()))
                {
                    MessageBox.Show("ip格式不正确！"); return;
                }
                if (!IsIpaddress(gunark_sub.Text.Trim()))
                {
                    MessageBox.Show("子网掩码格式不正确！"); return;
                }
                if (!IsIpaddress(gunark_gateway.Text.Trim()))
                {
                    MessageBox.Show("网关格式不正确！"); return;
                }

                string[] ip = new string[] { MaskedIP.Text.Trim() };
                string[] SubMark = new string[] { gunark_sub.Text.Trim() };
                string[] GateWay = new string[] { gunark_gateway.Text.Trim() };
                SetIPAddress(ip, SubMark, GateWay);
                MessageBox.Show("ip信息设置成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception er)
            {
                MessageBox.Show("设置出错：" + er.Message, "出错提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region
        /// <summary>
        /// 判断是否是正确的ip地址
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        protected bool IsIpaddress(string ipaddress)
        {
            string[] nums = ipaddress.Split('.');
            if (nums.Length != 4) return false;
            foreach (string num in nums)
            {
                if (Convert.ToInt32(num) < 0 || Convert.ToInt32(num) > 255) return false;
            }
            return true;
        }
        /// <summary>
        /// 设置本机IP地址
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="submask"></param>
        /// <param name="getway"></param>
        private void SetIPAddress(string[] ip, string[] submask, string[] getway)
        {
            try
            {
                ManagementClass wmi = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = wmi.GetInstances();
                ManagementBaseObject inPar = null;
                ManagementBaseObject outPar = null;

                foreach (ManagementObject mo in moc)
                {
                    //如果没有启用IP设置的网络设备则跳过
                    if (!(bool)mo["IPEnabled"])
                        continue;
                    //设置IP地址和掩码

                    inPar = mo.GetMethodParameters("EnableStatic");
                    inPar["IPAddress"] = ip;
                    inPar["SubnetMask"] = submask;
                    outPar = mo.InvokeMethod("EnableStatic", inPar, null);

                    inPar = mo.GetMethodParameters("SetGateways");
                    inPar["DefaultIPGateway"] = getway;
                    outPar = mo.InvokeMethod("SetGateways", inPar, null);


                }
                // MessageBox.Show("设置成功！");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void gunark_ip_Selected()//获取IP地址
        {
            string IP = selectSetting("GUNARK_IP");

            MaskedIP.Text = IP;

        }
        private void gunark_sub_Selected()// 获取子网掩码
        {

            //string[] sub = { "255.255.255.0", "255.255.0.0", "255.0.0.0" };
            string[] sub = selectconfig("GUNARK_SUBNET");
            for (int i = 0; i < sub.Length; i++)
            {
                gunark_sub.Items.Add(sub[i]);

            }
            int n = sub.Length - 1;
            gunark_sub.Text = sub[n];
        }
        private void gunark_gateway_Selected()//获取网关
        {
            string gateway = selectSetting("GUNARK_GATEWAY");

            gunark_gateway.Text = gateway;

        }
        public string selectSetting(string value)
        {
            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection conn = new MySqlConnection(str);
            conn.Open();
            string sqlFinger = "select " + value + " from gunark_gunark";
            MySqlCommand cmd = new MySqlCommand(sqlFinger, conn);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            DataTable dt = ds.Tables[0];
            int len1 = ds.Tables[0].Rows.Count;
            int n = len1 - 1;
            string ID = "";

            ID = ds.Tables[0].Rows[n][0].ToString();
            string IP = ID;
            ID = "";

            return IP;
        }

        public string[] selectconfig(string value)
        {
            string conn1 = "Server = localhost;User ID = root; Password = 123456; Database = qdgl;charset = utf8; ";
            MySqlConnection conn = new MySqlConnection(conn1);
            conn.Open();
            string selectSub = "select " + value + " from gunark_gunark";
            MySqlCommand cmd = new MySqlCommand(selectSub, conn);
            MySqlDataAdapter dada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dada.Fill(ds);
            DataTable dt = ds.Tables[0];
            int len1 = dt.Rows.Count;
            string[] config = new string[len1];
            int i = 0;
            for (i = 0; i < len1; i++)
            {
                config[i] = dt.Rows[i][0].ToString();


            }
            return config;

        }
        #endregion
    }
}
