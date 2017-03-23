using System;
using System.Collections.Generic;
using System.Linq;

namespace Malt.Clustering
{
    public static partial class Clustering
    {
        public static Cluster<T> AggregativeHierarchicalClustering<T>(IEnumerable<T> data, Func<T, T, double> calcPointwiseDistance, Func<(double, int), (double, int), double> calcClusterwiseDistance)
        {
            var clusters = data.Distinct().Select(Cluster).ToList();
            while (clusters.Count != 1)
            {
                var min = double.MaxValue;
                var c1 = clusters.First();
                var c2 = clusters.Last();
                for (var i = 0; i < clusters.Count; i++)
                {
                    for (var j = i + 1; j < clusters.Count; j++)
                    {
                        var d = clusters[i].DistanceTo(clusters[j], calcPointwiseDistance, calcClusterwiseDistance);
                        if (d < min)
                        {
                            min = d;
                            c1 = clusters[i];
                            c2 = clusters[j];
                        }
                    }
                }
                clusters.Add(Cluster(c1, c2));
                clusters.Remove(c1);
                clusters.Remove(c2);
            }
            return clusters.First();
        }
    }
}