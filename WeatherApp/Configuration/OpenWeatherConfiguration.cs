using System.Collections.Specialized;

namespace WeatherApp.Configuration
{
    public class OpenWeatherConfiguration : ProviderConfiguration
    {
        public override NameValueCollection GetDefaultRequestParams()
        {
            return new NameValueCollection
            {
                {"appid", APIKey},
                {"units", "metric"}
            };
        }
    }
}