namespace Body4U.Application.Features.Articles.Commands.Edit
{
    using Body4U.Application.Common;
    using Body4U.Application.Contracts;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Account;

    public class EditArticleCommand : IRequest<Result>
    {
        public EditArticleCommand(
            int id,
            string title,
            string content,
            IFormFile image,
            int articleType,
            string sources)
        {
            this.Title = title;
            this.Content = content;
            this.Image = image;
            this.ArticleType = articleType;
            this.Sources = sources;
        }

        public int Id { get; }

        public string Title { get; }

        public string Content { get; }

        public IFormFile Image { get; }

        public int ArticleType { get; }

        public string Sources { get; }

        public class EditArticleCommandHandler : IRequestHandler<EditArticleCommand, Result>
        {
            private readonly IArticleRepository articleRepository;
            private readonly ICurrentUserService currentUserService;

            public EditArticleCommandHandler(
                IArticleRepository articleRepository,
                ICurrentUserService currentUserService)
            {
                this.articleRepository = articleRepository;
                this.currentUserService = currentUserService;
            }

            public async Task<Result> Handle(EditArticleCommand request, CancellationToken cancellationToken)
            {
                if (currentUserService.UserId == null || currentUserService.TrainerId == null)
                {
                    return Result.Failure(Unauthorized);
                }

                return await this.articleRepository.Edit(request, currentUserService.UserId, (int)currentUserService.TrainerId, cancellationToken);
            }
        }
    }
}
