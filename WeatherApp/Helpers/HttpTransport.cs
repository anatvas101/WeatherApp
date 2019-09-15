using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace WeatherApp.Helpers
{
    public class HttpTransport : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public HttpTransport(string baseUri)
        {
            _baseUri = baseUri;

            _httpClient = new HttpClient {BaseAddress = new Uri(_baseUri)};
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetApiResponse(string endPoint, NameValueCollection queryParams = null)
        {
            string responseString;
            Uri requestUri = BuildRequestUri(endPoint, queryParams);

            using (HttpResponseMessage response = await _httpClient.GetAsync(requestUri))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(
                        $"API request to endpoint {endPoint} failed with status {response.StatusCode} - {response.ReasonPhrase}.");
                }

                responseString = await response.Content.ReadAsStringAsync();
            }

            return responseString;
        }

        /// <summary>
        /// Builds an URL for requesting API endpoint
        /// </summary>
        /// <param name="endPoint">Base endpoint</param>
        /// <param name="queryParams">Query params</param>
        private Uri BuildRequestUri(string endPoint, NameValueCollection queryParams = null)
        {
            UriBuilder builder = new UriBuilder(_baseUri) {Path = endPoint};
            if (queryParams != null)
            {
                builder.Query = string.Join("&", queryParams.AllKeys
                    .Where(key => !String.IsNullOrWhiteSpace(queryParams[(string) key]))
                    .Select(key => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(queryParams[key])}")
                );
            }

            return new Uri(builder.ToString());
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}