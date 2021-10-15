namespace Body4U.Application.Features.Articles.Commands.CreateArticle
{
    using Body4U.Application.Common;
    using Body4U.Domain.Common;
    using Body4U.Domain.Factories.Articles;
    using Body4U.Domain.Models.Articles;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Article;
    using static Body4U.Application.Common.GlobalConstants.System;

    public class CreateArticleCommand : IRequest<Result<CreateArticleOutputModel>>
    {
        public CreateArticleCommand(
            string title,
            IFormFile image,
            string content,
            int articleType,
            string sources)
        {
            this.Title = title;
            this.Image = image;
            this.Content = content;
            this.ArticleType = articleType;
            this.Sources = sources;
        }

        public string Title { get; }

        public IFormFile Image { get; }

        public string Content { get; }

        public int ArticleType { get; }

        public string Sources { get; }

        public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Result<CreateArticleOutputModel>>
        {
            private readonly IArticleRepository articleRepository;
            private readonly IArticleFactory articleFactory;

            public CreateArticleCommandHandler(
                IArticleRepository articleRepository,
                IArticleFactory articleFactory)
            {
                this.articleRepository = articleRepository;
                this.articleFactory = articleFactory;
            }

            public async Task<Result<CreateArticleOutputModel>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
            {
                //TODO: Да допиша дали треньорът има право да създава статия, след като добавя репо за треньори

                var isTitleTaken = await this.articleRepository.HasArticleWithTitle(request.Title, cancellationToken);
                if (isTitleTaken)
                {
                    return Result<CreateArticleOutputModel>.Failure(ArticleTitleExsists);
                }

                var imageResult = this.ImageConverter(request.Image);
                if (!imageResult.Succeeded)
                {
                    return Result<CreateArticleOutputModel>.Failure(imageResult.Errors);
                }

                var allowedArticleValues = Enumeration.GetAll<ArticleType>().Select(x => x.Value);

                if (!allowedArticleValues.Any(x => x == request.ArticleType))
                {
                    return Result<CreateArticleOutputModel>.Failure(WrongArticleType);
                }

                var articleType = Enumeration.FromValue<ArticleType>(request.ArticleType);

                var article = this.articleFactory
                    .WithTitle(request.Title)
                    .WithImage(imageResult.Data)
                    .WithContent(request.Content)
                    .WithType(articleType)
                    .WithSources(request.Sources)
                    .Build();

                await this.articleRepository.Save(article, cancellationToken);

                return Result<CreateArticleOutputModel>.SuccessWith(new CreateArticleOutputModel(article.Id));
            }

            private Result<byte[]> ImageConverter(IFormFile file)
            {
                if (file == null || file.Length == 0)
                {
                    return Result<byte[]>.Failure(FileIsEmpty);
                }
                else if (file != null && file.ContentType != "image/jpeg" && file.ContentType != "image/png")
                {
                    return Result<byte[]>.Failure(WrongImageFormat);
                }

                var result = new byte[file!.Length];

                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    result = stream.ToArray();
                }

                return Result<byte[]>.SuccessWith(result);
            }
        }
    }
}
