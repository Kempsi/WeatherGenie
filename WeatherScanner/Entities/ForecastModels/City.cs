using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherScanner.Entities.Forecast
{
	public class City
	{
		public int id { get; set; }
		public string name { get; set; }
		public Coord coord { get; set; }
		public string country { get; set; }
		public int population { get; set; }
		public int timezone { get; set; }
		public long sunrise { get; set; }
		public long sunset { get; set; }
	}
}
