using System;
using System.Text;
using System.Runtime.InteropServices;
using Toasted.Logic;
using System.Globalization;

namespace Toasted.App
{
	public static class Menu
	{

		[DllImport("kernel32.dll")]
		private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

		[DllImport("kernel32.dll")]
		private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetStdHandle(int nStdHandle);

		private const int STD_OUTPUT_HANDLE = -11;
		private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
		public static string currentView { get; set; } = "Main Menu";
		// public static void SetCurrentView(string currentView = "Main Menu")
		// {
		// 	Menu.currentView = currentView;
		// }

		public static void GetCurrentView()
		{
			Console.WriteLine("+++++++++++++++++++++++++");
			Console.WriteLine($" {Menu.currentView}");
			Console.WriteLine("+++++++++++++++++++++++++\n");
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

		public static void DisplayForecastItems(List<StringBuilder> forecastItemOutput)
		{
			for (int i = 0; i < forecastItemOutput.Count; i++)
			{
				Console.WriteLine(forecastItemOutput[i]);
			}
		}

		public static List<StringBuilder> GenerateForecastStringBuilderList(ForecastList forecastList)
		{
			List<StringBuilder> sbList = new List<StringBuilder>();
			foreach (ForecastItem i in forecastList.forecastItems.Take(4).ToList())
			{
				// Create new StringBuilder for current forecast item
				StringBuilder sb = new StringBuilder();
				// Get the icon from the current forecast item
				Icon icon = GetCurrentIcon(i.weather.main);
				// Append each line of the icon to the StringBuilder
				foreach (string line in icon.text)
				{
					sb.AppendLine(line);
				}
				// Description (Moderate Rain, Heavy Rain, etc.)
				sb.AppendLine(TitleCase(i.weather.Description));
				// Temperature (12°C · 54°F)
				double celsius = FahrenheitToCelsius(i.main.temp);

				sb.AppendLine(FormatTemperatureInColor(celsius, i.main.temp));
				sbList.Add(sb);
			}
			return sbList;
		}

		public static void DisplayForecast(ForecastApiResponse forecastApiResponse)
		{
			List<StringBuilder> forecastStringBuilderList = GenerateForecastStringBuilderList(forecastApiResponse.forecastList);
			DisplayForecastItems(forecastStringBuilderList);
		}

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
			DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTime);

			TimeSpan offset = TimeSpan.FromSeconds(timezoneOffsetInSeconds);
			DateTimeOffset dateTimeWithOffset = dateTimeOffset.ToOffset(offset);

			// Format the DateTimeOffset to a readable string
			string formattedDateTime = dateTimeWithOffset.ToString("yyyy-MM-dd H:m:s tt", CultureInfo.InvariantCulture);

			return formattedDateTime;
		}

		public static void DisplayExitMessage()
		{
			Console.WriteLine("Successfully Exited Application.\n");
		}
	}
}