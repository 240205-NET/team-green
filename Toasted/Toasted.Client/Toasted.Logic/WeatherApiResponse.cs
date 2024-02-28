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
		public long Dt { get; set; }
		public long Sunrise { get; set; }
		public long Sunset { get; set; }
		public double Temp { get; set; }
		public double FeelsLike { get; set; }
		public Weather Weather { get; set; }
	}

	public class Weather
	{
		public int id { get; set; }
		public string main { get; set; }
		public string Description { get; set; }
		public string Icon { get; set; }
	}
}