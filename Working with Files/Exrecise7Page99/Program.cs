using System.Text.RegularExpressions;
using System.Text;

namespace Exrecise7Page99
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string testFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.txt");
                string wordsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "words.txt");
                string resultFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.txt");

                string[] separators = { ", ", " ", ". ", ": " };

                List<string> testText = File.ReadAllText(testFilePath).Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
                List<string> words = File.ReadAllText(wordsFilePath).Split(", ").ToList();

                var wordOcurrances = new Dictionary<string, int>();

                foreach (string word in testText)
                {
                    foreach (string listedWord in words)
                    {
                        if (listedWord == word)
                        {
                            if (wordOcurrances.ContainsKey(listedWord))
                            {
                                wordOcurrances[listedWord]++;
                            }
                            else
                            {
                                wordOcurrances.Add(listedWord, 1);
                            }
                        }
                    }
                }

                var sb = new StringBuilder();
                foreach ((string key, int value) in wordOcurrances)
                {
                    sb.AppendLine($"{key} - {value}");
                }

                File.WriteAllText(resultFilePath, sb.ToString().TrimEnd());
                 
                Console.WriteLine(sb.ToString().TrimEnd());
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