using System.Collections.Specialized;
using System.Threading.Tasks;
using WeatherApp.Configuration;
using WeatherApp.Helpers;

namespace WeatherApp.Providers
{
    public class BaseWeatherProvider
    {
        private readonly ProviderConfiguration _settings;

        protected BaseWeatherProvider(ProviderConfiguration settings)
        {
            _settings = settings;
        }

        protected NameValueCollection PrepareRequestParams(NameValueCollection customRequestParams = null)
        {
            NameValueCollection requestParams = customRequestParams ?? new NameValueCollection();
            NameValueCollection defaultParams = _settings.GetDefaultRequestParams();
            foreach (var key in defaultParams.AllKeys)
            {
                requestParams.Add(key, defaultParams[key]);
            }

            return requestParams;
        }

        protected async Task<string> SendRequest(string endPoint, NameValueCollection queryParams = null)
        {
            using (HttpTransport httpTransport = new HttpTransport(_settings.BaseUrl))
            {
                return await httpTransport.GetApiResponse(endPoint, queryParams);
            }
        }
    }
}