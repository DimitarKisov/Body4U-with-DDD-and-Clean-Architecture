namespace Body4U.Application.Features.Administration.Queries.SearchUsers
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class SearchUsersQuery : SearchInputModel, IRequest<Result<SearchUsersOutputModel>>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, Result<SearchUsersOutputModel>>
        {
            private readonly IIdentityRepository identityRepository;

            public SearchUsersQueryHandler(IIdentityRepository identityRepository)
                => this.identityRepository = identityRepository;

            public async Task<Result<SearchUsersOutputModel>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
                => await this.identityRepository.Users(request, cancellationToken);
        }
    }
}
