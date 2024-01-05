using System.Reflection;
using System.Text;

namespace Exercuse2Page104
{
    public class Worker
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get => $"{FirstName} {LastName}"; set {}
        }
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

        public string GetCharacteristics(bool detailed)
        {
            if (detailed)
            {
                return $"{FullName}, Age: {Age}, Salary: ${Salary}";
            }
            else
            {
                return $"{FullName}";
            }
        }
    }

    public class Program
    {
        static void Main()
        {
            Worker johnDoe = new Worker("John", "Doe", 30, 50000);

            Type workerType = johnDoe.GetType();

            PropertyInfo fullNameProperty = workerType.GetProperty("FullName");
            if (fullNameProperty == null)
            {
                fullNameProperty.SetValue(johnDoe, "Ivan Draganov");
            }

            MethodInfo getCharacteristicsMethod = workerType.GetMethod("GetCharacteristics");

            if (getCharacteristicsMethod != null)
            {
                object result = getCharacteristicsMethod.Invoke(johnDoe, new object[] { false });

                Console.WriteLine($"GetCharacteristics result: {result}");
            }
        }
    }
}
