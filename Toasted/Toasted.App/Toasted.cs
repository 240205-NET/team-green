using System.ComponentModel;
using Toasted.Logic;

namespace Toasted.App
{
	public class Toasted
	{
		public async Task Run()
		{
			Menu.DisplayWelcomeMessage();
			// main program loop
			while (true)
			{
				Menu.DisplayMenuView();
				string userInput = Console.ReadKey().KeyChar.ToString();
				Console.WriteLine();
				switch (userInput)
				{
					case "1":
						Console.Clear();
						this.ContentWrapper(Register);
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
		public void ContentWrapper(Action content)
		{
			Menu.GetCurrentView();
			content();
		}
		public async void Register()
		{
			// username, firstName, lastName, password, email, location, 
			// 1) prompt for username (check if username already exists)
			// 2) prompt for password (min length?)
			// 3) prompt for email (check if valid email (ez stuff), check if duplicate)
			// 4) prompt for location (5 digit validation for now )
			string username = "";
			string password = "";
			string firstName = "";
			string lastName = "";
			string email = "";
			string zipcode = "";
			string countryCode = "";

			bool registering = true;
			while (registering)
			{
				try
				{
					username = inputFormatter("Please enter your username: ");
					UserValidityChecks.IsUsernameValid(username);
					registering = false;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			registering = true;
			while (registering)
			{
				try
				{
					password = inputFormatter("Please enter your password: ");
					UserValidityChecks.isPassWordValid(password);
					registering = false;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			registering = true;
			while (registering)
			{
				try
				{
					firstName = inputFormatter("Please enter your first name: ");
					UserValidityChecks.isNameValid(firstName);
					registering = false;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			registering = true;
			while (registering)
			{
				try
				{
					lastName = inputFormatter("Please enter your last name: ");
					UserValidityChecks.isNameValid(lastName);
					registering = false;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			registering = true;
			while (registering)
			{
				try
				{
					email = inputFormatter("Please enter your email: ");
					UserValidityChecks.isEmailValid(email);
					registering = false;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			registering = true;
			while (registering)
			{
				try
				{
					zipcode = inputFormatter("Please enter your zipcode: ");
					UserValidityChecks.isZipcodeValid(zipcode);
					registering = false;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
			registering = true;
			while (registering)
			{
				try
				{
					countryCode = inputFormatter("Please enter your 2-character country code: ");
					if (UserValidityChecks.isCountryCodeValid(countryCode))
					{
						// Country code is valid, exit the loop
						registering = false;
					}
					else
					{
						// Country code is not valid, prompt for re-entry
						Console.WriteLine("Invalid country code. Please try again.");
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			string encryptedPassword = PasswordEncryptor.Encrypt(password);

			// for now we'll assume the user 
			Location location = await Request.GetLocation("<API KEY HERE>", Int32.Parse(zipcode), countryCode);
			User u = new User(username, password, firstName, lastName, 1, email, new Location());

		}
		private string inputFormatter(string s)
		{
			Console.WriteLine(s);
			return Console.ReadLine();
		}
	}
}