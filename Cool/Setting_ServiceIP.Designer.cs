namespace Cool
{
    partial class Setting_ServiceIP
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
            this.txtSubMark = new System.Windows.Forms.Label();
            this.Alter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MaskedIP = new System.Windows.Forms.MaskedTextBox();
            this.txtIP = new System.Windows.Forms.Label();
            this.gunark_sub = new System.Windows.Forms.ComboBox();
            this.txtGateWay = new System.Windows.Forms.Label();
            this.gunark_gateway = new System.Windows.Forms.ComboBox();
            this.myPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // myPanel1
            // 
            this.myPanel1.BackColor = System.Drawing.Color.Transparent;
            this.myPanel1.BackgroundImage = global::Cool.Properties.Resources.bg;
            this.myPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.myPanel1.Controls.Add(this.groupBox2);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPanel1.Location = new System.Drawing.Point(0, 0);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(874, 548);
            this.myPanel1.TabIndex = 69;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtSubMark);
            this.groupBox2.Controls.Add(this.Alter);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.MaskedIP);
            this.groupBox2.Controls.Add(this.txtIP);
            this.groupBox2.Controls.Add(this.gunark_sub);
            this.groupBox2.Controls.Add(this.txtGateWay);
            this.groupBox2.Controls.Add(this.gunark_gateway);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox2.Location = new System.Drawing.Point(250, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 392);
            this.groupBox2.TabIndex = 72;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置库室服务器IP";
            // 
            // txtSubMark
            // 
            this.txtSubMark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtSubMark.AutoSize = true;
            this.txtSubMark.BackColor = System.Drawing.Color.Transparent;
            this.txtSubMark.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.txtSubMark.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtSubMark.Location = new System.Drawing.Point(45, 174);
            this.txtSubMark.Name = "txtSubMark";
            this.txtSubMark.Size = new System.Drawing.Size(123, 30);
            this.txtSubMark.TabIndex = 61;
            this.txtSubMark.Text = "子网掩码：";
            // 
            // Alter
            // 
            this.Alter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Alter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alter.Location = new System.Drawing.Point(289, 351);
            this.Alter.Name = "Alter";
            this.Alter.Size = new System.Drawing.Size(83, 35);
            this.Alter.TabIndex = 66;
            this.Alter.Text = "确定";
            this.Alter.UseVisualStyleBackColor = true;
            this.Alter.Click += new System.EventHandler(this.Alter_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 26F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(47, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 46);
            this.label1.TabIndex = 68;
            this.label1.Text = "修改本地服务器IP";
            // 
            // MaskedIP
            // 
            this.MaskedIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.MaskedIP.Location = new System.Drawing.Point(187, 116);
            this.MaskedIP.Name = "MaskedIP";
            this.MaskedIP.Size = new System.Drawing.Size(146, 26);
            this.MaskedIP.TabIndex = 63;
            // 
            // txtIP
            // 
            this.txtIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtIP.AutoSize = true;
            this.txtIP.BackColor = System.Drawing.Color.Transparent;
            this.txtIP.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.txtIP.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtIP.Location = new System.Drawing.Point(61, 116);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(107, 30);
            this.txtIP.TabIndex = 60;
            this.txtIP.Text = "IP 地址：";
            // 
            // gunark_sub
            // 
            this.gunark_sub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.gunark_sub.FormattingEnabled = true;
            this.gunark_sub.Location = new System.Drawing.Point(187, 174);
            this.gunark_sub.Name = "gunark_sub";
            this.gunark_sub.Size = new System.Drawing.Size(146, 24);
            this.gunark_sub.TabIndex = 62;
            // 
            // txtGateWay
            // 
            this.txtGateWay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtGateWay.AutoSize = true;
            this.txtGateWay.BackColor = System.Drawing.Color.Transparent;
            this.txtGateWay.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.txtGateWay.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtGateWay.Location = new System.Drawing.Point(45, 241);
            this.txtGateWay.Name = "txtGateWay";
            this.txtGateWay.Size = new System.Drawing.Size(123, 30);
            this.txtGateWay.TabIndex = 64;
            this.txtGateWay.Text = "默认网关：";
            // 
            // gunark_gateway
            // 
            this.gunark_gateway.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.gunark_gateway.FormattingEnabled = true;
            this.gunark_gateway.Location = new System.Drawing.Point(187, 241);
            this.gunark_gateway.Name = "gunark_gateway";
            this.gunark_gateway.Size = new System.Drawing.Size(147, 24);
            this.gunark_gateway.TabIndex = 65;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 548);
            this.Controls.Add(this.myPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.myPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyPanel myPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label txtSubMark;
        private System.Windows.Forms.Button Alter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox MaskedIP;
        private System.Windows.Forms.Label txtIP;
        private System.Windows.Forms.ComboBox gunark_sub;
        private System.Windows.Forms.Label txtGateWay;
        private System.Windows.Forms.ComboBox gunark_gateway;
    }
}