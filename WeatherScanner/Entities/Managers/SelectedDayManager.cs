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
			SelectedDayPanel.FeelsLike = GetFeelsLike();
			SelectedDayPanel.WindSpeed = GetWindSpeed();
			SelectedDayPanel.MyVisibility = GetVisibility();
			SelectedDayPanel.Timezone = GetTimeZone();
			SelectedDayPanel.Humidity = GetHumidity();
			SelectedDayPanel.Sunset = GetSunset();
			SelectedDayPanel.Sunrise = GetSunrise();
			
		}

		#endregion Day panel population

		#region Data fetching and binding

		// Returns the city name
		private string GetCity()
		{
			if (Response.city.name == "Globe")
			{
				return "Globe";
			}

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

		// Returns middays feels like tempature from the clicked date 
		private string GetFeelsLike()
		{
			// Get the active card
			var activeCard = AllCards.FirstOrDefault(card => card.IsActive == true);

			foreach (var item in Response.list)
			{
				DateTime date = DateTime.Parse(item.dt_txt);

				// If the active card is the first one
				// Get the lates feels like temp
				if (activeCard == AllCards[0])
				{
					if (date.Date == SelectedDate.Date)
					{
						int roundedTemp = Convert.ToInt32(Math.Round(item.main.feels_like));
						return "Feels like " + roundedTemp.ToString() + "°";
					}
				}

				else
				{
					// Else we search for matching dates with time of day of 12:00
					if (date.Date == SelectedDate.Date && date.TimeOfDay.Hours == 12)
					{
						int roundedTemp = Convert.ToInt32(Math.Round(item.main.feels_like));
						return "Feels like " + roundedTemp.ToString() + "°";
					}



				}


			}

			return "Error";
		}

		// Returns middays windspeed from the clicked date 
		private string GetWindSpeed()
		{
			// Get the active card
			var activeCard = AllCards.FirstOrDefault(card => card.IsActive == true);

			foreach (var item in Response.list)
			{
				DateTime date = DateTime.Parse(item.dt_txt);

				// If the active card is the first one
				// Get the lates windspeed
				if (activeCard == AllCards[0])
				{
					if (date.Date == SelectedDate.Date)
					{
						int roundedSpeed = Convert.ToInt32(Math.Round(item.wind.speed));
						return "Wind " + roundedSpeed.ToString() + "km/h";
					}
				}

				else
				{
					// Else we search for matching dates with time of day of 12:00
					if (date.Date == SelectedDate.Date && date.TimeOfDay.Hours == 12)
					{
						int roundedSpeed = Convert.ToInt32(Math.Round(item.wind.speed));
						return "Wind " + roundedSpeed.ToString() + "km/h";
					}



				}


			}

			return "Error";
		}

		// Returns middays visibility from the clicked date
		private string GetVisibility()
		{
			// Get the active card
			var activeCard = AllCards.FirstOrDefault(card => card.IsActive == true);

			foreach (var item in Response.list)
			{
				DateTime date = DateTime.Parse(item.dt_txt);

				// If the active card is the first one
				// Get the lates visibility
				if (activeCard == AllCards[0])
				{
					if (date.Date == SelectedDate.Date)
					{
						var visibilityKM = item.visibility / 1000;

						return visibilityKM.ToString() + "km";
					}
				}

				else
				{
					// Else we search for matching dates with time of day of 12:00
					if (date.Date == SelectedDate.Date && date.TimeOfDay.Hours == 12)
					{
						var visibilityKM = item.visibility / 1000;

						return visibilityKM.ToString() + "km";
					}



				}


			}

			return "Error";
		}

		// Returns timezone offset in UTC
		private string GetTimeZone()
		{
			// Offset from the specific city
			var timezoneOffSet = Response.city.timezone;
			var offSet = TimeSpan.FromSeconds(timezoneOffSet);

			// Return positive offset
			if (offSet.Hours > 0)
			{
				string offSetString = offSet.ToString().Remove(5);
				return "Timezone +" + offSetString + " UTC";
			}

			// Else we return negative offset
			else
			{
				string offSetString = offSet.ToString().Remove(6);
				return "Timezone " + offSetString + " UTC";
			}

			
		}

		// Returns middays humidity from the clicked date
		private string GetHumidity()
		{
			// Get the active card
			var activeCard = AllCards.FirstOrDefault(card => card.IsActive == true);

			foreach (var item in Response.list)
			{
				DateTime date = DateTime.Parse(item.dt_txt);

				// If the active card is the first one
				// Get the lates humidity
				if (activeCard == AllCards[0])
				{
					if (date.Date == SelectedDate.Date)
					{
						return "Humidity " + item.main.humidity.ToString() + "%";
					}
				}

				else
				{
					// Else we search for matching dates with time of day of 12:00
					if (date.Date == SelectedDate.Date && date.TimeOfDay.Hours == 12)
					{
						return "Humidity " + item.main.humidity.ToString() + "%";
					}



				}


			}

			return "Error";
		}

		// Returns sunset in the selected city
		private string GetSunset()
		{
			var unixTime = Response.city.sunset;
			var offSet = DateTimeOffset.FromUnixTimeSeconds(unixTime);

			var timeZoneOffset = TimeSpan.FromSeconds(Response.city.timezone);

			var cityLocalTime = offSet.ToOffset(timeZoneOffset);

			return "Sunset " + cityLocalTime.ToString("HH:mm");
		}

		// Returns sunrise in the selected city
		private string GetSunrise()
		{
			var unixTime = Response.city.sunrise;
			var offSet = DateTimeOffset.FromUnixTimeSeconds(unixTime);

			var timeZoneOffset = TimeSpan.FromSeconds(Response.city.timezone);

			var cityLocalTime = offSet.ToOffset(timeZoneOffset);

			return "Sunrise " + cityLocalTime.ToString("HH:mm");
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
