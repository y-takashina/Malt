using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Malt.LinearAlgebra
{
    public static partial class Vector
    {
        public static void Print<T>(this IEnumerable<T> stream)
        {
            var array = stream as T[] ?? stream.ToArray();
            Console.Write("[");
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i] is IEnumerable<object> inner) inner.PrintLine();
                else Console.Write(array[i] + (i < array.Length - 1 ? ", " : ""));
            }
            Console.Write("]");
        }

        public static void PrintLine<T>(this IEnumerable<T> stream)
        {
            stream.Print();
            Console.WriteLine();
        }

        public static double[] Add(this IEnumerable<double> stream1, IEnumerable<double> stream2)
        {
            return stream1.Zip(stream2, (v1, v2) => v1 + v2).ToArray();
        }

        public static double[] Sub(this IEnumerable<double> stream1, IEnumerable<double> stream2)
        {
            return stream1.Zip(stream2, (v1, v2) => v1 - v2).ToArray();
        }

        public static double[] Mul(this IEnumerable<double> stream, double scalar)
        {
            return stream.Select(v => v * scalar).ToArray();
        }

        public static double[] Div(this IEnumerable<double> stream, double scalar)
        {
            return stream.Select(v => v / scalar).ToArray();
        }

        public static double InnerProduct(this IEnumerable<double> stream1, IEnumerable<double> stream2)
        {
            return stream1.Zip(stream2, (v, w) => v * w).Sum();
        }

        public static double[,] OuterProduct(this IEnumerable<double> stream1, IEnumerable<double> stream2)
        {
            var array1 = stream1 as double[] ?? stream1.ToArray();
            var array2 = stream2 as double[] ?? stream2.ToArray();
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

        public static double Norm(this IEnumerable<double> stream, double order = 2.0)
        {
            return Math.Pow(stream.Sum(v => Math.Pow(v, order)), 1 / order);
        }

        public static double[] Normalize(this IEnumerable<int> stream)
        {
            var array = stream as int[] ?? stream.ToArray();
            var sum = array.Sum();
            return array.Select(v => (double) v / sum).ToArray();
        }

        public static double[] Normalize(this IEnumerable<double> stream)
        {
            var array = stream as double[] ?? stream.ToArray();
            var sum = array.Sum();
            return array.Select(v => v / sum).ToArray();
        }

        public static double LinearCombinate(this IEnumerable<double> variables, IEnumerable<double> weights)
        {
            return InnerProduct(variables, weights);
        }

        public static double[] LinearCombinate(this IEnumerable<double[]> variables, IEnumerable<double> weights)
        {
            var n = weights.Count();
            return variables.Zip(weights, (v, w) => v.Mul(w)).Aggregate(Zeros(n), (v1, v2) => v1.Add(v2));
        }

        public static T[,] Reshape<T>(this IEnumerable<T> stream, (int, int) shape)
        {
            var array = stream as T[] ?? stream.ToArray();
            var results = new T[shape.Item1, shape.Item2];
            for (var i = 0; i < shape.Item1; i++)
            {
                for (var j = 0; j < shape.Item2; j++)
                {
                    results[i, j] = array[i + j];
                }
            }
            return results;
        }

        public static T[,,] Reshape<T>(this IEnumerable<T> stream, (int, int, int) shape)
        {
            var array = stream as T[] ?? stream.ToArray();
            var results = new T[shape.Item1, shape.Item2, shape.Item3];
            for (var i = 0; i < shape.Item1; i++)
            {
                for (var j = 0; j < shape.Item2; j++)
                {
                    for (var k = 0; k < shape.Item2; k++)
                    {
                        results[i, j, k] = array[i + j + k];
                    }
                }
            }
            return results;
        }

        public static T[,,,] Reshape<T>(this IEnumerable<T> stream, (int, int, int, int) shape)
        {
            var array = stream as T[] ?? stream.ToArray();
            var results = new T[shape.Item1, shape.Item2, shape.Item3, shape.Item4];
            for (var i = 0; i < shape.Item1; i++)
            {
                for (var j = 0; j < shape.Item2; j++)
                {
                    for (var k = 0; k < shape.Item3; k++)
                    {
                        for (var l = 0; l < shape.Item4; l++)
                        {
                            results[i, j, k, l] = array[i + j + k + l];
                        }
                    }
                }
            }
            return results;
        }
    }
}