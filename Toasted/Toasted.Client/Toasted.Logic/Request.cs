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

		public static async Task<ForecastApiResponse> GetForecastAsync(string appId, double? lat, double? lon, string[] units = null, string lang = "en")
		{
			units ??= new string[] { "imperial" };
			string unitsValues = string.Join(",", units);
			string uri = $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&units={unitsValues}&lang={lang}&appid={appId}";
			try
			{
				string response = await client.GetStringAsync(uri);
				using JsonDocument doc = JsonDocument.Parse(response);
				JsonElement root = doc.RootElement;

				var forecastApiResponse = new ForecastApiResponse
				{
					city = root.GetProperty("city").GetProperty("name").ToString(),
					country = root.GetProperty("city").GetProperty("country").ToString(),
					timezoneOffset = Int32.Parse(root.GetProperty("city").GetProperty("timezone").ToString()),
					forecastList = new ForecastList
					{
						forecastItems = new List<ForecastItem>(),

					}
				};

				foreach (var item in root.GetProperty("list").EnumerateArray())
				{
					var forecastItem = new ForecastItem
					{
						dt = item.GetProperty("dt").GetInt64(),
						main = new ForecastItemMain
						{
							temp = item.GetProperty("main").GetProperty("temp").GetDouble(),
							feelsLike = item.GetProperty("main").GetProperty("feels_like").GetDouble()
						},
						weather = new Weather
						{
							id = item.GetProperty("weather")[0].GetProperty("id").GetInt32(),
							main = item.GetProperty("weather")[0].GetProperty("main").GetString(),
							Description = item.GetProperty("weather")[0].GetProperty("description").GetString(),
							Icon = item.GetProperty("weather")[0].GetProperty("icon").GetString(),
						}
					};
					forecastApiResponse.forecastList.forecastItems.Add(forecastItem);
				}
				return forecastApiResponse;
			}
			catch (HttpRequestException e)
			{
				Console.WriteLine(e.Message);
			}
			return null;
		}

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
						Dt = root.GetProperty("dt").GetInt64(),
						Sunrise = root.GetProperty("sys").GetProperty("sunrise").GetInt64(),
						Sunset = root.GetProperty("sys").GetProperty("sunset").GetInt64(),
						Temp = root.GetProperty("main").GetProperty("temp").GetDouble(),
						FeelsLike = root.GetProperty("main").GetProperty("feels_like").GetDouble(),
						Weather = new Weather()
						{
							id = root.GetProperty("weather")[0].GetProperty("id").GetInt32(),
							main = root.GetProperty("weather")[0].GetProperty("main").GetString(),
							Description = root.GetProperty("weather")[0].GetProperty("description").GetString(),
							Icon = root.GetProperty("weather")[0].GetProperty("icon").GetString(),
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