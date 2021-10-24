namespace Body4U.Application.Features.Trainers.Queries.MyArticles
{
    using Body4U.Application.Common;
    using Body4U.Application.Contracts;
    using Body4U.Application.Features.Articles;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Account;

    public class MyArticlesQuery : SearchInputModel, IRequest<Result<MyArticlesOutputModel>>
    {
        public string? Title { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ArticleType { get; set; }

        public class MyArticlesQueryHandler : IRequestHandler<MyArticlesQuery, Result<MyArticlesOutputModel>>
        {
            private readonly IArticleRepository articleRepository;
            private readonly ICurrentUserService currentUserService;

            public MyArticlesQueryHandler(
                IArticleRepository articleRepository,
                ICurrentUserService currentUserService)
            {
                this.articleRepository = articleRepository;
                this.currentUserService = currentUserService;
            }

            public async Task<Result<MyArticlesOutputModel>> Handle(MyArticlesQuery request, CancellationToken cancellationToken)
            {
                if (currentUserService.TrainerId == null)
                {
                    return Result<MyArticlesOutputModel>.Failure(Unauthorized);
                }

                return await this.articleRepository.MyArticles(request, (int)currentUserService.TrainerId, cancellationToken);
            }
        }
    }
}
