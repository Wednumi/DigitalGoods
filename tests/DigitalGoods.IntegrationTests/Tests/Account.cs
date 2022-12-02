using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.IntegrationTests.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalGoods.IntegrationTests.Tests
{
    public class Account : ConfiguredTest
    {
        [Fact]
        public void Registration_with_valid_data_succeeded()
        {
            var validUser = CreateUser();

            var result = Registrate(validUser);

            Assert.True(result.Succeeded);
        }

        [Fact]
        public void Regstration_with_same_email_fails()
        {
            var firstUser = CreateUser(userName: "first");
            var secondUser = CreateUser(userName: "second");

            Registrate(firstUser);
            var result = Registrate(secondUser);

            Assert.False(result.Succeeded);
        }

        [Fact]
        public void Regstration_with_same_username_fails()
        {
            var firstUser = CreateUser(email: "first@mail.com");
            var secondUser = CreateUser(email: "second@mail.com");

            Registrate(firstUser);
            var result = Registrate(secondUser);

            Assert.False(result.Succeeded);
        }

        [Fact]
        public void SignIn_with_valid_data_succeeded()
        {
            var user = CreateUser();
            var password = "12345678";
            Registrate(user, password);

            var result = Authenticate(user, password);

            Assert.True(result.Succeeded);
        }

        [Fact]
        public void SignIn_with_invalid_password_fails()
        {
            var user = CreateUser();
            var password = "12345678";
            Registrate(user, password);

            var result = Authenticate(user, "invalid password");

            Assert.False(result.Succeeded);
        }

        private User CreateUser(string email = "testmail@mail.test", string userName = "TestUser")
        {
            return new User(email, userName);
        }

        private IdentityResult Registrate(User user, string password = "12345678")
        {
            var accountManager = ServiceProvider.GetRequiredService<IAccountManager>();

            return accountManager.RegisterAsync(user, password).Result;
        }

        private SignInResult Authenticate(User user, string password)
        {
            var accountManager = ServiceProvider.GetRequiredService<IAccountManager>();

            return accountManager.CheckForSignInAsync(user, password).Result;
        }
    }
}
