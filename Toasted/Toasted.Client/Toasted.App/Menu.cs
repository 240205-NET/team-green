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
	}
}