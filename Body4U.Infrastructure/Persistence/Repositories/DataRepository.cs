namespace Body4U.Infrastructure.Persistence.Repositories
{
    using Body4U.Application.Contracts;
    using Body4U.Domain.Common;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal abstract class DataRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IAggregateRoot
    {
        protected DataRepository(ApplicationDbContext dbContext) => this.Data = dbContext;

        protected ApplicationDbContext Data { get; }

        //protected IQueryable<TEntity> All() => this.Data.Set<TEntity>();

        public async Task Save(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Data.Update(entity);

            await this.Data.SaveChangesAsync(cancellationToken);
        }
    }
}
