namespace Body4U.Infrastructure.Persistence.Configurations
{
    using Body4U.Domain.Models.Trainers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class TrainerVideoConfiguration : IEntityTypeConfiguration<TrainerVideo>
    {
        public void Configure(EntityTypeBuilder<TrainerVideo> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.VideoUrl)
                .HasDefaultValue();
        }
    }
}
