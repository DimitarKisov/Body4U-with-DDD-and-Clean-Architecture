namespace Body4U.Infrastructure.Persistence.Repositories
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Articles;
    using Body4U.Application.Features.Articles.Queries.Get;
    using Body4U.Application.Features.Articles.Queries.Search;
    using Body4U.Domain.Models.Articles;
    using Microsoft.EntityFrameworkCore;
    using Serilog;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.System;

    internal class ArticleRepository : DataRepository<Article>, IArticleRepository
    {
        public ArticleRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<bool> HasArticleWithTitle(string title, CancellationToken cancellationToken)
            => await this.Data.Articles.AnyAsync(x => x.Title == title, cancellationToken);

        public async Task<Result<SearchArticlesOutputModel>> Search(SearchArticlesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = this.Data
                .Users
                .Where(x => x.Trainer != null)
                .Select(x => new
                {
                    FullName = x.FirstName + " " + x.LastName,
                    TrainerId = x.Trainer!.Id
                })
                .AsQueryable();

                var articles = this.Data
                    .Articles
                    .Select(x => new ArticleOutputModel
                    (
                        x.Id,
                        x.Title,
                        x.Content,
                        x.Image != null ? Convert.ToBase64String(x.Image) : default!,
                        x.CreatedOn,
                        users.First(y => y.TrainerId == x.TrainerId).FullName,
                        x.ArticleType.Value
                    ))
                    .AsQueryable();

                var totalRecords = await articles.CountAsync();

                var pageIndex = request.PageIndex;
                var pageSize = request.PageSize;
                var sortingOrder = request.OrderBy!;
                var sortingField = request.SortBy!;

                var orderBy = "Id";

                if (!string.IsNullOrWhiteSpace(sortingField))
                {
                    if (sortingField.ToLower() == "title")
                    {
                        orderBy = nameof(request.Title);
                    }
                    else if (sortingField.ToLower() == "author")
                    {
                        orderBy = nameof(request.Author);
                    }
                    else if (sortingField.ToLower() == "articletype")
                    {
                        orderBy = nameof(request.ArticleType);
                    }
                }

                if (sortingOrder != null && sortingOrder.ToLower() == Desc)
                {
                    articles = articles.OrderByDescending(x => orderBy);
                }
                else
                {
                    articles = articles.OrderBy(x => orderBy);
                }

                var data = await articles
                 .Skip(pageIndex * pageSize)
                 .Take(pageSize)
                 .ToListAsync(cancellationToken);

                return Result<SearchArticlesOutputModel>.SuccessWith(new SearchArticlesOutputModel(data, totalRecords));
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(ArticleRepository)}.{nameof(this.Search)}", ex);
                return Result<SearchArticlesOutputModel>.Failure(string.Format(Wrong, nameof(this.Search)));
            }
        }

        public async Task<Result<GetArticleOutputModel>> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                var article = await this.Data
                .Articles
                .Select(x => new
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Image = Convert.ToBase64String(x.Image),
                    CreatedOn = x.CreatedOn,
                    ArtcleType = x.ArticleType.Value,
                    TrainerId = x.TrainerId,
                    Author = this.Data.Users.First(y => y.Trainer!.Id == x.TrainerId).FullName,
                    AuthorProfilePicture = Convert.ToBase64String(this.Data.Users.First(y => y.Trainer!.Id == x.TrainerId).ProfilePicture!)
                })
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                var trainer = await this.Data
                    .Trainers
                    .FirstOrDefaultAsync(x => x.Id == article.TrainerId);

                var result = new GetArticleOutputModel
                    (
                        article.Id,
                        article.Title,
                        article.Content,
                        article.Image,
                        article.CreatedOn,
                        article.Author,
                        article.ArtcleType,
                        article.TrainerId,
                        trainer.ShortBio!,
                        article.AuthorProfilePicture,
                        trainer.FacebookUrl!,
                        trainer.InstagramUrl!,
                        trainer.YoutubeChannelUrl!
                    );

                return Result<GetArticleOutputModel>.SuccessWith(result);
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(ArticleRepository)}.{nameof(this.Get)}", ex);
                return Result<GetArticleOutputModel>.Failure(string.Format(Wrong, nameof(this.Get)));
            }
        }
    }
}
