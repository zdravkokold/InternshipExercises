using System.IO;

namespace Exercise1Page99
{
    public class Program
    {
        static void Main()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "taycan.txt");

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (i % 2 != 0)
                    {
                        Console.WriteLine(lines[i]);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            }
        }
    }
}
