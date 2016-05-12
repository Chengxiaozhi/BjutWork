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
    public partial class Homepage : Form
    {
        public Homepage()
        {
           

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task_query tq = new Task_query();
            tq.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Task_execute te = new Task_execute();
            te.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Management m = new Management();
            m.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Emergency em = new Emergency();
            em.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Form1.user.Equals("值班员"))
            {
                Fingerprint_input f = new Fingerprint_input();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有该模块的访问权限！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Form1.user.Equals("值班员"))
            {
            Query q = new Query();
            q.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有该模块的访问权限！");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Form1 f = new Form1();
            //f.Show();
            //this.Close();
            Application.Restart();

        }
    }
}
