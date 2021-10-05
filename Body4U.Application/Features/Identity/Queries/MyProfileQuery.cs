namespace Body4U.Application.Features.Identity.Queries
{
    using Body4U.Application.Common;
    using Body4U.Application.Contracts;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Account;

    public class MyProfileQuery : IRequest<Result<MyProfileOutputModel>>
    {
        public class MyProfileQueryHandler : IRequestHandler<MyProfileQuery, Result<MyProfileOutputModel>>
        {
            private readonly IIdentityRepository identityRepository;
            private readonly ICurrentUserService currentUserService;

            public MyProfileQueryHandler(
                IIdentityRepository identityRepository,
                ICurrentUserService currentUserService)
            {
                this.identityRepository = identityRepository;
                this.currentUserService = currentUserService;
            }

            public async Task<Result<MyProfileOutputModel>> Handle(MyProfileQuery request, CancellationToken cancellationToken)
            {
                if (currentUserService.UserId == null)
                {
                    return Result<MyProfileOutputModel>.Failure(Unauthorized);
                }

                var myProfleData = await identityRepository.MyProfile(currentUserService.UserId);
                if (!myProfleData.Succeeded)
                {
                    return Result<MyProfileOutputModel>.Failure(myProfleData.Errors);
                }

                var result = new MyProfileOutputModel(
                    myProfleData.Data.FirstName,
                    myProfleData.Data.LastName,
                    myProfleData.Data.Email,
                    myProfleData.Data.ProfilePicture,
                    myProfleData.Data.Age,
                    myProfleData.Data.PhoneNumber,
                    myProfleData.Data.Gender);

                return Result<MyProfileOutputModel>.SuccessWith(result);
            }
        }
    }
}
