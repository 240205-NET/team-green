namespace Toasted.App
{
	public class Toasted
	{
		public void Run()
		{
			Console.WriteLine("sup mawn");
			// main program loop
			while (true)
			{
				Console.Write("Please choose an option: ");
				string userInput = Console.ReadKey().KeyChar.ToString();
				Console.WriteLine();
				switch (userInput)
				{
					case "1":
						Console.Clear();
						Console.WriteLine("Register");
						break;
					case "2":
						Console.Clear();
						Console.WriteLine("Login");
						break;
					case "3":
						Console.Clear();
						Console.WriteLine("Exit");
						break;
					default:
						break;
				}
			}
			// functions that execute the options
		}

		public void Register()
		{
			bool registering = true;
			while (registering)
			{
				// username, firstName, lastName, password, email, location, 
				// 1) prompt for username (check if username already exists)
				// 2) prompt for password (min length?)
				// 3) prompt for email (check if valid email (ez stuff), check if duplicate)
				// 4) prompt for location (5 digit validation for now )
				// 
				Console.Write("Please enter your username: ");
				string username = Console.ReadLine();
				Console.Write("Please enter your password: ");
				string password = Console.ReadLine();
				Console.Write("Please enter your email: ");
				string email = Console.ReadLine();
				Console.Write("Please enter your 5 digit zip-code: ");
				int zip = Int32.Parse(Console.ReadLine());
			}
		}
		public string inputFormatter(string s)
		{
			Console.WriteLine(s);
			return Console.ReadLine();
		}
	}
}