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
    public partial class Setting_WebService_IP : Form
    {
        DataSet ds = new DataSet();
   
        public Setting_WebService_IP()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
        }

        private void Setting_WebService_IP_Load(object sender, EventArgs e)
        {
            //获取当前服务器IP地址
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            con.Open();
            string sql = "select IP from gunark_config";
            ds = DatabaseManager.ExecAndGetDs(sql,"gunark_config",con);
            DatabaseManager.Clear(con);

            WebService_IP.Text = ds.Tables[0].Rows[0][0].ToString();

        }

        private void WebService_IP_TextChanged(object sender, EventArgs e)
        {
            if (WebService_IP.Equals(ds.Tables[0].Rows[0][0].ToString()))
            {
                WebService_IP.Text = "";
            }
        }
        /// <summary>
        /// 修改IP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Alter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "您确定要修改远程服务器的IP么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
            {
                try
                {
                    if (!IsIpaddress(WebService_IP.Text.Trim()) && !IsIpaddress(PrintTB.Text.Trim()))
                    {
                        MessageBox.Show("ip格式不正确！"); return;
                    }
                    else
                    {
                        MySqlConnection con = null;
                        con = DatabaseManager.GetCon(con);
                        con.Open();
                        string sql = "update gunark_config set IP = '" + WebService_IP.Text + "' , PRINTIP = '"+ PrintTB.Text.Trim()+"'";
                        DatabaseManager.Exec(sql,con);
                        DatabaseManager.Clear(con);
                        MessageBox.Show("ip信息设置成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                  
                }
                catch
                {
                    MessageBox.Show(this, "设置失败，请重新设置！", "提示");
                }
            }
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
            foreach (string num in nums)
            {
                if (Convert.ToInt32(num) < 0 || Convert.ToInt32(num) > 255) return false;
            }
            return true;
        }
    }
}
