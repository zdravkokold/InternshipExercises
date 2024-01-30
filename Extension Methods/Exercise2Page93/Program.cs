using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Exercise2Page93
{   
    public static class EnumerableExtensions
    {
        public static T Sum<T>(this IEnumerable<T> source)
            where T : INumber<T>
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            T sum = T.Zero;
            foreach (var item in source)
            {
                sum += item;
            }
            return sum;
        }

        public static T Min<T>(this IEnumerable<T> source)
             where T : INumber<T>
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!source.Any())
                throw new InvalidOperationException("Sequence contains no elements.");

            T min = source.First();
            foreach (var item in source.Skip(1))
            {
                if (item < min)
                    min = item;
            }
            return min;
        }

        public static T Max<T>(this IEnumerable<T> source)
             where T : INumber<T>
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!source.Any())
                throw new InvalidOperationException("Sequence contains no elements.");

            T max = source.First();
            foreach (var item in source.Skip(1))
            {
                if (item > max)
                    max = item;
            }
            return max;
        }

        public static T Average<T>(this IEnumerable<T> source)
             where T : INumber<T>
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!source.Any())
                throw new InvalidOperationException("Sequence contains no elements.");

            T sum = T.Zero;
            T count = T.Zero;
            foreach (var item in source)
            {
                sum += item;
                count++;
            }
            return sum / count;
        }
    }

    public class Program
    {
        static void Main()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            Console.WriteLine($"Sum: {numbers.Sum()}");
            Console.WriteLine($"Min: {numbers.Min()}");
            Console.WriteLine($"Max: {numbers.Max()}");
            Console.WriteLine($"Average: {numbers.Average()}");
        }
    }
}
