namespace Body4U.Web.Features
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Trainers.Commands.EditTrainer;
    using Body4U.Application.Features.Trainers.Queries.MyArticles;
    using Body4U.Application.Features.Trainers.Queries.MyPhotos;
    using Body4U.Application.Features.Trainers.Queries.MyTrainerProfile;
    using Body4U.Application.Features.Trainers.Queries.MyVideos;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
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
        [Route(nameof(MyArticles))]
        public async Task<ActionResult<MyArticlesOutputModel>> MyArticles([FromQuery] MyArticlesQuery query)
            => await this.Send(query);

        [HttpPost]
        [Authorize(Roles = TrainerRoleName)]
        [Route(nameof(Edit))]
        public async Task<ActionResult<Result>> Edit(EditTrainerCommand command)
            => await this.Send(command);

        [HttpPost]
        [Authorize(Roles = TrainerRoleName)]
        [Route(nameof(MyPhotos))]
        public async Task<ActionResult<IEnumerable<MyPhotosOutputModel>>> MyPhotos(MyPhotosQuery query)
            => await this.Send(query);

        [HttpPost]
        [Authorize(Roles = TrainerRoleName)]
        [Route(nameof(MyVideos))]
        public async Task<ActionResult<IEnumerable<MyVideosOutputModel>>> MyVideos(MyVideosQuery query)
            => await this.Send(query);
    }
}
