namespace Body4U.Infrastructure.Persistence.Repositories
{
    using Body4U.Application.Features.Articles;
    using Body4U.Domain.Models.Articles;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ArticleRepository : DataRepository<Article>, IArticleRepository
    {
        public ArticleRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<bool> HasArticleWithTitle(string title, CancellationToken cancellationToken)
            => await this.Data.Articles.AnyAsync(x => x.Title == title, cancellationToken);
    }
}
