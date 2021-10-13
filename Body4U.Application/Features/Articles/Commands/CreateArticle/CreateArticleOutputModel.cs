namespace Body4U.Application.Features.Articles.Commands.CreateArticle
{
    public class CreateArticleOutputModel
    {
        public CreateArticleOutputModel(int id)
            => this.Id = id;

        public int Id { get; }
    }
}
