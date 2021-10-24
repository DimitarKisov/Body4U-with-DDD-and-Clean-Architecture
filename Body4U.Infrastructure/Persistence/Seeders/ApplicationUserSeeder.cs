namespace Body4U.Infrastructure.Persistence.Seeders
{
    using Body4U.Infrastructure.Identity.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal class ApplicationUserSeeder : ISeeder
    {
        private readonly IConfiguration configuration;

        public ApplicationUserSeeder(IConfiguration configuration)
            => this.configuration = configuration;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var email = configuration.GetSection("SeedInfo")["Email"];
            var phoneNumber = configuration.GetSection("SeedInfo")["PhoneNumber"];
            var firstName = configuration.GetSection("SeedInfo")["FirstName"];
            var lastName = configuration.GetSection("SeedInfo")["LastName"];
            var age = int.Parse(configuration.GetSection("SeedInfo")["Age"]);
            var passsword = configuration.GetSection("SeedInfo")["Password"];

            await SeedUserAsync(userManager, email, phoneNumber, firstName, lastName, age, passsword);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string email, string phoneNumber, string firstName, string lastName, int age, string password)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                var newUser = new ApplicationUser(
                    email,
                    phoneNumber,
                    firstName,
                    lastName,
                    age,
                    null,
                    Gender.Male);

                newUser.EmailConfirmed = true;

                var result = await userManager.CreateAsync(newUser, password);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
