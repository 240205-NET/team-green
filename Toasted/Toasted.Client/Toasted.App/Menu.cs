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
			Console.WriteLine("5: Get 12 Hour Forecast");
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
			// Shorten property references
			string main = weatherApiResponse.current.weather.main;
			string description = weatherApiResponse.current.weather.description;
			double temperature = Math.Truncate(weatherApiResponse.current.temp);
			double feelsLike = weatherApiResponse.current.feelsLike;
			string countryCode = defaultLocation.country;

			string countryName = GetCurrentCountry(countryCode);
			Icon icon = GetCurrentIcon(main);

			// City + Country
			Console.WriteLine($"{weatherApiResponse.name}, {countryName}");
			// ASCII icon
			Console.WriteLine(icon.ToString());
			// Description (Moderate Rain, Heavy Rain, etc.)
			Console.WriteLine(TitleCase(description));
			// Temperature (12°C · 54°F)
			double celsius = FahrenheitToCelsius(temperature);

			Console.ForegroundColor = GetTemperatureColor(celsius);
			Console.WriteLine($"{celsius}°C · {temperature}°F");
			Console.ResetColor();
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

		public static ConsoleColor GetTemperatureColor(double celsius)
		{
			return celsius switch
			{
				_ when celsius <= -20 => ConsoleColor.Blue, // Extreme Cold
				_ when celsius <= -10 => ConsoleColor.Cyan, // Very Cold
				_ when celsius < 0 => ConsoleColor.DarkCyan, // Cold
				_ when celsius < 10 => ConsoleColor.Green, // Cool
				_ when celsius < 20 => ConsoleColor.DarkGreen, // Mild
				_ when celsius < 30 => ConsoleColor.Yellow, // Warm
				_ when celsius < 40 => ConsoleColor.DarkYellow, // Hot
				_ when celsius < 50 => ConsoleColor.Red, // Very Hot
				_ => ConsoleColor.DarkRed, // Extreme Heat
			};
		}
	}
}