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
            chart1.Series[0].BorderWidth = 150;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double l1=5, l2=5;
            chart1.Series[0].Points.Clear();
            try
            {
                l1 = Convert.ToDouble(textBox1.Text);
                l2 = Convert.ToDouble(textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Некорректный ввод!");
                return;
            }
            if (l1==0 || l2==0)
            {
                MessageBox.Show("Некорректный ввод!");
                return;
            }
            var process = new ExtraordinaryProcess(l1, l2, textBox3, chart1);
            process.Process();
        }
    }
}
