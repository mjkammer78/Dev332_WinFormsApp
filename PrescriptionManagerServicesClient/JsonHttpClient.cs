using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PrescriptionManagerServicesClient
{
    public class JsonHttpClient
    {
        private const string ApplicationJsonMediaType = "application/json";

        private readonly string _baseUri;
        private readonly HttpClient _client;

        public JsonHttpClient(string baseUri, string accessToken = null)
        {
            _baseUri = baseUri;
            _client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            }
        }

        public async Task<string> GetAsync(string relativePath, (string key, string value)[] parameters = null)
        {
            var uri = $"{_baseUri}/{relativePath}";
            if (parameters != null)
            {
                var queryParameters = HttpUtility.ParseQueryString(string.Empty);
                foreach (var parameter in parameters)
                {
                    queryParameters[parameter.key] = parameter.value;
                }
                uri += $"?{queryParameters}";
            }
            var request = BuildGetRequest(uri);
            var result = await GetStringResultAsync(request);
            return result;
        }

        private HttpRequestMessage BuildGetRequest(string url)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJsonMediaType));

            return request;
        }

        private async Task<string> GetStringResultAsync(HttpRequestMessage request)
        {
            var responseMessage = await _client.SendAsync(request);
            responseMessage.EnsureSuccessStatusCode();
            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteAsync(string relativePath)
        {
            var request = BuildDeleteRequest($"{_baseUri}/{relativePath}");
            var result = await GetStringResultAsync(request);
            return result;
        }

        private HttpRequestMessage BuildDeleteRequest(string url)
        {
            var request = BuildGetRequest(url);
            request.Method = HttpMethod.Delete;
            return request;
        }

        public async Task<string> PostAsync(string relativePath, string jsonContent)
        {
            var request = BuildPostRequest($"{_baseUri}/{relativePath}", jsonContent);
            var result = await GetStringResultAsync(request);
            return result;
        }

        private HttpRequestMessage BuildPostRequest(string url, string jsonContent)
        {
            var request = BuildGetRequest(url);
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(jsonContent, Encoding.UTF8, ApplicationJsonMediaType);
            return request;
        }

        public async Task PutAsync(string relativePath, string jsonContent)
        {
            var request = BuildPutRequest($"{_baseUri}/{relativePath}", jsonContent);
            var responseMessage = await _client.SendAsync(request);
            responseMessage.EnsureSuccessStatusCode();
        }

        private HttpRequestMessage BuildPutRequest(string url, string content)
        {
            var request = BuildPostRequest(url, content);
            request.Method = HttpMethod.Put;
            return request;
        }
    }
}
