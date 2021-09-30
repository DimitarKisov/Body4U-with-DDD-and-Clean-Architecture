namespace Body4U.Web.Features
{
    using Body4U.Application.Features.Identity.Commands.CreateUser;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class IdentityController : ApiController
    {
        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(CreateUserCommand command)
        {
            return await this.Send(command);
        }
    }
}
