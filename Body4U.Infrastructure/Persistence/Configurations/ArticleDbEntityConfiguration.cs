namespace Body4U.Infrastructure.Persistence.Configurations
{
    using Body4U.Domain.Models.Articles;
    using Body4U.Infrastructure.Persistence.DbEntities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ArticleDbEntityConfiguration : IEntityTypeConfiguration<ArticleDbEntity>
    {
        public void Configure(EntityTypeBuilder<ArticleDbEntity> builder)
        {
            builder
                .HasBaseType<Article>();
        }
    }
}
