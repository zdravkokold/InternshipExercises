namespace Exercise3Page99
{
    public class Program
    {
        static void Main(string[] args)
        {
            string taycanFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "taycan.txt");
            string outputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "numberedLines.txt");

            try
            {
                string[] lines = File.ReadAllLines(taycanFilePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = $"{i + 1}. {lines[i]}";
                }

                File.WriteAllLines(outputFilePath, lines);

                Console.WriteLine("Line enumeration added and saved successfully.");
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }
    }
}
