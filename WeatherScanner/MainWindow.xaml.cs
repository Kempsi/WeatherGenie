using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WeatherScanner.Entities.Forecast;
using WeatherScanner.Entities.Managers;
using WeatherScanner.Entities.Models;
using WeatherScanner.Entities.Services;
using WeatherScanner.Entities.WeatherModels;
using WeatherScanner.UI.ForecastCard;
using WeatherScanner.UI.SelectedDayPanel;

namespace WeatherScanner
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		ForecastManager forecastManager = new ForecastManager();
		SelectedDayManager selectedDayManager;




		private ForecastResponse Response = new ForecastResponse();
		private ForecastCard[] AllCards = new ForecastCard[5];
		private GeoCoderAPI GeoCoderAPI = new GeoCoderAPI();
		private WeatherAPI WeatherAPI = new WeatherAPI();


		private string city = "Joensuu";
		private CityCords cityCords;




		public MainWindow()
		{
			InitializeComponent();
			EncryptConfig();
			InitializeAsync();





			forecastManager.ActiveCardChanged += OnActiveCardChanged;



		}

		// On click, fires off UpdateActiveCard method and updates 
		private void OnActiveCardChanged(ForecastCard obj)
		{
			selectedDayManager.UpdateActiveCard(forecastManager.GetActiveCard());
			selectedDayPanel.DataContext = selectedDayManager.GetSelectedDayPanel();
		}

		// Populates selected day panel with data
		private async Task PopulateDayPanel(ForecastResponse response, ForecastCard[] allCards)
		{
			selectedDayManager = new SelectedDayManager(response, allCards);
			selectedDayManager.SetCity(city);
			selectedDayPanel.DataContext = selectedDayManager.GetSelectedDayPanel();

		}



		#region Forecast card data population

		// Populates forecastcards with data
		private async Task PopulateForecast()
		{
			await forecastManager.InitializeForecast();

			for (int i = 0; i < forecastManager.forecastCards.Length; i++)
			{
				ForecastCard card = forecastManager.forecastCards[i];

				card.manager = forecastManager;

				card.DataContext = forecastManager.forecastCards[i];

				sp_forecastcards.Children.Add(card);
			}
		}

		#endregion Forecast card data population

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

		#region Async and API requests

		// Waits for all of the methods to finish loading data 
		private async Task InitializeAsync()
		{
			await RequestForAPIResponse();
			forecastManager.SetCity(city);
			await PopulateForecast();

			AllCards = forecastManager.GetCards();

			await PopulateDayPanel(Response, AllCards);
		}

		// Makes a separate request for selected day panel
		private async Task RequestForAPIResponse()
		{
			cityCords = await GeoCoderAPI.GetCords(city);
			Response = await WeatherAPI.GetForecast(cityCords.Lat, cityCords.Lon);
		}

		#endregion Async


	}




}