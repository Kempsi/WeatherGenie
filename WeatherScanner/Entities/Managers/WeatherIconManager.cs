using System;
using System.Collections.Generic;
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
                {"thunderstorm with light rain", "path" },
				{"thunderstorm with rain", "path" },
				{"thunderstorm with heavy rain", "path" },
				{"light thunderstorm", "path" },
				{"thunderstorm", "path" },
				{"heavy thunderstorm", "path" },
				{"thunderstorm", "path" },
				{"ragged thunderstorm", "path" },
				{"thunderstorm with light drizzle", "path" },
				{"thunderstorm with drizzle", "path" },
				{"thunderstorm with heavy drizzle", "path" },

				// Drizzle
				{"light intensity drizzle", "path" },
				{"drizzle", "path" },
				{"heavy intensity drizzle", "path" },
				{"light intensity drizzle rain", "path" },
				{"drizzle rain", "path" },
				{"heavy intensity drizzle rain", "path" },
				{"shower rain and drizzle", "path" },
				{"heavy shower rain and drizzle", "path" },
				{"shower drizzle", "path" },

				// Rain
				{"light rain", "path" },

				// Snow
				{"light snow", "path" },

				// Atmosphere
				{"mist", "path" },

				// Clear
				{"clear sky", "path" },

				// Clouds
				{"few clouds", "path" },
				{"scattered clouds", "path" },
				{"broken clouds", "path" },
				{"overcast clouds", "path" },




			};

            
        }


    }
}
