using System;
using System.Collections.Generic;

public class WeatherApiResponse
{
	public double Lat { get; set; }
	public double Lon { get; set; }
	public string Timezone { get; set; }
	public int TimezoneOffset { get; set; }
	public CurrentWeather Current { get; set; }
	public List<MinutelyForecast> Minutely { get; set; }
	public List<HourlyForecast> Hourly { get; set; }
	public List<DailyForecast> Daily { get; set; }
	public List<Alert> Alerts { get; set; }
}

public class CurrentWeather
{
	public long Dt { get; set; }
	public long Sunrise { get; set; }
	public long Sunset { get; set; }
	public double Temp { get; set; }
	public double FeelsLike { get; set; }
	public List<Weather> Weather { get; set; }
}

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

public class Weather
{
	public int Id { get; set; }
	public string Main { get; set; }
	public string Description { get; set; }
	public string Icon { get; set; }
}

public class Alert
{
	public string SenderName { get; set; }
	public string Event { get; set; }
	public long Start { get; set; }
	public long End { get; set; }
	public string Description { get; set; }
	// If there are tags, you can include a List<string> Tags property here
}