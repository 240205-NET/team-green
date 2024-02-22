namespace Toasted.Logic
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }  
        public string lastName { get; set; }
        public int userID { get; set; }
        public string email { get; set; }
        public Location defaultLocation { get; set; }

        public User(string username, string password, string firstName, string lastName, int userID, string email, Location defaultLocation)
        {
            this.username = username;
            this.password = password;    
            this.firstName = firstName;
            this.lastName = lastName;
            this.userID = userID;
            this.email = email;
            this.defaultLocation = defaultLocation;
        }

    }
}
