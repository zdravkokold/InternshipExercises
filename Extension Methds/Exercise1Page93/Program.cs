namespace Exercise1
{
    using System;
    using System.Text;

    public static class StringBuilderExtensions
    {
        public static string Substring(this StringBuilder stringBuilder, int index, int length)
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

            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = stringBuilder[index + i];
            }

            return new string(result);
        }
    }

    public class Program
    {
        static void Main()
        {
            StringBuilder sb = new StringBuilder("Hello, World!");

            string substring = sb.Substring(7, 5);
            Console.WriteLine(substring);
        }
    }
}