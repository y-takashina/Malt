using System;
using System.Collections.Generic;
using System.Linq;

namespace Malt.Metrics
{
    public static class Distance
    {
        public static double CalcMinkowskiDistance(IEnumerable<double> vector1, IEnumerable<double> vector2, double order)
        {
            var array1 = vector1.ToArray();
            var array2 = vector2.ToArray();
            if (array1.Length != array2.Length) throw new IndexOutOfRangeException("vector1 and vector2 must have the same length.");
            return Math.Pow(array1.Zip(array2, Tuple.Create).Sum(tuple => Math.Pow(Math.Abs(tuple.Item1 - tuple.Item2), order)), 1.0 / order);
        }

        public static double CalcManhattanDistance(IEnumerable<double> vector1, IEnumerable<double> vector2)
        {
            return CalcMinkowskiDistance(vector1, vector2, 1);
        }

        public static double CalcEuclideanDistance(IEnumerable<double> vector1, IEnumerable<double> vector2)
        {
            return CalcMinkowskiDistance(vector1, vector2, 2);
        }

        public static int CalcHammingDistance<T>(IEnumerable<T> vector1, IEnumerable<T> vector2) where T : struct
        {
            return vector1.Zip(vector2, Tuple.Create).Count(tuple => !tuple.Item1.Equals(tuple.Item2));
        }

        public static int CalcHammingDistance(string str1, string str2) => CalcHammingDistance(str1.ToCharArray(), str2.ToCharArray());
    }
}