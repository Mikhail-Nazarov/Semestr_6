using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double l1=5, l2=5,m=8,sigma=2;
            chart1.Series[0].Points.Clear();
            try
            {
                l1 = Convert.ToDouble(textBox1.Text);
                l2 = Convert.ToDouble(textBox2.Text);
                m = Convert.ToDouble(textBox4.Text);
                sigma = Convert.ToDouble(textBox5.Text);
            }
            catch
            {
                MessageBox.Show("Некорректный ввод!");
                return;
            }
            if (l1==0 || l2==0 || l1>l2 || m<1 || m-sigma<1)
            {
                MessageBox.Show("Некорректный ввод!");
                return;
            }
            var process = new ExtraordinaryProcess(l1, l2, m, sigma, textBox3,textBox6, chart1);
            process.Process();
        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
