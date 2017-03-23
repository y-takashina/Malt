using System;
using System.Linq;

namespace Malt.Clustering
{
    public static partial class Clustering
    {
        public static Tuple<double[], double[,]> FuzzyCMeansClustering(double[] data, int n)
        {
            var rand = new Random();
            var means = data.OrderBy(v => rand.Next()).Take(n).ToArray();
            double[,] assignments;

            while (true)
            {
                var prevMeans = means.Select(v => v).ToArray();
                assignments = CalcAssignments(data, means, 2);
                means = CalcMeans(data, assignments, n, 2);
                if (!Enumerable.Range(0, n).Any(i => Math.Abs(means[i] - prevMeans[i]) > 1e-6)) break;
            }
            return Tuple.Create(means, assignments);
        }

        private static double[,] CalcAssignments(double[] data, double[] means, double m)
        {
            var assignments = new double[data.Length, means.Length];
            for (var i = 0; i < data.Length; i++)
            {
                for (var j = 0; j < means.Length; j++)
                {
                    var dominant = 0.0;
                    for (var k = 0; k < means.Length; k++)
                    {
                        dominant += Math.Pow(Math.Abs(means[j] - data[i]) / Math.Abs(means[k] - data[i]), 2 / (m - 1));
                    }
                    assignments[i, j] = double.IsPositiveInfinity(dominant) ? 0 : double.IsNaN(dominant) ? 1 : 1 / dominant;
                }
            }
            return assignments;
        }

        private static double[] CalcMeans(double[] data, double[,] assignments, int n, double m)
        {
            var means = new double[n];
            for (var i = 0; i < n; i++)
            {
                var sum = 0.0;
                for (var j = 0; j < data.Length; j++)
                {
                    means[i] += data[j] * Math.Pow(assignments[j, i], m);
                    sum += Math.Pow(assignments[j, i], m);
                }
                means[i] = Math.Abs(sum) < 1e-6 ? means[i] : means[i] / sum;
            }
            return means;
        }
    }
}