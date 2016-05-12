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
    public partial class Managenment_clear_alert : Form
    {
        string user1 = Convert12.ByteArrToStr(ConnectionClient.CheckuserGroup0);
        string user2 = Convert12.ByteArrToStr(ConnectionClient.CheckuserGroup1);
        public Managenment_clear_alert()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int alarm_count = dataGridView1.Rows.Count;//待清除报警数
            string[] alarm_id1 = new string[alarm_count+200];//待消除报警主键
            int[] alarm_id = new int[200];


            if (alarm_count == 0)
            {
                MessageBox.Show("没有要清除的报警");
            }
            else
            {
                for (int i = 0; i < alarm_count; i++)//报警ID
                {
                    string rows = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    alarm_id[i] = int.Parse(rows);
                }
                for (int i = 0; i < alarm_count; i++)//服务器报警ID
                {
                    string rows = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    alarm_id1[i] = rows;
                }

                DateTime now = DateTime.Now;
                for (int i = 0; i < alarm_count; i++)
                {

                    //WebReference.gunServices client = new Bullet.WebReference.gunServices();
                    frmMain.webService.cancelAlarm(alarm_id1[i], Utils.getUserPoliceNumById(user1), Utils.getUserPoliceNumById(user2), now);
                    //报警状态 0到1
                    string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con = new MySqlConnection(str);//实例化链接
                    con.Open();//开启连接
                    string strcmd = "update gunark_log set GUNARK_STATUS=1 where GUNARK_ALARM_ID=" + alarm_id[i];

                    MySqlCommand cmd = new MySqlCommand(strcmd, con);
                    MySqlDataAdapter ada = new MySqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    ada.Fill(ds);//查询结果填充数据集

                    con.Close();//关闭连接

                }
                HomePage.alarmcount = 0;
                MessageBox.Show("报警已清除");
            }

            string str1 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con1 = new MySqlConnection(str1);//实例化链接
            con1.Open();//开启连接
            string strcmd1 = "select m.GUNARK_NAME,g.GUNARK_LOG_TIME,g.GUNARK_LOG_TYPE,g.GUNARK_ALARM,g.GUNARK_ALARM_ID,g.GUNARK_ALARM_ID1 from gunark_log g left join gunark_gunark m on m.GUNARK_IP = g.GUNARK_ID where g.GUNARK_STATUS =0";

            MySqlCommand cmd1 = new MySqlCommand(strcmd1, con1);
            MySqlDataAdapter ada1 = new MySqlDataAdapter(cmd1);

            DataSet ds1 = new DataSet();
            ada1.Fill(ds1);//查询结果填充数据集
            dataGridView1.DataSource = ds1.Tables[0];
            con1.Close();//关闭连接
            dataGridView1.Columns[0].HeaderText = "枪柜名";
            dataGridView1.Columns[1].HeaderText = "日志时间";
            dataGridView1.Columns[2].HeaderText = "日志类型";
            dataGridView1.Columns[3].HeaderText = "报警类型";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }


        private void Managenment_clear_alert_Load(object sender, EventArgs e)
        {
            string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
            MySqlConnection con = new MySqlConnection(str);//实例化链接
            con.Open();//开启连接
            string strcmd = "select m.GUNARK_NAME,g.GUNARK_LOG_TIME,g.GUNARK_LOG_TYPE,g.GUNARK_ALARM,g.GUNARK_ALARM_ID,g.GUNARK_ALARM_ID1 from gunark_log g left join gunark_gunark m on m.GUNARK_IP = g.GUNARK_ID where g.GUNARK_STATUS =0";

            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            ada.Fill(ds);//查询结果填充数据集
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();//关闭连接
            dataGridView1.Columns[0].HeaderText = "枪柜名称";
            dataGridView1.Columns[1].HeaderText = "日志时间";
            dataGridView1.Columns[2].HeaderText = "日志类型";
            dataGridView1.Columns[3].HeaderText = "报警类型";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;

        }
    }
}
