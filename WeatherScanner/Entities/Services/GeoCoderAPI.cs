using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Net.Http;
using WeatherScanner.Entities.Models;

namespace WeatherScanner.Entities.Services
{
    public class GeoCoderAPI
    {
        private string ApiKey;

        public GeoCoderAPI()
        {
            ApiKey = ConfigurationManager.AppSettings["ApiKey"];
		}

        private async Task<string> CallForCords(string city)
        {
            string url = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=1&appid={ApiKey}";

            using HttpClient client = new HttpClient();
            var result = await client.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return content;
            }

            return "Error";
        }

        public async Task<CityCords> GetCords(string city)
        {
            var json = await CallForCords(city);

            if (json == "Error")
            {
                return null;
            }

            var data = JArray.Parse(json);
            var cityCords = new CityCords()
            {
                City = (string)data[0]["name"],
                Lat = (double)data[0]["lat"],
                Lon = (double)data[0]["lon"]
            };

            return cityCords;
        }
    }
}
