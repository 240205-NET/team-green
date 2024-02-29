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
			int timezoneOffsetHours = response.timezoneOffset / 3600;
			var forecastsInLocalTime = response.forecastList.forecastItems
				.Select(item => new
				{
					Forecast = item,
					LocalDateTime = DateTimeOffset.FromUnixTimeSeconds(item.dt).AddHours(timezoneOffsetHours)
				})
				.ToList();

			var groupedByLocalDate = forecastsInLocalTime
				.GroupBy(item => item.LocalDateTime.Date)
				.ToList();

			var narrowedDownList = new List<ForecastItem>();

			// Process only the first five groups (days)
			foreach (var group in groupedByLocalDate.Take(5))
			{
				var now = DateTimeOffset.UtcNow.AddHours(timezoneOffsetHours);
				var today = now.Date;
				var groupDate = group.Key;

				ForecastItem selectedForecast = null;

				// For today, if it's after 1 PM, select the next available forecast.
				// For other days or today before 1 PM, aim for around noon.
				if (groupDate == today && now.Hour >= 13)
				{
					selectedForecast = group.FirstOrDefault(item => item.LocalDateTime.Hour > now.Hour)?.Forecast;
				}
				else
				{
					// Aim for the forecast closest to noon or the first available after noon
					var targetHour = groupDate == today && now.Hour >= 1 ? 13 : 12; // Use 13 (1 PM) if we're past 1 AM today, otherwise aim for 12
					selectedForecast = group.OrderBy(item => Math.Abs(item.LocalDateTime.Hour - targetHour))
											.ThenBy(item => item.LocalDateTime.Hour >= targetHour)
											.FirstOrDefault()?.Forecast;
				}

				if (selectedForecast != null)
				{
					narrowedDownList.Add(selectedForecast);
				}
			}
			return narrowedDownList;
		}
	}
}