namespace Body4U.Web.Features
{
    using Body4U.Application.Features.Articles.Commands.CreateArticle;
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
    }
}
