namespace Body4U.Web.Features
{
    using Body4U.Application.Features.Articles.Commands.CreateArticle;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ArticleController : ApiController
    {
        [HttpPost]
        //TODO: [Authorize(Roles = TrainerRoleName)]
        [Route(nameof(Create))]
        public async Task<ActionResult<CreateArticleOutputModel>> Create(CreateArticleCommand command)
            => await this.Send(command);
    }
}
