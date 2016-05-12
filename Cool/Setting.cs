using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Cool
{
    public partial class Setting : Form
    {
        private int ItemClick = 0;
        public Setting()
        {
            InitializeComponent();
           //this.IsMdiContainer = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            this.toolStripContainer1.ContentPanel.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(toolStripContainer1.ContentPanel, true, null);
            toolStripContainer1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(toolStripContainer1, true, null);
            myPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(myPanel1, true, null);
        }
        private void Setting_Load(object sender, EventArgs e)
        {
            toolStripContainer1.TopToolStripPanelVisible = false;
            toolStripContainer1.RightToolStripPanelVisible = false;
            toolStripContainer1.BottomToolStripPanelVisible = false;
            Setting_ServiceIP s = new Setting_ServiceIP();
            toolStripContainer1.ContentPanel.Controls.Clear();//这里是清空panel2中的控件的。
            s.FormBorderStyle = FormBorderStyle.None;
            s.TopLevel = false;
            s.Dock = System.Windows.Forms.DockStyle.Fill;//把子窗体设置为控件
            s.FormBorderStyle = FormBorderStyle.None;
            this.toolStripContainer1.ContentPanel.Controls.Add(s);
            s.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
            if (e.ClickedItem.ToString().Equals("设置服务器IP"))
            {
                ItemClick = 0;
            }
            if (e.ClickedItem.ToString().Equals("设置枪柜IP"))
            {
                ItemClick = 1;
            }

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            if (this.menuStrip1.Visible == true)
            {
                this.menuStrip1.Visible = false;
            }
            else
            {
                this.menuStrip1.Visible = true;
            }
            if (ItemClick == 0)
            {
                Setting_ServiceIP s = new Setting_ServiceIP();
                toolStripContainer1.ContentPanel.Controls.Clear();//这里是清空panel2中的控件的。
                s.FormBorderStyle = FormBorderStyle.None;
                s.TopLevel = false;
                s.Dock = System.Windows.Forms.DockStyle.Fill;//把子窗体设置为控件
                s.FormBorderStyle = FormBorderStyle.None;
                this.toolStripContainer1.ContentPanel.Controls.Add(s);
                s.Show();
            }
            else if (ItemClick == 1)
            {
                Setting_GunID s = new Setting_GunID();
                toolStripContainer1.ContentPanel.Controls.Clear();//这里是清空panel2中的控件的。
                s.FormBorderStyle = FormBorderStyle.None;
                s.TopLevel = false;
                s.Dock = System.Windows.Forms.DockStyle.Fill;//把子窗体设置为控件
                s.FormBorderStyle = FormBorderStyle.None;
                this.toolStripContainer1.ContentPanel.Controls.Add(s);
                s.Show();
            }
        }

        private void 设置服务器IPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting_ServiceIP s = new Setting_ServiceIP();
            toolStripContainer1.ContentPanel.Controls.Clear();//这里是清空panel2中的控件的。
            s.FormBorderStyle = FormBorderStyle.None;
            s.TopLevel = false;
            s.Dock = System.Windows.Forms.DockStyle.Fill;//把子窗体设置为控件
            s.FormBorderStyle = FormBorderStyle.None;
            this.toolStripContainer1.ContentPanel.Controls.Add(s);
            s.Show();
        }

        private void 设置枪柜IPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting_GunID s = new Setting_GunID();
            toolStripContainer1.ContentPanel.Controls.Clear();//这里是清空panel2中的控件的。
            s.FormBorderStyle = FormBorderStyle.None;
            s.TopLevel = false;
            s.Dock = System.Windows.Forms.DockStyle.Fill;//把子窗体设置为控件
            s.FormBorderStyle = FormBorderStyle.None;
            this.toolStripContainer1.ContentPanel.Controls.Add(s);
            s.Show();
        }

        private void 设置远程服务器IPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting_WebService_IP swi = new Setting_WebService_IP();
            toolStripContainer1.ContentPanel.Controls.Clear();//这里是清空panel2中的控件的。
            swi.FormBorderStyle = FormBorderStyle.None;
            swi.TopLevel = false;
            swi.Dock = System.Windows.Forms.DockStyle.Fill;//把子窗体设置为控件
            swi.FormBorderStyle = FormBorderStyle.None;
            this.toolStripContainer1.ContentPanel.Controls.Add(swi);
            swi.Show();
        }

        
    }
}
