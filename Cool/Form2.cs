using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Cool
{
    public partial class Form2 : Form
    {
        PrinterProtocol PtPro = new PrinterProtocol();
        DBprinter DbPt = new DBprinter();
        Convert12 Cver12 = new Convert12();
        public static int FingerNumber = 0;
    
        public Form2()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            FingerNumber = comboBox1.SelectedIndex + 1;

            //MessageBox.Show(FingerNumber.ToString());
            GetUserFinger();
           
            byte[] UserFinger =  PrinterClient.finger;
           // System.Threading.Thread.Sleep(5000);
            while (true )
            {
                System.Threading.Thread.Sleep(10000);
                if (PrinterClient.DownloadFinish == true)
                {
                    PrinterClient.DownloadFinish = false;
                    DownloadUserFinger();
                    //System.Threading.Thread.Sleep(5000);
                    MessageBox.Show("保存成功！");
                    this.Close();
                    break;
                }

                else
                {
                    MessageBox.Show("请工作人员再次录入指纹");
                    break;
                }
                      
            }
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(Fingerprint_input.user_number);
            comboBox1.SelectedIndex = 0;
        }
        private void DownloadUserFinger()
        {
            byte[] UserNumber =Cver12.NumToByteArr(Fingerprint_input.user_number.ToString());


            byte fingerId = (byte)FingerNumber;
            //string finger = DbPt.getDBfinger(Fingerprint_input.user_number.ToString());
            string finger = DbPt.getDBfinger(Fingerprint_input.user_number.ToString(),FingerNumber);
            byte[] data = PtPro.Severdownloadfinger(UserNumber, fingerId, finger);
            frmMain.sokPtClient.Send(data);

        }
        private void Test_Click()
        {
            byte[] data = PtPro.TextConnect();
            System.Threading.Thread.Sleep(1000);
            frmMain.sokPtClient.Send(data);
        }

        public void GetUserFinger()
        {
            byte[] data = PtPro.ClientDownloadfger();
            frmMain.sokPtClient.Send(data);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}

