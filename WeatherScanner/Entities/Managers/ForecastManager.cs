using WeatherScanner.Entities.Forecast;
using WeatherScanner.Entities.Services;
using WeatherScanner.UI.ForecastCard;

namespace WeatherScanner.Entities.Managers
{
	public class ForecastManager
	{
		public ForecastCard[] forecastCards = new ForecastCard[5];

		private ForecastCard activeCard;

		private WeatherAPI apiCaller;
		private GeoCoderAPI geocoder;
		private WeatherIconManager iconManager = new WeatherIconManager();

		private ForecastResponse response;

		// Idk, if i set this to nothing, it dont work
		// So now this stays as it is :)
		private string city = "New York";

		public ForecastManager()
		{
			InitializeForecastCards();
			InitializeForecast();

			// Set the first card as an active card
			SetActiveCard(forecastCards[0]);
		}


		#region Initializing actions


		// Creates forecastcards for the next 5 days
		private void InitializeForecastCards()
		{
			for (int i = 0; i < 5; i++)
			{
				ForecastCard card = new ForecastCard();
				forecastCards[i] = card;
			}

			
		}


		// Initializes the response to a variable and binds data to the cards
		public async Task InitializeForecast()
		{
			response = await GetForecastResponse();
			ConfigureForecastCards();
		}


		#endregion Initializing actions

		#region Active card highlighting


		public event Action<ForecastCard> ActiveCardChanged;

		// Updates active forecast cards
		public void SetActiveCard(ForecastCard newActiveCard)
		{
			// Change into non active if it's already active
			if (activeCard != null && activeCard != newActiveCard)
			{
				activeCard.IsActive = false;
			}

			// If new card is not active
			if (!newActiveCard.IsActive)
			{		
				activeCard = newActiveCard;
				activeCard.IsActive = true;

				ActiveCardChanged?.Invoke(activeCard);
			}

		}

		#endregion Active card highlighting

		#region Data fetching and binding


		// Makes an API request to fetch forecast data
		public async Task<ForecastResponse> GetForecastResponse()
		{
			apiCaller = new WeatherAPI();
			geocoder = new GeoCoderAPI();

			var cords = await geocoder.GetCords(city);

			response = await apiCaller.GetForecast(cords.Lat, cords.Lon);

			return response;
		}


		// Populates all of the forecast cards with information from API response
		private void ConfigureForecastCards()
		{
			for (int i = 0; i < forecastCards.Length; i++)
			{
				forecastCards[i].Day = GetDayOfTheWeek(DateTime.Now, i);
				forecastCards[i].Date = GetShortDate(DateTime.Now, i);
				forecastCards[i].TempHigh = GetTempHigh(DateTime.Now, i);
				forecastCards[i].TempLow = GetTempLow(DateTime.Now, i);
				forecastCards[i].Desc = GetDescription(DateTime.Now, i);
				forecastCards[i].ImageSource = GetMatchingWeatherIcon(forecastCards[i]);

				forecastCards[i].FullDate = GetFullDate(DateTime.Now, i);
			}
		}


		// Gets the day of the week from the wanted date
		private string GetDayOfTheWeek(DateTime currentDate, int addDay)
		{
			// Date, we want the day of the week from
			var wantedDate = currentDate.AddDays(addDay);


			// Shorten it and return it
			return wantedDate.DayOfWeek.ToString();
		}


		// Gets the shortened date from the wanted date
		private string GetShortDate(DateTime currentDate, int addDay)
		{
			// Date, we want the day of the week from
			var wantedDate = currentDate.AddDays(addDay);

			// Convert it to short mode
			var shortDate = wantedDate.Date.ToShortDateString();

			// Return only the first two characters
			return shortDate.Remove(2);

		}

		// Gets the shortened date from the wanted date
		private string GetFullDate(DateTime currentDate, int addDay)
		{
			var wantedDate = currentDate.AddDays(addDay);

			foreach (var item in response.list)
			{
				DateTime date = DateTime.Parse(item.dt_txt);

				if (date.Date == wantedDate.Date)
				{
					return date.ToString("dd:MM:yyyy HH:mm");
				}
			}
			return "Error";

		}


