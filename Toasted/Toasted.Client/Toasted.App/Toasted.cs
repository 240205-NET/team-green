using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using Toasted.Logic;

namespace Toasted.App
{
	public class Toasted
	{
		private IConfiguration _configuration;
		private string? OpenWeatherApiKey;

		public Toasted()
		{
			// Build configuration
			var builder = new ConfigurationBuilder()
				  .SetBasePath(Directory.GetCurrentDirectory())
				  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				  .AddEnvironmentVariables();
			_configuration = builder.Build();
			// Assign the OpenWeather API key
			this.OpenWeatherApiKey = _configuration["OpenWeatherMapSettings:ApiKey"];
		}
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
						this.ContentWrapper(Login);
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
					UserValidityChecks.isCountryCodeValid(countryCode);
					registering = false;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			string encryptedPassword = PasswordEncryptor.Encrypt(password);

			// Create a new Location and then create a new User and set their defaultLocation to the new Location.
			Location defaultLocation = await Request.GetLocation(this.OpenWeatherApiKey, zipcode, countryCode);
			User u = new User(username, encryptedPassword, firstName, lastName, 1, email, defaultLocation, countryCode);

			// This is purely for testing purposes (just to check that the objects are successfully created)
			Console.WriteLine("\nHere is your new User and Location objects:\n");
			Console.WriteLine($"Username: {u.username}");
			Console.WriteLine($"Password: {u.password}");
			Console.WriteLine($"First Name: {u.firstName}");
			Console.WriteLine($"Last Name: {u.lastName}");
			Console.WriteLine($"User ID: {u.userID}");
			Console.WriteLine($"Email: {u.email}\n");

			Console.WriteLine($"Location Name: {defaultLocation.zip}");
			Console.WriteLine($"Location Name: {defaultLocation.name}");
			Console.WriteLine($"Location Latitude: {defaultLocation.lat}");
			Console.WriteLine($"Location Longitude: {defaultLocation.lon}");
			Console.WriteLine($"Location Longitude: {defaultLocation.country}");
		}

		public async void Login()
		{

			string username = "";
			string password = "";

			bool loggingIn = true;
			while (loggingIn)
			{

				try
				{

				}
				catch
				{

				}
			}
		}
		private string inputFormatter(string s)
		{
			Console.WriteLine(s);
			return Console.ReadLine();
		}
	}
}