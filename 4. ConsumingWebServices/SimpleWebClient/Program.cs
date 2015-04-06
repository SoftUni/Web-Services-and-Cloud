namespace SimpleWebClient
{
    using System;
    using System.Net;

    public class Program
    {
        public static void Main()
        {
            var url = "http://localhost:37328/api/students";
            var client = new WebClient();

            var students = client.DownloadString(url);

            Console.WriteLine(students);

            // Equivalent of POST request. Note: Don't forget the content type
            var studentToPost = "{'FirstName': 'Hitar', 'LastName': 'Petar'}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url, studentToPost);

            students = client.DownloadString(url);

            Console.WriteLine(students);
        }
    }
}
