using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IPDomainChecker.Infrastructure
{
    public interface IRestClient
    {
        Task<T1> PostAsync<T, T1>(string uri, T input);

        Task<T> GetAsync<T>(string uri);

        Task<T> DeleteAsync<T>(string uri);

        Task<T1> PutAsync<T, T1>(string uri, T input);

        Task<T1> PatchAsync<T, T1>(string uri, T input);

        Task<HttpResponseMessage> DeleteAsync(string uri);

        Task<HttpResponseMessage> PostAsync(string uri);
    }

    public class RestClient : IRestClient
    {
        private readonly string _apiUrl;

        private readonly HttpClient _httpClient;

        public string BaseUri
        {
            get { return _apiUrl; }
        }

        public RestClient(string baseUri)
        {
            _apiUrl = !baseUri.EndsWith("/") ? baseUri + "/" : baseUri;
            _httpClient = new HttpClient() { BaseAddress = new Uri(_apiUrl) };
        }

        public async Task<T1> PostAsync<T, T1>(string uri, T input)
        {
            var payload = SerializeJson(input);
            return await await _httpClient.PostAsync(uri, payload).ContinueWith(ReadResponse<T1>());
        }

        public async Task<T> PostAsync<T>(string uri)
        {
            var payload = SerializeJson(string.Empty);
            return await await _httpClient.PostAsync(uri, payload).ContinueWith(ReadResponse<T>());
        }

        public async Task<HttpResponseMessage> PostAsync(string uri)
        {
            var payload = SerializeJson(string.Empty);
            return await _httpClient.PostAsync(uri, payload);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T input)
        {
            var payload = SerializeJson(input);
            return await _httpClient.PostAsync(uri, payload).ConfigureAwait(false);
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            return await await _httpClient.GetAsync(uri).ContinueWith(ReadResponse<T>());
        }

        public async Task<T> DeleteAsync<T>(string uri)
        {
            return await await _httpClient.DeleteAsync(uri).ContinueWith(ReadResponse<T>());
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            return await _httpClient.DeleteAsync(uri);
        }

        public async Task<T1> PutAsync<T, T1>(string uri, T input)
        {
            var payload = SerializeJson(input);
            return await await _httpClient.PostAsync(uri, payload).ContinueWith(ReadResponse<T1>());
        }

        public async Task<T1> PatchAsync<T, T1>(string uri, T input)
        {
            var payload = SerializeJson(input);
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri) { Content = payload };
            return await await _httpClient.SendAsync(request).ContinueWith(ReadResponse<T1>());
        }

        private static Func<Task<HttpResponseMessage>, Task<T>> ReadResponse<T>()
        {
            return async task =>
            {
                var response = await task;

                if (!response.IsSuccessStatusCode)
                {
                    throw new RestHttpResponseException(response.StatusCode, response.ReasonPhrase);
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                return DeserializeJson<T>(responseBody);
            };
        }

        private static StringContent SerializeJson<T>(T request)
        {
            var payload = JsonConvert.SerializeObject(request);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            return content;
        }

        private static T DeserializeJson<T>(string result)
        {
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
