namespace Exercise2Page99
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string taycanFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "taycan.txt");
                string etronFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "etron.txt");

                string taycanText = File.ReadAllText(taycanFilePath);
                string etronText = File.ReadAllText(etronFilePath);

                string combined = taycanText + etronText;

                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "combined.txt"), combined);

                Console.WriteLine("Files combined successfully.");
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}
