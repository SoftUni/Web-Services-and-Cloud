namespace UsingHttpClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Web;

    public class Program
    {
        private const string GetAllPostsEndpoint = "http://localhost:64411/api/posts";
        private const string AddNewPostEndpoint = "http://localhost:64411/api/posts";
        private const string UserSearchEndpoint = "http://localhost:64411/api/users/search";
        private const string LoginEndpoint = "http://localhost:64411/Token";

        static void Main()
        {
            try
            {
                PrintAllPosts();
            }
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions.Any(e => e is TaskCanceledException))
                {
                    Console.WriteLine("Connection timed out");
                }
                else
                {
                    throw ex;
                }
            }
            PrintSearchedUser();
            Console.WriteLine("Executed after the above methods.");

            var task = PrintAllPostsAsync();
            Console.WriteLine("Waiting for the above task to complete...");
            Console.ReadLine();
        }

        static async Task PrintAllPostsAsync()
        {
            var httpClient = new HttpClient();
            using (httpClient)
            {
                var response = await httpClient.GetAsync(GetAllPostsEndpoint);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(
                        response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    var posts = await response.Content
                       .ReadAsAsync<IEnumerable<PostDTO>>();
                    foreach (var post in posts)
                    {
                        Console.WriteLine(post.Content);
                    }
                }
            }
        }

        private static LoginData Login()
        {
            var httpClient = new HttpClient();
            using (httpClient)
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", "motikarq@nivata.bg"),
                    new KeyValuePair<string, string>("password", "M0tikarq!"),
                    new KeyValuePair<string, string>("grant_type", "password")   
                });

                var response = httpClient.PostAsync(LoginEndpoint, content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    // Nothing to return, throw a proper exception + message
                    throw new HttpRequestException(
                        response.Content.ReadAsStringAsync().Result);
                }

                return response.Content.ReadAsAsync<LoginData>().Result;
            }
        }

        private static void PrintSearchedUser()
        {
            var httpClient = new HttpClient();
            using (httpClient)
            {
                var loginData = Login();
                httpClient.DefaultRequestHeaders.Add(
                   "Authorization", "Bearer " + loginData.Access_Token);

                var builder = new UriBuilder(UserSearchEndpoint);
                var query = HttpUtility.ParseQueryString("");
                query["name"] = "mot";
                query["minAge"] = "14";
                query["maxAge"] = "60";
                builder.Query = query.ToString();

                var fullEndpoint = builder.ToString();

                var response = httpClient.GetAsync(fullEndpoint).Result;
                var json = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(json);
            }
        }

        private static void AddNewPost()
        {
            var httpClient = new HttpClient();
            using (httpClient)
            {
                var loginData = Login();
                httpClient.DefaultRequestHeaders.Add(
                    "Authorization", "Bearer " + loginData.Access_Token);

                var bodyData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("content", "nov post"),
                    new KeyValuePair<string, string>("wallOwnerUsername", "peicho"),
                });

                var response = httpClient.PostAsync(AddNewPostEndpoint, bodyData).Result;
                if (!response.IsSuccessStatusCode)
                {
                    // Print error message
                    Console.WriteLine(response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    var post = response.Content.ReadAsAsync<PostDTO>().Result;
                    Console.WriteLine(post.Content);
                }
            }
        }

        private static void PrintAllPosts()
        {
            var httpClient = new HttpClient();
            using (httpClient)
            {
                // Request will timeout after 3 seconds and throw an exception
                httpClient.Timeout = new TimeSpan(0, 0, 0, 3);
                var response = httpClient.GetAsync(GetAllPostsEndpoint).Result;

                if (!response.IsSuccessStatusCode)
                {
                    // Print error message
                    Console.WriteLine(
                        response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    var posts = response.Content
                       .ReadAsAsync<IEnumerable<PostDTO>>().Result;

                    foreach (var post in posts)
                    {
                        Console.WriteLine(post.Content);
                    }
                }
            }
        }
    }
}
