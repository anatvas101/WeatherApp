using System.Collections.Specialized;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Configuration;
using WeatherApp.DAL;
using WeatherApp.Response;

namespace WeatherApp.Providers
{
    public class OpenWeatherProvider : BaseWeatherProvider, IWeatherProvider
    {
        private const string CurrentWeatherApiEndpoint = "data/2.5/weather";

        public OpenWeatherProvider(ProviderConfiguration settings) : base(settings)
        {
        }

        public async Task<WeatherData> GetCurrentWeatherData(string cityName)
        {
            var jsonResponse = await SendRequest(CurrentWeatherApiEndpoint,
                PrepareRequestParams(new NameValueCollection
                {
                    {"q", cityName}
                }));
            var rawResponse = JsonConvert.DeserializeObject<OpenWeatherCurrentWeatherResponse>(jsonResponse);
            return rawResponse.MapToWeatherData();
        }
    }
}