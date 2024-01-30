using System.Reflection;
using System.Text;

namespace Exercise1Page103
{
    public class Program
    {
        static void Main(string[] args)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var sb = new StringBuilder();

            foreach (var type in types.Where(x => x.Name.StartsWith("Work")))
            {
                var typeInfo = type.GetTypeInfo();
                sb.AppendLine(type.Name);
            }

            Console.WriteLine(sb.ToString());
        }
    }

    public class Worker
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; private set; }

        public Worker(string firstName, string lastName, int age, decimal salary)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Salary = salary;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {FirstName} {LastName}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Salary: ${Salary}");
        }
    }
}
