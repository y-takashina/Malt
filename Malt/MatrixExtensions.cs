using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malt
{
    public static class MatrixExtensions
    {
        public static void Print<T>(this T[,] self)
        {
            var raws = self.GetLength(0);
            var cols = self.GetLength(1);
            for (var i = 0; i < raws; i++)
            {
                Console.Write("[");
                for (var j = 0; j < cols; j++)
                {
                    Console.Write(self[i, j] + (j < cols - 1 ? ", " : ""));
                }
                Console.WriteLine("]");
            }
        }

        public static double[,] Ones(int n)
        {
            var matrix = new double[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    matrix[i, j] = 1;
                }
            }
            return matrix;
        }

        public static double[,] Zeros(int n)
        {
            var matrix = new double[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            return matrix;
        }

        public static double[,] Eye(int n)
        {
            var matrix = new double[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    matrix[i, j] = i == j ? 1 : 0;
                }
            }
            return matrix;
        }

        public static int[] OneHot(int n, int k)
        {
            var vector = new int[n];
            vector[k] = 1;
            return vector;
        }

        public static TResult[,] Cast<TSource, TResult>(this TSource[,] self, Func<TSource, TResult> cast)
        {
            var m = self.GetLength(0);
            var n = self.GetLength(1);
            var matrix = new TResult[m, n];
            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    matrix[i, j] = cast(self[i, j]);
                }
            }
            return matrix;
        }

        public static T[,] SwapRaws<T>(this T[,] self, int r1, int r2) where T : struct
        {
            var raws = self.GetLength(0);
            var cols = self.GetLength(1);
            if (r1 > raws || r2 > raws) throw new IndexOutOfRangeException();
            for (var i = 0; i < cols; i++)
            {
                var tmp = self[r1, i];
                self[r1, i] = self[r2, i];
                self[r2, i] = tmp;
            }
            return self;
        }

        public static T[,] SwapCols<T>(this T[,] self, int c1, int c2) where T : struct
        {
            var raws = self.GetLength(0);
            var cols = self.GetLength(1);
            if (c1 > cols || c2 > cols) throw new IndexOutOfRangeException();
            for (var i = 0; i < raws; i++)
            {
                var tmp = self[i, c1];
                self[i, c1] = self[i, c2];
                self[i, c2] = tmp;
            }
            return self;
        }

        public static T[,] OrderRaws<T>(this T[,] self, int[] order) where T : struct
        {
            var raws = self.GetLength(0);
            var cols = self.GetLength(1);
            if (raws != order.Length) throw new IndexOutOfRangeException();
            var matrix = new T[raws, cols];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    matrix[i, j] = self[order[i], j];
                }
            }
            return matrix;
        }

        public static T[,] OrderCols<T>(this T[,] self, int[] order) where T : struct
        {
            var raws = self.GetLength(0);
            var cols = self.GetLength(1);
            if (cols != order.Length) throw new IndexOutOfRangeException();
            var matrix = new T[raws, cols];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    matrix[i, j] = self[i, order[j]];
                }
            }
            return matrix;
        }

        public static double[,] Add(this double[,] a, double[,] b)
        {
            var m = a.GetLength(0);
            var n = a.GetLength(1);
            var c = new double[m, n];
            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }
            return c;
        }

        public static int[] Mul(this int[,] a, int[] b)
        {
            var raws = a.GetLength(0);
            var cols = a.GetLength(1);
            var raws2 = b.Length;
            if (cols != raws2) throw new InvalidOperationException("matrix size mismatch");
            var c = new int[raws];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    c[i] += a[i, j] * b[j];
                }
            }
            return c;
        }

        public static int[,] Mul(this int[,] a, int[,] b)
        {
            var raws = a.GetLength(0);
            var cols = a.GetLength(1);
            var raws2 = b.GetLength(0);
            var cols2 = b.GetLength(1);
            if (cols != raws2) throw new InvalidOperationException("matrix size mismatch");
            var c = new int[raws, cols2];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    for (var k = 0; k < cols2; k++)
                    {
                        c[i, k] += a[i, j] * b[j, k];
                    }
                }
            }
            return c;
        }

        public static double[,] Mul(this double[,] a, double b)
        {
            var raws = a.GetLength(0);
            var cols = a.GetLength(1);
            var c = new double[raws, cols];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    c[i, j] = b * a[i, j];
                }
            }
            return c;
        }

        public static double[] Mul(this double[,] a, double[] b)
        {
            var raws = a.GetLength(0);
            var cols = a.GetLength(1);
            var raws2 = b.Length;
            if (cols != raws2) throw new InvalidOperationException("matrix size mismatch");
            var c = new double[raws];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    c[i] += a[i, j] * b[j];
                }
            }
            return c;
        }

        public static double[,] Mul(this double[,] a, double[,] b)
        {
            var raws = a.GetLength(0);
            var cols = a.GetLength(1);
            var raws2 = b.GetLength(0);
            var cols2 = b.GetLength(1);
            if (cols != raws2) throw new InvalidOperationException("matrix size mismatch");
            var c = new double[raws, cols2];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    for (var k = 0; k < cols2; k++)
                    {
                        c[i, k] += a[i, j] * b[j, k];
                    }
                }
            }
            return c;
        }

        public static T[,] Transpose<T>(this T[,] a)
        {
            var raws = a.GetLength(0);
            var cols = a.GetLength(1);
            var b = new T[cols, raws];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    b[j, i] = a[i, j];
                }
            }
            return b;
        }

        public static U[,] T<U>(this U[,] a)
        {
            return a.Transpose();
        }

        public static double[,] Inverse(this double[,] a)
        {
            throw new NotImplementedException();
        }

        public static double[,] PseudoInverse(this double[,] a)
        {
            return a.GetLength(0) > a.GetLength(1) ? a.T().Mul(a).Inverse().Mul(a.T()) : a.T().Mul(a.Mul(a.T()).Inverse());
        }

        public static double[] Normalize(this IEnumerable<int> a)
        {
            var array = a as int[] ?? a.ToArray();
            var sum = array.Sum();
            return array.Select(v => (double) v / sum).ToArray();
        }

        public static double[] Normalize(this IEnumerable<double> a)
        {
            var array = a as double[] ?? a.ToArray();
            var sum = array.Sum();
            return array.Select(v => v / sum).ToArray();
        }

        public static double[,] NormalizeToRaw(this double[,] a, double tolerance = 1e-6)
        {
            var raws = a.GetLength(0);
            var cols = a.GetLength(1);
            var b = new double[raws, cols];
            for (var i = 0; i < raws; i++)
            {
                var sum = 0.0;
                for (var j = 0; j < cols; j++)
                {
                    sum += a[i, j];
                }
                for (var j = 0; j < cols; j++)
                {
                    b[i, j] = Math.Abs(sum) < tolerance ? 1.0 / raws : a[i, j] / sum;
                }
            }
            return b;
        }
    }
}