namespace Cool
{
    partial class Setting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.myPanel1 = new Cool.MyPanel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置服务器IPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置枪柜IPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置远程服务器IPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.myPanel1.SuspendLayout();
            this.toolStripContainer1.LeftToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(71, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(256, 21);
            this.label5.TabIndex = 46;
            this.label5.Text = "当前位置->枪弹柜管理->系统设置";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Cool.Properties.Resources.blaster_gun_24px_1061573_easyicon_net;
            this.pictureBox1.Location = new System.Drawing.Point(30, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 25);
            this.pictureBox1.TabIndex = 48;
            this.pictureBox1.TabStop = false;
            // 
            // myPanel1
            // 
            this.myPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myPanel1.BackColor = System.Drawing.Color.Transparent;
            this.myPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.myPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.myPanel1.Controls.Add(this.toolStripContainer1);
            this.myPanel1.Location = new System.Drawing.Point(30, 40);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(840, 450);
            this.myPanel1.TabIndex = 47;
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Enabled = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.toolStripContainer1.ContentPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripContainer1.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(615, 415);
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            this.toolStripContainer1.LeftToolStripPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this.statusStrip1);
            this.toolStripContainer1.Location = new System.Drawing.Point(3, 3);
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.RightToolStripPanel
            // 
            this.toolStripContainer1.RightToolStripPanel.Enabled = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(830, 440);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Enabled = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置服务器IPToolStripMenuItem,
            this.设置枪柜IPToolStripMenuItem,
            this.设置远程服务器IPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(187, 415);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 设置服务器IPToolStripMenuItem
            // 
            this.设置服务器IPToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.设置服务器IPToolStripMenuItem.Image = global::Cool.Properties.Resources.SETTING_32px_1115307_easyicon_net;
            this.设置服务器IPToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.设置服务器IPToolStripMenuItem.Name = "设置服务器IPToolStripMenuItem";
            this.设置服务器IPToolStripMenuItem.Size = new System.Drawing.Size(180, 36);
            this.设置服务器IPToolStripMenuItem.Text = "设置库室服务器IP";
            this.设置服务器IPToolStripMenuItem.Click += new System.EventHandler(this.设置服务器IPToolStripMenuItem_Click);
            // 
            // 设置枪柜IPToolStripMenuItem
            // 
            this.设置枪柜IPToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.设置枪柜IPToolStripMenuItem.Image = global::Cool.Properties.Resources.setting_30_613333333333px_1147319_easyicon_net;
            this.设置枪柜IPToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.设置枪柜IPToolStripMenuItem.Name = "设置枪柜IPToolStripMenuItem";
            this.设置枪柜IPToolStripMenuItem.Size = new System.Drawing.Size(180, 35);
            this.设置枪柜IPToolStripMenuItem.Text = "设置枪柜IP        ";
            this.设置枪柜IPToolStripMenuItem.Click += new System.EventHandler(this.设置枪柜IPToolStripMenuItem_Click);
            // 
            // 设置远程服务器IPToolStripMenuItem
            // 
            this.设置远程服务器IPToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.设置远程服务器IPToolStripMenuItem.Image = global::Cool.Properties.Resources.Server_setting_32px_1138371_easyicon_net;
            this.设置远程服务器IPToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.设置远程服务器IPToolStripMenuItem.Name = "设置远程服务器IPToolStripMenuItem";
            this.设置远程服务器IPToolStripMenuItem.Size = new System.Drawing.Size(180, 36);
            this.设置远程服务器IPToolStripMenuItem.Text = "设置远程服务器IP";
            this.设置远程服务器IPToolStripMenuItem.Click += new System.EventHandler(this.设置远程服务器IPToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(187, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(28, 415);
            this.statusStrip1.TabIndex = 1;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(26, 385);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "<>";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Cool.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(904, 512);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.myPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Setting";
            this.Text = "系统设置";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Setting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.myPanel1.ResumeLayout(false);
            this.toolStripContainer1.LeftToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.LeftToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private MyPanel myPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置服务器IPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置枪柜IPToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem 设置远程服务器IPToolStripMenuItem;
    }
}