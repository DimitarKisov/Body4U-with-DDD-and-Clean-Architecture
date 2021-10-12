namespace Body4U.Application.Features.Administration.Queries.SearchRoles
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Administration.Queries.Common;
    using Body4U.Application.Features.Identity;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class SearchRolesQuery : IRequest<Result<IEnumerable<RolesOutputModel>>>
    {
        public class SearchRolesCommandHandler : IRequestHandler<SearchRolesQuery, Result<IEnumerable<RolesOutputModel>>>
        {
            private readonly IIdentityRepository identityRepository;

            public SearchRolesCommandHandler(IIdentityRepository identityRepository)
                => this.identityRepository = identityRepository;

            public async Task<Result<IEnumerable<RolesOutputModel>>> Handle(SearchRolesQuery request, CancellationToken cancellationToken)
                => await this.identityRepository.Roles(request, cancellationToken);
        }
    }
}
