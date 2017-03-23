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
                if (array[i] is IEnumerable<object> inner) inner.PrintLine();
                else Console.Write(array[i] + (i < array.Length - 1 ? ", " : ""));
            }
            Console.Write("]");
        }

        public static void PrintLine<T>(this IEnumerable<T> enumerable)
        {
            enumerable.Print();
            Console.WriteLine();
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

        public static double LinearCombinate(this IEnumerable<double> variables, IEnumerable<double> weights)
        {
            return variables.Zip(weights, (v, w) => v * w).Sum();
        }
    }
}