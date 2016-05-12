namespace Cool
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.WebService_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.StorageSever_state = new System.Windows.Forms.ToolStripStatusLabel();
            this.prclient_state = new System.Windows.Forms.ToolStripStatusLabel();
            this.prSever_state = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.主页ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.任务查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.任务执行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.常规任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.其他任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.其他任务ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.枪弹库管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.消除报警ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.紧急任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.紧急取枪弹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.紧急还枪弹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.人员录入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.人员录入ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.指纹录入ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.信息查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.枪支信息查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.人员信息查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出系统ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.退出系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new Cool.MyPanel();
            this.cboClient = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Info;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.WebService_Status,
            this.StorageSever_state,
            this.prclient_state,
            this.prSever_state,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 719);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1306, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(412, 21);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // WebService_Status
            // 
            this.WebService_Status.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.WebService_Status.ForeColor = System.Drawing.Color.Red;
            this.WebService_Status.Name = "WebService_Status";
            this.WebService_Status.Size = new System.Drawing.Size(198, 21);
            this.WebService_Status.Text = "【正在连接远程服务器...】";
            // 
            // StorageSever_state
            // 
            this.StorageSever_state.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.StorageSever_state.ForeColor = System.Drawing.Color.Red;
            this.StorageSever_state.Name = "StorageSever_state";
            this.StorageSever_state.Size = new System.Drawing.Size(170, 21);
            this.StorageSever_state.Text = "【未启动枪柜服务器】";
            // 
            // prclient_state
            // 
            this.prclient_state.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.prclient_state.ForeColor = System.Drawing.Color.Red;
            this.prclient_state.Name = "prclient_state";
            this.prclient_state.Size = new System.Drawing.Size(170, 21);
            this.prclient_state.Text = "【未连接指纹客户端】";
            // 
            // prSever_state
            // 
            this.prSever_state.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.prSever_state.ForeColor = System.Drawing.Color.Red;
            this.prSever_state.Name = "prSever_state";
            this.prSever_state.Size = new System.Drawing.Size(170, 21);
            this.prSever_state.Text = "【未连接指纹服务器】";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(171, 21);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Menu;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.主页ToolStripMenuItem,
            this.任务查询ToolStripMenuItem,
            this.任务执行ToolStripMenuItem,
            this.枪弹库管理ToolStripMenuItem,
            this.紧急任务ToolStripMenuItem,
            this.人员录入ToolStripMenuItem,
            this.信息查询ToolStripMenuItem,
            this.退出系统ToolStripMenuItem1,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1306, 56);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 主页ToolStripMenuItem
            // 
            this.主页ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.主页ToolStripMenuItem.Image = global::Cool.Properties.Resources.zhuye;
            this.主页ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.主页ToolStripMenuItem.Name = "主页ToolStripMenuItem";
            this.主页ToolStripMenuItem.Size = new System.Drawing.Size(105, 52);
            this.主页ToolStripMenuItem.Text = "主页";
            this.主页ToolStripMenuItem.Click += new System.EventHandler(this.主页ToolStripMenuItem_Click);
            // 
            // 任务查询ToolStripMenuItem
            // 
            this.任务查询ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.任务查询ToolStripMenuItem.Image = global::Cool.Properties.Resources.任务查询;
            this.任务查询ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.任务查询ToolStripMenuItem.Name = "任务查询ToolStripMenuItem";
            this.任务查询ToolStripMenuItem.Size = new System.Drawing.Size(142, 52);
            this.任务查询ToolStripMenuItem.Text = "任务查询";
            this.任务查询ToolStripMenuItem.Click += new System.EventHandler(this.任务查询ToolStripMenuItem_Click);
            // 
            // 任务执行ToolStripMenuItem
            // 
            this.任务执行ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.常规任务ToolStripMenuItem,
            this.其他任务ToolStripMenuItem,
            this.其他任务ToolStripMenuItem1});
            this.任务执行ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.任务执行ToolStripMenuItem.Image = global::Cool.Properties.Resources.任务执行;
            this.任务执行ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.任务执行ToolStripMenuItem.Name = "任务执行ToolStripMenuItem";
            this.任务执行ToolStripMenuItem.Size = new System.Drawing.Size(142, 52);
            this.任务执行ToolStripMenuItem.Text = "任务执行";
            this.任务执行ToolStripMenuItem.Click += new System.EventHandler(this.任务执行ToolStripMenuItem_Click);
            // 
            // 常规任务ToolStripMenuItem
            // 
            this.常规任务ToolStripMenuItem.Image = global::Cool.Properties.Resources.领取枪弹;
            this.常规任务ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.常规任务ToolStripMenuItem.Name = "常规任务ToolStripMenuItem";
            this.常规任务ToolStripMenuItem.Size = new System.Drawing.Size(168, 38);
            this.常规任务ToolStripMenuItem.Text = "领取枪弹";
            this.常规任务ToolStripMenuItem.Click += new System.EventHandler(this.领取枪弹ToolStripMenuItem_Click);
            // 
            // 其他任务ToolStripMenuItem
            // 
            this.其他任务ToolStripMenuItem.Image = global::Cool.Properties.Resources.归还枪弹;
            this.其他任务ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.其他任务ToolStripMenuItem.Name = "其他任务ToolStripMenuItem";
            this.其他任务ToolStripMenuItem.Size = new System.Drawing.Size(168, 38);
            this.其他任务ToolStripMenuItem.Text = "归还枪弹";
            this.其他任务ToolStripMenuItem.Click += new System.EventHandler(this.归还枪弹ToolStripMenuItem_Click);
            // 
            // 其他任务ToolStripMenuItem1
            // 
            this.其他任务ToolStripMenuItem1.Image = global::Cool.Properties.Resources.其他任务;
            this.其他任务ToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.其他任务ToolStripMenuItem1.Name = "其他任务ToolStripMenuItem1";
            this.其他任务ToolStripMenuItem1.Size = new System.Drawing.Size(168, 38);
            this.其他任务ToolStripMenuItem1.Text = "其他任务";
            this.其他任务ToolStripMenuItem1.Click += new System.EventHandler(this.其他任务ToolStripMenuItem1_Click);
            // 
            // 枪弹库管理ToolStripMenuItem
            // 
            this.枪弹库管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.消除报警ToolStripMenuItem,
            this.日志ToolStripMenuItem,
            this.系统管理ToolStripMenuItem});
            this.枪弹库管理ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.枪弹库管理ToolStripMenuItem.Image = global::Cool.Properties.Resources.枪弹库管理;
            this.枪弹库管理ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.枪弹库管理ToolStripMenuItem.Name = "枪弹库管理ToolStripMenuItem";
            this.枪弹库管理ToolStripMenuItem.Size = new System.Drawing.Size(160, 52);
            this.枪弹库管理ToolStripMenuItem.Text = "枪弹库管理";
            this.枪弹库管理ToolStripMenuItem.Click += new System.EventHandler(this.枪弹库管理ToolStripMenuItem_Click);
            // 
            // 消除报警ToolStripMenuItem
            // 
            this.消除报警ToolStripMenuItem.Image = global::Cool.Properties.Resources.消除警报;
            this.消除报警ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.消除报警ToolStripMenuItem.Name = "消除报警ToolStripMenuItem";
            this.消除报警ToolStripMenuItem.Size = new System.Drawing.Size(168, 38);
            this.消除报警ToolStripMenuItem.Text = "消除报警";
            this.消除报警ToolStripMenuItem.Click += new System.EventHandler(this.消除报警ToolStripMenuItem_Click);
            // 
            // 日志ToolStripMenuItem
            // 
            this.日志ToolStripMenuItem.Image = global::Cool.Properties.Resources.日志;
            this.日志ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.日志ToolStripMenuItem.Name = "日志ToolStripMenuItem";
            this.日志ToolStripMenuItem.Size = new System.Drawing.Size(168, 38);
            this.日志ToolStripMenuItem.Text = "日志";
            this.日志ToolStripMenuItem.Click += new System.EventHandler(this.日志ToolStripMenuItem_Click);
            // 
            // 系统管理ToolStripMenuItem
            // 
            this.系统管理ToolStripMenuItem.Image = global::Cool.Properties.Resources.系统设置;
            this.系统管理ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.系统管理ToolStripMenuItem.Name = "系统管理ToolStripMenuItem";
            this.系统管理ToolStripMenuItem.Size = new System.Drawing.Size(168, 38);
            this.系统管理ToolStripMenuItem.Text = "系统管理";
            this.系统管理ToolStripMenuItem.Click += new System.EventHandler(this.系统管理ToolStripMenuItem_Click);
            // 
            // 紧急任务ToolStripMenuItem
            // 
            this.紧急任务ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.紧急取枪弹ToolStripMenuItem,
            this.紧急还枪弹ToolStripMenuItem});
            this.紧急任务ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.紧急任务ToolStripMenuItem.Image = global::Cool.Properties.Resources.emercency01;
            this.紧急任务ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.紧急任务ToolStripMenuItem.Name = "紧急任务ToolStripMenuItem";
            this.紧急任务ToolStripMenuItem.Size = new System.Drawing.Size(142, 52);
            this.紧急任务ToolStripMenuItem.Text = "紧急任务";
            this.紧急任务ToolStripMenuItem.Click += new System.EventHandler(this.紧急任务ToolStripMenuItem_Click);
            // 
            // 紧急取枪弹ToolStripMenuItem
            // 
            this.紧急取枪弹ToolStripMenuItem.Image = global::Cool.Properties.Resources.领取枪弹;
            this.紧急取枪弹ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.紧急取枪弹ToolStripMenuItem.Name = "紧急取枪弹ToolStripMenuItem";
            this.紧急取枪弹ToolStripMenuItem.Size = new System.Drawing.Size(186, 38);
            this.紧急取枪弹ToolStripMenuItem.Text = "紧急取枪弹";
            this.紧急取枪弹ToolStripMenuItem.Click += new System.EventHandler(this.紧急取枪弹ToolStripMenuItem_Click);
            // 
            // 紧急还枪弹ToolStripMenuItem
            // 
            this.紧急还枪弹ToolStripMenuItem.Image = global::Cool.Properties.Resources.归还枪弹;
            this.紧急还枪弹ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.紧急还枪弹ToolStripMenuItem.Name = "紧急还枪弹ToolStripMenuItem";
            this.紧急还枪弹ToolStripMenuItem.Size = new System.Drawing.Size(186, 38);
            this.紧急还枪弹ToolStripMenuItem.Text = "归还枪弹";
            this.紧急还枪弹ToolStripMenuItem.Click += new System.EventHandler(this.紧急还枪弹ToolStripMenuItem_Click);
            // 
            // 人员录入ToolStripMenuItem
            // 
            this.人员录入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.人员录入ToolStripMenuItem1,
            this.指纹录入ToolStripMenuItem1});
            this.人员录入ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.人员录入ToolStripMenuItem.Image = global::Cool.Properties.Resources.信息录入;
            this.人员录入ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.人员录入ToolStripMenuItem.Name = "人员录入ToolStripMenuItem";
            this.人员录入ToolStripMenuItem.Size = new System.Drawing.Size(142, 52);
            this.人员录入ToolStripMenuItem.Text = "信息录入";
            // 
            // 人员录入ToolStripMenuItem1
            // 
            this.人员录入ToolStripMenuItem1.Image = global::Cool.Properties.Resources.add_user_group_new_48px_147_easyicon_net;
            this.人员录入ToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.人员录入ToolStripMenuItem1.Name = "人员录入ToolStripMenuItem1";
            this.人员录入ToolStripMenuItem1.Size = new System.Drawing.Size(168, 38);
            this.人员录入ToolStripMenuItem1.Text = "人员录入";
            this.人员录入ToolStripMenuItem1.Click += new System.EventHandler(this.人员录入ToolStripMenuItem1_Click);
            // 
            // 指纹录入ToolStripMenuItem1
            // 
            this.指纹录入ToolStripMenuItem1.Image = global::Cool.Properties.Resources.指纹录入;
            this.指纹录入ToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.指纹录入ToolStripMenuItem1.Name = "指纹录入ToolStripMenuItem1";
            this.指纹录入ToolStripMenuItem1.Size = new System.Drawing.Size(168, 38);
            this.指纹录入ToolStripMenuItem1.Text = "指纹录入";
            this.指纹录入ToolStripMenuItem1.Click += new System.EventHandler(this.指纹录入ToolStripMenuItem1_Click);
            // 
            // 信息查询ToolStripMenuItem
            // 
            this.信息查询ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.枪支信息查询ToolStripMenuItem,
            this.人员信息查询ToolStripMenuItem});
            this.信息查询ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.信息查询ToolStripMenuItem.Image = global::Cool.Properties.Resources.信息查询;
            this.信息查询ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.信息查询ToolStripMenuItem.Name = "信息查询ToolStripMenuItem";
            this.信息查询ToolStripMenuItem.Size = new System.Drawing.Size(142, 52);
            this.信息查询ToolStripMenuItem.Text = "信息查询";
            this.信息查询ToolStripMenuItem.Click += new System.EventHandler(this.信息查询ToolStripMenuItem_Click);
            // 
            // 枪支信息查询ToolStripMenuItem
            // 
            this.枪支信息查询ToolStripMenuItem.Image = global::Cool.Properties.Resources.枪支信息查询;
            this.枪支信息查询ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.枪支信息查询ToolStripMenuItem.Name = "枪支信息查询ToolStripMenuItem";
            this.枪支信息查询ToolStripMenuItem.Size = new System.Drawing.Size(204, 38);
            this.枪支信息查询ToolStripMenuItem.Text = "枪支信息查询";
            this.枪支信息查询ToolStripMenuItem.Click += new System.EventHandler(this.枪支信息查询ToolStripMenuItem_Click);
            // 
            // 人员信息查询ToolStripMenuItem
            // 
            this.人员信息查询ToolStripMenuItem.Image = global::Cool.Properties.Resources.人员信息查询;
            this.人员信息查询ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.人员信息查询ToolStripMenuItem.Name = "人员信息查询ToolStripMenuItem";
            this.人员信息查询ToolStripMenuItem.Size = new System.Drawing.Size(204, 38);
            this.人员信息查询ToolStripMenuItem.Text = "人员信息查询";
            this.人员信息查询ToolStripMenuItem.Click += new System.EventHandler(this.人员信息查询ToolStripMenuItem_Click);
            // 
            // 退出系统ToolStripMenuItem1
            // 
            this.退出系统ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出系统ToolStripMenuItem,
            this.重新登录ToolStripMenuItem});
            this.退出系统ToolStripMenuItem1.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.退出系统ToolStripMenuItem1.Image = global::Cool.Properties.Resources.退出系统;
            this.退出系统ToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.退出系统ToolStripMenuItem1.Name = "退出系统ToolStripMenuItem1";
            this.退出系统ToolStripMenuItem1.Size = new System.Drawing.Size(141, 52);
            this.退出系统ToolStripMenuItem1.Text = "退出系统";
            this.退出系统ToolStripMenuItem1.Click += new System.EventHandler(this.退出系统ToolStripMenuItem1_Click);
            // 
            // 退出系统ToolStripMenuItem
            // 
            this.退出系统ToolStripMenuItem.Image = global::Cool.Properties.Resources.退出系统2_net;
            this.退出系统ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.退出系统ToolStripMenuItem.Name = "退出系统ToolStripMenuItem";
            this.退出系统ToolStripMenuItem.Size = new System.Drawing.Size(168, 38);
            this.退出系统ToolStripMenuItem.Text = "退出系统";
            this.退出系统ToolStripMenuItem.Click += new System.EventHandler(this.退出系统ToolStripMenuItem_Click);
            // 
            // 重新登录ToolStripMenuItem
            // 
            this.重新登录ToolStripMenuItem.Image = global::Cool.Properties.Resources.重新登录;
            this.重新登录ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.重新登录ToolStripMenuItem.Name = "重新登录ToolStripMenuItem";
            this.重新登录ToolStripMenuItem.Size = new System.Drawing.Size(168, 38);
            this.重新登录ToolStripMenuItem.Text = "重新登录";
            this.重新登录ToolStripMenuItem.Click += new System.EventHandler(this.重新登录ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.关于ToolStripMenuItem.Image = global::Cool.Properties.Resources.AboutMe;
            this.关于ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(106, 52);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Cool.Properties.Resources.bg;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.cboClient);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1306, 663);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cboClient
            // 
            this.cboClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboClient.FormattingEnabled = true;
            this.cboClient.Location = new System.Drawing.Point(1138, 3);
            this.cboClient.Name = "cboClient";
            this.cboClient.Size = new System.Drawing.Size(161, 22);
            this.cboClient.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1306, 745);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "枪弹管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 任务查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 任务执行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 枪弹库管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 紧急任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 信息查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出系统ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 枪支信息查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 人员信息查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 常规任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 其他任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 消除报警ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 其他任务ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 退出系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重新登录ToolStripMenuItem;
        private System.Windows.Forms.ComboBox cboClient;
        private System.Windows.Forms.ToolStripStatusLabel StorageSever_state;
        private System.Windows.Forms.ToolStripStatusLabel prclient_state;
        private System.Windows.Forms.ToolStripStatusLabel prSever_state;
        public System.Windows.Forms.ToolStripMenuItem 人员录入ToolStripMenuItem;
        internal MyPanel panel1;
        private System.Windows.Forms.ToolStripMenuItem 主页ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 人员录入ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 指纹录入ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripStatusLabel WebService_Status;
        private System.Windows.Forms.ToolStripMenuItem 紧急取枪弹ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 紧急还枪弹ToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;

    }
}