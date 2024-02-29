using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace Toasted.Logic
{
    public class ToastedApiAsync
    {
        //this class should contain all static async methods for Toasted.Api
        public static async Task<bool> TryPostCheckUsername(string userName, string baseUrl) //should just past the base URL here, will add /UserCheck in code
        {

            // Create HttpClient instance
            using var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            // Serialize the username to JSON
            var content = new StringContent($"\"{userName}\"");

            // Set content type
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            // Make the POST request
            var response = await client.PostAsync("api/ExistingUser", content);

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


                

            public static async Task<bool> TryPostCheckEmail(string email, string baseUrl) 
            {

                // Create HttpClient instance
                using var client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);

                // Serialize the username to JSON
                var content = new StringContent($"\"{email}\"");

                // Set content type
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                // Make the POST request
                var response = await client.PostAsync("api/ExistingEmail", content);

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
                    throw new HttpRequestException($"Failed to check email. Status code: {response.StatusCode}");
                }
            }



        public static async Task<User> TryPostGetUser(string userName, string baseUrl) //should just past the base URL here, will add /UserCheck in code
        {

            // Create HttpClient instance
            using var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            // Serialize the username to JSON
            var content = new StringContent($"\"{userName}\"");

            // Set content type
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            // Make the POST request
            var response = await client.PostAsync("api/User", content);

            // Check if request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read response content as boolean
                User result = await response.Content.ReadAsAsync<User>();
                return result;
            }
            else
            {
                // Throw exception for unsuccessful response
                throw new HttpRequestException($"Failed to get user. Status code: {response.StatusCode}");
            }
        }





        //Get USER authentication. Send username and password as they are stored in the database.
        public static async Task<bool> TryPostAuthentication(string username, string encryptedPassword, string baseUrl)
        {
            bool authenticated = false;
            using var client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);

            string[] userPass = {username,encryptedPassword };
            var json = JsonConvert.SerializeObject(userPass);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Authentication", content);

            try
            {
              authenticated = await response.Content.ReadAsAsync<bool>();
            }
            catch
            {
                throw new HttpRequestException($"Server error. Status code: {response.StatusCode}");
            }





            return authenticated;
        }

        //Posts a new USER, should be used to create a new account as stored in a User object
        public static async Task<bool> TryPostNewAccount(User user, string baseUrl){


            // Create HttpClient instance
            using var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            //serialize
            string json = User.SerializeJson(user);
            var content =  new StringContent(json, Encoding.UTF8, "application/json");
            // Make the POST request
            var response = await client.PostAsync("api/Account", content);

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
                throw new HttpRequestException($"Failed to register user. Status code: {response.StatusCode}");
            }

      
        }

        //patch password
        public static async Task<bool> TryPatchPassword(string username, string encryptedPassword, string baseUrl)
        {
            bool patched = false;
            using var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            string[] userPass = { username, encryptedPassword };
            var json = JsonConvert.SerializeObject(userPass);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PatchAsync("api/EncryptedPassword", content);

            try
            {
                patched = await response.Content.ReadAsAsync<bool>();
            }
            catch
            {
                throw new HttpRequestException($"Server error. Status code: {response.StatusCode}");
            }
            return patched;

        }





        //patch location
        public static async Task<bool> TryPatchLocation(string username, Location location,string baseUrl)
        {
            LocationUpdateContainer locationUpdateContainer = new LocationUpdateContainer(username, location);
            var json = JsonConvert.SerializeObject(locationUpdateContainer);
            bool patched = false;
            using var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PatchAsync("api/Location", content);
            try
            {
                patched = await response.Content.ReadAsAsync<bool>();
            }
            catch
            {
                throw new HttpRequestException($"Server error. Status code: {response.StatusCode}");
            }
 
            return patched;



        }



        //patch temp unit
        public static async Task<bool> TryPatchTempUnit(string username, char tempUnit, string baseUrl)
        {
            bool patched = false;
            using var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            string[] userPass = { username, tempUnit.ToString() };
            var json = JsonConvert.SerializeObject(userPass);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PatchAsync("api/TempUnit", content);

            try
            {
                patched = await response.Content.ReadAsAsync<bool>();
            }
            catch
            {
                throw new HttpRequestException($"Server error. Status code: {response.StatusCode}");
            }
            return patched;


        }


    }
}
