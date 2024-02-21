namespace Toasted.Logic
{
	using System;
	using System.Text.RegularExpressions;

	public class UsernameIsValid
	{
		public bool IsUsernameValid(string username)
		{
			//RegEx expression
			string pattern = @"^\w+$";
			Regex regex = new Regex(pattern);
			
			//check if matches and return bool
			return regex.IsMatch(username);
		}
	}
}