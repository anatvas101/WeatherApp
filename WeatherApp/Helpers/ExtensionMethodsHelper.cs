using System;

namespace WeatherApp.Helpers
{
    public static class ExtensionMethodsHelper
    {
        public static DateTime FromUnixTime(this int unixTimestamp)
        {
            DateTime unixZero = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return unixZero.AddSeconds(unixTimestamp).ToLocalTime();
        }
    }
}