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
            ArticleType articleType,
            int trainerId)
            : base(title, image, content, sources, articleType)
        {
            this.TrainerId = trainerId;
        }

        private ArticleDbEntity(
            string title,
            byte[] image,
            string content,
            string? sources,
            int trainerId)
            : base(title, image, content, sources)
        {
            this.TrainerId = trainerId;
        }

        public int TrainerId { get; }
    }
}
