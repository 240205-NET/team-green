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
		private static async Task<Weather> GetCurrentWeatherAsync(string appId, double lat, double lon, string[] exclude = null, string[] units = null, string lang = "en")
		{
			exclude ??= new string[] { "minutely", "hourly", "daily", "alerts" };
			units ??= new string[] { "standard" };

			string excludeValues = string.Join(",", exclude);
			string unitsValues = string.Join(",", units);

			string uri = $"https://api.openweathermap.org/data/3.0/onecall?lat={lat}&lon={lon}&exclude={excludeValues}&units={unitsValues}&appid={appId}&lang={lang}";
			try
			{
				string response = await client.GetStringAsync(uri);
				Weather currentWeather = JsonSerializer.Deserialize<Weather>(response);
				return currentWeather;
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
