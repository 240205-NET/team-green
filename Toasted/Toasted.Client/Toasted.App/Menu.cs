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
			Console.WriteLine("Select a number (1-4) from the options below:\n");
			Console.WriteLine("1: Register");
			Console.WriteLine("2: Login");
			Console.WriteLine("3: Exit");
			Console.WriteLine("4: Display 5 Day Forecast");
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
			string dateTime = ConvertUnixTimeToDateTime(weatherApiResponse.current.Dt, weatherApiResponse.timezone);
			string[] dateTimeArray = dateTime.Split(" "); // [0] = date, [1] = time, [2] = am or pm
			string formattedDateTime = dateTimeArray[0] + " - " + dateTimeArray[1] + " " + dateTimeArray[2] + "\n";
			sb.AppendLine(formattedDateTime);
			// City + Country (Los Angeles, US)
			string cityAndCountry = $"{weatherApiResponse.name}, {GetCurrentCountry(defaultLocation.country)}\n";
			sb.AppendLine(cityAndCountry);
			// ASCII icon
			Icon icon = GetCurrentIcon(weatherApiResponse.current.Weather.main);
			sb.AppendLine(icon.ToString());
			// Description (Moderate Rain, Heavy Rain, etc.)
			sb.AppendLine(TitleCase(weatherApiResponse.current.Weather.Description));
			// Temperature (12°C · 54°F)
			double tempF = Math.Truncate(weatherApiResponse.current.Temp), tempC = FahrenheitToCelsius(tempF);
			sb.AppendLine(FormatTemperatureInColor(tempC, tempF));

			Console.WriteLine(sb);
		}

		public static void DisplayForecast(ForecastApiResponse forecastApiResponse)
		{
			List<StringBuilder> forecastStringBuilderList = GenerateForecastStringBuilderList(forecastApiResponse, 9); // 9 ForecastItem  objects for a full 24-hour forecast
			Console.WriteLine($"Showing 24-hour forecast for {forecastApiResponse.city}, {forecastApiResponse.country}");
			for (int i = 0; i < forecastStringBuilderList.Count; i++)
			{
				Console.WriteLine(forecastStringBuilderList[i]);
			};
		}

		/// <summary>
		/// This method is used to generate a List of StringBuilder objects containing the formatted output of ForecastItem objects that can then be iterated over and displayed neatly in the terminal.
		/// </summary>
		/// <param name="forecastApiResponse">A ForecastApiResponse object.</param>
		/// <param name="numItems">The number of ForecastItem objects to return from the ForecastApiResponse.</param> 
		/// <returns>A list of StringBuilder items each containing output for a single ForecastItem.</returns>
		public static List<StringBuilder> GenerateForecastStringBuilderList(ForecastApiResponse forecastApiResponse, int numItems)
		{
			List<StringBuilder> sbList = new List<StringBuilder>();
			foreach (ForecastItem i in forecastApiResponse.forecastList.forecastItems.Take(numItems).ToList())
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("---------------------------------------------\n"); // Divider between entries
				string dateAndTime = ConvertUnixTimeToDateTime(i.dt, forecastApiResponse.timezoneOffset);
				sb.AppendLine(dateAndTime + "\n");
				// Get the icon from the current forecast item
				Icon icon = GetCurrentIcon(i.weather.main);
				// Append each line of the icon to the StringBuilder
				foreach (string line in icon.text)
				{
					sb.AppendLine(line);
				}
				sb.AppendLine();
				// Description (Moderate Rain, Heavy Rain, etc.)
				sb.AppendLine(TitleCase(i.weather.Description));
				// Temperature (12°C · 54°F)
				double tempC = FahrenheitToCelsius(i.main.temp);

				sb.AppendLine(FormatTemperatureInColor(tempC, i.main.temp));
				sbList.Add(sb);
			}
			return sbList;
		}

		public static List<StringBuilder> GenerateFiveDayForecastStringBuilderList(List<ForecastItem> forecastItems, int timezoneOffset)
		{
			List<StringBuilder> sbList = new List<StringBuilder>();
			foreach (ForecastItem i in forecastItems)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("---------------------------------------------\n"); // Divider between entries
				string dateAndTime = ConvertUnixTimeToDateTime(i.dt, timezoneOffset);
				sb.AppendLine(dateAndTime + "\n");
				// Assuming GetCurrentIcon and other methods are defined elsewhere
				Icon icon = GetCurrentIcon(i.weather.main);
				foreach (string line in icon.text)
				{
					sb.AppendLine(line);
				}
				sb.AppendLine();
				sb.AppendLine(TitleCase(i.weather.Description));
				double tempC = FahrenheitToCelsius(i.main.temp);
				sb.AppendLine(FormatTemperatureInColor(tempC, i.main.temp));
				sbList.Add(sb);
			}
			return sbList;
		}

		public static void DisplayFiveDayForecast(ForecastApiResponse forecastApiResponse)
		{
			// Narrow down to one forecast per day
			var reducedForecastItems = WeatherForecastProcessor.NarrowDownForecasts(forecastApiResponse);

			List<StringBuilder> forecastStringBuilderList = GenerateFiveDayForecastStringBuilderList(reducedForecastItems, forecastApiResponse.timezoneOffset);
			Console.WriteLine($"Showing forecast for {forecastApiResponse.city}, {forecastApiResponse.country}");
			foreach (var sb in forecastStringBuilderList)
			{
				Console.WriteLine(sb);
			}
		}
		/*
		public static void DisplayForecast(ForecastApiResponse forecastApiResponse)
		{
			List<StringBuilder> forecastStringBuilderList = GenerateForecastStringBuilderList(forecastApiResponse.forecastList);
			DisplayForecastItems(forecastStringBuilderList);
		}
		*/

		/*
		public static void DisplayCurrentWeather(WeatherApiResponse weatherApiResponse, Location defaultLocation)
		{
			StringBuilder sb = new StringBuilder();
			// Shorten property references
			string main = weatherApiResponse.current.Weather.main; // Category - "Rain", "Snow", etc.
			string description = weatherApiResponse.current.Weather.Description; // Further Description - "Light Rain", "Heavy Rain", etc.
			double tempF = Math.Truncate(weatherApiResponse.current.Temp); // in Fahrenheit
			double feelsLike = weatherApiResponse.current.FeelsLike; // in Fahrenheit
			string countryCode = defaultLocation.country;

			string countryName = GetCurrentCountry(countryCode);
			Icon icon = GetCurrentIcon(main);
			string dateTime = ConvertUnixTimeToDateTime(weatherApiResponse.current.Dt, weatherApiResponse.timezone);
			string[] dateTimeArray = dateTime.Split(" ");
			string formattedDateTime = dateTimeArray[0] + " - " + dateTimeArray[1] + " " + dateTimeArray[2] + "\n";
			sb.AppendLine(formattedDateTime);
			// City + Country
			string cityAndCountry = $"{weatherApiResponse.name}, {countryName}\n";
			sb.AppendLine(cityAndCountry);
			// ASCII icon
			sb.AppendLine(icon.ToString());
			// Description (Moderate Rain, Heavy Rain, etc.)
			sb.AppendLine(TitleCase(description));
			// Temperature (12°C · 54°F)
			double tempC = FahrenheitToCelsius(tempF);

			sb.AppendLine(FormatTemperatureInColor(tempC, tempF));
			Console.WriteLine(sb);

		}
		*/

		// #################
		// ### Utilities ###
		// #################

		/// <summary>
		/// Converts a string to its title case (e.g., "hello world" becomes "Hello World").
		//  </summary>
		/// <param name="str">The target string.</param>
		/// <returns></returns>
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

			// string formattedDateTime = dateTimeWithOffset.ToString("yyyy-MM-dd HH:mm:ss tt", CultureInfo.InvariantCulture);
			string formattedDateTime = dateTimeWithOffset.ToString("dddd, MMMM dd, yyyy - h:mm tt", CultureInfo.InvariantCulture);



			return formattedDateTime;
		}

	}
}