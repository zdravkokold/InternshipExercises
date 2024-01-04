namespace Exercise2Page93
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static T Sum<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            dynamic sum = 0;
            foreach (var item in source)
            {
                sum += item;
            }
            return sum;
        }

        public static T Min<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!source.Any())
                throw new InvalidOperationException("Sequence contains no elements.");

            dynamic min = source.First();
            foreach (var item in source.Skip(1))
            {
                if (item < min)
                    min = item;
            }
            return min;
        }

        public static T Max<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!source.Any())
                throw new InvalidOperationException("Sequence contains no elements.");

            dynamic max = source.First();
            foreach (var item in source.Skip(1))
            {
                if (item > max)
                    max = item;
            }
            return max;
        }

        public static double Average<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!source.Any())
                throw new InvalidOperationException("Sequence contains no elements.");

            dynamic sum = 0;
            int count = 0;
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
