using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Net.Http;
using WeatherScanner.Entities.Forecast;
using WeatherScanner.Entities.WeatherModels;

namespace WeatherScanner.Entities.Services
{
    public class WeatherAPI
    {
        private string ApiKey { get; set; }
        public string City { get; set; }

        public WeatherAPI()
        {
            ApiKey = ConfigurationManager.AppSettings["ApiKey"];
		}

        // Makes a API call to get currents days weather info
        private async Task<string> CallForCurrentWeather(string city)
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={ApiKey}&units=metric";

            using HttpClient client = new HttpClient();
            var result = await client.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return content;
            }

            return "Error";
        }

        // Makes a API call to get next 5 days weather info
        private async Task<string> CallFor5DayWeather(double lat, double lon)
        {
            string url = $"http://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&appid={ApiKey}&units=metric";

            using HttpClient client = new HttpClient();
            var result = await client.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return content;
            }

            return "Error";
        }

        // Gets current daily weather and returns it as a WeatherData class
        public async Task<WeatherData> GetCurrentWeather(string city)
        {
            var json = await CallForCurrentWeather(city);

            if (json == "Error")
            {
                return null;
            }

            var data = JObject.Parse(json);
            var weatherData = new WeatherData()
            {
                City = (string)data["name"],
                Temp = (double)data["main"]["temp"],
                TempFeelsLike = (double)data["main"]["feels_like"],
                Main = (string)data["weather"][0]["main"],
                Description = (string)data["weather"][0]["description"]
            };

            return weatherData;
        }

        public async Task<ForecastResponse> Get5DayWeather(double lat, double lon)
        {
            var json = await CallFor5DayWeather(lat, lon);
            Console.WriteLine(json);

            if (json == "Error")
            {
                return null;
            }

            ForecastResponse forecastResponse = JsonConvert.DeserializeObject<ForecastResponse>(json);

            return forecastResponse;
        }



    }
}
