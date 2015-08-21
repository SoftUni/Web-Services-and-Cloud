namespace HttpRequestResponseMessage
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    class HttpRequester
    {
        private string baseUrl;
        private HttpClient client;

        public HttpRequester(string baseUrl)
        {
            this.baseUrl = baseUrl;
            this.client = new HttpClient();
        }

        public T Get<T>(string serviceUrl, string mediaType = "application/json")
        {
            var request = new HttpRequestMessage();

            request.RequestUri = new Uri(this.baseUrl + serviceUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Method = HttpMethod.Get;

            var response = client.SendAsync(request).Result;

            var returnObj = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(returnObj);
        }

        public T Post<T>(string serviceUrl, object data, string mediaType = "application/json")
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(this.baseUrl + serviceUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Method = HttpMethod.Post;

            request.Content = new StringContent(JsonConvert.SerializeObject(data));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);

            var response = client.SendAsync(request).Result;
            return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        }

        public Task<T> CreateGetRequestAsync<T>(string serviceUrl, string mediaType = "application/json")
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(this.baseUrl + serviceUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Method = HttpMethod.Get;

            return client.SendAsync(request).ContinueWith(
                (task) =>
                {
                    var response = task.Result;
                    var content = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<T>(content);
                    return result;
                });
        }

        public Task<T> PostAsync<T>(string serviceUrl,
            object data, string mediaType = "application/json")
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(this.baseUrl + serviceUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Method = HttpMethod.Post;

            request.Content = new StringContent(JsonConvert.SerializeObject(data));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);

            return client.SendAsync(request).ContinueWith((task) =>
            {
                var response = task.Result;
                var content = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<T>(content);
                return result;
            });
        }

        public Task PostAsync(string serviceUrl,
            object data, string mediaType = "application/json")
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(this.baseUrl + serviceUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Method = HttpMethod.Post;

            request.Content = new StringContent(JsonConvert.SerializeObject(data));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);

            return client.SendAsync(request);
        }
    }
}
