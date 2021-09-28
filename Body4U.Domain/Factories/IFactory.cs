namespace Body4U.Domain.Factories
{
    using Body4U.Domain.Common;

    public interface IFactory<TEntity>
        where TEntity : IAggregateRoot
    {
        TEntity Build();
    }
}
