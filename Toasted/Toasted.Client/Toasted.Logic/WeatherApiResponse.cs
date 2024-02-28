using System;
using System.Collections.Generic;
using System.Globalization;

namespace Toasted.Logic
{

	public class WeatherApiResponse
	{
		public double? lat { get; set; }
		public double? lon { get; set; }
		public int timezone { get; set; }
		public string name { get; set; }
		public int timezoneOffset { get; set; }
		public CurrentWeather current { get; set; }

	}

	public class CurrentWeather
	{
		public long dt { get; set; }
		public long sunrise { get; set; }
		public long sunset { get; set; }
		public double temp { get; set; }
		public double feelsLike { get; set; }
		public Weather weather { get; set; }
	}

	public class Weather
	{
		public int id { get; set; }
		public string main { get; set; }
		public string description { get; set; }
		public string icon { get; set; }
	}
}