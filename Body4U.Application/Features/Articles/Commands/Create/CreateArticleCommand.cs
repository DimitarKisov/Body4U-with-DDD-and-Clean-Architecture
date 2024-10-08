﻿namespace Body4U.Application.Features.Articles.Commands.Create
{
    using Body4U.Application.Common;
    using Body4U.Application.Contracts;
    using Body4U.Application.Features.Trainers;
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
    using static Body4U.Application.Common.GlobalConstants.Trainer;
    using static Body4U.Application.Common.GlobalConstants.System;

    public class CreateArticleCommand : IRequest<Result<CreateArticleOutputModel>>
    {
        public string Title { get; set; } = default!;

        public IFormFile Image { get; set; } = default!;

        public string Content { get; set; } = default!;

        public int ArticleType { get; set; }

        public string Sources { get; set; } = default!;

        public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Result<CreateArticleOutputModel>>
        {
            private readonly IArticleRepository articleRepository;
            private readonly IArticleFactory articleFactory;
            private readonly ICurrentUserService currentUserService;
            private readonly ITrainerRepository trainerRepository;

            public CreateArticleCommandHandler(
                IArticleRepository articleRepository,
                IArticleFactory articleFactory,
                ICurrentUserService currentUserService,
                ITrainerRepository trainerRepository)
            {
                this.articleRepository = articleRepository;
                this.articleFactory = articleFactory;
                this.currentUserService = currentUserService;
                this.trainerRepository = trainerRepository;
            }

            public async Task<Result<CreateArticleOutputModel>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
            {
                if (currentUserService.TrainerId == default)
                {
                    return Result<CreateArticleOutputModel>.Failure(NotTrainer);
                }

                var trainerResult = await this.trainerRepository.Find((int)currentUserService.TrainerId!, cancellationToken);
                if (!trainerResult.Data.IsReadyToWrite)
                {
                    return Result<CreateArticleOutputModel>.Failure(InfoMissing);
                }

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

                trainerResult.Data.AddArticle(article);

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
