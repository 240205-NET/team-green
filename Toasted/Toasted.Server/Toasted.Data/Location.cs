using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Toasted.Data
{
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

        public string JsonThis() //this objects needs to be serialized into JSON format
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }






        public string ToString()
		{

			return SerializeJson(this);
		}
    }
}
