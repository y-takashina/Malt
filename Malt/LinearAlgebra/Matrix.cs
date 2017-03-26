using System;
using System.Collections.Generic;

namespace Malt.LinearAlgebra
{
    public static partial class Matrix
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

        public static double[,] Ones(int m, int n) => Uniform(m, n, 1.0);

        public static double[,] Zeros(int m, int n) => new double[m, n];

        public static double[,] Eye(int n)
        {
            var results = new double[n, n];
            for (var i = 0; i < n; i++) results[i, i] = 1;
            return results;
        }
    }
}