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

		private string city = "Joensuu";

		public ForecastManager()
		{
			forecastCards = new ForecastCard[5];


			for (int i = 0; i < 5; i++)
			{
				ForecastCard card = new ForecastCard();
				forecastCards[i] = card;

			}

			// Test data
			forecastCards[0].TempHigh = 25;
			forecastCards[0].TempLow = 5;
			forecastCards[0].Day = "Fri";
			forecastCards[0].Date = 31;
			forecastCards[0].Desc = "A bit chilly";
			forecastCards[0].ImageSource = ConfigurationManager.AppSettings["SunnyIcon"];

			GetForecastResponse();
			PopulateCard(0);
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

			var forecastResponse = await apiCaller.Get5DayWeather(cords.Lat, cords.Lon);

			return forecastResponse;

		}

		private string GetShortDayOfTheWeek(DateTime currentDate, int addDay)
		{

			var day = currentDate.AddDays(addDay);

			var shortDay = day.DayOfWeek.ToString();

			shortDay = shortDay.Remove(3);
			return shortDay;
		}

		private int GetShortDate(DateTime currentDate, int addDay)
		{
			var dateTime = currentDate.AddDays(addDay);

			var shortDate = dateTime.Date.ToShortDateString();

			shortDate = shortDate.Remove(2);

			var dayDate = int.Parse(shortDate);

			return dayDate;
		}

		private void PopulateCard(int index)
		{
			

			for (int i = 0; i < forecastCards.Length; i++)
			{
				forecastCards[i].Day = GetShortDayOfTheWeek(DateTime.Now, i);
				forecastCards[i].Date = GetShortDate(DateTime.Now, i);
			}




		}


		// Returns all the cards
		public ForecastCard[] GetCards()
		{
			return forecastCards;
		}
	}
}
