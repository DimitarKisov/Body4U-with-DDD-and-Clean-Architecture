namespace Body4U.Infrastructure.Persistence.Configurations
{
    using Body4U.Domain.Models.Trainers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class TrainerImageConfiguration : IEntityTypeConfiguration<TrainerImage>
    {
        public void Configure(EntityTypeBuilder<TrainerImage> builder)
        {
            builder
                .HasKey(x => x.Id);
        }
    }
}
