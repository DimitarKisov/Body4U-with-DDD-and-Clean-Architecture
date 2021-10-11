namespace Body4U.Infrastructure.Persistence
{
    using Body4U.Domain.Models.Articles;
    using Body4U.Domain.Models.Trainers;
    using Body4U.Infrastructure.Identity.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    internal class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; } = default!;

        public DbSet<Trainer> Trainers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //#1
            base.OnModelCreating(modelBuilder);

            //#2
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
