using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Toasted.Logic
{
	/// <summary>
	/// This class adheres to the API response on https://openweathermap.org/api/geocoding-api
	/// An example of such an API response is the following:
	/// {
	///		"zip": "90210",
	///		"name": "Beverly Hills",
	///		"lat": 34.0901,
	///		"lon": -118.4065,
	///		"country": "US"
	///	}
	/// </summary>
	public class Location
	{
		public int? zip { get; set; }
		public string? name { get; set; }
		public double? lat { get; set; }
		public double? lon { get; set; }
		public string? country { get; set; }


        public Location() { }
		public Location(int zip, string name, double lat, double lon, string country)
		{
			this.zip = zip;
			this.name = name;
			this.lat = lat;
			this.lon = lon;
			this.country = country;
		}

        public static string SerializeJson(Location location) //this objects needs to be serialized into JSON format
        {
            string json = JsonConvert.SerializeObject(location);
            return json;
        }
    }

	public class LocationUpdateContainer //This class is used to store a username and its potentially updated Location. This will be sent to the server to update the database.
	{
		public LocationUpdateContainer() { }

        public LocationUpdateContainer(string username, Location location) {
			this.username = username;
			this.location = location;
		}
        public string username { get; set; }
        public Location location { get; set; }

    }
}