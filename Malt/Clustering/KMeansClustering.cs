using System;
using System.Linq;

namespace Malt.Clustering
{
    public static partial class Clustering
    {
        public static (double[] means, int[] assignments) KMeansClustering(double[] data, int n)
        {
            var rand = new Random();
            var means = data.OrderBy(v => rand.Next()).Take(n).ToArray();
            var assignments = new int[data.Length];
            while (true)
            {
                var prevAssignments = assignments.Select(v => v).ToArray();
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

        public static (double[] medoids, int[] assignments) KMedoidsClustering(double[] data, int n)
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
    }
}