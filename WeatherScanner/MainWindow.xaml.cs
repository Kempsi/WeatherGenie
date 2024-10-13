using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WeatherScanner.Entities.Forecast;
using WeatherScanner.Entities.Managers;
using WeatherScanner.Entities.Services;
using WeatherScanner.Entities.WeatherModels;
using WeatherScanner.UI.ForecastCard;

namespace WeatherScanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
		WeatherAPI apiCaller;
		GeoCoderAPI geocoder;
		ForecastManager forecastManager;


		public MainWindow()
		{
			InitializeComponent();
			EncryptConfig();
			PopulateForecast();
		}

		public async void GetWeather()
		{
			apiCaller = new WeatherAPI();
			geocoder = new GeoCoderAPI();
			var cords = await geocoder.GetCords("Joensuu");

			var forecastResponse = await apiCaller.Get5DayWeather(cords.Lat, cords.Lon);

		}

		// Populates 5 day forecastcards
		private void PopulateForecast()
		{
			forecastManager = new ForecastManager();

			for (int i = 0; i < forecastManager.forecastCards.Length; i++)
			{
				ForecastCard card = forecastManager.forecastCards[i];

				card.manager = forecastManager;

				card.DataContext = forecastManager.forecastCards[i];

				sp_forecastcards.Children.Add(card);
			}
		}


		#region Window controls

		// Enables dragging for window when holding mouse down
		private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}

		// Closes the window
		private void btn_CloseClicked(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		// Minimizes the window
		private void btn_MinClicked(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		#endregion Window controls

		#region Encryption 

		// Encrypts the app.config file
		private void EncryptConfig()
		{
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			if (!config.AppSettings.SectionInformation.IsProtected)
			{
				config.AppSettings.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
				config.Save();
			}
		}

		#endregion Encryption


	}




}