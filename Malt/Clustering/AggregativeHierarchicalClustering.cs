using System;
using System.Collections.Generic;
using System.Linq;

namespace Malt.Clustering
{
    public static partial class Clustering
    {
        public static Cluster<T> ClusterByAhc<T>(IEnumerable<T> data, Func<T, T, double> pointwiseDistance, Func<(double, int), (double, int), double> clusterwiseDistance = null)
        {
            clusterwiseDistance = clusterwiseDistance ?? ClusterwiseDistance.GroupAverage;
            var clusters = data.Distinct().Select(Cluster).ToList();
            while (clusters.Count != 1)
            {
                var min = double.MaxValue;
                var mini1 = 0;
                var mini2 = 1;
                for (var i = 0; i < clusters.Count; i++)
                {
                    for (var j = i + 1; j < clusters.Count; j++)
                    {
                        var d = clusters[i].DistanceTo(clusters[j], pointwiseDistance, clusterwiseDistance);
                        if (d < min)
                        {
                            min = d;
                            mini1 = i;
                            mini2 = j;
                        }
                    }
                }
                clusters.Add(Cluster(clusters[mini1], clusters[mini2]));
                clusters.Remove(clusters[mini1]);
                clusters.Remove(clusters[mini2]);
            }
            return clusters.First();
        }
    }
}