using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace Malt.Clustering
{
    public static partial class Clustering
    {
        public static Tuple<double[], int[]> KMeansClustering(double[] data, int n)
        {
            var rand = new Random();
            var means = data.OrderBy(v => rand.Next()).Take(n).ToList();
            var assignments = new int[data.Length];
            while (true)
            {
                var prevAssignments = assignments.Select(v => v).ToArray();
                assignments = data.Select(v => means.IndexOf(means.MinBy(m => Math.Pow(v - m, 2)))).ToArray();
                if (Enumerable.Range(0, data.Length).All(i => assignments[i] == prevAssignments[i])) break;
                means = means.Select((m, i) => data.Where((v, j) => i == assignments[j]).DefaultIfEmpty(m).Average()).ToList();
            }
            return Tuple.Create(means.ToArray(), assignments);
        }

        public static Tuple<double[], int[]> KMedoidsClustering(double[] data, int n)
        {
            var rand = new Random();
            var medoids = data.OrderBy(v => rand.Next()).Take(n).ToList();
            var assignments = new int[data.Length];
            while (true)
            {
                var prevAssignments = assignments.Select(v => v).ToArray();
                assignments = data.Select(v => medoids.IndexOf(medoids.MinBy(m => Math.Pow(v - m, 2)))).ToArray();
                if (Enumerable.Range(0, data.Length).All(i => assignments[i] == prevAssignments[i])) break;
                for (var i = 0; i < medoids.Count; i++)
                {
                    var members = data.Where((v, j) => i == assignments[j]).ToList();
                    medoids[i] = members.IndexOf(members.Min(m => members.Sum(v => Math.Pow(v - m, 2))));
                }
            }
            return Tuple.Create(medoids.ToArray(), assignments);
        }
    }
}