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
			PopulateDayPanel();

		}


		private async Task InitializeAsync()
		{
			//await ForecastManager.InitializeForecast();

			PopulateDayPanel();
		}



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
			SelectedDayPanel.Date = string.Empty;
			SelectedDayPanel.FeelsLike = string.Empty;
			SelectedDayPanel.WindSpeed = string.Empty;
			SelectedDayPanel.MyVisibility = string.Empty;
			SelectedDayPanel.Timezone = string.Empty;
			SelectedDayPanel.Humidity = string.Empty;
			SelectedDayPanel.Sunset = string.Empty;
			SelectedDayPanel.Sunrise = string.Empty;

			
		}

		private string GetCity()
		{
			return char.ToUpper(Response.city.name[0]) + Response.city.name.Substring(1) + ", ";
		}

		private string GetCountry()
		{
			return Response.city.country.ToUpper();
		}

		private string GetTemp()
		{
            foreach (var item in Response.list)
            {
				DateTime date = DateTime.Parse(item.dt_txt);


                if (date.Date == SelectedDate.Date)
				{
					int roundedTemp = Convert.ToInt32(Math.Round(item.main.temp_max));
					return roundedTemp.ToString() + "°";
				}
            }

			return "Error";
        }

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

		private string GetUpdatedDate()
		{
			return "Updated as of " + AllCards[0].FullDate;
		}


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
	}
}
