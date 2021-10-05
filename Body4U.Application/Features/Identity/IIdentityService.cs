namespace Body4U.Application.Features.Identity
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity.Commands.ChangePassword;
    using Body4U.Application.Features.Identity.Commands.CreateUser;
    using Body4U.Application.Features.Identity.Commands.EditUser;
    using Body4U.Application.Features.Identity.Commands.LoginUser;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        Task<Result<IUser>> Register(CreateUserCommand request);

        Task<Result<LoginOutputModel>> Login(LoginUserCommand request);

        Task<Result> ChangePassword(ChangePasswordCommand request, string userId);

        Task<Result<IUser>> EditMyProfile(EditUserCommand request, IUser user, CancellationToken cancellationToken);
    }
}
