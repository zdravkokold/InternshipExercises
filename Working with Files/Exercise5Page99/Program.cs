using System.Text.RegularExpressions;

namespace Exercise5Page99
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testText.txt");

                string text = File.ReadAllText(filePath);

                string pattern = @"^[0-9a-zA-Z]+$";

                List<string> words = text.Split(", ").ToList();

                words.RemoveAll(x => x.StartsWith("test") && Regex.IsMatch(x, pattern));

                File.WriteAllText(filePath, string.Join(", ", words));

                Console.WriteLine("File processing completed successfully.");
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occurred during file operation: {e.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
