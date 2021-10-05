namespace Body4U.Application.Features.Identity.Commands.GenerateRefreshToken
{
    public class GenerateRefreshTokenOutputModel
    {
        public GenerateRefreshTokenOutputModel(string token)
            => this.Token = token;

        public string Token { get; }
    }
}
