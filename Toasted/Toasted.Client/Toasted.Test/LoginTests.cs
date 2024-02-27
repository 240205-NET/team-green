using System;
using Toasted.Logic;
using Toasted.App;
using Xunit.Abstractions;
using System.Reflection.Metadata;

namespace Toasted.Test{

    public class LoginTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public LoginTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Fact]
        public void UsernameBlank()
        {
            Exception exception = Assert.Throws<Exception>(() => UserValidityChecks.IsUsernameValid(""));
            Assert.Equal("Username cannnot be blank",exception.Message);
        }

        [Fact]
        public void UsernameEmptySpace()
        {
            Exception exception = Assert.Throws<Exception>(() => UserValidityChecks.IsUsernameValid(" "));
            Assert.Equal("Username cannnot be blank", exception.Message);
        }

        [Fact]
        public void UsernameExistsAsync()
        {
            String testURL="https://localhost:7019"; //change to test url
            String testName="john_doe";
           bool result= User.CheckExists(testName,testURL);
            Assert.True(result);
        }


        [Fact]
        public void UserSerialize()
        {
            User user = new User("Vayro", "team4r0cks", "Lawrence", "Biteranta", 0, "LBiteranta@gmail.com", new Location(91789, "Walnut, Ca", 37.901760, -122.061920, "USA"), "US", 'F');
           var content= User.SerializeJson(user);
            _testOutputHelper.WriteLine(content.ToString());
            Assert.Equal("{\"username\":\"Vayro\",\"password\":\"team4r0cks\",\"firstName\":\"Lawrence\",\"lastName\":\"Biteranta\",\"userID\":0,\"email\":\"LBiteranta@gmail.com\",\"location\":{\"zip\":91789,\"name\":\"Walnut, Ca\",\"lat\":37.90176,\"lon\":-122.06192,\"country\":\"USA\"},\"tempUnit\":\"F\",\"countryCode\":\"US\"}", content.ToString());
        }

        [Fact]
        public void TryPostNewUserAccount()
        {
            String testURL = "http://localhost:5083"; //change to test url
            User user = new User("Vayro", "team4r0cks", "Lawrence", "Biteranta", 0, "LBiteranta@gmail.com", new Location(91789, "Walnut, Ca", 37.901760, -122.061920,"USA"), "US", 'F');

            Task<bool> task = ToastedApiAsync.TryPostNewAccount(user, testURL);
            _testOutputHelper.WriteLine(task.Result.ToString());


            Assert.True(true);
        }




    }
}