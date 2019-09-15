using System;

namespace WeatherApp.DAL
{
    public struct Coordinates
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class WeatherData
    {
        public string Provider { get; set; }
        public string City { get; set; }
        public Coordinates Coordinates { get; set; }
        public DateTime DateTime { get; set; }
        public string Condition { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double Pressure { get; set; }

        public override string ToString()
        {
            return $"{DateTime}; {City} ({Coordinates.Lat:F3}; {Coordinates.Lon:F3}):\n" +
                   $"{Condition}.\n" +
                   $"Temperature: {Temperature}°C\n" +
                   $"Humidity: {Humidity}%\n" +
                   $"Wind speed: {WindSpeed} m/s\n" +
                   $"Pressure: {Pressure} hPa\n" +
                   $"Based on {Provider} data.";
        }
    }
}