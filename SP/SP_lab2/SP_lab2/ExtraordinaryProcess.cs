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
        TextBox tPr = new TextBox();
        TextBox count = new TextBox();
        Chart chart = new Chart(); 
        int Tn=100, n = 0;
        double lyambda1, lyambda2;
        double t = 0, t_pr = 0;
        double m, sigma;
        double r1, r2, t1, t2;
        List <int> middleCount = new List<int>();
        public ExtraordinaryProcess(double lyambda1, double lyambda2, double m, double sigma, TextBox t_pr, TextBox count, Chart chart)
        {
            this.chart = chart;
            this.tPr = t_pr;
            this.lyambda1 = lyambda1;
            this.lyambda2 = lyambda2;
            this.m = m;
            this.sigma = sigma;
            this.count = count;
        }

        public void Process()
        {
            var rand = new Random();
            r1 = rand.NextDouble();
            t1 = -1 / lyambda1 * Math.Log(r1);
            r2 = rand.NextDouble();
            t2 = -1 / lyambda2 * Math.Log(r2);
            while (t < Tn)
            {
                if (t1 < t2)
                {
                    r1 = rand.NextDouble();
                    r2 = rand.NextDouble();
                    double z = Math.Sqrt(-2 * Math.Log(r1)) * Math.Cos(2 * Math.PI * r2);
                    int x = (int)Math.Round(m + sigma * z);
                    n += x;
                    r1 = rand.NextDouble();
                    t1 += (-1 / lyambda1 * Math.Log(r1));
                    t += t1;
                }
                else
                {
                    if (n > 0)
                    {
                        n--;
                    }
                    else 
                    {
                        t_pr +=t2;
                    }
                    r2 = rand.NextDouble();
                    t2 += (-1 / lyambda2 * Math.Log(r2));
                    t += t2;
                }
                chart.Series[0].Points.AddXY(t, n);
            }
            count.Text = n.ToString();
            tPr.Text = t_pr.ToString();
        }
    }
}
