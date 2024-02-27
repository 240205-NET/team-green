using System.Text;
namespace Toasted.Logic

{

	public static class Icons
	{
		public static List<Icon> list { set; get; }
		// https://openweathermap.org/weather-conditions#How-to-get-icon-URL
		// public static void DisplayMultipleIcons(List<Icon> icons)
		// {
		// 	for (int j = 0; j < icons[0].text.Count; j++)
		// 	{
		// 		for (int i = 0; i < icons.Count; i++)
		// 		{
		// 			Console.Write(icons[i].text[j] + "   ");
		// 		}
		// 		Console.WriteLine();
		// 	}
		// }

		static Icons()
		{
			list = new List<Icon>
			{
				new Icon {
					name = "Thunderstorm",
					text = new List<string>
					{
						"\u001b[38;5;250m     .-.     \u001b[0m",
						"\u001b[38;5;250m    (   ).   \u001b[0m",
						"\u001b[38;5;250m   (___(__)  \u001b[0m",
						"\u001b[38;5;111m  ‚‘<‘‚>‚‘   \u001b[0m",
						"\u001b[38;5;111m  ‚’‚’<’‚’   \u001b[0m",
					}
				},
				new Icon {
					name = "Drizzle",
					text = new List<string>
					{
						"\u001b[38;5;250m     .-.     \u001b[0m",
						"\u001b[38;5;250m    (   ).   \u001b[0m",
						"\u001b[38;5;250m   (___(__)  \u001b[0m",
						"\u001b[38;5;111m    ‘ ‘ ‘ ‘  \u001b[0m",
						"\u001b[38;5;111m   ‘ ‘ ‘ ‘   \u001b[0m",
					}
				},
				new Icon {
					name = "Rain",
					text = new List<string>
					{
						"\u001b[38;5;240;1m     .-.     \u001b[0m",
						"\u001b[38;5;240;1m    (   ).   \u001b[0m",
						"\u001b[38;5;240;1m   (___(__)  \u001b[0m",
						"\u001b[38;5;21;1m   ‚‘‚‘‚‘‚‘   \u001b[0m",
						"\u001b[38;5;21;1m   ‚’‚’‚’‚’   \u001b[0m",
					}
				},
				new Icon {
					name = "Snow",
					text = new List<string>
					{
						"\u001b[38;5;240;1m     .-.     \u001b[0m",
						"\u001b[38;5;240;1m    (   ).   \u001b[0m",
						"\u001b[38;5;240;1m   (___(__)  \u001b[0m",
						"\u001b[38;5;255;1m   * * * *   \u001b[0m",
						"\u001b[38;5;255;1m  * * * *    \u001b[0m",
					}
				},
					new Icon{
					name = "Clear",
					text = new List<string>
					{
						"\u001b[38;5;226m    \\   /    \u001b[0m",
						"\u001b[38;5;226m     .-.     \u001b[0m",
						"\u001b[38;5;226m  ― (   ) ―  \u001b[0m",
						"\u001b[38;5;226m    `-’     \u001b[0m",
						"\u001b[38;5;226m    /  \\    \u001b[0m",
					}
				},
				/*
				#####################
				Group 7XX: Atmosphere
				#####################
				*/
				new Icon {
					name = "Mist",
					text = new List<string>
					{
						"\u001b[38;5;250m   ~ ~ ~ ~   \u001b[0m",
						"\u001b[38;5;250m   ~ ~ ~ ~   \u001b[0m",
						"\u001b[38;5;250m   ~ ~ ~ ~   \u001b[0m",
						"\u001b[38;5;250m   ~ ~ ~ ~   \u001b[0m",
						"\u001b[38;5;250m   ~ ~ ~ ~   \u001b[0m",
					}
				},

				new Icon {
					name = "Smoke",
					text = new List<string>
					{
						"\u001b[38;5;250m   ))) (     \u001b[0m",
						"\u001b[38;5;250m   ( (((     \u001b[0m",
						"\u001b[38;5;250m   ))) )     \u001b[0m",
						"\u001b[38;5;250m   )) ))     \u001b[0m",
						"\u001b[38;5;250m   ) ) )     \u001b[0m",
					}
				},

				new Icon {
					name = "Haze",
					text = new List<string>
					{
						"\u001b[38;5;250m   - - - -   \u001b[0m",
						"\u001b[38;5;250m    - - -    \u001b[0m",
						"\u001b[38;5;250m   - - - -   \u001b[0m",
						"\u001b[38;5;250m    - - -    \u001b[0m",
						"\u001b[38;5;250m   - - - -   \u001b[0m",
					}
				},

				new Icon {
					name = "Dust",
					text = new List<string>
					{
						"\u001b[38;5;250m   * * *     \u001b[0m",
						"\u001b[38;5;250m    * * *    \u001b[0m",
						"\u001b[38;5;250m   * * *     \u001b[0m",
						"\u001b[38;5;250m    * * *    \u001b[0m",
						"\u001b[38;5;250m   * * *     \u001b[0m",
					}
				},

				new Icon {
					name = "Fog",
					text = new List<string>
					{
						"\u001b[38;5;250m   ~ ~ ~     \u001b[0m",
						"\u001b[38;5;250m    ~ ~      \u001b[0m",
						"\u001b[38;5;250m   ~ ~ ~     \u001b[0m",
						"\u001b[38;5;250m    ~ ~      \u001b[0m",
						"\u001b[38;5;250m   ~ ~ ~     \u001b[0m",
					}
				},

				new Icon {
					name = "Sand",
					text = new List<string>
					{
						"\u001b[38;5;250m   . . .     \u001b[0m",
						"\u001b[38;5;250m    . . .    \u001b[0m",
						"\u001b[38;5;250m   . . .     \u001b[0m",
						"\u001b[38;5;250m    . . .    \u001b[0m",
						"\u001b[38;5;250m   . . .     \u001b[0m",
					}
				},

				new Icon {
					name = "Dust",
					text = new List<string>
					{
						"\u001b[38;5;250m   * * *     \u001b[0m",
						"\u001b[38;5;250m    * * *    \u001b[0m",
						"\u001b[38;5;250m   * * *     \u001b[0m",
						"\u001b[38;5;250m    * * *    \u001b[0m",
						"\u001b[38;5;250m   * * *     \u001b[0m",
					}
				},

				new Icon {
					name = "Ash",
					text = new List<string>
					{
						"\u001b[38;5;250m   @ @ @     \u001b[0m",
						"\u001b[38;5;250m    @ @      \u001b[0m",
						"\u001b[38;5;250m   @ @ @     \u001b[0m",
						"\u001b[38;5;250m    @ @      \u001b[0m",
						"\u001b[38;5;250m   @ @ @     \u001b[0m",
					}
				},

				new Icon {
					name = "Squall",
					text = new List<string>
					{
						"\u001b[38;5;250m   /// //    \u001b[0m",
						"\u001b[38;5;250m   // ///    \u001b[0m",
						"\u001b[38;5;250m   /// //    \u001b[0m",
						"\u001b[38;5;250m   // ///    \u001b[0m",
						"\u001b[38;5;250m   /// //    \u001b[0m",
					}
				},
				new Icon {
					name = "Tornado",
					text = new List<string>
					{
						"\u001b[38;5;250m     --_-_-_-_---     \u001b[0m",
						"\u001b[38;5;250m       -_-_-_        \u001b[0m",
						"\u001b[38;5;111m          _-         \u001b[0m",
						"\u001b[38;5;111m          -_         \u001b[0m",
						"\u001b[38;5;111m           _-        \u001b[0m"
					}
				},
				/*
				#####################
				Group 8XX: Clouds 
				#####################
				*/
				new Icon
				{
					name = "Clouds",
					text = new List<string>
					{

					"\u001b[38;5;250m     .--.       .--.    \u001b[0m",
					"\u001b[38;5;250m  .-(    ).   (    ).  \u001b[0m",
					"\u001b[38;5;250m (___.__)__) (___(__)  \u001b[0m",
					"\u001b[38;5;250m             (      )  \u001b[0m",
					"\u001b[38;5;250m              `----'   \u001b[0m",
					}
				},
			};
		}


	}

	public class Icon
	{
		public string name { get; set; }
		public List<string> text { get; set; }

		public override string ToString()
		{
			StringBuilder output = new StringBuilder();
			foreach (string line in this.text)
			{
				output.AppendLine(line);
			}
			return output.ToString();
		}
	}

}