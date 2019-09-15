using System.Collections.Specialized;

namespace WeatherApp.Configuration
{
    public class WeatherBitConfiguration : ProviderConfiguration
    {
        public override NameValueCollection GetDefaultRequestParams()
        {
            return new NameValueCollection
            {
                {"key", APIKey}
            };
        }
    }
}