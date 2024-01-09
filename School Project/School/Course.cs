using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School
{
    public class Course
    {
        private List<Student> students;
        public Course()
        {
            students = new List<Student>();
        }
        public List<Student> Students
        {
            get => students;
            set { students = value; }
        }

        public void AddStudent(Student student)
        {
            if (students.Count > 30)
            {
                throw new ArgumentException("Students must be maximum 30.");
            }
            Students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            if (Students.Contains(student))
            {
                Students.Remove(student);
            }
            else
            {
                throw new ArgumentException("Student does not exist!");
            }
        }
    }
}
