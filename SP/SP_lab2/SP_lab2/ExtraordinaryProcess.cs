using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SP_lab2
{
    class ExtraordinaryProcess
    {
        TextBox text = new TextBox();
        Chart chart = new Chart(); 
        int Tn=100, skl = 0, D;
        double lyambda1, lyambda2;
        double t = 0, t_pr = 0;
        bool flag = false;
        double r1, r2, t1, t2;
        public ExtraordinaryProcess(double lyambda1, double lyambda2, TextBox text, Chart chart)
        {
            this.chart = chart;
            this.text = text;
            this.lyambda1 = lyambda1;
            this.lyambda2 = lyambda2;
        }

        public void Process()
        {
            var rand = new Random();
            r1 = rand.NextDouble();
            t1 = -1 / lyambda1 * Math.Log(r1);
            r2 = rand.NextDouble();
            t2 = -1 / lyambda2 * Math.Log(r2);
            if (t1 > t2) t = t1;
            else t = t2;
            do
            {
                if (t1 < t2)
                {
                    r1 = rand.NextDouble();
                    r2 = rand.NextDouble();
                    double z = Math.Sqrt(-2 * Math.Log(r1)) * Math.Cos(2 * Math.PI * r2);
                    int x = (int)Math.Round(8 + 2 * z);
                    skl += x;
                    r1 = rand.NextDouble();
                    t1 += (-1 / lyambda1 * Math.Log(r1));
                    t += t1;
                }
                else
                {
                    if (skl > 0)
                    {
                        flag = false;
                        skl--;
                    }
                    else if (!flag)
                    {
                        flag = true;
                        D++;
                        t_pr += (t - t2);
                    }
                    r2 = rand.NextDouble();
                    t2 += (-1 / lyambda2 * Math.Log(r2));
                    t += t2;
                }
                chart.Series[0].Points.AddXY(t, skl);
            } while (t < Tn);
            text.Text = t_pr.ToString();
        }
    }
}
