using System.Xml.Serialization;


namespace Toasted.Data
{
	public class User
	{
		public string username { get; set; }
		public string password { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public int userId { get; set; }
		public string email { get; set; }
		public Location location { get; set; } //default location choice
		private static XmlSerializer Serializer = new XmlSerializer(typeof(User));
		public char tempUnit { get; set; }
		
		public string countryCode { get; set; }


		public User(int userId,string username,string email,Location location,string firstName,string lastName,string password,char tempUnit,string countryCode)
		{
			this.countryCode = countryCode;
			this.username = username;	
			this.password = password;
			this.firstName = firstName;	
			this.lastName = lastName;	
			this.tempUnit = tempUnit;
			this.userId = userId;
			this.email = email;	
			this.location = location;	

		}
        public User()
        {
			userId = 0;

        }



    }
}