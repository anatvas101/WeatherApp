using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.DAL;

namespace WeatherApp.Providers
{
    public class WeatherProvidersCollection
    {
        private readonly List<IWeatherProvider> _providersList = new List<IWeatherProvider>();

        public void Add(IWeatherProvider provider)
        {
            if (!_providersList.Contains(provider))
            {
                _providersList.Add(provider);
            }
        }

        public async Task<WeatherData> GetCurrentWeatherData(string cityName)
        {
            List<Task<WeatherData>> weatherRequestTasks = new List<Task<WeatherData>>();
            foreach (IWeatherProvider provider in _providersList)
            {
                weatherRequestTasks.Add(provider.GetCurrentWeatherData(cityName));
            }

            Task<WeatherData> firstFinishedTask = await Task.WhenAny(weatherRequestTasks);
            return await firstFinishedTask;
        }
    }
}