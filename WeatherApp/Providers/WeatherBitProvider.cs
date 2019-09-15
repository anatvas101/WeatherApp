using System.Collections.Specialized;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Configuration;
using WeatherApp.DAL;
using WeatherApp.Response;

namespace WeatherApp.Providers
{
    public class WeatherBitProvider : BaseWeatherProvider, IWeatherProvider
    {
        private const string CurrentWeatherApiEndpoint = "v2.0/current";

        public WeatherBitProvider(ProviderConfiguration settings) : base(settings)
        {
        }

        public async Task<WeatherData> GetCurrentWeatherData(string cityName)
        {
            var jsonResponse = await SendRequest(CurrentWeatherApiEndpoint,
                PrepareRequestParams(new NameValueCollection
                {
                    {"city", cityName},
                }));
            var rawResponse = JsonConvert.DeserializeObject<WeatherBitCurrentWeatherResponse>(jsonResponse);
            return rawResponse.MapToWeatherData();
        }
    }
}