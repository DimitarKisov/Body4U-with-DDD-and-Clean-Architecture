namespace Body4U.Infrastructure.Persistence.Configurations
{
    using Body4U.Infrastructure.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Body4U.Domain.Models.ModelContants.User;

    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .Property(x => x.FirstName)
                .HasMaxLength(MaxFirstNameLength)
                .IsRequired();

            builder
                .Property(x => x.LastName)
                .HasMaxLength(MaxLastNameLength)
                .IsRequired();

            builder
                .Property(x => x.Age)
                .HasMaxLength(MaxAge)
                .IsRequired();

            builder
                .Property(x => x.ProfilePicture)
                .IsRequired();

            builder
                .OwnsOne(x => x.Gender, y =>
                  {
                      y.WithOwner();

                      y.Property(z => z.Value);
                  });

            builder
                .HasOne(x => x.Trainer)
                .WithOne()
                .HasForeignKey<ApplicationUser>("TrainerId")
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey("ApplicationUserId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey("ApplicationUserId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey("ApplicationUserId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
