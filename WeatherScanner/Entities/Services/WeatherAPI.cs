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

		#region API requests

		// Makes an API call to get next 5 days weather info
		private async Task<string> RequestForecast(double lat, double lon)
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

		// Returns json response as a ForecastResponse class
		public async Task<ForecastResponse> GetForecast(double lat, double lon)
		{
			var json = await RequestForecast(lat, lon);
			Console.WriteLine(json);

			if (json == "Error")
			{
				return null;
			}

			ForecastResponse forecastResponse = JsonConvert.DeserializeObject<ForecastResponse>(json);

			return forecastResponse;
		}

		#endregion API requests



	}
}
