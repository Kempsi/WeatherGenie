using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WeatherScanner.Entities.Forecast;
using WeatherScanner.Entities.Services;
using WeatherScanner.UI.ForecastCard;

namespace WeatherScanner.Entities.Managers
{
	public class ForecastManager
	{
		public ForecastCard[] forecastCards;

		private ForecastCard activeCard;

		private WeatherAPI apiCaller;
		private GeoCoderAPI geocoder;

		private ForecastResponse response;

		private string city = "Joensuu";

		public ForecastManager()
		{
			forecastCards = new ForecastCard[5];

			for (int i = 0; i < 5; i++)
			{
				ForecastCard card = new ForecastCard();
				forecastCards[i] = card;

			}

			

			SetUpForecast();

		}

		public async Task SetUpForecast()
		{
			response = await GetForecastResponse();
			PopulateCards();
		}

		// Updates active forecast cards
		public void SetActiveCard(ForecastCard newActiveCard)
		{
			// Change into non active if it's already active
			if (activeCard != null && activeCard != newActiveCard)
			{
				activeCard.IsActive = false;
			}

			// Set the new active card
			activeCard = newActiveCard;
			activeCard.IsActive = true;
		}

		public async Task<ForecastResponse> GetForecastResponse()
		{
			apiCaller = new WeatherAPI();
			geocoder = new GeoCoderAPI();


			var cords = await geocoder.GetCords(city);

			response = await apiCaller.Get5DayWeather(cords.Lat, cords.Lon);

			return response;

		}

		private string GetShortDayOfTheWeek(DateTime currentDate, int addDay)
		{

			var day = currentDate.AddDays(addDay);

			var shortDay = day.DayOfWeek.ToString();

			shortDay = shortDay.Remove(3);
			return shortDay;
		}

		private string GetShortDate(DateTime currentDate, int addDay)
		{
			var dateTime = currentDate.AddDays(addDay);

			var shortDate = dateTime.Date.ToShortDateString();

			shortDate = shortDate.Remove(2);

			return shortDate;
		}

		private string GetTempHigh(DateTime currentDate, int addDay)
		{
			var wantedDate = currentDate.AddDays(addDay);
			var temps = new List<int>();

			foreach (var item in response.list)
			{
				var itemDate = DateTime.Parse(item.dt_txt);

				if (itemDate.Date == wantedDate.Date)
				{
					int roundedTemp = Convert.ToInt32(Math.Round(item.main.temp_max));
					temps.Add(roundedTemp);
				}
			}

			if (temps.Any())
			{
				return temps.Max().ToString() + "°";
			}

			return string.Empty;
		}

		private string GetTempLow(DateTime currentDate, int addDay)
		{
			var wantedDate = currentDate.AddDays(addDay);
			var temps = new List<int>();
		
			foreach (var item in response.list)
			{
				var itemDate = DateTime.Parse(item.dt_txt);

				if (itemDate.Date == wantedDate.Date)
				{	
					int roundedTemp = Convert.ToInt32(Math.Round(item.main.temp_min));	
					temps.Add(roundedTemp);
				}
			}
	
			if (temps.Any())
			{
				return temps.Min().ToString() + "°";
			}

			return string.Empty;
		}

		private string GetDescription(DateTime currentDate, int addDay)
		{
			var wantedDate = currentDate.AddDays(addDay);
			var descriptions = new List<string>();

			foreach (var item in response.list)
			{
				var itemDate = DateTime.Parse(item.dt_txt);

				if (itemDate.Date == wantedDate.Date)
				{
					foreach (var weather in item.weather)
					{
						descriptions.Add(weather.description);
						Debug.WriteLine(weather.description);
					}
				}
			}

			if (wantedDate.Day == DateTime.Now.Day)
			{
				var closestDesc = descriptions[0];
				closestDesc = char.ToUpper(closestDesc[0]) + closestDesc.Substring(1);
				return closestDesc;
			}

			else
			{
				var mostFrequent = descriptions.GroupBy(x => x).OrderByDescending(x => x.Key).FirstOrDefault()?.Key;
				mostFrequent = char.ToUpper(mostFrequent[0]) + mostFrequent.Substring(1);
				return mostFrequent;
			}
		}

		private void PopulateCards()
		{
			
			for (int i = 0; i < forecastCards.Length; i++)
			{
				forecastCards[i].Day = GetShortDayOfTheWeek(DateTime.Now, i);
				forecastCards[i].Date = GetShortDate(DateTime.Now, i);
				forecastCards[i].TempHigh = GetTempHigh(DateTime.Now, i);
				forecastCards[i].TempLow = GetTempLow(DateTime.Now, i);
				forecastCards[i].Desc = GetDescription(DateTime.Now, i);
			}

		}


		// Returns all the cards
		public ForecastCard[] GetCards()
		{
			return forecastCards;
		}
	}
}
