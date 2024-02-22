namespace Toasted.App
{
	public static class Request
	{
		/*
		public static LogIn()
		{

		};

		public static Register()
		{

		};
		*/
		/* TODO
		- Add API Key
		*/

		// https://api.openweathermap.org/data/3.0/onecall?lat={lat}&lon={lon}&exclude={part}&appid={API key}
		/*
		private static async GetCurrentWeatherAsync(double lat,
													double lon,
													string[] exclude = new string { "minutely", "hourly", "daily", "alerts" },
													string[] units = new string { "standard" },
													string appId,
													string lang = "en")
		{
			HttpClient client = new HttpClient;
			// Turn the 'exclude' array into a comma-delimited string, i.e "minutely,hourly,daily,alerts". If there's only one value in the array, then just return that value (exclude[0]) since there's no reason to delimit one word.
			string excludeValues = exclude.Length == 1 ? exclude[0] : exclude.Join(",", excludes);
			// The API specifies that the default value is "standard"; There's likely no reason to change it. 
			string unitsValues = units.Length == 1 ? units[0] : units.Join(",", units);
			string uri = $"https://api.openweathermap.org/data/3.0/onecall?lat={lat}&lon={lon}&exclude={excludeValues}&units={unitValues}&appid={appId}&lang={lang}"
			string response = await client.GetStringAsync(uri);
			// Still need to implement "Weather" class...
			Weather currentWeather = JsonSerializer.Deserialize<Weather>(response);
			return currentWeather;
		}

		private static async GetLocation(int zip, string countryCode, string appId)
		{
			HttpClient client = new HttpClient;
			string uri = $"http://api.openweathermap.org/geo/1.0/zip?zip={zip},{countryCode}&appid={appId}"
			string response = await client.GetStringAsync(uri);
			Location location = JsonSerializer.Deserialize<Location>(response);
			return location;
		}
		*/
	}

}