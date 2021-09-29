namespace Body4U.Infrastructure.Persistence
{
    using Body4U.Domain.Models.Articles;
    using Body4U.Domain.Models.Trainers;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; } = default!;

        public DbSet<Trainer> Trainers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
