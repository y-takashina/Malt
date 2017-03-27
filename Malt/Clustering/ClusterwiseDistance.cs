using System;

namespace Malt.Clustering
{
    public static class ClusterwiseDistance
    {
        public static Func<(double d, int n), (double d, int n), double> Shortest = (c1, c2) => c1.d < c2.d ? c1.d : c2.d;
        public static Func<(double d, int n), (double d, int n), double> Longest = (c1, c2) => c1.d > c2.d ? c1.d : c2.d;
        public static Func<(double d, int n), (double d, int n), double> GroupAverage = (c1, c2) => (double) c1.n / (c1.n + c2.n) * c1.d + (double) c2.n / (c1.n + c2.n) * c2.d;
    }
}