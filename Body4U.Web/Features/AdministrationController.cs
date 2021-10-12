namespace Body4U.Web.Features
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Administration.Commands;
    using Body4U.Application.Features.Administration.Queries.Common;
    using Body4U.Application.Features.Administration.Queries.SearchRoles;
    using Body4U.Application.Features.Administration.Queries.SearchUsers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.System;

    [Authorize(Roles = AdministratorRoleName)]
    public class AdministrationController : ApiController
    {
        [HttpPost]
        [Route(nameof(Users))]
        public async Task<ActionResult<SearchUsersOutputModel>> Users([FromQuery] SearchUsersQuery query)
            => await this.Send(query);

        [HttpPost]
        [Route(nameof(Roles))]
        public async Task<ActionResult<IEnumerable<RolesOutputModel>>> Roles(SearchRolesQuery query)
            => await this.Send(query);

        [HttpPost]
        [Route(nameof(EditUserRoles))]
        public async Task<ActionResult<Result>> EditUserRoles(EditUserRolesCommand command)
            => await this.Send(command);
    }
}
