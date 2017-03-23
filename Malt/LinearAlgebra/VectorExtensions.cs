﻿using System.Collections.Generic;
using System.Linq;

namespace Malt.LinearAlgebra
{
    public static class VectorExtensions
    {
        public static double[] Normalize(this IEnumerable<int> enumerable)
        {
            var array = enumerable as int[] ?? enumerable.ToArray();
            var sum = array.Sum();
            return array.Select(v => (double) v / sum).ToArray();
        }

        public static double[] Normalize(this IEnumerable<double> enumerable)
        {
            var array = enumerable as double[] ?? enumerable.ToArray();
            var sum = array.Sum();
            return array.Select(v => v / sum).ToArray();
        }
    }
}