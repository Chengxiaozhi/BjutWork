namespace Cool
{
    partial class Setting_WebService_IP
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Alter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.WebService_IP = new System.Windows.Forms.MaskedTextBox();
            this.txtIP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PrintTB = new System.Windows.Forms.MaskedTextBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.Alter);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.PrintTB);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.WebService_IP);
            this.groupBox2.Controls.Add(this.txtIP);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox2.Location = new System.Drawing.Point(249, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(407, 356);
            this.groupBox2.TabIndex = 73;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置远程服务器IP";
            // 
            // Alter
            // 
            this.Alter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Alter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alter.Location = new System.Drawing.Point(318, 315);
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
            this.label1.Location = new System.Drawing.Point(61, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 46);
            this.label1.TabIndex = 68;
            this.label1.Text = "修改远程服务器IP";
            // 
            // WebService_IP
            // 
            this.WebService_IP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.WebService_IP.Location = new System.Drawing.Point(201, 120);
            this.WebService_IP.Name = "WebService_IP";
            this.WebService_IP.Size = new System.Drawing.Size(146, 26);
            this.WebService_IP.TabIndex = 63;
            this.WebService_IP.TextChanged += new System.EventHandler(this.WebService_IP_TextChanged);
            // 
            // txtIP
            // 
            this.txtIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtIP.AutoSize = true;
            this.txtIP.BackColor = System.Drawing.Color.Transparent;
            this.txtIP.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.txtIP.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtIP.Location = new System.Drawing.Point(75, 116);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(107, 30);
            this.txtIP.TabIndex = 60;
            this.txtIP.Text = "IP 地址：";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(75, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 30);
            this.label2.TabIndex = 60;
            this.label2.Text = "指纹仪IP：";
            // 
            // PrintTB
            // 
            this.PrintTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.PrintTB.Location = new System.Drawing.Point(201, 173);
            this.PrintTB.Name = "PrintTB";
            this.PrintTB.Size = new System.Drawing.Size(146, 26);
            this.PrintTB.TabIndex = 63;
            this.PrintTB.TextChanged += new System.EventHandler(this.WebService_IP_TextChanged);
            // 
            // Setting_WebService_IP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Cool.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(904, 512);
            this.Controls.Add(this.groupBox2);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Setting_WebService_IP";
            this.Text = "Setting_WebService_IP";
            this.Load += new System.EventHandler(this.Setting_WebService_IP_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Alter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox WebService_IP;
        private System.Windows.Forms.Label txtIP;
        private System.Windows.Forms.MaskedTextBox PrintTB;
        private System.Windows.Forms.Label label2;
    }
}