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
    public partial class User_input : Form
    {
        public static string user_number;
        Convert12 Cver12 = new Convert12();
        PrinterProtocol PtPro = new PrinterProtocol();
        DataSet ds = new DataSet();
        public User_input()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            //显示任务
            Display();
        }

        private void Display()
        {
            //显示任务
            MySqlConnection con = null;
            con = DatabaseManager.GetCon(con);
            con.Open();//开启连接
            //string strcmd = "SELECT USER_REALNAME,USER_SEX,USER_PRIVIEGES,USER_POLICENUMB,UNITINFO_CODE FROM gunark_user where USER_DOWN_STATE = 0";
            string strcmd = "insert into gunark_user_finger(USER_REALNAME,USER_SEX,USER_PRIVIEGES,USER_POLICENUMB,UNITINFO_CODE) SELECT USER_REALNAME,USER_SEX,USER_PRIVIEGES,USER_POLICENUMB,UNITINFO_CODE FROM gunark_user where USER_DOWN_STATE = 0";
            DatabaseManager.Exec(strcmd,con);
            DatabaseManager.Clear(con);

            MySqlConnection con1 = null;
            con1 = DatabaseManager.GetCon(con);
            con1.Open();
            string sql1 = "update gunark_user set USER_DOWN_STATE = 1";
            DatabaseManager.Exec(sql1,con1);
            DatabaseManager.Clear(con1);

            MySqlConnection con2 = null;
            con2 = DatabaseManager.GetCon(con2);
            con2.Open();
            string sql = "SELECT USER_REALNAME,USER_SEX,USER_PRIVIEGES,USER_POLICENUMB,UNITINFO_CODE,ID FROM gunark_user_finger where IsInputUser = 0";
            
            ds = DatabaseManager.ExecAndGetDs(sql, "gunark_user_finger",con2);
            DatabaseManager.Clear(con2);


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][1].Equals("0"))
                {
                    ds.Tables[0].Rows[i][1] = "男";
                }
                else if (ds.Tables[0].Rows[i][1].Equals("1"))
                {
                    ds.Tables[0].Rows[i][1] = "女";
                }
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][2].Equals("1"))
                {
                    ds.Tables[0].Rows[i][2] = "普通用户";
                }
                else if (ds.Tables[0].Rows[i][2].Equals("2"))
                {
                    ds.Tables[0].Rows[i][2] = "系统管理员";
                }
                else if (ds.Tables[0].Rows[i][2].Equals("3"))
                {
                    ds.Tables[0].Rows[i][2] = "枪柜管理员";
                }
                else if (ds.Tables[0].Rows[i][2].Equals("4"))
                {
                    ds.Tables[0].Rows[i][2] = "弹柜管理员";
                }
                else if (ds.Tables[0].Rows[i][2].Equals("5"))
                {
                    ds.Tables[0].Rows[i][2] = "审核员";
                }
                else if (ds.Tables[0].Rows[i][2].Equals("6"))
                {
                    ds.Tables[0].Rows[i][2] = "审批员";
                }
            }

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[1].HeaderText = "姓名";
            dataGridView1.Columns[2].HeaderText = "性别";
            dataGridView1.Columns[3].HeaderText = "用户权限";
            dataGridView1.Columns[4].HeaderText = "警号";
           // dataGridView1.Columns[5].HeaderText = "组织机构";
            dataGridView1.Columns[5].Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
           int count = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    count++;
                    user_number = ds.Tables[0].Rows[i][5].ToString();
                }
            }
            if (count == 0)
            {
                MessageBox.Show("请选择人员！");

            }
            else
            {
                DataTable dt = new DataTable();  //建立一个准备存储数据的新dt
                dt.Columns.Add("c1");
                dt.Columns.Add("c2");
                dt.Columns.Add("c3");
                dt.Columns.Add("c4");
                dt.Columns.Add("c5");
                dt.Columns.Add("c6");
                dt.Columns.Add("c7");
                dt.Columns.Add("c8");
                dt.Columns.Add("c9");
                dt.Columns.Add("c10");
                dt.Columns.Add("c11");
                dt.Columns.Add("c12");
                foreach (DataGridViewRow dr in dataGridView1.Rows)   //遍历dgv1
                {
                    DataRow r = dt.NewRow();
                    if (dr.Cells[0].Value != null && (bool)dr.Cells[0].Value == true)  //如果checked,开始复制数据
                    {
                        for (int i = 1; i <= 5; i++)
                        {
                            r[i - 1] = dr.Cells[i].Value;
                        }
                        dt.Rows.Add(r);

                    }

                }
                //将选择的用户信息插入到新表“gunark_user_finger”中
            
                

                //此处调用底层代码将用户下载到服务器
                byte[] UserNumber = Cver12.NumToByteArr(user_number);
                byte[] data = PtPro.DownloadUser(0x01, UserNumber);
               
                frmMain.sokPtClient.Send(data);
                while (true)
                {
                    if (PrinterClient.DownloadUserFinish == true)
                    {
                        MessageBox.Show("录入用户成功");
                        PrinterClient.DownloadUserFinish = false;
                        System.Threading.Thread.Sleep(1688);
                        ///如果服务上同步下人员指纹自动同步到本地指纹仪上。
                        ///
                        DBprinter db = new DBprinter();
                        DataSet ds = db.getfingerID(user_number);
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string finger_id = ds.Tables[0].Rows[i][0].ToString();
                            string finger =  db.getfingerAndID(user_number, finger_id);
                            byte fingerId = Convert.ToByte(finger_id);
                            byte[] data1 = PtPro.Severdownloadfinger(UserNumber, fingerId, finger);
                            frmMain.sokPtClient.Send(data1);
                            while (true)
                            {
                                if (PrinterClient.DownloadFingerFinish == true)
                                {
                                    PrinterClient.DownloadFingerFinish = false;
                                    
                                    break;
                                }
                            }
                            MessageBox.Show("用户" + db.getUsername(user_number) + finger_id + "号指纹同步成功");
                        }
                      
                       // MessageBox.Show("录入用户成功");
                        
                        break;
                    }
                }
          
                UpdateUser(user_number);
                
           }
            Display();
        }

        

        private void UpdateUser(String user_number)
        {
            //链接数据库
            MySqlConnection con5 = null;
            con5 = DatabaseManager.GetCon(con5);
            con5.Open();
            //SQL语句
            String strsql = "update  gunark_user_finger set IsInputUser = 1 WHERE  ID = " + user_number;
            //执行sql语句
            DatabaseManager.Exec(strsql,con5);
            //关闭数据库连接
            DatabaseManager.Clear(con5);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int index = 0;
            if (e.RowIndex == -1)
            {
                index = 0;
            }
            else
            {
                index = e.RowIndex;
            }
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewCheckBoxCell check = (DataGridViewCheckBoxCell)dataGridView1.Rows[index].Cells[0];
                check.Value = true;
                //MessageBox.Show(e.RowIndex.ToString());
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    check = (DataGridViewCheckBoxCell)row.Cells[0];
                    if (row.Index != e.RowIndex)
                    {
                        check.Value = false;
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int count = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    count++;
                    user_number = ds.Tables[0].Rows[i][5].ToString();
                }
            }
            if (count == 0)
            {
                //MessageBox.Show("请选择人员！");

            }
            else
            {
                DataTable dt = new DataTable();  //建立一个准备存储数据的新dt
                dt.Columns.Add("c1");
                dt.Columns.Add("c2");
                dt.Columns.Add("c3");
                dt.Columns.Add("c4");
                dt.Columns.Add("c5");
                dt.Columns.Add("c6");
                dt.Columns.Add("c7");
                dt.Columns.Add("c8");
                dt.Columns.Add("c9");
                dt.Columns.Add("c10");
                dt.Columns.Add("c11");
                dt.Columns.Add("c12");
                foreach (DataGridViewRow dr in dataGridView1.Rows)   //遍历dgv1
                {
                    DataRow r = dt.NewRow();
                    if (dr.Cells[0].Value != null && (bool)dr.Cells[0].Value == true)  //如果checked,开始复制数据
                    {
                        for (int i = 1; i <= 5; i++)
                        {
                            r[i - 1] = dr.Cells[i].Value;
                        }
                        dt.Rows.Add(r);

                    }

                }
                //将选择的用户信息插入到新表“gunark_user_finger”中



                //此处调用底层代码将用户下载到服务器
                byte[] UserNumber = Cver12.NumToByteArr(user_number);
                byte[] data = PtPro.DownloadUser(0x01, UserNumber);

                frmMain.sokPtClient.Send(data);
                while (true)
                {
                    if (PrinterClient.DownloadUserFinish == true)
                    {
                        MessageBox.Show("录入用户成功");
                        PrinterClient.DownloadUserFinish = false;
                        System.Threading.Thread.Sleep(1688);
                        ///如果服务上同步下人员指纹自动同步到本地指纹仪上。
                        ///
                        DBprinter db = new DBprinter();
                        DataSet ds = db.getfingerID(user_number);
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string finger_id = ds.Tables[0].Rows[i][0].ToString();
                            string user_policenumb = ds.Tables[0].Rows[i][1].ToString();
                            string finger = db.getfingerAndID(user_number, finger_id);
                            byte fingerId = Convert.ToByte(finger_id);
                            byte[] data1 = PtPro.Severdownloadfinger(UserNumber, fingerId, finger);
                            frmMain.sokPtClient.Send(data1);
                            while (true)
                            {
                                if (PrinterClient.DownloadFingerFinish == true)
                                {
                                    PrinterClient.DownloadFingerFinish = false;

                                    break;
                                }
                            }
                            db.setupdateFinger(user_policenumb, finger_id);
                            db.SetIsInputFinger( Convert.ToInt32(user_number),  Convert.ToInt32(finger_id));
                            MessageBox.Show("用户" + db.getUsername(user_number) + finger_id + "号指纹同步成功");
                        }

                        // MessageBox.Show("录入用户成功");

                        break;
                    }
                }

                UpdateUser(user_number);

            }
            Display();
        }

        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = "鼠标左键双击选中行可选择该人员";
        }

        private void User_input_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DefaultCellStyle.Font = new Font("宋体", 15);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15);
        }
    }
}
