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
    public partial class Management_log : Form
    {
        public Management_log()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string aaa = "";
            //如果选中
            if (comboBox1.SelectedIndex > -1)
            {
                aaa = comboBox1.SelectedItem.ToString();//方法2
            }
            //MessageBox.Show(aaa);

            switch (aaa)
            {
                case "全部":
                    string str9 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con9 = new MySqlConnection(str9);//实例化链接
                    con9.Open();//开启连接
                    string strcmd9 = "select GUNARK_LOG_TIME,GUNARK_LOG_TYPE,GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER,GUNARK_BULLET_NUMBER from gunark_log where (GUNARK_LOG_TIME between '" + dateTimePicker1.Value.ToShortDateString() + " 00:00:00' and '" + dateTimePicker2.Value.ToShortDateString() + " 23:59:59') order by  GUNARK_LOG_TIME desc";

                    MySqlCommand cmd9 = new MySqlCommand(strcmd9, con9);
                    MySqlDataAdapter ada9 = new MySqlDataAdapter(cmd9);

                    DataSet ds9 = new DataSet();
                    ada9.Fill(ds9);//查询结果填充数据集
                    dataGridView2.DataSource = ds9.Tables[0];
                    con9.Close();//关闭连接
                    dataGridView2.Columns[0].HeaderText = "日志时间";
                    dataGridView2.Columns[1].HeaderText = "日志类型";
                    dataGridView2.Columns[2].HeaderText = "操作管理员1";
                    dataGridView2.Columns[3].HeaderText = "操作管理员2";
                    dataGridView2.Columns[4].HeaderText = "枪支数量";
                    dataGridView2.Columns[5].HeaderText = "弹药数量";
                    //dataGridView2.Columns[6].HeaderText = "枪柜名称";

                    break;
                case "领取枪弹":
                    string str = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con = new MySqlConnection(str);//实例化链接
                    con.Open();//开启连接
                    string strcmd = "select GUNARK_LOG_TIME,GUNARK_LOG_TYPE,GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER,GUNARK_BULLET_NUMBER from gunark_log where GUNARK_LOG_TYPE ='领取枪弹' and (GUNARK_LOG_TIME between '" + dateTimePicker1.Value.ToShortDateString() + " 00:00:00' and '" + dateTimePicker2.Value.ToShortDateString() + " 23:59:59')";

                    MySqlCommand cmd = new MySqlCommand(strcmd, con);
                    MySqlDataAdapter ada = new MySqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    ada.Fill(ds);//查询结果填充数据集
                    dataGridView2.DataSource = ds.Tables[0];
                    con.Close();//关闭连接
                    dataGridView2.Columns[0].HeaderText = "日志时间";
                    dataGridView2.Columns[1].HeaderText = "日志类型";
                    dataGridView2.Columns[2].HeaderText = "操作管理员1";
                    dataGridView2.Columns[3].HeaderText = "操作管理员2";
                    dataGridView2.Columns[4].HeaderText = "领取枪支数量";
                    dataGridView2.Columns[5].HeaderText = "领取弹药数量";

                    break;
                case "归还枪弹":

                    string str1 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con1 = new MySqlConnection(str1);//实例化链接
                    con1.Open();//开启连接
                    string strcmd1 = "select GUNARK_LOG_TIME,GUNARK_LOG_TYPE,GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER,GUNARK_BULLET_NUMBER from gunark_log where GUNARK_LOG_TYPE ='归还枪弹' and (GUNARK_LOG_TIME between '" + dateTimePicker1.Value.ToShortDateString() + " 00:00:00' and '" + dateTimePicker2.Value.ToShortDateString() + " 23:59:59')";

                    MySqlCommand cmd1 = new MySqlCommand(strcmd1, con1);
                    MySqlDataAdapter ada1 = new MySqlDataAdapter(cmd1);

                    DataSet ds1 = new DataSet();
                    ada1.Fill(ds1);//查询结果填充数据集
                    dataGridView2.DataSource = ds1.Tables[0];
                    con1.Close();//关闭连接

                    dataGridView2.Columns[0].HeaderText = "日志时间";
                    dataGridView2.Columns[1].HeaderText = "日志类型";
                    dataGridView2.Columns[2].HeaderText = "操作管理员1";
                    dataGridView2.Columns[3].HeaderText = "操作管理员2";
                    dataGridView2.Columns[4].HeaderText = "归还枪支数量";
                    dataGridView2.Columns[5].HeaderText = "归还弹药数量";

                    break;
                
                case "枪弹入柜":

                    string str3 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con3 = new MySqlConnection(str3);//实例化链接
                    con3.Open();//开启连接
                    string strcmd3 = "select GUNARK_LOG_TIME,GUNARK_LOG_TYPE,GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER from gunark_log where GUNARK_LOG_TYPE ='枪弹入柜' and (GUNARK_LOG_TIME between '" + dateTimePicker1.Value.ToShortDateString() + " 00:00:00' and '" + dateTimePicker2.Value.ToShortDateString() + " 23:59:59')";

                    MySqlCommand cmd3 = new MySqlCommand(strcmd3, con3);
                    MySqlDataAdapter ada3 = new MySqlDataAdapter(cmd3);

                    DataSet ds3 = new DataSet();
                    ada3.Fill(ds3);//查询结果填充数据集
                    dataGridView2.DataSource = ds3.Tables[0];
                    con3.Close();//关闭连接
                    dataGridView2.Columns[0].HeaderText = "日志时间";
                    dataGridView2.Columns[1].HeaderText = "日志类型";
                    dataGridView2.Columns[2].HeaderText = "操作管理员1";
                    dataGridView2.Columns[3].HeaderText = "操作管理员2";
                    dataGridView2.Columns[4].HeaderText = "入柜数量";

                    break;
                case "枪弹调拨":

                    string str4 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con4 = new MySqlConnection(str4);//实例化链接
                    con4.Open();//开启连接
                    string strcmd4 = "select GUNARK_LOG_TIME,GUNARK_LOG_TYPE,GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER from gunark_log where GUNARK_LOG_TYPE ='枪弹调拨' and (GUNARK_LOG_TIME between '" + dateTimePicker1.Value.ToShortDateString() + " 00:00:00' and '" + dateTimePicker2.Value.ToShortDateString() + " 23:59:59')";

                    MySqlCommand cmd4 = new MySqlCommand(strcmd4, con4);
                    MySqlDataAdapter ada4 = new MySqlDataAdapter(cmd4);

                    DataSet ds4 = new DataSet();
                    ada4.Fill(ds4);//查询结果填充数据集
                    dataGridView2.DataSource = ds4.Tables[0];
                    con4.Close();//关闭连接
                    dataGridView2.Columns[0].HeaderText = "日志时间";
                    dataGridView2.Columns[1].HeaderText = "日志类型";
                    dataGridView2.Columns[2].HeaderText = "操作管理员1";
                    dataGridView2.Columns[3].HeaderText = "操作管理员2";
                    dataGridView2.Columns[4].HeaderText = "调拨数量";

                    break;

                case "枪弹封存":

                    string str7 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con7 = new MySqlConnection(str7);//实例化链接
                    con7.Open();//开启连接
                    string strcmd7 = "select GUNARK_LOG_TIME,GUNARK_LOG_TYPE,GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER from gunark_log where GUNARK_LOG_TYPE ='枪弹封存' and (GUNARK_LOG_TIME between '" + dateTimePicker1.Value.ToShortDateString() + " 00:00:00' and '" + dateTimePicker2.Value.ToShortDateString() + " 23:59:59')";

                    MySqlCommand cmd7 = new MySqlCommand(strcmd7, con7);
                    MySqlDataAdapter ada7 = new MySqlDataAdapter(cmd7);

                    DataSet ds7 = new DataSet();
                    ada7.Fill(ds7);//查询结果填充数据集
                    dataGridView2.DataSource = ds7.Tables[0];
                    con7.Close();//关闭连接
                    dataGridView2.Columns[0].HeaderText = "日志时间";
                    dataGridView2.Columns[1].HeaderText = "日志类型";
                    dataGridView2.Columns[2].HeaderText = "操作管理员1";
                    dataGridView2.Columns[3].HeaderText = "操作管理员2";
                    dataGridView2.Columns[4].HeaderText = "封存数量";

                    break;

                case "枪弹报废":

                    string str8 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con8 = new MySqlConnection(str8);//实例化链接
                    con8.Open();//开启连接
                    string strcmd8 = "select GUNARK_LOG_TIME,GUNARK_LOG_TYPE,GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER,GUNARK_BULLET_NUMBER from gunark_log where GUNARK_LOG_TYPE ='枪弹报废' and (GUNARK_LOG_TIME between '" + dateTimePicker1.Value.ToShortDateString() + " 00:00:00' and '" + dateTimePicker2.Value.ToShortDateString() + " 23:59:59')";

                    MySqlCommand cmd8 = new MySqlCommand(strcmd8, con8);
                    MySqlDataAdapter ada8 = new MySqlDataAdapter(cmd8);

                    DataSet ds8 = new DataSet();
                    ada8.Fill(ds8);//查询结果填充数据集
                    dataGridView2.DataSource = ds8.Tables[0];
                    con8.Close();//关闭连接
                    dataGridView2.Columns[0].HeaderText = "日志时间";
                    dataGridView2.Columns[1].HeaderText = "日志类型";
                    dataGridView2.Columns[2].HeaderText = "操作管理员1";
                    dataGridView2.Columns[3].HeaderText = "操作管理员2";
                    dataGridView2.Columns[4].HeaderText = "报废枪支数量";
                    dataGridView2.Columns[5].HeaderText = "报废枪支数量";
                    break;
                case "枪弹保养":

                    string str5 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con5 = new MySqlConnection(str5);//实例化链接
                    con5.Open();//开启连接
                    string strcmd5 = "select GUNARK_LOG_TIME,GUNARK_LOG_TYPE,GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER from gunark_log where GUNARK_LOG_TYPE ='枪弹保养' and (GUNARK_LOG_TIME between '" + dateTimePicker1.Value.ToShortDateString() + " 00:00:00' and '" + dateTimePicker2.Value.ToShortDateString() + " 23:59:59')";

                    MySqlCommand cmd5 = new MySqlCommand(strcmd5, con5);
                    MySqlDataAdapter ada5 = new MySqlDataAdapter(cmd5);

                    DataSet ds5 = new DataSet();
                    ada5.Fill(ds5);//查询结果填充数据集
                    dataGridView2.DataSource = ds5.Tables[0];
                    con5.Close();//关闭连接
                    dataGridView2.Columns[0].HeaderText = "日志时间";
                    dataGridView2.Columns[1].HeaderText = "日志类型";
                    dataGridView2.Columns[2].HeaderText = "操作管理员1";
                    dataGridView2.Columns[3].HeaderText = "操作管理员2";
                    dataGridView2.Columns[4].HeaderText = "保养数量";

                    break;
                case "紧急取枪":

                    string str6 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con6 = new MySqlConnection(str6);//实例化链接
                    con6.Open();//开启连接
                    string strcmd6 = "select GUNARK_LOG_TIME,GUNARK_LOG_TYPE,GUNARK_ADMIN1,GUNARK_ADMIN2,GUNARK_NUMBER from gunark_log where GUNARK_LOG_TYPE ='紧急取枪' and (GUNARK_LOG_TIME between '" + dateTimePicker1.Value.ToShortDateString() + " 00:00:00' and '" + dateTimePicker2.Value.ToShortDateString() + " 23:59:59')";

                    MySqlCommand cmd6 = new MySqlCommand(strcmd6, con6);
                    MySqlDataAdapter ada6 = new MySqlDataAdapter(cmd6);

                    DataSet ds6 = new DataSet();
                    ada6.Fill(ds6);//查询结果填充数据集
                    dataGridView2.DataSource = ds6.Tables[0];
                    con6.Close();//关闭连接
                    dataGridView2.Columns[0].HeaderText = "日志时间";
                    dataGridView2.Columns[1].HeaderText = "日志类型";
                    dataGridView2.Columns[2].HeaderText = "操作管理员1";
                    dataGridView2.Columns[3].HeaderText = "操作管理员2";
                    dataGridView2.Columns[4].HeaderText = "紧急取枪数量";


                    break;
                case "日常报警":
                    string str11 = "Server=localhost;User ID=root;Password=123456;Database=qdgl;CharSet=utf8;";
                    MySqlConnection con11 = new MySqlConnection(str11);//实例化链接
                    con11.Open();//开启连接
                    string strcmd11 = "select m.GUNARK_NAME,g.GUNARK_LOG_TIME,g.GUNARK_LOG_TYPE,g.GUNARK_ALARM from gunark_log g left join gunark_gunark m on m.GUNARK_ID = g.GUNARK_ID where g.GUNARK_LOG_TYPE ='日常报警' and (g.GUNARK_LOG_TIME between '" + dateTimePicker1.Value.ToShortDateString() + " 00:00:00' and '" + dateTimePicker2.Value.ToShortDateString() + " 23:59:59')";

                    MySqlCommand cmd11 = new MySqlCommand(strcmd11, con11);
                    MySqlDataAdapter ada11 = new MySqlDataAdapter(cmd11);

                    DataSet ds11 = new DataSet();
                    ada11.Fill(ds11);//查询结果填充数据集
                    dataGridView2.DataSource = ds11.Tables[0];
                    con11.Close();//关闭连接
                    dataGridView2.Columns[0].HeaderText = "枪柜名";
                    dataGridView2.Columns[1].HeaderText = "日志时间";
                    dataGridView2.Columns[2].HeaderText = "日志类型";
                    dataGridView2.Columns[3].HeaderText = "报警类型";
                    break;
                default:
                    break;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Management_log_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            this.dataGridView2.DefaultCellStyle.Font = new Font("宋体", 15);
            this.dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15);
        }

       
    }
}
