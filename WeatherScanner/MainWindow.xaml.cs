using System.Configuration;
using System.Windows;
using System.Windows.Input;
using WeatherScanner.Entities.Forecast;
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


		public MainWindow()
		{
			InitializeComponent();
			EncryptConfig();	
		}

		public async void GetWeather()
		{
			apiCaller = new WeatherAPI();
			geocoder = new GeoCoderAPI();
			var cords = await geocoder.GetCords("Joensuu");

			// IT WORKS
			var forecastResponse = await apiCaller.Get5DayWeather(cords.Lat, cords.Lon);

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