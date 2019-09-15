using System;
using System.Collections.Generic;
using WeatherApp.DAL;
using WeatherApp.Helpers;

namespace WeatherApp.Response
{
    public class OpenWeatherGeneralData
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class OpenWeatherMainData
    {
        public double Temp { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
    }

    public class OpenWeatherWindData
    {
        public double Speed { get; set; }
        public double Deg { get; set; }
    }

    public class OpenWeatherCurrentWeatherResponse
    {
        public Coordinates Coord { get; set; }
        public List<OpenWeatherGeneralData> Weather { get; set; }
        public OpenWeatherMainData Main { get; set; }
        public OpenWeatherWindData Wind { get; set; }
        public int Dt { get; set; }
        public string Name { get; set; }
        public string Provider = "Open Weather";

        private string FormatGeneralConditions()
        {
            return Weather.Count > 0
                ? Weather[0].Main + ", " + Weather[0].Description
                : String.Empty;
        }

        public WeatherData MapToWeatherData()
        {
            return new WeatherData
            {
                Condition = FormatGeneralConditions(),
                Temperature = Main.Temp,
                Pressure = Main.Pressure,
                Humidity = Main.Humidity,
                WindSpeed = Wind.Speed,
                Coordinates = Coord,
                City = Name,
                Provider = Provider,
                DateTime = Dt.FromUnixTime()
            };
        }
    }
}