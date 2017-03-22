using System;
using System.Collections.Generic;
using System.Text;

namespace Malt
{
    public static class Matrix
    {
        public static double[,] Ones(int n)
        {
            var results = new double[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    results[i, j] = 1;
                }
            }
            return results;
        }

        public static double[,] Zeros(int n)
        {
            var results = new double[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    results[i, j] = 0;
                }
            }
            return results;
        }

        public static double[,] Eye(int n)
        {
            var results = new double[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    results[i, j] = i == j ? 1 : 0;
                }
            }
            return results;
        }

        public static (int i, int j, double value)[] ToCoo(this double[,] matrix, double threshold = 1e-300)
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