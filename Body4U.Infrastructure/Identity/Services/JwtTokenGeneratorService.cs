namespace Body4U.Infrastructure.Identity.Services
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity;
    using Body4U.Application.Features.Identity.Commands.GenerateRefreshToken;
    using Body4U.Infrastructure.Identity.Models;
    using Body4U.Infrastructure.Persistence;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Serilog;
    using System;
    using System.Collections.Generic;
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
        private readonly UserManager<ApplicationUser> userManager;

        public JwtTokenGeneratorService(
            IConfiguration configuration,
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager)
        {
            this.configuration = configuration;
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        //public Result<GenerateRefreshTokenOutputModel> GenerateToken(IUser user)
        //{
        //    try
        //    {
        //        var castedUsed = user as ApplicationUser;
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var configKey = this.configuration.GetSection("JwtSettings")["Secret"];
        //        var key = Encoding.ASCII.GetBytes(configKey);

        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new[]
        //            {
        //            new Claim(ClaimTypes.NameIdentifier, castedUsed!.Id),
        //            new Claim(ClaimTypes.Name, castedUsed!.Email)
        //        }),
        //            Expires = DateTime.UtcNow.AddDays(7),
        //            SigningCredentials = new SigningCredentials(
        //                new SymmetricSecurityKey(key),
        //                SecurityAlgorithms.HmacSha256Signature)
        //        };

        //        var token = tokenHandler.CreateToken(tokenDescriptor);
        //        var encryptedToken = tokenHandler.WriteToken(token);

        //        return Result<GenerateRefreshTokenOutputModel>.SuccessWith(new GenerateRefreshTokenOutputModel(encryptedToken));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error($"{nameof(JwtTokenGeneratorService)}.{nameof(this.GenerateToken)}", ex);
        //        return Result<GenerateRefreshTokenOutputModel>.Failure(Wrong);
        //    }

        //}

        public async Task<Result<GenerateRefreshTokenOutputModel>> GenerateToken(IUser user)
        {
            try
            {
                var castedUsed = user as ApplicationUser;
                var configKey = this.configuration.GetSection("JwtSettings")["Secret"];
                var encodedKey = Encoding.ASCII.GetBytes(configKey);

                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, castedUsed!.Id),
                    new Claim(ClaimTypes.Email, castedUsed.Email)
                };

                var userRolesId = (await this.userManager
                    .GetRolesAsync(castedUsed)).ToList();

                userRolesId.ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x)));

                SymmetricSecurityKey key = new SymmetricSecurityKey(encodedKey);
                SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                DateTime expires = DateTime.Now.AddMinutes(60);

                JwtSecurityToken token = new JwtSecurityToken(
                    "http://yourdomain.com",
                    "http://yourdomain.com",
                    claims,
                    expires: expires,
                    signingCredentials: signingCredentials
                );

                var encryptedToken = new JwtSecurityTokenHandler().WriteToken(token);
                return Result<GenerateRefreshTokenOutputModel>.SuccessWith(new GenerateRefreshTokenOutputModel(encryptedToken));
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(JwtTokenGeneratorService)}.{nameof(this.GenerateToken)}", ex);
                return Result<GenerateRefreshTokenOutputModel>.Failure(string.Format(Wrong, nameof(this.GenerateToken)));
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

                return await this.GenerateToken(user);
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(JwtTokenGeneratorService)}.{nameof(this.GenerateRefreshToken)}", ex);
                return Result<GenerateRefreshTokenOutputModel>.Failure(string.Format(Wrong, nameof(this.GenerateRefreshToken)));
            }

        }
    }
}
