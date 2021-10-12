namespace Body4U.Application.Features.Identity
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Administration.Commands;
    using Body4U.Application.Features.Identity.Commands.ChangePassword;
    using Body4U.Application.Features.Identity.Commands.CreateUser;
    using Body4U.Application.Features.Identity.Commands.EditUser;
    using Body4U.Application.Features.Identity.Commands.ForgotPassword;
    using Body4U.Application.Features.Identity.Commands.LoginUser;
    using Body4U.Application.Features.Identity.Commands.ResetPassword;
    using Body4U.Application.Features.Identity.Commands.VerifyEmail;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        Task<Result<CreateUserOutputModel>> Register(CreateUserCommand request);

        Task<Result<LoginOutputModel>> Login(LoginUserCommand request);

        Task<Result> ChangePassword(ChangePasswordCommand request, string userId);

        Task<Result<IUser>> EditMyProfile(EditUserCommand request, IUser user, CancellationToken cancellationToken);

        Task<Result> VerifyEmail(VerifyEmailCommand request);

        Task<Result<ForgotPasswordOutputModel>> ForgotPassword(ForgotPasswordCommand request);

        Task<Result> ResetPassword(string userId, string token, ResetPasswordCommand request);

        Task<Result> EditUserRoles(EditUserRolesCommand request, CancellationToken cancellationToken);
    }
}
