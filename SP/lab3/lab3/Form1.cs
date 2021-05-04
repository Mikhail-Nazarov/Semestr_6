using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double a01, a10, a02, a21, a13, a30;
            int time;
            int count;
            chart1.Series[0].Points.Clear();
            try
            {
                count = Convert.ToInt32(textBox6.Text);
                time = Convert.ToInt32(textBox1.Text);
                a01 = Convert.ToDouble(flow01.Text);
                a10 = Convert.ToDouble(flow10.Text);
                a02 = Convert.ToDouble(flow02.Text);
                a21 = Convert.ToDouble(flow21.Text);
                a13 = Convert.ToDouble(flow13.Text);
                a30 = Convert.ToDouble(flow30.Text);
            }
            catch
            {
                MessageBox.Show("Некорректный ввод!");
                return;
            }
            MarkovProcess process = new MarkovProcess(a01, a10, a02, a21, a13, a30, chart1);
            process.Tn = time;
            process.iterationsCount = count;
            double[] iterationsCount = process.Process();
            textBox4.Text = iterationsCount[0].ToString();
            textBox2.Text = iterationsCount[1].ToString();
            textBox3.Text = iterationsCount[2].ToString();
            textBox5.Text = iterationsCount[3].ToString();
        }
    }
}
