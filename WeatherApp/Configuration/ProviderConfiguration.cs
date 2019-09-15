using System.Collections.Specialized;

namespace WeatherApp.Configuration
{
    public abstract class ProviderConfiguration
    {
        public string APIKey { get; set; }
        public string BaseUrl { get; set; }

        public abstract NameValueCollection GetDefaultRequestParams();
    }
}