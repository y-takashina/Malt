using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Malt.Clustering
{
    public abstract class Cluster<T>
    {
        public int Count;

        public void Print(string indent = "")
        {
            // ポリモーフィズムで書くと散らばって読みづらい。
            if (this is Single<T>) Console.WriteLine(indent + this);
            else
            {
                var c = (Couple<T>) this;
                c.Left.Print(indent + "|-");
                c.Right.Print(Regex.Replace(indent, @"\-|\+", " ") + "+-");
            }
        }

        public Cluster<T>[] Extract(int n)
        {
            if (Count < n) throw new Exception("Count of cluster must be greater than n.");
            var clusters = new List<Cluster<T>> {this};
            for (var i = 1; i < n; i++)
            {
                var curr = clusters.First();
                var max = double.MinValue;
                foreach (var c in clusters)
                {
                    if (c.Count > max)
                    {
                        curr = c;
                        max = c.Count;
                    }
                }
                var couple = (Couple<T>) curr;
                clusters.Add(couple.Left);
                clusters.Add(couple.Right);
                clusters.Remove(curr);
            }
            return clusters.ToArray();
        }

        public IEnumerable<TResult> SelectMany<TResult>(Func<T, TResult> selector, List<TResult> acc = null)
        {
            acc = acc ?? new List<TResult>();
            if (this is Single<T> single) acc.Add(selector(single.Value));
            else
            {
                var couple = (Couple<T>) this;
                acc.AddRange(couple.Left.SelectMany(selector));
                acc.AddRange(couple.Right.SelectMany(selector));
            }
            return acc;
        }

        public abstract double DistanceTo(Cluster<T> to, Func<T, T, double> calcPointwiseDistance, Func<(double, int), (double, int), double> calcClusterwiseDistance);
    }

    public class Single<T> : Cluster<T>
    {
        public readonly T Value;

        public Single(T value)
        {
            Value = value;
            Count = 1;
        }

        public override bool Equals(object obj)
        {
            return obj is Single<T> single && Equals(single);
        }

        protected bool Equals(Single<T> other)
        {
            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }

        public override string ToString()
        {
            return "Single(" + Value + ")";
        }

        public override double DistanceTo(Cluster<T> to, Func<T, T, double> calcPointwiseDistance, Func<(double, int), (double, int), double> calcClusterwiseDistance)
        {
            if (to is Single<T> single) return calcPointwiseDistance(Value, single.Value);
            var couple = to as Couple<T>;
            if (couple == null) throw new InvalidCastException("`to` must be Single<T> or Couple<T>.");
            var left = DistanceTo(couple.Left, calcPointwiseDistance, calcClusterwiseDistance);
            var right = DistanceTo(couple.Right, calcPointwiseDistance, calcClusterwiseDistance);
            return calcClusterwiseDistance((left, couple.Left.Count), (right, couple.Right.Count));
        }
    }

    public class Couple<T> : Cluster<T>
    {
        public readonly Cluster<T> Left;
        public readonly Cluster<T> Right;

        public Couple(Cluster<T> left, Cluster<T> right)
        {
            Left = left;
            Right = right;
            Count = left.Count + right.Count;
        }

        public override bool Equals(object obj)
        {
            return obj is Couple<T> couple && Equals(couple);
        }

        protected bool Equals(Couple<T> other)
        {
            return Left.Equals(other.Left) && Right.Equals(other.Right) ||
                   Left.Equals(other.Right) && Right.Equals(other.Left);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Left?.GetHashCode() ?? 0) * 397) ^ (Right?.GetHashCode() ?? 0);
            }
        }

        public override string ToString()
        {
            return "Couple(" + Left + ", " + Right + ")";
        }

        public override double DistanceTo(Cluster<T> to, Func<T, T, double> calcPointwiseDistance, Func<(double, int), (double, int), double> calcClusterwiseDistance)
        {
            var left = Left.DistanceTo(to, calcPointwiseDistance, calcClusterwiseDistance);
            var right = Right.DistanceTo(to, calcPointwiseDistance, calcClusterwiseDistance);
            return calcClusterwiseDistance((left, Left.Count), (right, Right.Count));
        }
    }

    public static partial class Clustering
    {
        public static Cluster<T> Cluster<T>(T value)
        {
            return new Single<T>(value);
        }

        public static Cluster<T> Cluster<T>(Cluster<T> left, Cluster<T> right)
        {
            return new Couple<T>(left, right);
        }
    }
}