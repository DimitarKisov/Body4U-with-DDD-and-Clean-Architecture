namespace Body4U.Web.Features
{
    using Body4U.Application.Features.Identity.Commands.CreateUser;
    using Body4U.Application.Features.Identity.Commands.LoginUser;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class IdentityController : ApiController
    {
        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(CreateUserCommand command)
            => await this.Send(command);
        
        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginOutputModel>> Login(LoginUserCommand command)
            => await this.Send(command);
    }
}
