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

                return await identityRepository.MyProfile(currentUserService.UserId, cancellationToken);
            }
        }
    }
}
