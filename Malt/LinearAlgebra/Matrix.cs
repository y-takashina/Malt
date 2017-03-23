using System;
using System.Collections.Generic;

namespace Malt.LinearAlgebra
{
    public static class Matrix
    {
        public static double[,] Uniform(int m, int n, double value)
        {
            var results = new double[m, n];
            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    results[i, j] = value;
                }
            }
            return results;
        }

        public static double[,] Uniform(int n, double value)
        {
            return Uniform(n, n, value);
        }

        public static double[,] Ones(int n)
        {
            return Uniform(n, 1.0);
        }

        public static double[,] Zeros(int n)
        {
            return new double[n, n];
        }

        public static double[,] Eye(int n)
        {
            var results = new double[n, n];
            for (var i = 0; i < n; i++)
            {
                results[i, i] = 1;
            }
            return results;
        }

        public static (int i, int j, double value)[] ToCoordinateList(this double[,] matrix, double threshold = 1e-300)
        {
            var raws = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            var results = new List<(int, int, double)>();
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    if (Math.Abs(matrix[i, j]) < threshold)
                    {
                        var value = matrix[i, j];
                        results.Add((i, j, value));
                    }
                }
            }
            return results.ToArray();
        }
    }
}