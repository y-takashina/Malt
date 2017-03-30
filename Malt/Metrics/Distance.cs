using System;
using System.Collections.Generic;
using System.Linq;

namespace Malt.Metrics
{
    public static class Distance
    {
        public static double CalcMinkowskiDistance(IEnumerable<double> stream1, IEnumerable<double> stream2, double order)
        {
            var array1 = stream1.ToArray();
            var array2 = stream2.ToArray();
            if (array1.Length != array2.Length) throw new IndexOutOfRangeException("stream1 and stream2 must have the same length.");
            return Math.Pow(array1.Zip(array2, Tuple.Create).Sum(tuple => Math.Pow(Math.Abs(tuple.Item1 - tuple.Item2), order)), 1.0 / order);
        }

        public static double CalcManhattanDistance(IEnumerable<double> stream1, IEnumerable<double> stream2)
        {
            return CalcMinkowskiDistance(stream1, stream2, 1);
        }

        public static double CalcEuclideanDistance(IEnumerable<double> stream1, IEnumerable<double> stream2)
        {
            return CalcMinkowskiDistance(stream1, stream2, 2);
        }

        public static int CalcHammingDistance<T>(IEnumerable<T> stream1, IEnumerable<T> stream2) where T : struct
        {
            return stream1.Zip(stream2, Tuple.Create).Count(tuple => !tuple.Item1.Equals(tuple.Item2));
        }

        public static int CalcHammingDistance(string str1, string str2) => CalcHammingDistance(str1.ToCharArray(), str2.ToCharArray());
    }
}