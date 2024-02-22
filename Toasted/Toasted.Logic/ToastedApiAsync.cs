using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Toasted.Logic
{
	public class ToastedApiAsync
	{
		//this class should contain all static async methods for Toasted.Api

		public static async Task<bool> TryPostCheckUsername(string jsonData, string url)
		{
			Console.WriteLine("Posting to: " + url);
			// Create StringContent from the JSON data
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			// Create HttpClient instance
			using (HttpClient client = new HttpClient())
			{
				// Post data to the URL
				HttpResponseMessage response = await client.PostAsync(url, content);

				// Check response status
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Data posted successfully. Status code: " + response.StatusCode);

					// Read the response content as string
					string responseContent = await response.Content.ReadAsStringAsync();

					// Deserialize JSON response content into a dictionary
					Dictionary<string, string> dic = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);

					// Check if the dictionary contains a "boolResponse" key
					if (dic.ContainsKey("boolResponse"))
					{
						// Check the value associated with "boolResponse"
						if (dic["boolResponse"] != "true")
						{
							return true;
						}
						else
						{
							return false;
						}
					}
					else
					{
						// Handle the case where "boolResponse" key is missing
						Console.WriteLine("Missing 'boolResponse' key in the response.");
						return false; // Or throw an exception based on your requirement
					}
				}
				else
				{
					Console.WriteLine("Server Error, please try again " + response.StatusCode);
					return false;
				}
			}
		}
	}
}
