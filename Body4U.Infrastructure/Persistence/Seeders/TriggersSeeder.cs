namespace Body4U.Infrastructure.Persistence.Seeders
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

    internal class TriggersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var triggerQuery = @"
                      CREATE TRIGGER Articles_Insert 
                      ON  [Body4U_DDD].[dbo].[Articles]
                      AFTER INSERT AS
                      BEGIN
                        SET NOCOUNT ON;
                        UPDATE Articles
                        SET Discriminator = 'ArticleDbEntity'
                        WHERE Id = (SELECT TOP(1) Id FROM Articles ORDER BY CreatedOn DESC)
                      END";

            await dbContext.Database.ExecuteSqlRawAsync(triggerQuery);
        }
    }
}
