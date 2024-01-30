namespace School.Tests
{
    [TestClass]
    public class SchoolTests
    {        
        [TestMethod]
        public void TestStudentConstructor()
        {
            var student = new Student("Jo", 10200);

            Assert.AreEqual(student.Name, "Jo");
            Assert.AreEqual(student.UniqueNumber, 10200);
        }

        [TestMethod]
        public void TestStudentConstructorWithInvalidName()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => new Student(" ", 10200));
            Assert.AreEqual("Name cannot be empty or whitespace.", exception.Message);
        }

        [TestMethod]
         public void TestStudentConstructorWithInvalidUniqueNumber()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => new Student("John", 1000));
            Assert.AreEqual("Unique Number must be between 10000 and 99999.", exception.Message);
        }

        [TestMethod]
        public void TestAddStudentToCourse()
        {
            var student = new Student("Jo", 10200);
            var course = new Course();
            course.AddStudent(student);

            CollectionAssert.Contains(course.Students, student);
        }

        [TestMethod]
        public void TestRemoveStudentFromCourse()
        {
            var student = new Student("Jo", 10200);
            var course = new Course();
            course.AddStudent(student);
            course.RemoveStudent(student);

            Assert.AreEqual(0, course.Students.Count);
        }

        [TestMethod]
        public void TestThrowsExceptionWhenStudentsAreOver30()
        {
            var course = new Course();

            var students = Enumerable.Range(30000, 30)
            .Select(number => new Student($"Student{number}", number))
            .ToList();

            foreach (var student in students)
            {
                course.AddStudent(student);
            }

            var exception = Assert.ThrowsException<ArgumentException>(() => course.AddStudent(new Student("Jo", 22222)));
            Assert.AreEqual("Students must be maximum 30.", exception.Message);
        }
    }
}