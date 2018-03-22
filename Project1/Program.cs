using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1
{
    class Student
    {
        public string Surname { get; private set; }
        public IDictionary<string, int> Subject { get; private set; }
        public double avg { get; private set; }

        public Student(string surname, IDictionary<string, int> subject)
        {
            this.Surname = surname;
            this.Subject = subject;
            double AVG = 0;
            foreach (var s in subject)
                AVG += s.Value;
            avg = AVG / this.Subject.Count;
        }

        public override string ToString()
        {
            string res = Surname + " ";
            foreach (var s in Subject)
                res += " " + s.Value + "  ";
            res += avg;
            return res;
        }

        public static bool StudentAVG(Student obj1, Student obj2)
        {
            return obj1.avg < obj2.avg;
        }
    }

    class ArraySort
    {
        public static IEnumerable<T> QSort<T>(IEnumerable<T> sortArray, Func<T, T, bool> res)
        {
            if (sortArray.Any())
            {
                var x = sortArray.First();
                var smaller = QSort(sortArray.Skip(1).Where(elem => res(elem, x)), res);
                var bigger = QSort(sortArray.Skip(1).Where(elem => res(x, elem)), res);
                return smaller.Concat(new[] {x}).Concat(bigger);
            }

            return Enumerable.Empty<T>();
        }
    }


    internal class Program
    {
        public static void Main(string[] args)
        {
            IDictionary<string, int>[] students =
            {
                new Dictionary<string, int>(),
                new Dictionary<string, int>(),
                new Dictionary<string, int>(),
                new Dictionary<string, int>()
            };

            Random r = new Random();

            foreach (var s in students)
            {
                s.Add("Math", 100 + r.Next(0, 50) * (-1) ^ r.Next(2, 3) - 2);
                s.Add("Java", 100 + r.Next(0, 50) * (-1) ^ r.Next(2, 3) - 2);
                s.Add("English", 100 + r.Next(0, 50) * (-1) ^ r.Next(2, 3) - 2);
                s.Add("C#", 100 + r.Next(0, 50) * (-1) ^ r.Next(2, 3) - 2);
            }

            Student[] studentinfo =
            {
                new Student("Fedorov", students[0]),
                new Student("Smirnov", students[1]),
                new Student("Ivanov", students[2]),
                new Student("Miller", students[3])
            };

            studentinfo = ArraySort.QSort(studentinfo, Student.StudentAVG).ToArray();

            Console.WriteLine("Sorting students by average grades:     \n" +
                              "----------------------------------------\n" +
                              "Surname  Math  Java  English  C#  average\n");

            foreach (var s in studentinfo)
                Console.WriteLine(s);

            Console.ReadLine();
        }
    }
}