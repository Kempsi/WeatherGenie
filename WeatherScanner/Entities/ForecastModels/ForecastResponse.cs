using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherScanner.Entities.Forecast
{
	public class ForecastResponse
	{
		public string cod { get; set; }
		public int message { get; set; }
		public int cnt { get; set; }
		public List<Forecast> list { get; set; }
		public City city { get; set; }

	}
}
