namespace Body4U.Infrastructure.Persistence.DbEntities
{
    using Body4U.Domain.Models.Articles;

    public class ArticleDbEntity : Article
    {
        public ArticleDbEntity(
            string title,
            byte[] image,
            string content,
            string? sources,
            ArticleType articleType)
            : base(title, image, content, sources, articleType)
        {
        }

        public int TrainerId { get; private set; }
    }
}
