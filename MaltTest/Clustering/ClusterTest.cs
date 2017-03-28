using System;
using System.Linq;
using Xunit;
using Malt.Clustering;
using static Malt.Clustering.Clustering;

namespace MaltTest.Clustering
{
    public class ClusterTest
    {
        [Fact]
        public void PrintTest()
        {
            var cluster = Cluster(Cluster(35), Cluster(Cluster(Cluster(Cluster(77), Cluster(Cluster(34), Cluster(22))), Cluster(Cluster(Cluster(69),
                Cluster(Cluster(69), Cluster(33))), Cluster(Cluster(95), Cluster(22)))), Cluster(Cluster(Cluster(8), Cluster(38)), Cluster(72))));
            cluster.Print();
        }
    }
}