		// Gets highest tempature information from API response
		private string GetTempHigh(DateTime currentDate, int addDay)
		{
			// Date, that we are getting the highest temps from
			var wantedDate = currentDate.AddDays(addDay);

			// List for all of the temps
			var temps = new List<int>();

			// Go through all of the items in response.list
			foreach (var item in response.list)
			{
				// Convert the dt_txt to DateTime
				var itemDate = DateTime.Parse(item.dt_txt);

				// If we found the wanted date:
				if (itemDate.Date == wantedDate.Date)
				{
					// Round up temp and add it to the list
					int roundedTemp = Convert.ToInt32(Math.Round(item.main.temp_max));
					temps.Add(roundedTemp);
				}
			}

			// If the list isn't empty, return the highest temp
			if (temps.Any())
			{
				return temps.Max().ToString() + "°";
			}

			// Else, return error
			return "Ex.";
		}


		// Gets lowest tempature information from API response
		private string GetTempLow(DateTime currentDate, int addDay)
		{
			// Date, that we are getting the lowest temps from
			var wantedDate = currentDate.AddDays(addDay);

			// List for all of the temps
			var temps = new List<int>();

			// Go through all of the items in response.list
			foreach (var item in response.list)
			{
				// Convert the dt_txt to DateTime
				var itemDate = DateTime.Parse(item.dt_txt);

				// If we found the wanted date:
				if (itemDate.Date == wantedDate.Date)
				{
					// Round up temp and add it to the list
					int roundedTemp = Convert.ToInt32(Math.Round(item.main.temp_min));
					temps.Add(roundedTemp);
				}
			}

			// If the list isn't empty, return the lowest temp
			if (temps.Any())
			{
				return temps.Min().ToString() + "°";
			}

			// Else, return error
			return "Ex.";
		}


		// Gets description information from API response
		private string GetDescription(DateTime currentDate, int addDay)
		{
			// Date, that we are getting description for
			var wantedDate = currentDate.AddDays(addDay);

			// List for all descriptions
			var descriptions = new List<string>();

			// Go through all of the API response.list collection (3 hour forecast)
			foreach (var item in response.list)
			{
				// Get current items DateTime
				var itemDate = DateTime.Parse(item.dt_txt);

				// If it matches with the wanted date, go through the actual wanted
				// dates weather collection, which includes all of the descriptions
				if (itemDate.Date == wantedDate.Date)
				{
					foreach (var weather in item.weather)
					{
						// Add them to the list
						descriptions.Add(weather.description);
					}
				}
			}

			// If its current day, return the first description (closest to the actual hour of time)
			if (wantedDate.Day == DateTime.Now.Day)
			{
				var closestDesc = descriptions[0];
				closestDesc = char.ToUpper(closestDesc[0]) + closestDesc.Substring(1);
				return closestDesc;
			}

			// Else return the most frequently appearing one of the other 4 days of the week
			else
			{
				var mostFrequent = descriptions.GroupBy(x => x).OrderByDescending(x => x.Key).FirstOrDefault()?.Key;
				mostFrequent = char.ToUpper(mostFrequent[0]) + mostFrequent.Substring(1);
				return mostFrequent;
			}
		}


		#endregion Data fetching and binding

		#region Weather icon status logic

		// Returns correct matching weather icon patch, depending on weather description
		private string GetMatchingWeatherIcon(ForecastCard card)
		{
			var weatherIcons  = iconManager.GetWeatherIcons();

            foreach (var icon in weatherIcons)
            {
                if (icon.Key == card.Desc.ToLower())
				{
					return icon.Value;
				}
            }

			return "Error";
        }

		#endregion Weather icon status logic

		#region Other

		// Returns all the cards
		public ForecastCard[] GetCards()
		{
			return forecastCards;
		}

		// Returns the active card
		public ForecastCard GetActiveCard()
		{
			return activeCard;
		}

		// Returns the response
		public ForecastResponse GetResponse()
		{
			return response;
		}

		// Sets the city
		public void SetCity(string city)
		{
			this.city = city;
		}

		#endregion Other












	}
}
