namespace Body4U.Application.Features.Identity
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity.Commands.CreateUser;
    using Body4U.Application.Features.Identity.Commands.LoginUser;
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        Task<Result<IUser>> Register(CreateUserCommand command);

        Task<Result<LoginOutputModel>> Login(LoginUserCommand command);
    }
}
