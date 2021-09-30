namespace Body4U.Infrastructure.Persistence.Seeders
{
    using Body4U.Infrastructure.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.System;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedRoleAsync(roleManager, AdministratorRoleName);
            await SeedRoleAsync(roleManager, TrainerRoleName);
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            try
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    var result = await roleManager.CreateAsync(new ApplicationRole(roleName));

                    if (!result.Succeeded)
                    {
                        //TODO: Log
                    }
                }
            }
            catch (Exception)
            {
                //TODO: Log
            }
        }
    }
}
