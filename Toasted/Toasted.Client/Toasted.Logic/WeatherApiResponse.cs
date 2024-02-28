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
		/*
		public List<MinutelyForecast> minutely { get; set; }
		public List<HourlyForecast> hourly { get; set; }
		public List<DailyForecast> daily { get; set; }
		public List<Alert> Alerts { get; set; }
		*/
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
	/*
	public class MinutelyForecast
	{
		public long Dt { get; set; }
		public double Precipitation { get; set; }
	}

	public class HourlyForecast
	{
		public long Dt { get; set; }
		public double Temp { get; set; }
		// Other properties...
		public List<Weather> Weather { get; set; }
	}

	public class DailyForecast
	{
		public long Dt { get; set; }
		public long Sunrise { get; set; }
		public long Sunset { get; set; }
		// Other properties...
		public Temperature Temp { get; set; }
		public FeelsLike FeelsLike { get; set; }
		public List<Weather> Weather { get; set; }
	}
	*/
	/*
	public class Temperature
	{
		public double Day { get; set; }
		public double Min { get; set; }
		public double Max { get; set; }
		// Other properties...
	}

	public class FeelsLike
	{
		public double Day { get; set; }
		public double Night { get; set; }
		// Other properties...
	}
	*/

	/*
	public class Alert
	{
		public string SenderName { get; set; }
		public string Event { get; set; }
		public long Start { get; set; }
		public long End { get; set; }
		public string Description { get; set; }
		// If there are tags, you can include a List<string> Tags property here
	}
	*/
}