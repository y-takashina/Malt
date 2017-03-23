using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Malt.LinearAlgebra
{
    public static class VectorExtensions
    {
        public static void Print<T>(this IEnumerable<T> enumerable)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();
            Console.Write("[");
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i] is IEnumerable<object> inner) inner.Print();
                else Console.Write(array[i] + (i < array.Length - 1 ? ", " : ""));
            }
            Console.WriteLine("]");
        }

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