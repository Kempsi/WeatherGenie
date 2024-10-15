using System.Diagnostics;
using WeatherScanner.Entities.Forecast;
using WeatherScanner.Entities.Models;
using WeatherScanner.Entities.Services;
using WeatherScanner.UI.ForecastCard;
using WeatherScanner.UI.SelectedDayPanel;

namespace WeatherScanner.Entities.Managers
{
	public class SelectedDayManager
	{

		private DateTime SelectedDate;
		private string City;
		private CityCords Cords;


		private SelectedDayPanel SelectedDayPanel;
		private ForecastManager ForecastManager;
		private ForecastResponse Response;
		private GeoCoderAPI GeoCoderAPI;
		private WeatherAPI WeatherAPI;

		private ForecastCard[] AllCards;


		public SelectedDayManager(ForecastResponse response, ForecastCard[] allCards)
		{
			SelectedDayPanel = new SelectedDayPanel();
			ForecastManager = new ForecastManager();
			Response = new ForecastResponse();
			GeoCoderAPI = new GeoCoderAPI();
			WeatherAPI = new WeatherAPI();

			Response = response;
			AllCards = allCards;

			InitializeAsync();

		}

		#region Async

		private async Task InitializeAsync()
		{
			PopulateDayPanel();
		}

		#endregion Async

		#region Day panel population

		// Populates the day panel with information
		private void PopulateDayPanel()
		{
			foreach (var item in AllCards)
			{
				if (item.IsActive)
				{
					SelectedDate = DateTime.Parse(item.FullDate);
				}
			}

			SelectedDayPanel.City = GetCity();
			SelectedDayPanel.Country = GetCountry();
			SelectedDayPanel.Temp = GetTemp();
			SelectedDayPanel.ImageSource = GetIcon();
			SelectedDayPanel.Desc = GetDesc();
			SelectedDayPanel.Date = GetUpdatedDate();
			SelectedDayPanel.FeelsLike = string.Empty;
			SelectedDayPanel.WindSpeed = string.Empty;
			SelectedDayPanel.MyVisibility = string.Empty;
			SelectedDayPanel.Timezone = string.Empty;
			SelectedDayPanel.Humidity = string.Empty;
			SelectedDayPanel.Sunset = string.Empty;
			SelectedDayPanel.Sunrise = string.Empty;

		}

		#endregion Day panel population

		#region Data fetching and binding

		// Returns the city name
		private string GetCity()
		{
			return char.ToUpper(Response.city.name[0]) + Response.city.name.Substring(1) + ", ";
		}

		// Returns the country name
		private string GetCountry()
		{
			return Response.city.country.ToUpper();
		}

		// Returns middays tempature from the clicked date 
		private string GetTemp()
		{
			// Get the active card
			var activeCard = AllCards.FirstOrDefault(card => card.IsActive == true);

			foreach (var item in Response.list)
			{
				DateTime date = DateTime.Parse(item.dt_txt);

				// If the active card is the first one
				// Get the lates tempature
				if (activeCard == AllCards[0])
				{
					if (date.Date == SelectedDate.Date)
					{
						int roundedTemp = Convert.ToInt32(Math.Round(item.main.temp));
						return roundedTemp.ToString() + "°";
					}
				}

				else
				{
					// Else we search for matching dates with time of day of 12:00
					if (date.Date == SelectedDate.Date && date.TimeOfDay.Hours == 12)
					{
						int roundedTemp = Convert.ToInt32(Math.Round(item.main.temp));
						return roundedTemp.ToString() + "°";
					}



				}


			}

			return "Error";
		}

		// Returns the correct icon
		private string GetIcon()
		{
			foreach (var item in AllCards)
			{
				if (item.IsActive)
				{
					return item.ImageSource;
				}
			}

			return "Error";
		}

		// Returns the correct description
		private string GetDesc()
		{
			foreach (var item in AllCards)
			{
				if (item.IsActive)
				{
					return item.Desc;
				}
			}

			return "Error";
		}

		// Returns the lates information about update time
		private string GetUpdatedDate()
		{
			return "Updated as of " + AllCards[0].FullDate;
		}


		#endregion Data fetching and binding

		#region Assisting methods

		// Returns the selected day panel
		public SelectedDayPanel GetSelectedDayPanel()
		{
			return SelectedDayPanel;
		}

		// Sets the city for day panel
		public void SetCity(string city)
		{
			City = city;
		}

		// Updates the active card and selected date to a new one
		public void UpdateActiveCard(ForecastCard activeCard)
		{
			SelectedDate = DateTime.Parse(activeCard.FullDate);
			PopulateDayPanel();
		}

		#endregion Assisting methods


	}
}
