using System;
using System.Linq;
using Malt.LinearAlgebra;
using Xunit;

namespace MaltTest
{
    public class MatrixTest
    {
        [Fact]
        public void ZerosTest() {}

        [Fact]
        public void OnesTest() {}

        [Fact]
        public void UniformTest()
        {
            var matrix = Matrix.Uniform(10, 1.0);
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    Assert.Equal(1.0, matrix[i, j]);
                }
            }
        }

        [Fact]
        public void EyeTest()
        {
            var matrix = Matrix.Eye(10);
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    Assert.Equal(i == j ? 1.0 : 0.0, matrix[i, j]);
                }
            }
        }
    }
}