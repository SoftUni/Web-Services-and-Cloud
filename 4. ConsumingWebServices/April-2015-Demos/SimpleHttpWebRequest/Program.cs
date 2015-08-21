using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpWebRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            var request = WebRequest.Create("http://localhost:37328/api/students") as HttpWebRequest;

            request.ContentType = "application/json";
            request.Method = "GET";

            var response = request.GetResponse();
            var responseReader = new StreamReader(response.GetResponseStream());
            Console.WriteLine(responseReader.ReadToEnd());
            responseReader.Close();

            var postResponse = PostRequest();
            var postResponseReader = new StreamReader(postResponse.GetResponseStream());
            Console.WriteLine(postResponseReader.ReadToEnd());
            postResponseReader.Close();
        }

        static WebResponse PostRequest()
        {
            var request = WebRequest.Create("http://localhost:37328/api/students/PostStudent") as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "POST";

            var requestWritter = new StreamWriter(request.GetRequestStream());
            requestWritter.Write("{'FirstName': 'Minka', 'LastName': 'Bugova'}");
            requestWritter.Close();
            var response = request.GetResponse();

            return response;
        }
    }
}
