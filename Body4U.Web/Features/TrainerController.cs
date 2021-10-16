namespace Body4U.Web.Features
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Trainers.Commands.EditTrainer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.System;

    public class TrainerController : ApiController
    {
        [HttpPost]
        [Authorize(Roles = TrainerRoleName)]
        public async Task<ActionResult<Result>> Edit(EditTrainerCommand command)
            => await this.Send(command);
    }
}
