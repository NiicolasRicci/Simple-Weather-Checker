using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Weather_Finder
{
    class Weather
    {
        public dynamic weatherData;
        private string weatherApiKey = ""; //Weatherapi.com API Key

        public string city { get; set; }
        public string country { get; set; }
        public int temp_c { get; set; }
        public int temp_f { get; set; }
        public int feelslike_c { get; set; }
        public int feelslike_f { get; set; }
        public decimal precip_mm { get; set; }
        public decimal precip_in { get; set; }
        public int humidity { get; set; }
        public decimal wind_mph { get; set; }
        public decimal wind_kph { get; set; }

        public void cityInfo()
        {
            Console.Write("Enter a City: ");
            city = Console.ReadLine();
        }
        public string ApiUrl()
        {
         string weatherApiUrl = $"http://api.weatherapi.com/v1/current.json?key={weatherApiKey}&q={city}&aqi=no"; //Weatherapi.com URL with parameters
         return weatherApiUrl;
        }
        public void CollectData()
        {
            country = weatherData.location.country;
            temp_c = weatherData.current.temp_c;
            temp_f = weatherData.current.temp_f;
            feelslike_c = weatherData.current.feelslike_c;
            feelslike_f = weatherData.current.feelslike_f;
            precip_mm = weatherData.current.precip_mm;
            precip_in = weatherData.current.precip_in;
            humidity = weatherData.current.humidity;
            wind_mph = weatherData.current.wind_mph;
            wind_kph = weatherData.current.wind_kph;

        }

        public void displayData()
        {
            Console.WriteLine();
            Console.WriteLine($"Here's the current forecast for {city}, {country}:");
            Console.WriteLine();
            Console.WriteLine($"Temperature:{temp_c}°C | {temp_f}°F");
            Console.WriteLine($"Feels like: {feelslike_c}°C | {feelslike_f}°F");
            Console.WriteLine($"Precipitation: {precip_mm} mm | {precip_in} in");
            Console.WriteLine($"Humidity: {humidity}%");
            Console.WriteLine($"Wind: {wind_kph} km/h | {wind_mph} mph");
            Console.WriteLine();
        }
    }
    class Program
    {

        static async Task Main(string[] args)
        {
            Weather weather = new Weather();

            weather.cityInfo(); //Asking City Name


            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(weather.ApiUrl());
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    dynamic weatherData = JsonConvert.DeserializeObject(result);
                    weather.weatherData = weatherData;
                    weather.CollectData();
                    weather.displayData();

                }
                else
                {
                    Console.WriteLine($"Could not get weather data for {weather.city}. (Verify API key and/or spelling)");
                }
            }

        }
    }
}

