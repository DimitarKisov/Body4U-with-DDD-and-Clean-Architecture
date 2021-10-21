namespace Body4U.Application.Features.Articles.Commands.Delete
{
    using Body4U.Application.Common;
    using Body4U.Application.Contracts;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Account;

    public class DeleteArticleCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Result>
        {
            private readonly IArticleRepository articleRepository;
            private readonly ICurrentUserService currentUserService;

            public DeleteArticleCommandHandler(
                IArticleRepository articleRepository,
                ICurrentUserService currentUserService)
            {
                this.articleRepository = articleRepository;
                this.currentUserService = currentUserService;
            }

            public async Task<Result> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
            {
                if (currentUserService.UserId == null || currentUserService.TrainerId == null)
                {
                    return Result.Failure(Unauthorized);
                }

                return await this.articleRepository.Delete(request.Id, currentUserService.UserId, (int)currentUserService.TrainerId, cancellationToken);
            }
        }
    }
}
