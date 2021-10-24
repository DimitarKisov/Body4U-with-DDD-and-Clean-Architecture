namespace Body4U.Application.Features.Articles
{
    using Body4U.Application.Common;
    using Body4U.Application.Contracts;
    using Body4U.Application.Features.Articles.Commands.Edit;
    using Body4U.Application.Features.Articles.Queries.Get;
    using Body4U.Application.Features.Articles.Queries.Search;
    using Body4U.Application.Features.Trainers.Queries.MyArticles;
    using Body4U.Domain.Models.Articles;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IArticleRepository : IRepository<Article>
    {
        Task<bool> HasArticleWithTitle(string title, CancellationToken cancellationToken);

        Task<Result<SearchArticlesOutputModel>> Search(SearchArticlesQuery request, CancellationToken cancellationToken);

        Task<Result<GetArticleOutputModel>> Get(int id, CancellationToken cancellationToken);

        Task<Result<MyArticlesOutputModel>> MyArticles(MyArticlesQuery request, int trainerId, CancellationToken cancellationToken);

        Task<Result> Edit(EditArticleCommand request, string loggedInUserId, int loggedInTrainerId, CancellationToken cancellationToken);

        Task<Result> Delete(int id, string loggedInUserId, int loggedInTrainerId, CancellationToken cancellationToken);
    }
}
