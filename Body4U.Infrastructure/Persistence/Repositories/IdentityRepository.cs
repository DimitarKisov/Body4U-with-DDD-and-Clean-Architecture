namespace Body4U.Infrastructure.Persistence.Repositories
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity;
    using Body4U.Application.Features.Identity.Queries;
    using Serilog;
    using System;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Account;
    using static Body4U.Application.Common.GlobalConstants.System;

    internal class IdentityRepository : DataRepository<IUser>, IIdentityRepository
    {
        public IdentityRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Result<MyProfileOutputModel>> MyProfile(string userId)
        {
            try
            {
                var user = await this.Data.Users.FindAsync(userId);
                if (user == null)
                {
                    return Result<MyProfileOutputModel>.Failure(string.Format(WrongId, userId));
                }

                var profilePicture = user.ProfilePicture != null 
                    ? Convert.ToBase64String(user.ProfilePicture) 
                    : null;

                return Result<MyProfileOutputModel>.SuccessWith(
                    new MyProfileOutputModel(
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    profilePicture,
                    user.Age,
                    user.PhoneNumber,
                    user.Gender.Value));
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(IdentityRepository)}.{nameof(this.MyProfile)}", ex);
                return Result<MyProfileOutputModel>.Failure(Wrong);
            }
        }
    }
}
