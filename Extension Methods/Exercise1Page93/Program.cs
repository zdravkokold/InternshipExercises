using System;
using System.Text;

namespace Exercise1
{   
    public static class StringBuilderExtensions
    {
        public static StringBuilder Substring(this StringBuilder stringBuilder, int index, int length)
        {
            if (stringBuilder == null)
            {
                throw new ArgumentNullException(nameof(stringBuilder));
            }

            if (index < 0 || index >= stringBuilder.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (length < 0 || index + length > stringBuilder.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(stringBuilder[index + i]);
            }

            return result;
        }
    }

    public class Program
    {
        static void Main()
        {
            StringBuilder sb = new StringBuilder("Hello, World!");

            StringBuilder substring = sb.Substring(7, 5);
            Console.WriteLine(substring);
        }
    }
}