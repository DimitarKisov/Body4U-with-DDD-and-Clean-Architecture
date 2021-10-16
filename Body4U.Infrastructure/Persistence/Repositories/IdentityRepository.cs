namespace Body4U.Infrastructure.Persistence.Repositories
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Administration.Queries.Common;
    using Body4U.Application.Features.Administration.Queries.SearchRoles;
    using Body4U.Application.Features.Administration.Queries.SearchUsers;
    using Body4U.Application.Features.Identity;
    using Body4U.Application.Features.Identity.Queries;
    using Microsoft.EntityFrameworkCore;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Account;
    using static Body4U.Application.Common.GlobalConstants.System;

    internal class IdentityRepository : DataRepository<IUser>, IIdentityRepository
    {
        public IdentityRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IUser> Find(string userId, CancellationToken cancellationToken)
            => await this.Data.Users.Include(x => x.Trainer).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        public async Task<Result<MyProfileOutputModel>> MyProfile(string userId, CancellationToken cancellationToken)
        {
            try
            {
                var user = await this.Data.Users.Include(x => x.Trainer).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

                if (user == null)
                {
                    return Result<MyProfileOutputModel>.Failure(string.Format(WrongId, userId));
                }

                var profilePicture = user.ProfilePicture != null
                    ? Convert.ToBase64String(user.ProfilePicture)
                    : null;

                return Result<MyProfileOutputModel>.SuccessWith(
                    new MyProfileOutputModel(
                        user.Id,
                        user.FirstName,
                        user.LastName,
                        user.Email,
                        profilePicture,
                        user.Age,
                        user.PhoneNumber,
                        user.Gender.Value,
                        user.Trainer != null ? user.Trainer.Id : default!));
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(IdentityRepository)}.{nameof(this.MyProfile)}", ex);
                return Result<MyProfileOutputModel>.Failure(string.Format(Wrong, nameof(this.MyProfile)));
            }
        }

        public async Task<Result<SearchUsersOutputModel>> Users(SearchUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = this.Data
                .Users
                .Select(x => new UserOutputModel
                (
                    x.Id,
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    x.PhoneNumber,
                    this.Data.Roles
                    .Where(y => x.Roles
                        .Any(z => z.RoleId == y.Id))
                    .Select(y => new RolesOutputModel(y.Id, y.Name))
                ))
                .AsQueryable();

                var totalRecords = await users.CountAsync(cancellationToken);

                int pageIndex = request.PageIndex;
                int pageSize = request.PageSize;
                string sortingOrder = request.OrderBy!;
                string sortingField = request.SortBy!;

                var orderBy = "Id";

                if (!string.IsNullOrWhiteSpace(sortingField))
                {
                    if (sortingField.ToLower() == "firstname")
                    {
                        orderBy = nameof(request.FirstName);
                    }
                    else if (sortingField.ToLower() == "lastname")
                    {
                        orderBy = nameof(request.LastName);
                    }
                    else if (sortingField.ToLower() == "email")
                    {
                        orderBy = nameof(request.Email);
                    }
                    else if (sortingField.ToLower() == "phonenumber")
                    {
                        orderBy = nameof(request.PhoneNumber);
                    }
                }

                if (sortingOrder != null && sortingOrder.ToLower() == Desc)
                {
                    users = users.OrderByDescending(x => orderBy);
                }
                else
                {
                    users = users.OrderBy(x => orderBy);
                }

                var data = await users
                 .Skip(pageIndex * pageSize)
                 .Take(pageSize)
                 .ToListAsync(cancellationToken);

                return Result<SearchUsersOutputModel>.SuccessWith(new SearchUsersOutputModel(data, totalRecords));
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(IdentityRepository)}.{nameof(this.Users)}", ex);
                return Result<SearchUsersOutputModel>.Failure(string.Format(Wrong, nameof(this.Users)));
            }
        }

        public async Task<Result<IEnumerable<RolesOutputModel>>> Roles(SearchRolesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var roles = await this.Data.Roles.Select(x => new RolesOutputModel(x.Id, x.Name)).ToListAsync(cancellationToken);

                return Result<IEnumerable<RolesOutputModel>>.SuccessWith(roles);
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(IdentityRepository)}.{nameof(this.Roles)}", ex);
                return Result<IEnumerable<RolesOutputModel>>.Failure(string.Format(Wrong, nameof(this.Roles)));
            }
        }
    }
}
