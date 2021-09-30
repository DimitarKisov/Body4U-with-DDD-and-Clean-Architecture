namespace Body4U.Infrastructure.Persistence.Seeders
{
    using System;
    using System.Threading.Tasks;

    internal interface ISeeder
    {
        Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
    }
}
