namespace Body4U.Web.Features
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Trainers.Commands.EditTrainer;
    using Body4U.Application.Features.Trainers.Queries.MyTrainerProfile;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.System;

    public class TrainerController : ApiController
    {
        [HttpPost]
        [Authorize(Roles = TrainerRoleName)]
        [Route(nameof(MyTrainerProfile))]
        public async Task<ActionResult<MyTrainerProfileOutputModel>> MyTrainerProfile(MyTrainerProfileQuery query)
            => await this.Send(query);

        [HttpPost]
        [Authorize(Roles = TrainerRoleName)]
        [Route(nameof(Edit))]
        public async Task<ActionResult<Result>> Edit(EditTrainerCommand command)
            => await this.Send(command);

        //TODO: MyArticle и MyPhotos; Проверка на всички останали TODO-та и приключване
    }
}
