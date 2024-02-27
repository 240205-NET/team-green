using System.Text.Json;


namespace Toasted.Logic
{
	public static class Request
	{
		private static readonly HttpClient client = new HttpClient();
		// public static void LogIn()
		// {
		// }

		// public static void Register()
		// {
		// }
		/*
		public static async Task<WeatherApiResponse> GetCurrentWeatherAsync(string appId, double? lat, double? lon, string[] exclude = null, string[] units = null, string lang = "en")
		{
			exclude ??= new string[] { "minutely", "hourly", "daily", "alerts" };
			units ??= new string[] { "imperial" };

			string excludeValues = string.Join(",", exclude);
			string unitsValues = string.Join(",", units);

			string uri = $"https://api.openweathermap.org/data/2.0/onecall?lat={lat}&lon={lon}&exclude={excludeValues}&units={unitsValues}&appid={appId}&lang={lang}";
			try
			{
				string response = await client.GetStringAsync(uri);

				using JsonDocument doc = JsonDocument.Parse(response);
				JsonElement root = doc.RootElement;

				WeatherApiResponse weatherApiResponse = new WeatherApiResponse()
				{
					lat = root.GetProperty("lat").GetDouble(),
					lon = root.GetProperty("lon").GetDouble(),
					timezone = root.GetProperty("timezone").GetString(),
					current = new CurrentWeather()
					{
						dt = root.GetProperty("dt").GetInt64(),
						sunrise = root.GetProperty("sunrise").GetInt64(),
						sunset = root.GetProperty("sunset").GetInt64(),
						temp = root.GetProperty("temp").GetDouble(),
						feelsLike = root.GetProperty("feels_like").GetDouble(),
						weather = new Weather()
						{
							id = Int32.Parse(root.GetProperty("id").GetString()),
							main = root.GetProperty("main").GetString(),
							description = root.GetProperty("description").GetString(),
							icon = root.GetProperty("icon").GetString(),

						}
					}
				};
				return weatherApiResponse;
			}
			catch (HttpRequestException e)
			{
				Console.WriteLine(e.Message);
			}
			return null;
		}
*/

		public static async Task<WeatherApiResponse> GetCurrentWeatherAsync(string appId, double? lat, double? lon, string[] units = null, string lang = "en")
		{
			units ??= new string[] { "imperial" };

			string unitsValues = string.Join(",", units);

			string uri = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units={units[0]}&lang={lang}&appid={appId}";
			try
			{
				string response = await client.GetStringAsync(uri);

				using JsonDocument doc = JsonDocument.Parse(response);
				JsonElement root = doc.RootElement;

				WeatherApiResponse weatherApiResponse = new WeatherApiResponse()
				{
					lat = root.GetProperty("coord").GetProperty("lat").GetDouble(),
					lon = root.GetProperty("coord").GetProperty("lon").GetDouble(),
					timezone = root.GetProperty("timezone").GetInt32(),
					name = root.GetProperty("name").GetString(),
					current = new CurrentWeather()
					{
						dt = root.GetProperty("dt").GetInt64(),
						sunrise = root.GetProperty("sys").GetProperty("sunrise").GetInt64(),
						sunset = root.GetProperty("sys").GetProperty("sunset").GetInt64(),
						temp = root.GetProperty("main").GetProperty("temp").GetDouble(),
						feelsLike = root.GetProperty("main").GetProperty("feels_like").GetDouble(),
						weather = new Weather()
						{
							id = root.GetProperty("weather")[0].GetProperty("id").GetInt32(),
							main = root.GetProperty("weather")[0].GetProperty("main").GetString(),
							description = root.GetProperty("weather")[0].GetProperty("description").GetString(),
							icon = root.GetProperty("weather")[0].GetProperty("icon").GetString(),
						}
					}
				};
				return weatherApiResponse;
			}
			catch (HttpRequestException e)
			{
				Console.WriteLine(e.Message);
			}
			return null;
		}
		public static async Task<Location?> GetLocation(string appId, string zip, string countryCode)
		{
			string uri = $"http://api.openweathermap.org/geo/1.0/zip?zip={zip},{countryCode}&appid={appId}";

			try
			{
				string response = await client.GetStringAsync(uri);
				using JsonDocument doc = JsonDocument.Parse(response);
				JsonElement root = doc.RootElement;

				Location location = new Location
				{
					zip = Int32.Parse(root.GetProperty("zip").GetString()),
					name = root.GetProperty("name").GetString(),
					lat = root.GetProperty("lat").GetDouble(),
					lon = root.GetProperty("lon").GetDouble(),
					country = root.GetProperty("country").GetString()
				};
				return location;
			}
			catch (HttpRequestException e)
			{
				Console.WriteLine($"HTTP request error: {e.Message}");
			}
			catch (JsonException e)
			{
				Console.WriteLine($"JSON deserialization error: {e.Message}");
			}
			catch (Exception e)
			{
				Console.WriteLine($"General error: {e.Message}");
			}
			return null;
		}
	}
}
