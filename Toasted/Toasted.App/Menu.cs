using System;
using System.Text;

namespace Toasted.App
{
	public static class Menu
	{
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
		}

		public static void DisplayWelcomeMessage()
		{
			Console.WriteLine("Welcome to Toasted - Your Daily Slice of Weather!\n");
		}
	}
}