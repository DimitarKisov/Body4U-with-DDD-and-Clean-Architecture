namespace Body4U.Application.Features.Articles.Queries.Get
{
    using Body4U.Application.Common;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetArticleQuery : IRequest<Result<GetArticleOutputModel>>
    {
        public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, Result<GetArticleOutputModel>>
        {
            private readonly IArticleRepository articleRepository;

            public GetArticleQueryHandler(IArticleRepository articleRepository)
                => this.articleRepository = articleRepository;

            public Task<Result<GetArticleOutputModel>> Handle(GetArticleQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
