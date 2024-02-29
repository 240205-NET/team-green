using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toasted.Logic;
using Toasted.App;
using Xunit.Abstractions;
using Newtonsoft.Json;

namespace Toasted.Test
{
    public class AsyncTests
    {

        private readonly ITestOutputHelper _testOutputHelper;
        String testURL = "http://localhost:5083"; //change to test url
        public AsyncTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void UsernameExists() //test USER class method
        {

            String testName = "john_doe";
            bool result = User.CheckExists(testName, testURL);
            Assert.True(result);
        }

        [Fact]
        public void EmailExists() //test EMAIL class method
        {

            String testName = "LBLUCK@GMAIL.com";
            bool result = ToastedApiAsync.TryPostCheckEmail(testName, testURL).Result;
            Assert.True(result);
        }
        [Fact]
        public void EmailExistsNOT() //test EMAIL class method
        {

            String testName = "LBLUCK34@GMAIL.com";
            bool result = ToastedApiAsync.TryPostCheckEmail(testName, testURL).Result;
            Assert.False(result);
        }


        [Fact]
        public void TryUsernameExistsAsync() //Tests ASYNC method
        {

            String testName = "Vayro";
            bool result = ToastedApiAsync.TryPostCheckUsername(testName, testURL).Result;
            Assert.True(result);
        }









        [Fact]
        public void GetUserAsync()
        {

            String userName = "Vayro";
       
                User user = ToastedApiAsync.TryPostGetUser(userName, testURL).Result;
                _testOutputHelper.WriteLine(user.ToString());
                Assert.Equal(userName, user.username);

            


          //  Assert.Fail("Cannot connect");
        }


        [Fact]
        public void UserSerialize()
        {
            User user = new User("Vayro", "team4r0cks", "Lawrence", "Biteranta", 0, "LBiteranta@gmail.com", new Location(91789, "Walnut, Ca", 37.901760, -122.061920, "USA"), "US", 'F');
            var content = User.SerializeJson(user);
            _testOutputHelper.WriteLine(content.ToString());
            Assert.Equal("{\"username\":\"Vayro\",\"password\":\"team4r0cks\",\"firstName\":\"Lawrence\",\"lastName\":\"Biteranta\",\"userID\":0,\"email\":\"LBiteranta@gmail.com\",\"location\":{\"zip\":91789,\"name\":\"Walnut, Ca\",\"lat\":37.90176,\"lon\":-122.06192,\"country\":\"USA\"},\"tempUnit\":\"F\",\"countryCode\":\"US\"}", content.ToString());
        }

        [Fact]
        public async void TryPostNewUserAccountAsync() //generates random user everytime test is ran
        {
            int seed = (int)DateTime.Now.Ticks;
            var rand = new Random(seed);
            string username = rand.Next(1,10000000).ToString(); //random test username
            string password = rand.Next(1, 999999999).ToString(); //random test username
            PasswordEncryptor.Encrypt(password);
            User user = new User($"TestUsername{username}", password, "Test", "User", 0, $"{username}@Test.com", new Location(91789, "Walnut, Ca", 37.901760, -122.061920, "USA"), "US", 'F');
            _testOutputHelper.WriteLine(user.ToString());
            bool result =await ToastedApiAsync.TryPostNewAccount(user, testURL);
           _testOutputHelper.WriteLine(result.ToString());



            Assert.True(true);
        }


        [Fact]
        public async void TryPatchPasswordAsync()
        {
            int seed = (int)DateTime.Now.Ticks;
            var rand = new Random(seed);
            string username = "Vayro";
            string password = "Test" + rand.Next(1, 999999999).ToString();
            bool result = await ToastedApiAsync.TryPatchPassword(username,password, testURL);
            Assert.True(result);
        }

        [Fact]
        public async void TryPatchLocationAsync()
        {

            Location local = new Location(91789, "Walnut, Ca", 37.901760, -122.061920, "USA");
            string username = "Vayro";
            LocationUpdateContainer locationUpdateContainer = new LocationUpdateContainer(username, local);
            var json = JsonConvert.SerializeObject(locationUpdateContainer);
            _testOutputHelper.WriteLine(json);
            bool result = await ToastedApiAsync.TryPatchLocation(username, local, testURL);
            Assert.True(result);
        }


        [Fact]
        public async void TryPatchTempUnitAsync()
        {
            string username = "jane_smith";
            char cf = 'C';
            bool result = await ToastedApiAsync.TryPatchTempUnit(username, cf, testURL);
            Assert.True(result);
        }

    }
}
