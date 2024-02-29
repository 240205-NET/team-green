using Toasted.App;
using Toasted.Logic;

namespace Toasted.App;

public class UserMenu
{
    private User currentUser;
    private Toasted t;

    public UserMenu(User u)
    {
        this.currentUser = u;
        this.t = new Toasted();
    }

    public void DisplayUserMenu()
    {
        DisplayUserWelcome();
        bool exit = false;
        while (!exit)
        {
            this.currentUser = ToastedApiAsync.TryPostGetUser(this.currentUser.username, t.LocalUrl).Result;
            DisplayUserOptions();
            string optionChoice = Console.ReadKey().KeyChar.ToString();
            Console.WriteLine();
            switch (optionChoice)
            {
                case "1":
                    t.DisplayWeatherHomepage(this.currentUser).Wait();
                    break;
                case "2":
                    DisplayUser();
                    break;
                case "3":
                    ChangeLocation().Wait();
                    break;
                case "4":
                    SwitchTempUnit().Wait();
                    Console.WriteLine("Successfully Switched Temperature Unit");
                    break;
                case "5":
                    break;
                case "6":
                    ChangePassword().Wait();
                    break;
                case "7":
                    Search().Wait();
                    break;
                case "8":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Menu Option does not exist!");
                    break;
            }
        }
    }

    public void DisplayUserOptions()
    {
        Console.WriteLine("Select a number (1-8) from the options below:\n");
        Console.WriteLine("1: Display Current Weather");
        Console.WriteLine("2: View User Details");
        Console.WriteLine("3: Change Current Location");
        Console.WriteLine("4: Switch Temperature Preference");
        Console.WriteLine("5: Add Location");
        Console.WriteLine("6: Change Password");
        Console.WriteLine("7: Search");
        Console.WriteLine("8: Log Out");
    }

    private void DisplayUserWelcome()
    {
        Console.Clear();
        string s = $"Welcome {currentUser.firstName} {currentUser.lastName}!";
        Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
        Console.WriteLine(s);
    }

    private void DisplayUser()
    {
        Console.WriteLine("Username: "+this.currentUser.username);
        Console.WriteLine("Name: "+this.currentUser.firstName+" "+this.currentUser.lastName);
        Console.WriteLine("Email: "+this.currentUser.email);
        Console.WriteLine("Default Location: "+this.currentUser.location.name+", "+this.currentUser.location.country);
        Console.WriteLine("Temperature Preference: "+this.currentUser.tempUnit);
    }

    private async Task<bool> ChangeLocation()
    {
        string zipcode = "";
        Console.WriteLine("Current zip: "+this.currentUser.location.zip);
        bool changed = false;
        while (!changed)
        {
            try
            {
                zipcode = Toasted.inputFormatter("Please Enter New Zip: ");
                UserValidityChecks.isZipcodeValid(zipcode);
                Location defaultLocation = Request.GetLocation(t.OpenWeatherApiKey, zipcode, this.currentUser.countryCode).Result;
                changed = await ToastedApiAsync.TryPatchLocation(this.currentUser.username, defaultLocation, t.LocalUrl);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        return !changed;
    }

    private async Task<bool> SwitchTempUnit()
    {
        bool switched = false;
        char currentTempUnit = this.currentUser.tempUnit;
        try
        {
            if (currentTempUnit == 'F')
            {
                switched = await ToastedApiAsync.TryPatchTempUnit(this.currentUser.username, 'C', t.LocalUrl);
            }
            else
            {
                switched = await ToastedApiAsync.TryPatchTempUnit(this.currentUser.username, 'F', t.LocalUrl);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return switched;
    }
    
    private async Task<bool> ChangePassword()
    {
        string password = "";
        Console.WriteLine("Current password: "+PasswordEncryptor.Decrypt(this.currentUser.password));
        bool changed = false;
        while (!changed)
        {
            try
            {
                password = Toasted.inputFormatter("Please Enter New Password: ");
                UserValidityChecks.isPassWordValid(password);
                changed = await ToastedApiAsync.TryPatchPassword(this.currentUser.username, PasswordEncryptor.Encrypt(password), t.LocalUrl);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        return !changed;
    }

    private async Task Search()
    {
        string input = Toasted.inputFormatter("Enter Zip Code of Location: ");
        Location location = await Request.GetLocation(t.OpenWeatherApiKey,input,"US");
        WeatherApiResponse weatherResponse =
            await Request.GetCurrentWeatherAsync(t.OpenWeatherApiKey, location.lat, location.lon);
        ForecastApiResponse forecastApiResponse = await Request.GetForecastAsync(t.OpenWeatherApiKey, location.lat, location.lon);
        Weather weather = weatherResponse.current.Weather;
        CurrentWeather currentWeather = weatherResponse.current;
        SearchWeather sw = new SearchWeather(weather, currentWeather, location);
        sw.DisplayCurrentWeather();
        sw.DisplayForecast(forecastApiResponse);
        await t.GetFiveDayForecast(location);
    }
}