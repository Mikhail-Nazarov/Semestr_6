using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace lab3
{
    class MarkovProcess
    {
        public int iterationsCount { get; set; } = 100;
        private double[] averageCount= { 0, 0, 0, 0 };
        Chart chart;
        double T = 0;
        double a01, a10, a02, a21, a13, a30;
        public int Tn { get; set; }
        int State=0;
        Random rand = new Random();
        public MarkovProcess(double a01, double a10, double a02, double a21, double a13, double a30, Chart chart)
        {
            this.a02 = a02;
            this.a21 = a21;
            this.a01 = a01;
            this.a10 = a10;
            this.a13 = a13;
            this.a30 = a30;
            this.chart = chart;
        }
        public double[] Process()
        {
            for (int i = 0; i < iterationsCount; i++)
            {
                T = 0;
                while (T < Tn)
                {
                    double t01 = -1 / a01 * Math.Log(rand.NextDouble());
                    double t02 = -1 / a02 * Math.Log(rand.NextDouble());
                    double t21 = -1 / a21 * Math.Log(rand.NextDouble());
                    double t10 = -1 / a10 * Math.Log(rand.NextDouble());
                    double t13 = -1 / a13 * Math.Log(rand.NextDouble());
                    double t30 = -1 / a01 * Math.Log(rand.NextDouble());
                    switch (State)
                    {
                        case 0:
                            averageCount[0]++;
                            if (t01 <= t02)
                            {
                                State = 1;
                                T += t01;
                            }
                            else
                            {
                                State = 2;
                                T += t02;
                            }
                            break;

                        case 1:
                            averageCount[1]++;
                            if (t10 <= t13)
                            {
                                State = 0;
                                T += t10;
                            }
                            else
                            {
                                State = 3;
                                T += t13;
                            }
                            break;

                        case 2:
                            averageCount[2]++;
                            State = 1;
                            T += t21;
                            break;

                        case 3:
                            averageCount[3]++;
                            State = 0;
                            T += t30;
                            break;
                    }
                    if(i==0)
                        chart.Series[0].Points.AddXY(T, State);
                }
            }
            for (int i = 0; i < averageCount.Length; i++)
                averageCount[i] /= iterationsCount;
            return averageCount;
        }
    }
}