using System;
using Toasted.Logic;
using Toasted.App;
using Xunit.Abstractions;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Toasted.Test{

    public class LoginTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        String testURL = "https://localhost:5083"; //change to test url
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


 




    }
}