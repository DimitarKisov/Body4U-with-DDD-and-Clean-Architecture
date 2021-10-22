namespace Body4U.Web.Features
{
    using Body4U.Application.Common;
    using Body4U.Application.Features;
    using Body4U.Application.Features.Identity.Commands.ChangePassword;
    using Body4U.Application.Features.Identity.Commands.CreateUser;
    using Body4U.Application.Features.Identity.Commands.EditUser;
    using Body4U.Application.Features.Identity.Commands.ForgotPassword;
    using Body4U.Application.Features.Identity.Commands.GenerateRefreshToken;
    using Body4U.Application.Features.Identity.Commands.LoginUser;
    using Body4U.Application.Features.Identity.Commands.ResetPassword;
    using Body4U.Application.Features.Identity.Commands.SendEmail;
    using Body4U.Application.Features.Identity.Commands.VerifyEmail;
    using Body4U.Application.Features.Identity.Queries.MyProfile;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Account;

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
                var sendMailResult = await this.CreateConfirmationLinkAndSend(nameof(this.VerifyEmail), registrationResult.Value.UserId, registrationResult.Value.Token, registrationResult.Value.Email, EmailConfirmSubject, EmailConfirmHtmlContent);

                return sendMailResult;
            }
            #endregion

            return registrationResult.Result;
        }

        [HttpPost]
        [Route(nameof(ForgotPassword))]
        public async Task<ActionResult<Result>> ForgotPassword(ForgotPasswordCommand command)
        {
            var forgotPasswordResult = await this.Send(command);

            #region Logic
            if (forgotPasswordResult.Value != null)
            {
                if (forgotPasswordResult.Value.Email == default && forgotPasswordResult.Value.Token == default && forgotPasswordResult.Value.UserId == default)
                {
                    return new ActionResult<Result>(Result.Success);
                }

                await this.CreateConfirmationLinkAndSend(nameof(this.ResetPassword), forgotPasswordResult.Value.UserId, forgotPasswordResult.Value.Token!, forgotPasswordResult.Value.Email!, ForgotPasswordSubject, ForgotPasswordHtmlContent);
            }
            #endregion

            //Ако е възникнала неочаквана грешка, връщаме нея, иначе винаги ще връщаме успех, за да избегнем брут форс атаки
            return forgotPasswordResult.Result == null
                ? new ActionResult<Result>(Result.Success)
                : forgotPasswordResult.Result;
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

        [HttpPost]
        [Route(nameof(ResetPassword))]
        public async Task<ActionResult<Result>> ResetPassword([FromQuery]string userId, [FromQuery]string token, ResetPasswordCommand command)
            => await this.Send(command.SetId(userId).SetToken(token));

        #region Private actions
        [HttpPost]
        private async Task<ActionResult<Result>> SendEmail(SendEmailCommand command)
            => await this.Send(command);

        private async Task<ActionResult<Result>> CreateConfirmationLinkAndSend(string redirectActionName, string userId, string token, string email, string subject, string htmlContent)
        {
            var confirmationLink = Url.Action(redirectActionName, "Identity",
                   new { userId = userId, token = token }, Request.Scheme, Request.Host.ToString());

            return await this.SendEmail(new SendEmailCommand(email, confirmationLink, subject, string.Format(htmlContent, confirmationLink)));
        }
        #endregion
    }
}
