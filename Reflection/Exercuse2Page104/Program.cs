using System.Reflection;
using System.Text;

namespace Exercuse2Page104
{
    public class Worker
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int Age { get; set; }
        public decimal Salary { get; private set; }

        public Worker(string firstName, string lastName, int age, decimal salary)
        {
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
            Age = age;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {FullName}");
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
            Worker worker = new Worker("John", "Doe", 30, 50000);

            Type workerType = worker.GetType();

            PropertyInfo firstNameProperty = workerType.GetProperty("FirstName");
            PropertyInfo lastNameProperty = workerType.GetProperty("LastName");
            if (firstNameProperty != null && firstNameProperty != null)
            {
                firstNameProperty.SetValue(worker, "Ivan");
                lastNameProperty.SetValue(worker, "Draganov");
            }

            MethodInfo getCharacteristicsMethod = workerType.GetMethod("GetCharacteristics");

            if (getCharacteristicsMethod != null)
            {
                object result = getCharacteristicsMethod.Invoke(worker, new object[] { true });

                Console.WriteLine($"GetCharacteristics result: {result}");
            }
        }
    }
}
