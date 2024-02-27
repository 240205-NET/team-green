using System;
namespace Toasted.Logic
{
	public class ForecastApiResponse
	{
		public ForecastList forecastList { get; set; }
	}

	public class ForecastList
	{
		public List<ForecastItem> forecastItems { get; set; }
	}

	/// <summary>
	/// Represents a single item in a weather forecast.
	/// Includes date and time (dt) of the item,
	/// the main weather data for that item (main),
	/// and a Weather object (weather)
	/// </summary>
	public class ForecastItem
	{
		public long dt { get; set; }
		public ForecastItemMain main { get; set; }
		public Weather weather { get; set; }
	}

	public class ForecastItemMain
	{
		public double temp { set; get; }
		public double feelsLike { get; set; }
	}
}