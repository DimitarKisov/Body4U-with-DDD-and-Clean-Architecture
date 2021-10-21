namespace Body4U.Application.Features.Articles.Commands.Create
{
    public class CreateArticleOutputModel
    {
        public CreateArticleOutputModel(int id)
            => this.Id = id;

        public int Id { get; }
    }
}
