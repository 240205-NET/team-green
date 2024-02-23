namespace Toasted.Logic
{
	using System;
	using System.Text.RegularExpressions;
	using System.Net.Mail;

	public class UserValidityChecks
	{
		public static bool IsUsernameValid(string username)
		{
			if (username.Equals("")) throw new Exception("username cannot be empty");
			else if (username.Contains(" ")) throw new Exception("username cannot have space");
			return true;
		}

		public static bool isPassWordValid(string password)
		{
			string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";
			Regex r = new Regex(pattern);
			if (!r.IsMatch(password)) throw new Exception("Invalid Password Format");
			return true;
		}

		public static bool isNameValid(string name)
		{
			if (name.Contains(" ")) throw new Exception("Name cannot contain space");
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
				throw new Exception("Not Valid Email Address");
			}
			return true;
		}

		public static bool isZipcodeValid(string zip)
		{
			string pattern = @"^(?=.*\d).{5}$";
			Regex r = new Regex(pattern);
			if (!r.IsMatch(zip)) throw new Exception("Invalid ZIP Format");
			return true;
		}

		public static bool isCountryCodeValid(string countryCode)
		{
			return Enum.IsDefined(typeof(Countries), countryCode);
		}

	}
}