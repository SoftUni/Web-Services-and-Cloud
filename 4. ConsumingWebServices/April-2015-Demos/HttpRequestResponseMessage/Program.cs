namespace HttpRequestResponseMessage
{
    using System;
    using System.Collections.Generic;
    using HttpRequestResponseMessage.Models;

    public class Program
    {
        public static void Main()
        {
            var baseUrl = "http://localhost:37328/api/";
            var requester = new HttpRequester(baseUrl);

            var newStudent = new Student()
            {
                FirstName = "Vladimir",
                LastName = "Georgiev"
            };

            var createStudentTask = requester.PostAsync<Student>("students", newStudent);
            createStudentTask.GetAwaiter()
                             .OnCompleted(() =>
                             {
                                 Console.WriteLine("Student {0} created!", createStudentTask.Result.FullName);
                                 var students = requester.Get<IEnumerable<Student>>("students");
                                 foreach (var student in students)
                                 {
                                     Console.WriteLine(student.FullName);
                                 }
                             });
            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}