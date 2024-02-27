using Newtonsoft.Json;
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
        public Location location { get; set; }
        //      private static XmlSerializer Serializer = new XmlSerializer(typeof(User));
        public char tempUnit { get; set; }

        public string countryCode { get; set; }

        public User(string username, string password, string firstName, string lastName, int userID, string email, Location defaultLocation, string CountryCode, char temperaturePreference)
        {
            this.username = username;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.userID = userID;
            this.email = email;
            this.location = defaultLocation;
            this.countryCode = CountryCode;
            this.tempUnit = temperaturePreference;
        }
        public User()
        {
            //required for serialization?
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



        public string ToString()
        {

            return SerializeJson(this);
        }


        //this works well but I actually am going to manually convert it into JSON because we will be now using a nested LOCATION object that I Want to also be in JSON format to store in our database.
        public static string SerializeJson(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            return json;
        }




        public static string SerializeToJsonCustom(User user)
        {
            // Construct the JSON string manually
            string jsonString = "{";

            // Add properties to the JSON string
            jsonString += "\"username\":\"" + user.username + "\",";
            jsonString += "\"password\":\"" + user.password + "\",";
            jsonString += "\"firstName\":\"" + user.firstName + "\",";
            jsonString += "\"lastName\":\"" + user.lastName + "\",";
            jsonString += "\"userID\":" + user.userID + ",";
            jsonString += "\"email\":\"" + user.email + "\",";
            jsonString += "\"location\":\"" + Location.SerializeJson(user.location) + "\","; //this is a nested json string... 
            jsonString += "\"temperaturePreference\":\"" + user.tempUnit + "\",";
            jsonString += "\"countryCode\":\"" + user.countryCode + "\"";

            // Close the JSON object
            jsonString += "}";

            return jsonString;




        }



















    }
}
