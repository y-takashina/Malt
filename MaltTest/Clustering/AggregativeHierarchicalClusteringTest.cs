using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malt.Clustering;
using Xunit;
using static Malt.Clustering.Clustering;

namespace MaltTest.Clustering
{
    public class AggregativeHierarchicalClusteringTest
    {
        private readonly Cluster<int> _single;
        private readonly Cluster<int> _cluster;
        private readonly Cluster<int> _largeCluster;

        public AggregativeHierarchicalClusteringTest()
        {
            _single = Cluster(1);
            _cluster = Cluster(Cluster(Cluster(60), Cluster(Cluster(68), Cluster(31))), Cluster(Cluster(99), Cluster(19)));
            _largeCluster = Cluster(Cluster(35), Cluster(Cluster(Cluster(Cluster(77), Cluster(Cluster(34), Cluster(22))), Cluster(Cluster(Cluster(69),
                Cluster(Cluster(69), Cluster(33))), Cluster(Cluster(95), Cluster(22)))), Cluster(Cluster(Cluster(8), Cluster(38)), Cluster(72))));
        }


        [Fact]
        public void ExtractTest()
        {
            var clusters = _cluster.Extract(2);
            var c1 = Cluster(Cluster(60), Cluster(Cluster(68), Cluster(31)));
            var c2 = Cluster(Cluster(99), Cluster(19));
            Assert.Equal(clusters[0].ToString(), c1.ToString());
            Assert.Equal(clusters[1].ToString(), c2.ToString());
        }

        [Fact]
        public void GetMembersTest()
        {
            // 目視する必要がある。
            var members = _cluster.SelectMany();
            members.ForEach(Console.WriteLine);
        }

        [Fact]
        public void ShortestDistanceFromSingleToClusterTest()
        {
            var d = _single.DistanceTo(_cluster, (x, y) => Math.Abs(x - y), ClusterwiseDistance.Shortest);
            Assert.Equal(d, 18);
            d = _single.DistanceTo(_largeCluster, (x, y) => Math.Abs(x - y), ClusterwiseDistance.Shortest);
            Assert.Equal(d, 7);
        }

        [Fact]
        public void ShortestDistanceFromClusterToClusterTest()
        {
            var d = _cluster.DistanceTo(_largeCluster, (x, y) => Math.Abs(x - y), ClusterwiseDistance.Shortest);
            Assert.Equal(d, 1);
            d = _largeCluster.DistanceTo(_cluster, (x, y) => Math.Abs(x - y), ClusterwiseDistance.Shortest);
            Assert.Equal(d, 1);
        }

        [Fact]
        public void LongestDistanceFromSingleToClusterTest()
        {
            var d1 = _single.DistanceTo(_cluster, (x, y) => Math.Abs(x - y), ClusterwiseDistance.Longest);
            Assert.Equal(d1, 98);
            var d2 = _single.DistanceTo(_largeCluster, (x, y) => Math.Abs(x - y), ClusterwiseDistance.Longest);
            Assert.Equal(d2, 94);
        }

        [Fact]
        public void LongestDistanceFromClusterToClusterTest()
        {
            var d1 = _single.DistanceTo(_cluster, (x, y) => Math.Abs(x - y), ClusterwiseDistance.Longest);
            Assert.Equal(d1, 98);
            var d2 = _single.DistanceTo(_largeCluster, (x, y) => Math.Abs(x - y), ClusterwiseDistance.Longest);
            Assert.Equal(d2, 94);
            var d3 = _largeCluster.DistanceTo(_cluster, (x, y) => Math.Abs(x - y), ClusterwiseDistance.Longest);
            Assert.Equal(d3, 91);
            var d4 = _cluster.DistanceTo(_largeCluster, (x, y) => Math.Abs(x - y), ClusterwiseDistance.Longest);
            Assert.Equal(d4, 91);
        }

        [Fact]
        public void GroupAverageDistanceFromSingleToClusterTest()
        {
            var d1 = _single.DistanceTo(_cluster, (x, y) => Math.Abs(x - y), ClusterwiseDistance.GroupAverage);
            Assert.Equal(d1, 54.4);
            var d2 = _single.DistanceTo(_largeCluster, (x, y) => Math.Abs(x - y), ClusterwiseDistance.GroupAverage);
            Assert.Equal(d2, 140.5 / 3);
        }

        [Fact]
        public void GroupAverageDistanceFromClusterToClusterTest() {}
    }
}