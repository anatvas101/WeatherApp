using System.Threading.Tasks;
using WeatherApp.DAL;

namespace WeatherApp.Providers
{
    public interface IWeatherProvider
    {
        Task<WeatherData> GetCurrentWeatherData(string cityName);
    }
}