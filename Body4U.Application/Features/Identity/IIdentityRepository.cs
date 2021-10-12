namespace Body4U.Application.Features.Identity
{
    using Body4U.Application.Common;
    using Body4U.Application.Contracts;
    using Body4U.Application.Features.Administration.Queries.SearchUsers;
    using Body4U.Application.Features.Identity.Queries;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IIdentityRepository : IRepository<IUser>
    {
        Task<Result<MyProfileOutputModel>> MyProfile(string userId, CancellationToken cancellationToken);

        Task<IUser> Find(string userId, CancellationToken cancellationToken);

        Task<Result<SearchUsersOutputModel>> Users(SearchUsersQuery request, CancellationToken cancellationToken);
    }
}
