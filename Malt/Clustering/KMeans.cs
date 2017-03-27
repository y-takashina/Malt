using System;
using System.Linq;

namespace Malt.Clustering
{
    public static partial class Clustering
    {
        public static (double[] means, int[] assignments) ClusterByKMeans(double[] data, int n)
        {
            var rand = new Random();
            var means = data.OrderBy(v => rand.Next()).Take(n).ToArray();
            var assignments = new int[data.Length];
            while (true)
            {
                var prevAssignments = assignments.Select(v => v).ToArray();
                // calc assignments
                for (var i = 0; i < data.Length; i++)
                {
                    var min = double.MaxValue;
                    var mini = 0;
                    for (var j = 0; j < means.Length; j++)
                    {
                        var d = Math.Pow(data[i] - means[j], 2);
                        if (d < min)
                        {
                            min = d;
                            mini = j;
                        }
                    }
                    assignments[i] = mini;
                }
                if (Enumerable.Range(0, data.Length).All(i => assignments[i] == prevAssignments[i])) break;
                means = means.Select((m, i) => data.Where((v, j) => i == assignments[j]).DefaultIfEmpty(m).Average()).ToArray();
            }
            return (means, assignments);
        }

        public static (double[] medoids, int[] assignments) ClusterByKMedoids(double[] data, int n)
        {
            var rand = new Random();
            var medoids = data.OrderBy(v => rand.Next()).Take(n).ToArray();
            var assignments = new int[data.Length];
            while (true)
            {
                var prevAssignments = assignments.Select(v => v).ToArray();
                for (var i = 0; i < data.Length; i++)
                {
                    var min = double.MaxValue;
                    var mini = 0;
                    for (var j = 0; j < medoids.Length; j++)
                    {
                        var d = Math.Pow(data[i] - medoids[j], 2);
                        if (d < min)
                        {
                            min = d;
                            mini = j;
                        }
                    }
                    assignments[i] = mini;
                }
                if (Enumerable.Range(0, data.Length).All(i => assignments[i] == prevAssignments[i])) break;
                for (var i = 0; i < medoids.Length; i++)
                {
                    var members = data.Where((v, j) => i == assignments[j]).ToList();
                    medoids[i] = members.IndexOf(members.Min(m => members.Sum(v => Math.Pow(v - m, 2))));
                }
            }
            return (medoids, assignments);
        }

        public static (double[] means, double[,] assignments) ClusterByFuzzyCMeans(double[] data, int n, int m = 2)
        {
            var rand = new Random();
            var means = data.OrderBy(v => rand.Next()).Take(n).ToArray();
            var assignments = new double[data.Length, n];

            while (true)
            {
                var prevMeans = means.Select(v => v).ToArray();
                // calc assignments
                for (var i = 0; i < data.Length; i++)
                {
                    for (var j = 0; j < means.Length; j++)
                    {
                        var dominant = 0.0;
                        foreach (var mean in means)
                        {
                            dominant += Math.Pow(Math.Abs(means[j] - data[i]) / Math.Abs(mean - data[i]), 2 / (m - 1));
                        }
                        assignments[i, j] = double.IsPositiveInfinity(dominant) ? 0 : double.IsNaN(dominant) ? 1 : 1 / dominant;
                    }
                }
                // calc means
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
                if (Enumerable.Range(0, n).All(i => Math.Abs(means[i] - prevMeans[i]) < 1e-6)) break;
            }
            return (means, assignments);
        }
    }
}