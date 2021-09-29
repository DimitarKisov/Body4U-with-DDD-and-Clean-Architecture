namespace Body4U.Infrastructure.Persistence.Configurations
{
    using Body4U.Domain.Models.Trainers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Body4U.Domain.Models.ModelContants.Trainer;

    internal class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Bio)
                .HasMaxLength(MaxBioLength);

            builder
                .Property(x => x.ShortBio)
                .HasMaxLength(MaxShortBioLength);

            builder
                .Property(x => x.IsReadyToVisualize)
                .HasDefaultValue(false);

            builder
                .Property(x => x.IsReadyToWrite)
                .HasDefaultValue(false);

            builder
                .HasMany(x => x.Articles)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict)
                .Metadata
                .PrincipalToDependent
                .SetField("articles");

            builder
                .HasMany(x => x.TrainerImages)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict)
                .Metadata
                .PrincipalToDependent
                .SetField("trainerImages");

            builder
                .HasMany(x => x.TrainerVideos)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict)
                .Metadata
                .PrincipalToDependent
                .SetField("trainerVideos");
        }
    }
}
