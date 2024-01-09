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
            Assert.ThrowsException<ArgumentException>(() => new Student(" ", 10200), "Name cannot be empty or whitespace.");
        }

        [TestMethod]
        public void TestStudentConstructorWithInvalidUniqueNumber()
        {
            Assert.ThrowsException<ArgumentException>(() => new Student(" ", 1000), "Unique Number must be between 10000 and 99999.");
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

            var students = Enumerable.Range(20000, 31)
            .Select(number => new Student($"Student{number}", number))
            .ToList();

            foreach (var student in students)
            {
                course.AddStudent(student);
            }

            Assert.ThrowsException<ArgumentException>(() => course.AddStudent(new Student("Jo", 40000)), "Students must be maximum 30.");
        }
    }
}