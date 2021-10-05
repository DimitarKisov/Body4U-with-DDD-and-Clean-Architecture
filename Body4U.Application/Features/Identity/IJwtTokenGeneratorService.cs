namespace Body4U.Application.Features.Identity
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity.Commands.GenerateRefreshToken;
    using System.Threading.Tasks;

    public interface IJwtTokenGeneratorService
    {
        Result<GenerateRefreshTokenOutputModel> GenerateToken(IUser user);

        Task<Result<GenerateRefreshTokenOutputModel>> GenerateRefreshToken();
    }
}
