using System;
using System.Text;
using Toasted.Logic;
using System.Globalization;

namespace Toasted.App
{
	public static class Menu
	{
		public static string currentView { get; set; } = "Main Menu";

		public static void GetCurrentView()
		{
			Console.WriteLine("+++++++++++++++++++++++++");
			Console.WriteLine($" {Menu.currentView}");
			Console.WriteLine("+++++++++++++++++++++++++\n");
		}

		public static void DisplayExitMessage()
		{
			Console.WriteLine("Successfully Exited Application.\n");
		}

		public static void DisplayMenuView()
		{
			Console.WriteLine("Select a number (1-5) from the options below:\n");
			Console.WriteLine("1: Register");
			Console.WriteLine("2: Login");
			Console.WriteLine("3: Exit");
			Console.WriteLine("======= TESTING ======");
			Console.WriteLine("4: Get Current Weather");
			Console.WriteLine("5: Get Hourly Forecast");
		}

		public static void DisplayWelcomeMessage()
		{
			Console.WriteLine(@"
 _____               _           _           __   __                ______      _ _         _____ _ _                   __   _    _            _   _               _ 
|_   _|             | |         | |          \ \ / /                |  _  \    (_) |       /  ___| (_)                 / _| | |  | |          | | | |             | |
  | | ___   __ _ ___| |_ ___  __| |  ______   \ V /___  _   _ _ __  | | | |__ _ _| |_   _  \ `--.| |_  ___ ___    ___ | |_  | |  | | ___  __ _| |_| |__   ___ _ __| |
  | |/ _ \ / _` / __| __/ _ \/ _` | |______|   \ // _ \| | | | '__| | | | / _` | | | | | |  `--. \ | |/ __/ _ \  / _ \|  _| | |/\| |/ _ \/ _` | __| '_ \ / _ \ '__| |
  | | (_) | (_| \__ \ ||  __/ (_| |            | | (_) | |_| | |    | |/ / (_| | | | |_| | /\__/ / | | (_|  __/ | (_) | |   \  /\  /  __/ (_| | |_| | | |  __/ |  |_|
  \_/\___/ \__,_|___/\__\___|\__,_|            \_/\___/ \__,_|_|    |___/ \__,_|_|_|\__, | \____/|_|_|\___\___|  \___/|_|    \/  \/ \___|\__,_|\__|_| |_|\___|_|  (_)
                                                                                     __/ |                                                                           
                                                                                    |___/     
			");
		}

		public static void DisplayCurrentWeather(WeatherApiResponse weatherApiResponse, Location defaultLocation)
		{
			StringBuilder sb = new StringBuilder();
			// Date and time (2024-02-27 19:00:00 PM)
			string dateTime = ConvertUnixTimeToDateTime(weatherApiResponse.current.dt, weatherApiResponse.timezone);
			string[] dateTimeArray = dateTime.Split(" "); // [0] = date, [1] = time, [2] = am or pm
			string formattedDateTime = dateTimeArray[0] + " - " + dateTimeArray[1] + " " + dateTimeArray[2] + "\n";
			sb.AppendLine(formattedDateTime);
			// City + Country (Los Angeles, US)
			string cityAndCountry = $"{weatherApiResponse.name}, {GetCurrentCountry(defaultLocation.country)}\n";
			sb.AppendLine(cityAndCountry);
			// ASCII icon
			Icon icon = GetCurrentIcon(weatherApiResponse.current.weather.main);
			sb.AppendLine(icon.ToString());
			// Description (Moderate Rain, Heavy Rain, etc.)
			sb.AppendLine(TitleCase(weatherApiResponse.current.weather.description));
			// Temperature (12°C · 54°F)
			double tempF = Math.Truncate(weatherApiResponse.current.temp), tempC = FahrenheitToCelsius(tempF);
			sb.AppendLine(FormatTemperatureInColor(tempC, tempF));

			Console.WriteLine(sb);
		}

		public static void DisplayForecast(ForecastApiResponse forecastApiResponse)
		{
			List<StringBuilder> forecastStringBuilderList = GenerateForecastStringBuilderList(forecastApiResponse);
			Console.WriteLine($"Showing 24-hour forecast for {forecastApiResponse.city}, {forecastApiResponse.country}");
			for (int i = 0; i < forecastStringBuilderList.Count; i++)
			{
				Console.WriteLine(forecastStringBuilderList[i]);
			};
		}

		public static List<StringBuilder> GenerateForecastStringBuilderList(ForecastApiResponse forecastApiResponse)
		{
			List<StringBuilder> sbList = new List<StringBuilder>();
			foreach (ForecastItem i in forecastApiResponse.forecastList.forecastItems.Take(4).ToList())
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("------------------------------------");
				string dateAndTime = ConvertUnixTimeToDateTime(i.dt, forecastApiResponse.timezoneOffset);
				sb.AppendLine(dateAndTime);
				// Get the icon from the current forecast item
				Icon icon = GetCurrentIcon(i.weather.main);
				// Append each line of the icon to the StringBuilder
				foreach (string line in icon.text)
				{
					sb.AppendLine(line);
				}
				// Description (Moderate Rain, Heavy Rain, etc.)
				sb.AppendLine(TitleCase(i.weather.description));

				sb.AppendLine(dateAndTime);
				// Temperature (12°C · 54°F)
				double tempC = FahrenheitToCelsius(i.main.temp);

				sb.AppendLine(FormatTemperatureInColor(tempC, i.main.temp));
				sbList.Add(sb);
			}
			return sbList;
		}

		// #################
		// ### Utilities ###
		// #################

		public static string TitleCase(string str)
		{
			TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
			return textInfo.ToTitleCase(str);
		}

		public static Icon GetCurrentIcon(string category)
		{
			Icon icon = Icons.list.Find(i => i.name == category);
			return icon;
		}

		public static string GetCurrentCountry(string countryCode)
		{
			// Using reflection to get the country name from the description of the country code in the Countries enum
			string countryName = CountryCode.GetEnumDescription((Countries)Enum.Parse(typeof(Countries), countryCode));
			return countryName;
		}

		public static double FahrenheitToCelsius(double fahrenheit)
		{
			double celsius = Math.Truncate((fahrenheit - 32) * 5 / 9);
			return celsius;
		}

		public static string FormatTemperatureInColor(double celsius, double fahrenheit)
		{
			string color;
			var colorMap = new Dictionary<Func<double, bool>, string>
			{
				{ temp => temp <= -20, "\u001b[34m" }, // Blue for Extreme Cold
				{ temp => temp <= -10, "\u001b[36m" }, // Cyan for Very Cold
				{ temp => temp < 0, "\u001b[46m" }, // Dark Cyan for Cold
				{ temp => temp < 10, "\u001b[32m" }, // Green for Cool
				{ temp => temp < 20, "\u001b[34m" }, // Dark Green for Mild
				{ temp => temp < 30, "\u001b[33m" }, // Yellow for Warm
				{ temp => temp < 40, "\u001b[33;1m" }, // Dark Yellow (Bright) for Hot
				{ temp => temp < 50, "\u001b[31m" }, // Red for Very Hot
				{ temp => true, "\u001b[31;1m" } // Bright Red for Extreme Heat
			};
			foreach (var entry in colorMap)
			{
				if (entry.Key(celsius))
				{
					color = entry.Value;
					return $"{color}{celsius}°C · {fahrenheit}°F \u001b[0m";
				}
			}
			return "\u001b[0m"; // this is the default color (stripped)
		}

		public static string ConvertUnixTimeToDateTime(long unixTime, int timezoneOffsetInSeconds)
		{
			DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTime); // The date and time with the UTC offset - looks like: 2021-01-01 18:00:00 +05:00
			TimeSpan offset = TimeSpan.FromSeconds(timezoneOffsetInSeconds); // this is the actual offset relative to UTC - it would look like "-01:30:00" if you gave it -5400 or "01:00:00" if you gave it 3600.
			DateTimeOffset dateTimeWithOffset = dateTimeOffset.ToOffset(offset); // applies the offset to the date and time

			string formattedDateTime = dateTimeWithOffset.ToString("yyyy-MM-dd HH:mm:ss tt", CultureInfo.InvariantCulture);
			return formattedDateTime;
		}
	}
}