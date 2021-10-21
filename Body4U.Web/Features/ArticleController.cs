namespace Body4U.Web.Features
{
    using Body4U.Application.Features.Articles.Commands.Create;
    using Body4U.Application.Features.Articles.Commands.Delete;
    using Body4U.Application.Features.Articles.Commands.Edit;
    using Body4U.Application.Features.Articles.Queries.Search;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.System;

    public class ArticleController : ApiController
    {
        [HttpPost]
        [Authorize(Roles = TrainerRoleName)]
        [Route(nameof(Create))]
        public async Task<ActionResult<CreateArticleOutputModel>> Create(CreateArticleCommand command)
            => await this.Send(command);

        [HttpPost]
        [Route(nameof(Search))]
        public async Task<ActionResult<SearchArticlesOutputModel>> Search([FromQuery] SearchArticlesQuery query)
            => await this.Send(query);

        [HttpPut]
        [Authorize(Roles = TrainerRoleName)]
        [Route(nameof(Edit))]
        public async Task<ActionResult> Edit(EditArticleCommand command)
            => await this.Send(command);

        [HttpDelete]
        [Authorize(Roles = TrainerRoleName)]
        [Route(nameof(Delete))]
        public async Task<ActionResult> Delete(DeleteArticleCommand command)
            => await this.Send(command);
    }
}
