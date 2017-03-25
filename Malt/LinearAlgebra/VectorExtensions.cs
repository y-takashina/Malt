using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Malt.LinearAlgebra
{
    public static partial class Vector
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

        public static double Norm(this IEnumerable<double> vector, double order = 2)
        {
            return vector.Sum(v => Math.Pow(v, order));
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

        public static double InnerProduct(this IEnumerable<double> vector1, IEnumerable<double> vector2)
        {
            return LinearCombinate(vector1, vector2);
        }

        public static double[,] OuterProduct(this IEnumerable<double> vector1, IEnumerable<double> vector2)
        {
            var array1 = vector1 as double[] ?? vector1.ToArray();
            var array2 = vector2 as double[] ?? vector2.ToArray();
            var results = new double[array1.Length, array2.Length];
            for (var i = 0; i < array1.Length; i++)
            {
                for (var j = 0; j < array2.Length; j++)
                {
                    results[j, i] = array1[i] * array2[j];
                }
            }
            return results;
        }

        public static double LinearCombinate(this IEnumerable<double> variables, IEnumerable<double> weights)
        {
            return variables.Zip(weights, (v, w) => v * w).Sum();
        }
    }
}