namespace Body4U.Web.Features
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity.Commands.ChangePassword;
    using Body4U.Application.Features.Identity.Commands.CreateUser;
    using Body4U.Application.Features.Identity.Commands.EditUser;
    using Body4U.Application.Features.Identity.Commands.GenerateRefreshToken;
    using Body4U.Application.Features.Identity.Commands.LoginUser;
    using Body4U.Application.Features.Identity.Commands.SendEmailConfirmation;
    using Body4U.Application.Features.Identity.Commands.VerifyEmail;
    using Body4U.Application.Features.Identity.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class IdentityController : ApiController
    {
        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult<Result>> Register(CreateUserCommand command)
        {
            var registrationResult = await this.Send(command);

            #region Logic
            if (registrationResult.Value != null)
            {
                var confirmationLink = Url.Action(nameof(this.VerifyEmail), "Identity",
                new { userId = registrationResult.Value.UserId, token = registrationResult.Value.Token }, Request.Scheme, Request.Host.ToString());

                registrationResult.Value.UpdateConfirmationLink(confirmationLink);

                var sendMailResult = await this.SendEmailConfirmation(new SendEmailConfirmationCommand(registrationResult.Value.Email, confirmationLink));

                if (sendMailResult.Value != null && sendMailResult.Value.Succeeded)
                {
                    return sendMailResult;
                }

                return new ActionResult<Result>(sendMailResult.Result);

            }
            #endregion

            return registrationResult.Result;
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginOutputModel>> Login(LoginUserCommand command)
            => await this.Send(command);

        [HttpPut]
        [Authorize]
        [Route(nameof(ChangePassword))]
        public async Task<ActionResult<Result>> ChangePassword(ChangePasswordCommand command)
            => await this.Send(command);

        [HttpPost]
        [Authorize]
        [Route(nameof(GenerateRefreshToken))]
        public async Task<ActionResult<GenerateRefreshTokenOutputModel>> GenerateRefreshToken(GenerateRefreshTokenCommand command)
            => await this.Send(command);

        [HttpPost]
        [Authorize]
        [Route(nameof(MyProfile))]
        public async Task<ActionResult<MyProfileOutputModel>> MyProfile(MyProfileQuery query)
            => await this.Send(query);

        [HttpPut]
        [Route(nameof(Edit))]
        public async Task<ActionResult<Result>> Edit(EditUserCommand command)
            => await this.Send(command);

        [HttpPost]
        [Route(nameof(VerifyEmail))]
        public async Task<ActionResult<Result>> VerifyEmail([FromQuery] VerifyEmailCommand command)
            => await this.Send(command);

        #region Private actions
        [HttpPost]
        private async Task<ActionResult<Result>> SendEmailConfirmation(SendEmailConfirmationCommand command)
            => await this.Send(command);
        #endregion
    }
}
