using System;
namespace Toasted.Logic
{
	/// <summary>
	/// Represents a response object from OpenWeather API's 5 day weather forecast endpoint.
	/// <para>Includes a list of weather forecast items, city and country names, and timezone offset.</para>
	/// <seealso href="https://openweathermap.org/forecast5">OpenWeatherMap 5 day forecast</seealso>
	/// </summary>
	public class ForecastApiResponse
	{
		public ForecastList forecastList { get; set; }
		public string city { get; set; }
		public string country { get; set; }
		public int timezoneOffset { get; set; }
	}

	/// <summary>
	/// Represents a list of ForecastItem instances. Includes forecastItems (List<ForecastItem>).
	/// </summary>
	public class ForecastList
	{
		public List<ForecastItem> forecastItems { get; set; }
	}

	/// <summary>
	/// Represents a single item in a weather forecast. Includes date and time (dt) of the item, the main weather data for that item (main), and a Weather object (weather).
	/// </summary>
	public class ForecastItem
	{
		public long dt { get; set; }
		public ForecastItemMain main { get; set; }
		public Weather weather { get; set; }
	}

	/// <summary>
	/// Represents the "main" property in a ForecastItem instance. Includes the temp (double) and feelsLike (double).
	/// </summary>
	public class ForecastItemMain
	{
		public double temp { set; get; }
		public double feelsLike { get; set; }
	}

	public static class WeatherForecastProcessor
	{
		public static List<ForecastItem> NarrowDownForecasts(ForecastApiResponse response)
		{
			// Adjust timezoneOffset from seconds to hours (if your API provides offset in seconds)
			int timezoneOffsetHours = response.timezoneOffset / 3600;

			// Group by date, considering the timezone offset
			var groupedByDate = response.forecastList.forecastItems
				.GroupBy(item => DateTimeOffset.FromUnixTimeSeconds(item.dt).AddHours(timezoneOffsetHours).Date)
				.ToList();

			// Select one forecast per group/date
			var narrowedDownList = new List<ForecastItem>();
			foreach (var group in groupedByDate)
			{
				// Here, we're simply taking the first forecast of each day.
				// You can adjust the logic to select a different forecast per day if needed.
				narrowedDownList.Add(group.First());
			}

			return narrowedDownList;
		}
	}
}