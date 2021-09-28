namespace Body4U.Domain.Factories.Articles
{
    using Body4U.Domain.Models.Articles;

    public interface IArticleFactory : IFactory<Article>
    {
        IArticleFactory WithTitle(string title);

        IArticleFactory WithImage(byte[] image);

        IArticleFactory WithContent(string content);

        IArticleFactory WithType(ArticleType articleType);

        IArticleFactory WithSources(string sources);
    }
}
