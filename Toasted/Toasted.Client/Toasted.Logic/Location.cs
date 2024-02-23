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
		public Location(int zip, string name, long lat, long lon, string country)
		{
			this.zip = zip;
			this.name = name;
			this.lat = lat;
			this.lon = lon;
			this.country = country;
		}
	}
}