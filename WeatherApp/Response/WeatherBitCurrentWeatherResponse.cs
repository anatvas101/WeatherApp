using System.Collections.Generic;
using WeatherApp.DAL;
using WeatherApp.Helpers;

namespace WeatherApp.Response
{
    public class WeatherBitGeneralData
    {
        public string Description { get; set; }
    }

    public class WeatherBitData
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
        public double Temp { get; set; }
        public double Wind_spd { get; set; }
        public double Pres { get; set; }
        public double Rh { get; set; }
        public int Ts { get; set; }
        public string City_name { get; set; }
        public WeatherBitGeneralData Weather { get; set; }
    }

    public class WeatherBitCurrentWeatherResponse
    {
        public string Provider = "Weather Bit";
        public List<WeatherBitData> Data { get; set; }
        public int Count { get; set; }

        public WeatherData MapToWeatherData()
        {
            if (Count == 0)
            {
                return new WeatherData();
            }

            WeatherBitData data = Data[0];
            return new WeatherData
            {
                Condition = data.Weather.Description,
                Temperature = data.Temp,
                Pressure = data.Pres,
                Humidity = data.Rh,
                WindSpeed = data.Wind_spd,
                Coordinates = new Coordinates
                {
                    Lat = data.Lat,
                    Lon = data.Lon
                },
                City = data.City_name,
                Provider = Provider,
                DateTime = data.Ts.FromUnixTime()
            };
        }
    }
}