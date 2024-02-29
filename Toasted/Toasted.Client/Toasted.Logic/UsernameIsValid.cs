namespace Toasted.Logic
{
	using System;
	using System.Text.RegularExpressions;
	using System.Net.Mail;
	using static Toasted.Logic.ToastedApiAsync;

	public class UserValidityChecks
	{
		private const string LocalUrl = "http://localhost:5083";

		public static bool IsUsernameValid(string username)
		{
			var userNameCheck = TryPostCheckUsername(username, LocalUrl);
			if (username.Equals(""))
			{
				throw new Exception("Username cannot be empty.");
			}
			else if (username.Contains(" "))
			{
				throw new Exception("Username cannot have space.");
			}
			else if (userNameCheck.Result)
			{
				throw new Exception("Username already exists");
			}
			return true;
		}

		public static bool isPassWordValid(string password)
		{
			string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";
			Regex r = new Regex(pattern);
			if (!r.IsMatch(password)) throw new Exception("Invalid Password Format.");
			return true;
		}

		public static bool isNameValid(string name)
		{
			if (name.Contains(" ")) throw new Exception("Name cannot contain space.");
			return true;
		}

		public static bool isEmailValid(string email)
		{
			try
			{
				var emailAddress = new MailAddress(email);
			}
			catch
			{
				throw new Exception("Not Valid Email Address.");
			}
			var emailCheck = TryPostCheckEmail(email, LocalUrl);
			if (emailCheck.Result)
			{
				throw new Exception("Email already exists");
			}
			return true;
		}

		public static bool isZipcodeValid(string zip)
		{
			string pattern = @"^(?=.*\d).{5}$";
			Regex r = new Regex(pattern);
			if (!r.IsMatch(zip)) throw new Exception("Invalid ZIP Format.");
			return true;
		}

		public static bool isCountryCodeValid(string countryCode)
		{
			if (!Enum.IsDefined(typeof(Countries), countryCode)) throw new Exception("Invalid Country Code");
			return true;
		}

		public static bool IsLoginValid(string username, string password)
		{
			var check = TryPostAuthentication(username, password,LocalUrl);
			if (!check.Result) throw new Exception("Username and Password Combo does not match!");
			return true;
		}

	}
}