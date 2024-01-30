namespace School
{
    public class Student
    {
        private string name;
        private int uniqueNumber;

        public Student(string name, int uniqueNumber)
        {
            Name = name;
            UniqueNumber = uniqueNumber;
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty or whitespace.");
                }

                name = value;
            }
        }
        public int UniqueNumber
        {
            get => uniqueNumber;
            set
            {
                if (value < 10000 || value > 99999)
                {
                    throw new ArgumentException("Unique Number must be between 10000 and 99999.");
                }

                uniqueNumber = value;
            }
        }
    }    
}
