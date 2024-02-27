using System;
namespace Toasted.Logic
{
	public class ForecastApiResponse
	{
		public ForecastList forecastList { get; set; }
	}

	public class ForecastList
	{
		List<ForecastItem> forecastItems { get; set; }
	}

	public class ForecastItem
	{
		public long dt { get; set; }
		public double feelsLike { get; set; }
		public Weather weather { get; set; }

	}
}