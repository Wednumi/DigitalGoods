using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.IntegrationTests.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalGoods.IntegrationTests.Tests
{
    public class Account : ConfiguredTest
    {
        private User CreateUser(string email = "testmail@mail.test", string userName = "TestUser")
        {
            return new User(email, userName);
        }

        private async Task<IdentityResult> Registrate(User user, string password = "12345678")
        {
            var accountManager = ServiceProvider.GetRequiredService<IAccountManager>();

            return await accountManager.Register(user, password);
        }

        private async Task<SignInResult> Authenticate(User user, string password)
        {
            var accountManager = ServiceProvider.GetRequiredService<IAccountManager>();

            return await accountManager.CheckForSignInAsync(user, password);
        }

        [Fact]
        public async Task Registration_with_valid_data_succeededAsync()
        {
            var validUser = CreateUser();

            var result = await Registrate(validUser);

            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task Regstration_with_same_email_failsAsync()
        {
            var firstUser = CreateUser(userName: "first");
            var secondUser = CreateUser(userName: "second");

            await Registrate(firstUser);
            var result = await Registrate(secondUser);

            Assert.False(result.Succeeded);
        }

        [Fact]
        public async Task Regstration_with_same_username_failsAsync()
        {
            var firstUser = CreateUser(email: "first@mail.com");
            var secondUser = CreateUser(email: "second@mail.com");

            await Registrate(firstUser);
            var result = await Registrate(secondUser);

            Assert.False(result.Succeeded);
        }

        [Fact]
        public async Task SignIn_with_valid_data_succeededAsync()
        {
            var user = CreateUser();
            var password = "12345678";
            await Registrate(user, password);

            var result = await Authenticate(user, password);

            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task SignIn_with_invalid_password_failsAsync()
        {
            var user = CreateUser();
            var password = "12345678";
            await Registrate(user, password);

            var result = await Authenticate(user, "87654321");

            Assert.False(result.Succeeded);
        }
    }
}
