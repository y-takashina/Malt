using System;
using System.Collections.Generic;
using System.Text;

namespace Malt
{
    public static class Vector
    {
        public static int[] OneHot(int n, int k)
        {
            var vector = new int[n];
            vector[k] = 1;
            return vector;
        }
    }
}