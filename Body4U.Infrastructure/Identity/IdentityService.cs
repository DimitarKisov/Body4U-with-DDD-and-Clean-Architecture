namespace Body4U.Infrastructure.Identity
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity;
    using Body4U.Application.Features.Identity.Commands.ChangePassword;
    using Body4U.Application.Features.Identity.Commands.CreateUser;
    using Body4U.Application.Features.Identity.Commands.LoginUser;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Serilog;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Account;
    using static Body4U.Application.Common.GlobalConstants.System;

    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IJwtTokenGeneratorService jwtTokenGeneratorService;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtTokenGeneratorService jwtTokenGeneratorService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtTokenGeneratorService = jwtTokenGeneratorService;
        }

        public async Task<Result<IUser>> Register(CreateUserCommand request)
        {
            try
            {
                var imageResult = ImageConverter(request.ProfilePicture);
                if (!imageResult.Succeeded)
                {
                    return Result<IUser>.Failure(imageResult.Errors);
                }

                var gender = Gender.FromValue<Gender>(request.Gender);

                var user = new ApplicationUser(
                    request.Email,
                    request.PhoneNumber,
                    request.FirstName,
                    request.LastName,
                    request.Age,
                    imageResult.Data,
                    gender);

                var result = await this.userManager.CreateAsync(user, request.Password);

                return result.Succeeded 
                    ? Result<IUser>.SuccessWith(user)
                    : Result<IUser>.Failure(RegistrationUnssuccesful);
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(IdentityService)}.{nameof(this.Register)}", ex);
                return Result<IUser>.Failure(Wrong);
            }
        }

        public async Task<Result<LoginOutputModel>> Login(LoginUserCommand request)
        {
            try
            {
                var user = await this.userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return Result<LoginOutputModel>.Failure(WrongUsernameOrPassword);
                }

                var passwordValid = await this.userManager.CheckPasswordAsync(user, request.Password);
                if (!passwordValid)
                {
                    return Result<LoginOutputModel>.Failure(WrongUsernameOrPassword);
                }

                var result = await this.signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, user.LockoutEnabled);
                if (result.Succeeded)
                {
                    var tokenResult = this.jwtTokenGeneratorService.GenerateToken(user);

                    if (tokenResult.Succeeded)
                    {
                        return Result<LoginOutputModel>.SuccessWith(new LoginOutputModel(tokenResult.Data.Token));
                    }

                    return Result<LoginOutputModel>.Failure(LoginFailed);
                }

                return user.LockoutEnabled && user.LockoutEnd != null && user.LockoutEnd.Value > DateTime.Now
                        ? Result<LoginOutputModel>.Failure(Locked)
                        : Result<LoginOutputModel>.Failure(WrongUsernameOrPassword);
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(IdentityService)}.{nameof(this.Login)}", ex);
                return Result<LoginOutputModel>.Failure(Wrong);
            }
        }

        public async Task<Result> ChangePassword(ChangePasswordCommand request, string userId)
        {
            try
            {
                var user = await this.userManager.FindByIdAsync(userId);
                var result = await this.userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

                if (result.Succeeded)
                {
                    return Result.Success;
                }

                var errors = result.Errors.Select(e => e.Description);

                return Result.Failure(errors);
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(IdentityService)}.{nameof(this.ChangePassword)}", ex);
                return Result<LoginOutputModel>.Failure(Wrong);
            }
        }

        #region Private methods
        private Result<byte[]> ImageConverter(IFormFile file)
        {
            if (file == null)
            {
                return Result<byte[]>.SuccessWith(new byte[0]);
            }
            if (file != null && file.ContentType != "image/jpeg" && file.ContentType != "image/png")
            {
                return Result<byte[]>.Failure(WrongImageFormat);
            }

            var result = new byte[file!.Length];

            if (file!.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    result = stream.ToArray();
                }
            }

            return Result<byte[]>.SuccessWith(result);
        }
        #endregion
    }
}
