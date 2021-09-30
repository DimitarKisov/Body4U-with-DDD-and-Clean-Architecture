namespace Body4U.Application.Features.Identity
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity.Commands.CreateUser;
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        Task<Result<IUser>> Register(CreateUserCommand command);
    }
}
