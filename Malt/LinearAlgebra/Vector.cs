using System;
using System.Linq;
using Malt.General;

namespace Malt.LinearAlgebra
{
    public static partial class Vector
    {
        public static double[] Uniform(int n, double value)
        {
            var vector = new double[n];
            for (var i = 0; i < n; i++) vector[i] = value;
            return vector;
        }

        public static double[] Ones(int n) => Uniform(n, 1.0);

        public static double[] Zeros(int n) => new double[n];

        /// <summary>
        /// Returns an array whose values are randomly initialized with a uniform distribution.
        /// </summary>
        /// <param name="n">The dimension of the array.</param>
        /// <param name="mean"></param>
        /// <param name="amplitude"></param>
        /// <returns></returns>
        public static double[] Random(int n, double mean, double amplitude)
        {
            var rand = new Random();
            var vector = new double[n];
            for (var i = 0; i < n; i++) vector[i] = mean - amplitude / 2 + amplitude * rand.NextDouble();
            return vector;
        }

        /// <summary>
        /// Returns an array whose values are randomly initialized with a Gaussian distribution.
        /// </summary>
        /// <param name="n">The dimension of the array.</param>
        /// <param name="mean"></param>
        /// <param name="deviation"></param>
        /// <returns></returns>
        public static double[] Gauss(int n, double mean, double deviation)
        {
            return Random(n, 0.5, 1).Select(v => Statistics.Gauss(v, mean, deviation)).ToArray();
        }

        public static int[] OneHot(int n, int k)
        {
            var vector = new int[n];
            vector[k] = 1;
            return vector;
        }
    }
}