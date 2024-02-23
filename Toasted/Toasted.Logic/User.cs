using System.Xml.Serialization;


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
        private static XmlSerializer Serializer = new XmlSerializer(typeof(User));
        public char temperaturePreference { get; set; }

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








        public  static bool checkAvailability(string username)
        {
            //check availability of username, send serialized object to API
            //should call ASYNC function from an ASYNC class that returns TRUE or FALSE depending on availability by asking the API
                    // Post the JSON data to the specified URL
        String url="";
        try
        {
            var task = ToastedApiAsync.TryPostCheckUsername(username,url);
            Console.WriteLine("Attempting post...");
            task.Wait();
            return task.Result;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        }






















        public string[] ToString()
        {

            return SerializeXML(this);
        }

        public static string[] SerializeXML(User user)
        {
            var stringWriter = new StringWriter();
            Serializer.Serialize(stringWriter, user);
            stringWriter.Close();
            string[] s = { stringWriter.ToString() };
            return s;
        }




    }
}
