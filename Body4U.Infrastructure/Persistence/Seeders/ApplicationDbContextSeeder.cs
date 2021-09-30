namespace Body4U.Infrastructure.Persistence.Seeders
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class ApplicationDbContextSeeder : ISeeder
    {
        private readonly IConfiguration configuration;

        public ApplicationDbContextSeeder(IConfiguration configuration) => this.configuration = configuration;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                //TODO: Log
                return;
            }

            if (serviceProvider == null)
            {
                //TODO: Log
                return;
            }

            var seeders = new List<ISeeder>
                {
                    new ApplicationUserSeeder(configuration),
                    new RolesSeeder(),
                    new RoleToApplicationUserSeeder(configuration),
                };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
