using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherScanner.UI.ForecastCard;

namespace WeatherScanner.Entities.Managers
{
	public class ForecastManager
	{
		public ForecastCard[] forecastCards;

        public ForecastCard activeCard;

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
			forecastCards[0].ImageSource = "F:\\Visual Studio Enterprise 2022 projektit\\Oman ajan extra tehtäviä\\WeatherScanner\\WeatherScanner\\Icons\\sunny_icon.png";
		}

		// Updates active forecast cards
		public void SetActiveCard(ForecastCard newActiveCard)
		{
			// Change into non active if it's already active
			if (activeCard != null && activeCard != newActiveCard)
			{
				activeCard.IsActive = false;
				Debug.WriteLine("Kortti on nyt: " + activeCard.IsActive);
			}

			// Set the new active card
			activeCard = newActiveCard;
			activeCard.IsActive = true;
		}

		// Return all the cards
		public ForecastCard[] GetCards()
		{
			return forecastCards;
		}
	}
}
