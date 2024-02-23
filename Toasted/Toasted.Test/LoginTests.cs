using System;
using Toasted.Logic;
using Toasted.App;

namespace Toasted.Test{

    public class LoginTests
    {
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
        /* Should make a real test to test User.checkAvailabilityReturn() at some point
        [Fact]
        public void checkAvailabilityReturn()
        {
           // User = new User();
            bool r = User.checkAvailability("test","http://localhost:5083/");
            Assert.Fail(r.ToString());
        }
        */ 
    }
}