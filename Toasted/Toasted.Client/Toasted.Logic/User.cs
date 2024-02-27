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

        public string countryCode { get; set; }

        public User(string username, string password, string firstName, string lastName, int userID, string email, Location defaultLocation, string CountryCode)
        {
            this.username = username;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.userID = userID;
            this.email = email;
            this.defaultLocation = defaultLocation;
            this.countryCode = CountryCode;
        }

        public static bool CheckExists(string username, string url)
        {
            //check availability of username, send serialized object to API
            //should call ASYNC function from an ASYNC class that returns TRUE or FALSE depending on availability by asking the API
            try
            {
                // Call the asynchronous method to check username availability
                var task = ToastedApiAsync.TryPostCheckUsername(username, url);

                Console.WriteLine("Attempting post...");

                // Block and wait for the task to complete synchronously
                bool exists = task.Result;

                // Return the availability status
                return exists;
            }
            catch (AggregateException ex)
            {
                // If the exception is an AggregateException, we want to throw it along with its inner exceptions
                throw ex;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the process
                Console.WriteLine(ex.Message);
                throw; // Re-throw the exception
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
