namespace Body4U.Web.Features
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity;
    using Body4U.Application.Features.Identity.Commands.ChangePassword;
    using Body4U.Application.Features.Identity.Commands.CreateUser;
    using Body4U.Application.Features.Identity.Commands.EditUser;
    using Body4U.Application.Features.Identity.Commands.GenerateRefreshToken;
    using Body4U.Application.Features.Identity.Commands.LoginUser;
    using Body4U.Application.Features.Identity.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class IdentityController : ApiController
    {
        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult<IUser>> Register(CreateUserCommand command)
            => await this.Send(command);
        
        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginOutputModel>> Login(LoginUserCommand command)
            => await this.Send(command);

        [HttpPost]
        [Authorize]
        [Route(nameof(ChangePassword))]
        public async Task<ActionResult<Result>> ChangePassword(ChangePasswordCommand command)
            => await this.Send(command);

        [HttpPost]
        [Route(nameof(GenerateRefreshToken))]
        public async Task<ActionResult<GenerateRefreshTokenOutputModel>> GenerateRefreshToken(GenerateRefreshTokenCommand command)
            => await this.Send(command);

        [HttpPost]
        [Authorize]
        [Route(nameof(MyProfile))]
        public async Task<ActionResult<MyProfileOutputModel>> MyProfile(MyProfileQuery query)
            => await this.Send(query);

        [HttpPut]
        [Route(nameof(EditMyProfile))]
        public async Task<ActionResult<Result>> EditMyProfile(EditUserCommand command)
            => await this.Send(command);
    }
}
