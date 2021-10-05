namespace Body4U.Infrastructure.Identity
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity;
    using Body4U.Application.Features.Identity.Commands.GenerateRefreshToken;
    using Body4U.Infrastructure.Persistence;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Serilog;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Account;
    using static Body4U.Application.Common.GlobalConstants.System;

    internal class JwtTokenGeneratorService : IJwtTokenGeneratorService
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext dbContext;

        public JwtTokenGeneratorService(
            IConfiguration configuration,
            ApplicationDbContext dbContext)
        {
            this.configuration = configuration;
            this.dbContext = dbContext;
        }

        public Result<GenerateRefreshTokenOutputModel> GenerateToken(IUser user)
        {
            try
            {
                var castedUsed = user as ApplicationUser;
                var tokenHandler = new JwtSecurityTokenHandler();
                var configKey = this.configuration.GetSection("JwtSettings")["Secret"];
                var key = Encoding.ASCII.GetBytes(configKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, castedUsed!.Id),
                    new Claim(ClaimTypes.Name, castedUsed!.Email)
                }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var encryptedToken = tokenHandler.WriteToken(token);

                return Result<GenerateRefreshTokenOutputModel>.SuccessWith(new GenerateRefreshTokenOutputModel(encryptedToken));
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(JwtTokenGeneratorService)}.{nameof(this.GenerateToken)}", ex);
                return Result<GenerateRefreshTokenOutputModel>.Failure(Wrong);
            }
            
        }

        public async Task<Result<GenerateRefreshTokenOutputModel>> GenerateRefreshToken()
        {
            try
            {
                var accessor = new HttpContextAccessor();
                var claims = accessor?.HttpContext?.User?.Claims;
                var userId = string.Empty;

                if (claims != null)
                {
                    userId = claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                }

                var user = await this.dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

                if (user.IsDisabled || user.LockoutEnd != null)
                {
                    return Result<GenerateRefreshTokenOutputModel>.Failure(Locked);
                }

                return this.GenerateToken(user);
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(JwtTokenGeneratorService)}.{nameof(this.GenerateRefreshToken)}", ex);
                return Result<GenerateRefreshTokenOutputModel>.Failure(Wrong);
            }
            
        }
    }
}
