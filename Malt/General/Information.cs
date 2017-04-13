using System;
using System.Collections.Generic;
using System.Linq;

namespace Malt.General
{
    public static class Information
    {
        public static double Entropy(this IEnumerable<double> probabilities)
        {
            return probabilities.Where(p => Math.Abs(p) > 1e-300).Sum(p => -p * Math.Log(p, 2));
        }

        public static double Entropy<T>(this IEnumerable<T> stream)
        {
            var array = stream.ToArray();
            var points = array.Distinct().ToArray();
            var probabilities = points.Select(v1 => (double) array.Count(v2 => Equals(v1, v2)) / array.Length);
            return probabilities.Entropy();
        }

        public static double JointEntropy(IEnumerable<int> stream1, IEnumerable<int> stream2)
        {
            return stream1.Zip(stream2, Tuple.Create).Entropy();
        }

        public static double MutualInformation(IEnumerable<int> stream1, IEnumerable<int> stream2)
        {
            return stream1.Entropy() + stream2.Entropy() - JointEntropy(stream1, stream2);
        }

        public static double[,] MutualInformation(IEnumerable<IEnumerable<int>> streams)
        {
            var array = streams as IEnumerable<int>[] ?? streams.ToArray();
            var n = array.Length;
            var matrix = new double[n, n];
            for (var j = 0; j < n; j++)
            {
                for (var k = j; k < n; k++)
                {
                    matrix[j, k] = matrix[k, j] = MutualInformation(array[j], array[k]);
                }
            }
            return matrix;
        }
    }
}