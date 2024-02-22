using System;
using Toasted.Logic;
using Toasted.App;

namespace Toasted.Test{

    public class LoginTests
    {
        [Fact]
        public void UsernameBlank()
        {
            Exception exception = Assert.Throws<Exception>(() => UsernameIsValid.IsUsernameValid(""));
            Assert.Equal("Username cannnot be blank",exception.Message);
        }

        [Fact]
        public void UsernameEmptySpace()
        {
            Exception exception = Assert.Throws<Exception>(() => UsernameIsValid.IsUsernameValid(" "));
            Assert.Equal("Username cannnot be blank", exception.Message);
        }

    }
}