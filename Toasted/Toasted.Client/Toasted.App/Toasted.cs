using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using Toasted.App;
using Toasted.Logic;
using static Toasted.Logic.ToastedApiAsync;

namespace Toasted.App
{
	public class Toasted
	{
		private IConfiguration _configuration;
		public string? OpenWeatherApiKey { get; }
		public string LocalUrl { get; } = "http://localhost:5083";

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
			bool exit = false;
			while (!exit)
			{
				Menu.DisplayMenuView();
				string userInput = Console.ReadKey().KeyChar.ToString();
				Console.WriteLine();
				switch (userInput)
				{
					case "1":
						Console.Clear();
						Menu.currentView = "Register";
						User registeredUser = this.ContentWrapper(Register).Result;
						//this.ContentWrapper(DisplayWeatherHomepage, registeredUser);
						UserMenu um = new UserMenu(registeredUser);
						um.DisplayUserMenu();
						break;
					case "2":
						Console.Clear();
						Menu.currentView = "Login";
						User loggedInUser = this.ContentWrapper(Login).Result;
						//	Console.WriteLine(loggedInUser.ToString());
						//this.ContentWrapper(DisplayWeatherHomepage, loggedInUser);
						UserMenu userMenu = new UserMenu(loggedInUser);
						userMenu.DisplayUserMenu();
						break;
					case "3":
						Console.Clear();
						Menu.DisplayExitMessage();
						exit = true;
						break;
					default:
						Console.WriteLine("Option Does Not Exist");
						break;
				}
			}
			// functions that execute the options
		}
		public async Task GetFiveDayForecast(Location l)
		{
			ForecastApiResponse forecastApiResponse = await Request.GetForecastAsync(this.OpenWeatherApiKey, l.lat, l.lon);
			Menu.DisplayFiveDayForecast(forecastApiResponse);
		}
		public User ContentWrapper<User>(Func<User> func)
		{
			User result = default(User);
			ContentWrapper(() => { result = func(); });
			return result;
		}

		public void ContentWrapper(Action<User> content, User u)
		{
			Menu.GetCurrentView();
			content(u);
		}

		public void ContentWrapper(Action content)
		{
			Menu.GetCurrentView();
			content();
		}
		//register method with loops for each field and check for errors
		public async Task<User> Register()
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
			User u = new User(username, encryptedPassword, firstName, lastName, 1, email, defaultLocation, countryCode, 'F');
			//   Console.Write(u.ToString());
			// Get the current weather using the data in defaultLocation
			// WeatherApiResponse currentWeather = await Request.GetCurrentWeatherAsync(this.OpenWeatherApiKey, defaultLocation.lat, defaultLocation.lon);
			bool check = ToastedApiAsync.TryPostNewAccount(u, LocalUrl).Result;

			//   Console.Write("test " + u.ToString());

			if (!check)
			{
				throw new Exception("Registration Failed");
			}


			return await TryPostGetUser(username, LocalUrl);

			// This is purely for testing purposes (just to check that the objects are successfully created)
			/*
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
			*/
		}

		//login method with loop for checking incorrect user pass combo
		public async Task<User> Login()
		{
			User u = null!;
			string username = "";
			string password = "";

			bool loggingIn = true;
			while (loggingIn)
			{
				try
				{
					username = inputFormatter("Username: ");
					password = inputFormatter("Password: ");
					string encryptedPassword = PasswordEncryptor.Encrypt(password);
					UserValidityChecks.IsLoginValid(username, encryptedPassword);
					u = await TryPostGetUser(username, LocalUrl);
					loggingIn = false;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			return u;
		}
		public static string inputFormatter(string s)
		{
			Console.WriteLine(s);
			return Console.ReadLine();
		}

		public async Task DisplayWeatherHomepage(User u)
		{
			Location defaultLocation;
			WeatherApiResponse currentWeather;
			try
			{
				defaultLocation = await Request.GetLocation(this.OpenWeatherApiKey,
					u.location.zip < 10000 ? "0" + u.location.zip.ToString() : u.location.zip.ToString(),
					u.countryCode);
				currentWeather = await Request.GetCurrentWeatherAsync(this.OpenWeatherApiKey, defaultLocation.lat, defaultLocation.lon);

			}
			catch
			{
				throw new Exception("Location/Weather was not attainable from API");
			}

			Weather w = currentWeather.current.Weather;
			CurrentWeather cw = currentWeather.current;
			WeatherHomepage homepage = new WeatherHomepage(w, cw, u);
			ForecastApiResponse forecastApiResponse = await Request.GetForecastAsync(this.OpenWeatherApiKey, defaultLocation.lat, defaultLocation.lon);
			homepage.DisplayCurrentWeather();
			homepage.DisplayForecast(forecastApiResponse);
			await GetFiveDayForecast(defaultLocation);
			
		}
	}
}