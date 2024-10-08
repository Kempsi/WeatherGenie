using System.Configuration;
using System.Windows;
using WeatherScanner.Entities.Forecast;
using WeatherScanner.Entities.Services;
using WeatherScanner.Entities.WeatherModels;

namespace WeatherScanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
		WeatherAPI apiCaller;
		GeoCoderAPI geocoder;

		public MainWindow()
		{
			InitializeComponent();
			EncryptConfig();
			
		}

		private void EncryptConfig()
		{
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			if (!config.AppSettings.SectionInformation.IsProtected)
			{
				config.AppSettings.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
				config.Save();
			}
		}

		public async void GetWeather()
		{
			apiCaller = new WeatherAPI();
			geocoder = new GeoCoderAPI();
			var cords = await geocoder.GetCords("Joensuu");

			// IT WORKS
			var forecastResponse = await apiCaller.Get5DayWeather(cords.Lat, cords.Lon);
			
			//foreach (var forecast in forecastResponse.list)
			//{
			//	// Testing API data to see how it is
			//	ForecastListBox.Items.Add(
			//		$"Aika: {forecast.dt_txt}, " +
			//		$"Lämpötila: {forecast.main.temp}, " +
			//		$"Sää: {forecast.weather[0].description}, " +
			//		$"Kaupunki: {forecastResponse.city.name}, " + 
			//		$"DateTime Unix: {forecast.dt}");
			//}
		}

		// Test method for displaying current days weather info
		private void DisplayWeather(WeatherData weatherData)
		{
			if (weatherData != null)
			{
				string weatherInfo = "City: " + weatherData.City + "\n" +
					"Temperature: " + weatherData.Temp + "°C\n" +
					"Feels like: " + weatherData.TempFeelsLike + "°C\n" +
					"Main: " + weatherData.Main + "\n" +
					"Description: " + weatherData.Description;

				MessageBox.Show(weatherInfo);
			}

			else
			{
				MessageBox.Show("Error fetching data");
			}



			
		}



	}




}