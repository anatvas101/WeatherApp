using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using WeatherApp.Configuration;
using WeatherApp.DAL;
using WeatherApp.Providers;

namespace WeatherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherProvidersCollection providers = new WeatherProvidersCollection();
            var configuration = BuildConfiguration();

            var openWeatherConfiguration = new OpenWeatherConfiguration();
            configuration.GetSection("OpenWeather").Bind(openWeatherConfiguration);
            providers.Add(new OpenWeatherProvider(openWeatherConfiguration));

            var weatherBitMapConfig = new WeatherBitConfiguration();
            configuration.GetSection("WeatherBit").Bind(weatherBitMapConfig);
            providers.Add(new WeatherBitProvider(weatherBitMapConfig));

            var cityName = args.Length > 0 ? args[0] : "Kyiv";
            WeatherData response = providers.GetCurrentWeatherData(cityName).GetAwaiter().GetResult();
            WriteToFile("weather.txt", response);
            Console.WriteLine(response);
        }

        private static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        private static void WriteToFile(string filePath, WeatherData data)
        {
            using (var streamWriter = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                streamWriter.WriteLine(data);
                streamWriter.WriteLine("---- ---- ---- ---- ----");
                streamWriter.Close();
            }
        }
    }
}