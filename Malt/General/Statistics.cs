using System;
using System.Collections.Generic;
using System.Linq;

namespace Malt.General
{
    public static class Statistics
    {
        public static double Mean(this IEnumerable<double> enumerable) => enumerable.Average();

        public static double Deviation(this IEnumerable<double> stream)
        {
            var array = stream as double[] ?? stream.ToArray();
            if (!array.Any()) return 0;
            var avg = array.Average();
            var sum = array.Sum(x => Math.Pow(x - avg, 2));
            return sum / array.Length;
        }

        public static double StandardDeviation(this IEnumerable<double> stream)
        {
            return Math.Sqrt(stream.Deviation());
        }

        public static double Erf(double x)
        {
            const double a1 = 0.254829592;
            const double a2 = -0.284496736;
            const double a3 = 1.421413741;
            const double a4 = -1.453152027;
            const double a5 = 1.061405429;
            const double p = 0.3275911;

            var sign = Math.Sign(x);
            x = Math.Abs(x);

            var t = 1.0 / (1.0 + p * x);
            var y = 1.0 - ((((a5 * t + a4) * t + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return sign * y;
        }

        public static double Erfc(double x)
        {
            return 1 - Erf(x);
        }

        public static double QFunction(double x, double mean, double standardDeviation)
        {
            if (x < mean) x = 2 * mean - x;
            var z = (x - mean) / standardDeviation;
            return Erfc(z / Math.Sqrt(2)) / 2;
        }

        public static double Gauss(double x, double mean, double deviation)
        {
            return 1 / Math.Sqrt(2 * Math.PI * deviation) * Math.Exp(-Math.Pow(x - mean, 2) / 2 / deviation);
        }
    }
}