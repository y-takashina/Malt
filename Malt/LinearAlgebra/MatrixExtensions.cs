using System;
using System.Collections.Generic;

namespace Malt.LinearAlgebra
{
    public static partial class Matrix
    {
        public static void PrintLine<T>(this T[,] matrix)
        {
            var raws = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            for (var i = 0; i < raws; i++)
            {
                Console.Write("[");
                for (var j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + (j < cols - 1 ? ", " : ""));
                }
                Console.WriteLine("]");
            }
        }

        public static TResult[,] Select<TSource, TResult>(this TSource[,] matrix, Func<TSource, TResult> selector)
        {
            var raws = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            var results = new TResult[raws, cols];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    results[i, j] = selector(matrix[i, j]);
                }
            }
            return results;
        }

        public static int Raws<T>(this T[,] matrix) => matrix.GetLength(0);
        public static int Cols<T>(this T[,] matrix) => matrix.GetLength(1);
        public static (int raws, int cols) Shape<T>(this T[,] matrix) => (matrix.Raws(), matrix.Cols());

        public static T[,] SwapRaws<T>(this T[,] matrix, int r1, int r2)
        {
            var raws = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            if (r1 > raws || r2 > raws) throw new IndexOutOfRangeException();
            for (var i = 0; i < cols; i++)
            {
                var tmp = matrix[r1, i];
                matrix[r1, i] = matrix[r2, i];
                matrix[r2, i] = tmp;
            }
            return matrix;
        }

        public static T[,] SwapCols<T>(this T[,] matrix, int c1, int c2)
        {
            var raws = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            if (c1 > cols || c2 > cols) throw new IndexOutOfRangeException();
            for (var i = 0; i < raws; i++)
            {
                var tmp = matrix[i, c1];
                matrix[i, c1] = matrix[i, c2];
                matrix[i, c2] = tmp;
            }
            return matrix;
        }

        public static T[,] OrderRaws<T>(this T[,] matrix, int[] order)
        {
            var raws = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            if (raws != order.Length) throw new IndexOutOfRangeException();
            var results = new T[raws, cols];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    results[i, j] = matrix[order[i], j];
                }
            }
            return results;
        }

        public static T[,] OrderCols<T>(this T[,] matrix, int[] order)
        {
            var raws = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            if (cols != order.Length) throw new IndexOutOfRangeException();
            var results = new T[raws, cols];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    results[i, j] = matrix[i, order[j]];
                }
            }
            return results;
        }

        public static double[,] Add(this double[,] matrix1, double[,] matrix2)
        {
            var raws = matrix1.GetLength(0);
            var cols = matrix1.GetLength(1);
            var results = new double[raws, cols];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    results[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }
            return results;
        }

        public static int[,] Mul(this int[,] matrix1, int[,] matrix2)
        {
            var raws = matrix1.GetLength(0);
            var cols = matrix1.GetLength(1);
            var raws2 = matrix2.GetLength(0);
            var cols2 = matrix2.GetLength(1);
            if (cols != raws2) throw new InvalidOperationException("matrix size mismatch");
            var results = new int[raws, cols2];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    for (var k = 0; k < cols2; k++)
                    {
                        results[i, k] += matrix1[i, j] * matrix2[j, k];
                    }
                }
            }
            return results;
        }

        public static double[,] Mul(this double[,] matrix, double scalar)
        {
            var raws = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            var results = new double[raws, cols];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    results[i, j] = scalar * matrix[i, j];
                }
            }
            return results;
        }

        public static int[] Mul(this int[,] matrix, int[] vector)
        {
            var raws = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            var raws2 = vector.Length;
            if (cols != raws2) throw new InvalidOperationException("matrix size mismatch");
            var results = new int[raws];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    results[i] += matrix[i, j] * vector[j];
                }
            }
            return results;
        }

        public static double[] Mul(this double[,] matrix, double[] vector)
        {
            var raws = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            var raws2 = vector.Length;
            if (cols != raws2) throw new InvalidOperationException("matrix size mismatch");
            var results = new double[raws];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    results[i] += matrix[i, j] * vector[j];
                }
            }
            return results;
        }

        public static double[,] Mul(this double[,] matrix1, double[,] matrix2)
        {
            var raws = matrix1.GetLength(0);
            var cols = matrix1.GetLength(1);
            var raws2 = matrix2.GetLength(0);
            var cols2 = matrix2.GetLength(1);
            if (cols != raws2) throw new InvalidOperationException("matrix size mismatch");
            var results = new double[raws, cols2];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    for (var k = 0; k < cols2; k++)
                    {
                        results[i, k] += matrix1[i, j] * matrix2[j, k];
                    }
                }
            }
            return results;
        }

        public static T[,] Transpose<T>(this T[,] matrix)
        {
            var raws = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            var results = new T[cols, raws];
            for (var i = 0; i < raws; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    results[j, i] = matrix[i, j];
                }
            }
            return results;
        }

        public static U[,] T<U>(this U[,] matrix) => matrix.Transpose();

        public static double[,] Inverse(this double[,] matrix)
        {
            throw new NotImplementedException();
        }

        public static double[,] PseudoInverse(this double[,] matrix)
        {
            return matrix.GetLength(0) > matrix.GetLength(1) ?
                matrix.T().Mul(matrix).Inverse().Mul(matrix.T()) :
                matrix.T().Mul(matrix.Mul(matrix.T()).Inverse());
        }

        public static double[,] NormalizeToRaw(this double[,] matrix, double tolerance = 1e-6)
        {
            var raws = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            var results = new double[raws, cols];
            for (var i = 0; i < raws; i++)
            {
                var sum = 0.0;
                for (var j = 0; j < cols; j++)
                {
                    sum += matrix[i, j];
                }
                for (var j = 0; j < cols; j++)
                {
                    results[i, j] = Math.Abs(sum) < tolerance ? 1.0 / raws : matrix[i, j] / sum;
                }
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