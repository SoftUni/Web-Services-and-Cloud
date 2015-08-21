using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS
{
    using RestSharp;

    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://localhost:37328/api/");

            var request = new RestRequest("students/{id}", Method.GET);
            request.AddUrlSegment("id", "5");

            var response = client.Execute(request);

            Console.WriteLine(response.Content);

            var serializedResponse = client.Execute<Student>(request);
            Console.WriteLine(serializedResponse.Data);
        }
    }

    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, FirstName: {1}, LastName: {2}", this.Id, this.FirstName, this.LastName);
        }
    }
}
