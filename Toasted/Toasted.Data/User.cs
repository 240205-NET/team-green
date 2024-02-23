using System.Xml.Serialization;

namespace Toasted.Data
{
	public class User
	{
		public string username { get; set; }
		public string password { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public int userID { get; set; }
		public string email { get; set; }
		public int defaultLocation { get; set; }
		private static XmlSerializer Serializer = new XmlSerializer(typeof(User));
		public char temperaturePreference { get; set; }
		
		public string countryCode { get; set; }
	}
}