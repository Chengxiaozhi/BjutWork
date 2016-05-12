namespace Cool
{
    partial class Setting_GunID
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
            this.myPanel1 = new Cool.MyPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gunark_gateway = new System.Windows.Forms.TextBox();
            this.gunark_subway = new System.Windows.Forms.TextBox();
            this.gunark_ip = new System.Windows.Forms.TextBox();
            this.gunark_num = new System.Windows.Forms.TextBox();
            this.gunark_name = new System.Windows.Forms.ComboBox();
            this.txtSubMark = new System.Windows.Forms.Label();
            this.txtGateWay = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Alter = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.myPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // myPanel1
            // 
            this.myPanel1.BackgroundImage = global::Cool.Properties.Resources.bg;
            this.myPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.myPanel1.Controls.Add(this.groupBox2);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPanel1.Location = new System.Drawing.Point(0, 0);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(873, 549);
            this.myPanel1.TabIndex = 70;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.gunark_gateway);
            this.groupBox2.Controls.Add(this.gunark_subway);
            this.groupBox2.Controls.Add(this.gunark_ip);
            this.groupBox2.Controls.Add(this.gunark_num);
            this.groupBox2.Controls.Add(this.gunark_name);
            this.groupBox2.Controls.Add(this.txtSubMark);
            this.groupBox2.Controls.Add(this.txtGateWay);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.Alter);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox2.Location = new System.Drawing.Point(214, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(478, 444);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置枪柜IP";
            // 
            // gunark_gateway
            // 
            this.gunark_gateway.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.gunark_gateway.Location = new System.Drawing.Point(240, 285);
            this.gunark_gateway.Name = "gunark_gateway";
            this.gunark_gateway.Size = new System.Drawing.Size(146, 26);
            this.gunark_gateway.TabIndex = 77;
            // 
            // gunark_subway
            // 
            this.gunark_subway.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.gunark_subway.Location = new System.Drawing.Point(240, 248);
            this.gunark_subway.Name = "gunark_subway";
            this.gunark_subway.Size = new System.Drawing.Size(146, 26);
            this.gunark_subway.TabIndex = 76;
            // 
            // gunark_ip
            // 
            this.gunark_ip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.gunark_ip.Location = new System.Drawing.Point(240, 205);
            this.gunark_ip.Name = "gunark_ip";
            this.gunark_ip.Size = new System.Drawing.Size(146, 26);
            this.gunark_ip.TabIndex = 75;
            // 
            // gunark_num
            // 
            this.gunark_num.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.gunark_num.Location = new System.Drawing.Point(240, 160);
            this.gunark_num.Name = "gunark_num";
            this.gunark_num.Size = new System.Drawing.Size(146, 26);
            this.gunark_num.TabIndex = 74;
            // 
            // gunark_name
            // 
            this.gunark_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.gunark_name.FormattingEnabled = true;
            this.gunark_name.Location = new System.Drawing.Point(240, 119);
            this.gunark_name.Name = "gunark_name";
            this.gunark_name.Size = new System.Drawing.Size(146, 24);
            this.gunark_name.TabIndex = 73;
            this.gunark_name.Text = "请选择枪柜";
            this.gunark_name.TextChanged += new System.EventHandler(this.gunark_name_TextChanged);
            // 
            // txtSubMark
            // 
            this.txtSubMark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtSubMark.AutoSize = true;
            this.txtSubMark.BackColor = System.Drawing.Color.Transparent;
            this.txtSubMark.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.txtSubMark.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtSubMark.Location = new System.Drawing.Point(111, 243);
            this.txtSubMark.Name = "txtSubMark";
            this.txtSubMark.Size = new System.Drawing.Size(123, 30);
            this.txtSubMark.TabIndex = 69;
            this.txtSubMark.Text = "子网掩码：";
            // 
            // txtGateWay
            // 
            this.txtGateWay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtGateWay.AutoSize = true;
            this.txtGateWay.BackColor = System.Drawing.Color.Transparent;
            this.txtGateWay.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.txtGateWay.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtGateWay.Location = new System.Drawing.Point(111, 285);
            this.txtGateWay.Name = "txtGateWay";
            this.txtGateWay.Size = new System.Drawing.Size(123, 30);
            this.txtGateWay.TabIndex = 71;
            this.txtGateWay.Text = "默认网关：";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(111, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "枪柜编号：";
            // 
            // Alter
            // 
            this.Alter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Alter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alter.Location = new System.Drawing.Point(389, 403);
            this.Alter.Name = "Alter";
            this.Alter.Size = new System.Drawing.Size(83, 35);
            this.Alter.TabIndex = 66;
            this.Alter.Text = "确定";
            this.Alter.UseVisualStyleBackColor = true;
            this.Alter.Click += new System.EventHandler(this.Alter_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 26F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(143, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 46);
            this.label4.TabIndex = 68;
            this.label4.Text = "设置枪柜IP";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(90, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "枪柜IP地址：";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(111, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "枪柜名称：";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // Setting_GunID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 549);
            this.Controls.Add(this.myPanel1);
            this.Name = "Setting_GunID";
            this.Text = "GunIp";
            this.Load += new System.EventHandler(this.GunIp_Load);
            this.myPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyPanel myPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox gunark_gateway;
        private System.Windows.Forms.TextBox gunark_subway;
        private System.Windows.Forms.TextBox gunark_ip;
        private System.Windows.Forms.TextBox gunark_num;
        private System.Windows.Forms.ComboBox gunark_name;
        private System.Windows.Forms.Label txtSubMark;
        private System.Windows.Forms.Label txtGateWay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Alter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}