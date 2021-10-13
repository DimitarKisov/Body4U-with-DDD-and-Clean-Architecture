namespace Body4U.Infrastructure.Persistence.Configurations
{
    using Body4U.Domain.Models.Articles;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Body4U.Domain.Models.ModelConstants.Article;

    internal class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Title)
                .HasMaxLength(MaxTitleLength)
                .IsRequired();

            builder
                .Property(x => x.Image)
                .IsRequired();

            builder
                .Property(x => x.Content)
                .HasMaxLength(MaxContentLength)
                .IsRequired();

            builder
                .OwnsOne(x => x.ArticleType, y =>
                  {
                      y.WithOwner();

                      y.Property(z => z.Value);
                  });
        }
    }
}
