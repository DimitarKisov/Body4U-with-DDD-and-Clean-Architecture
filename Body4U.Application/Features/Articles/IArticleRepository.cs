namespace Body4U.Application.Features.Articles
{
    using Body4U.Application.Common;
    using Body4U.Application.Contracts;
    using Body4U.Application.Features.Articles.Queries.Get;
    using Body4U.Application.Features.Articles.Queries.Search;
    using Body4U.Domain.Models.Articles;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IArticleRepository : IRepository<Article>
    {
        Task<bool> HasArticleWithTitle(string title, CancellationToken cancellationToken);

        Task<Result<SearchArticlesOutputModel>> Search(SearchArticlesQuery request, CancellationToken cancellationToken);

        Task<Result<GetArticleOutputModel>> Get(int id, CancellationToken cancellationToken);
    }
}
