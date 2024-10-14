using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WeatherScanner.Entities.Managers
{
	public class WeatherIconManager
	{
		public Dictionary<string, string> Icons { get; set; }

		public WeatherIconManager()
		{
			Icons = new Dictionary<string, string>()
			{
				// Thunderstorm
                {"thunderstorm with light rain", ConfigurationManager.AppSettings["ThunderstormIcon"] },
				{"thunderstorm with rain", ConfigurationManager.AppSettings["ThunderstormIcon"] },
				{"thunderstorm with heavy rain", ConfigurationManager.AppSettings["ThunderstormIcon"] },
				{"light thunderstorm", ConfigurationManager.AppSettings["ThunderstormIcon"] },
				{"thunderstorm", ConfigurationManager.AppSettings["ThunderstormIcon"] },
				{"heavy thunderstorm", ConfigurationManager.AppSettings["ThunderstormIcon"] },
				{"ragged thunderstorm", ConfigurationManager.AppSettings["ThunderstormIcon"] },
				{"thunderstorm with light drizzle", ConfigurationManager.AppSettings["ThunderstormIcon"] },
				{"thunderstorm with drizzle", ConfigurationManager.AppSettings["ThunderstormIcon"] },
				{"thunderstorm with heavy drizzle", ConfigurationManager.AppSettings["ThunderstormIcon"] },

				// Drizzle
				{"light intensity drizzle", ConfigurationManager.AppSettings["RainIcon"] },
				{"drizzle", ConfigurationManager.AppSettings["RainIcon"] },
				{"heavy intensity drizzle", ConfigurationManager.AppSettings["RainIcon"] },
				{"light intensity drizzle rain", ConfigurationManager.AppSettings["RainIcon"] },
				{"drizzle rain", ConfigurationManager.AppSettings["RainIcon"] },
				{"heavy intensity drizzle rain", ConfigurationManager.AppSettings["RainIcon"] },
				{"shower rain and drizzle", ConfigurationManager.AppSettings["RainIcon"] },
				{"heavy shower rain and drizzle", ConfigurationManager.AppSettings["RainIcon"] },
				{"shower drizzle", ConfigurationManager.AppSettings["RainIcon"] },

				// Rain
				{"light rain", ConfigurationManager.AppSettings["RainIcon"] },
				{"moderate rain", ConfigurationManager.AppSettings["RainIcon"] },
				{"heavy intensity rain", ConfigurationManager.AppSettings["ShowerRainIcon"] },
				{"very heavy rain", ConfigurationManager.AppSettings["ShowerRainIcon"] },
				{"extreme rain", ConfigurationManager.AppSettings["ShowerRainIcon"] },
				{"freezing rain", ConfigurationManager.AppSettings["ShowerRainIcon"] },
				{"light intensity shower rain", ConfigurationManager.AppSettings["ShowerRainIcon"] },
				{"shower rain", ConfigurationManager.AppSettings["ShowerRainIcon"] },
				{"heavy intensity shower rain", ConfigurationManager.AppSettings["ShowerRainIcon"] },
				{"ragged shower rain", ConfigurationManager.AppSettings["ShowerRainIcon"] },

				// Snow
				{"light snow", ConfigurationManager.AppSettings["SnowIcon"] },
				{"snow", ConfigurationManager.AppSettings["SnowIcon"] },
				{"heavy snow", ConfigurationManager.AppSettings["SnowIcon"] },
				{"sleet", ConfigurationManager.AppSettings["SnowIcon"] },
				{"light shower sleet", ConfigurationManager.AppSettings["SnowIcon"] },
				{"shower sleet", ConfigurationManager.AppSettings["SnowIcon"] },
				{"light rain and snow", ConfigurationManager.AppSettings["SnowIcon"] },
				{"rain and snow", ConfigurationManager.AppSettings["SnowIcon"] },
				{"light shower snow", ConfigurationManager.AppSettings["SnowIcon"] },
				{"shower snow", ConfigurationManager.AppSettings["SnowIcon"] },
				{"heavy shower snow", ConfigurationManager.AppSettings["SnowIcon"] },
				

				// Atmosphere
				{"mist", ConfigurationManager.AppSettings["MistIcon"] },
				{"smoke", ConfigurationManager.AppSettings["MistIcon"] },
				{"haze", ConfigurationManager.AppSettings["MistIcon"] },
				{"sand/dust whirls", ConfigurationManager.AppSettings["ScatteredCloudsIcon"] },
				{"fog", ConfigurationManager.AppSettings["MistIcon"] },
				{"sand", ConfigurationManager.AppSettings["ScatteredCloudsIcon"] },
				{"dust", ConfigurationManager.AppSettings["ScatteredCloudsIcon"] },
				{"volcanic ash", ConfigurationManager.AppSettings["ScatteredCloudsIcon"] },
				{"squalls", ConfigurationManager.AppSettings["ScatteredCloudsIcon"] },
				{"tornado", ConfigurationManager.AppSettings["ScatteredCloudsIcon"] },

				// Clear
				{"clear sky", ConfigurationManager.AppSettings["ClearIcon"] },

				// Clouds
				{"few clouds", ConfigurationManager.AppSettings["FewCloudsIcon"] },
				{"scattered clouds", ConfigurationManager.AppSettings["ScatteredCloudsIcon"] },
				{"broken clouds", ConfigurationManager.AppSettings["BrokenCloudsIcon"] },
				{"overcast clouds", ConfigurationManager.AppSettings["OvercastCloudsIcon"] },


			};


		}

		public Dictionary<string, string> GetWeatherIcons()
		{
			return Icons;
		}


	}
}
