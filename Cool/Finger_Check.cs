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
    public partial class Finger_Check : Form
    {
        public static bool isPassAlram = false;
        public Finger_Check()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int flag = 0;
            //返回值判断

            if (frmMain.OpenDoorState == true)
            {
                flag = 1;
                this.timer1.Enabled = false;
                this.timer1.Stop();
            }
            //页面跳转
            
            if (flag == 1)
            {
                frmMain.OpenDoorState = false;
                if (frmMain.isGet == 0)
                {
                    Getgunsuccess ggs = new Getgunsuccess();
                    ggs.ShowDialog();
                    this.Close();
                }
                else if (frmMain.isGet == 1)
                {
                    Retgunsuccess rgs = new Retgunsuccess();
                    rgs.ShowDialog();
                    this.Close();
                }
                else if (frmMain.isGet == 2)
                {
                    if (frmMain.taskbigtype.Equals("枪支封存") || frmMain.taskbigtype.Equals("枪支报废") || frmMain.taskbigtype.Equals("枪弹调拨") || frmMain.taskbigtype.Equals("枪支解封"))
                    {
                        Diedgunsuccess dgs = new Diedgunsuccess();
                        dgs.ShowDialog();
                        this.Close();
                    }
                    else if (frmMain.taskbigtype.Equals("枪支保养") || frmMain.taskbigtype.Equals("枪弹检查"))
                    {
                        Getgunsuccess ggs = new Getgunsuccess();
                        ggs.ShowDialog();
                        this.Close();
                    }
                    //修改处
                    else if (frmMain.taskbigtype.Equals("枪弹入柜"))
                    {
                        AddSuccess ass = new AddSuccess();
                        ass.ShowDialog();
                        this.Close();
                    }

                }
                else if (frmMain.isGet == 9)
                {
                    Emergencysuccess es = new Emergencysuccess();
                    es.ShowDialog();
                    this.Close();
                }
                else if (frmMain.isGet == 10)
                {
                    ReturnEmergencyGunSuccess re = new ReturnEmergencyGunSuccess();
                    re.ShowDialog();
                    this.Close() ;
                }
                else if (frmMain.isGet == 11)
                {
                    isPassAlram = true;
                    this.Close();
                }
            }
        }

        private void Finger_Check_Load(object sender, EventArgs e)
        {

        }

        private void myPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
