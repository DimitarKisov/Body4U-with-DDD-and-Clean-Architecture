namespace Body4U.Application.Features.Articles.Queries.Search
{
    using Body4U.Application.Common;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class SearchArticlesQuery : SearchInputModel, IRequest<Result<SearchArticlesOutputModel>>
    {
        public string? Title { get; set; }

        public string? Author { get; set; }

        public int? ArticleType { get; set; }

        public class SearchArticlesQueryHandler : IRequestHandler<SearchArticlesQuery, Result<SearchArticlesOutputModel>>
        {
            private readonly IArticleRepository articleRepository;

            public SearchArticlesQueryHandler(IArticleRepository articleRepository)
                => this.articleRepository = articleRepository;

            public async Task<Result<SearchArticlesOutputModel>> Handle(SearchArticlesQuery request, CancellationToken cancellationToken)
                => await this.articleRepository.Search(request, cancellationToken);
        }
    }
}
