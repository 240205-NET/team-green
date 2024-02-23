using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Formatting;

namespace Toasted.Logic
{
	public class ToastedApiAsync
	{
		//this class should contain all static async methods for Toasted.Api
  public static async Task<bool> TryPostCheckUsername(string userName, string BaseUrl) //should just past the base URL here, will add /UserCheck in code
    {
		
        // Create HttpClient instance
        using var client = new HttpClient();
        client.BaseAddress = new Uri(BaseUrl);

        // Serialize the username to JSON
        var content = new StringContent($"\"{userName}\"");

        // Set content type
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        // Make the POST request
        var response = await client.PostAsync("UserCheck", content);

        // Check if request was successful
        if (response.IsSuccessStatusCode)
        {
            // Read response content as boolean
            var result = await response.Content.ReadAsAsync<bool>();
            return result;
        }
        else
        {
            // Throw exception for unsuccessful response
            throw new HttpRequestException($"Failed to check username. Status code: {response.StatusCode}");
        }
    }
		
	}
}
