using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace lab_2
{
    class Signal
    {
        public PointPairList generateSignal(int N)
        {
            PointPairList points = new PointPairList();
            for (int i = 0; i < N; i++)
            {
                points.Add(i, Math.Round(20 * Math.Cos(2 * Math.PI * 10 * i / N), 3));
            }
            return points;
        }
    }
    class PolyHarmonicSignal
    {
        int[] ampArray = { 1, 3, 4, 10, 11, 14, 17 };
        double[] phaseArray = { Math.PI / 6, Math.PI / 4, Math.PI / 3, Math.PI / 2, 3 * Math.PI / 4, Math.PI };
        public PointPairList generateSignal(int N)
        {
            double sum = 0;
            Random rnd = new Random();
            PointPairList points = new PointPairList();
            for (int i = 0; i <= N - 1; i++)
            {
                sum = 0;
                for (int j = 0; j <= 29; j++)
                {

                    sum += ampArray[rnd.Next(0, 6)] * Math.Cos(2 * Math.PI * i * j / N - phaseArray[rnd.Next(0, 5)]);
                }
                points.Add(i, sum);
            }
            return points;
        }
    }
}
