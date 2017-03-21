using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Malt
{
    public static class Vector
    {
        public static double[] Ones(int n)
        {
            var vector = new double[n];
            for (var i = 0; i < n; i++) vector[i] = 1;
            return vector;
        }

        public static double[] Zeros(int n)
        {
            return new double[n];
        }

        public static double[] Random(int n, double mean = 0, double amplitude = 1)
        {
            var rand = new Random();
            var vector = new double[n];
            for (var i = 0; i < n; i++) vector[i] = amplitude * (rand.NextDouble() - 0.5) * 2 + mean;
            return vector;
        }

        public static int[] OneHot(int n, int k)
        {
            var vector = new int[n];
            vector[k] = 1;
            return vector;
        }
    }
}