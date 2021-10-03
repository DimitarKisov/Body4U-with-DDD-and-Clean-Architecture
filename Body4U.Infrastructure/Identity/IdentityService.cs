namespace Body4U.Infrastructure.Identity
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity;
    using Body4U.Application.Features.Identity.Commands.CreateUser;
    using Body4U.Application.Features.Identity.Commands.LoginUser;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Account;
    using static Body4U.Application.Common.GlobalConstants.System;

    public class IdentityService : IIdentityService
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

        public async Task<Result<IUser>> Register(CreateUserCommand command)
        {
            var image = ImageConverter(command.ProfilePicture).Data;
            var gender = Gender.FromValue<Gender>(command.Gender);

            var user = new ApplicationUser(
                command.Email,
                command.PhoneNumber,
                command.FirstName,
                command.LastName,
                command.Age,
                image,
                gender);

            var result = await this.userManager.CreateAsync(user, command.Password);

            return result.Succeeded ?
                Result<IUser>.SuccessWith(user) : 
                Result<IUser>.Failure(RegistrationUnssuccesful);
        }

        public async Task<Result<LoginOutputModel>> Login(LoginUserCommand command)
        {
            var user = await this.userManager.FindByEmailAsync(command.Email);
            if (user == null)
            {
                return Result<LoginOutputModel>.Failure(WrongUsernameOrPassword);
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, command.Password);
            if (!passwordValid)
            {
                return Result<LoginOutputModel>.Failure(WrongUsernameOrPassword);
            }

            var result = await signInManager.PasswordSignInAsync(user, command.Password, command.RememberMe, user.LockoutEnabled);
            if (result.Succeeded)
            {
                var token = this.jwtTokenGeneratorService.GenerateToken(user);

                return Result<LoginOutputModel>.SuccessWith(new LoginOutputModel(token));
            }

            return user.LockoutEnabled && user.LockoutEnd != null && user.LockoutEnd.Value > DateTime.Now
                    ? Result<LoginOutputModel>.Failure(Locked)
                    : Result<LoginOutputModel>.Failure(WrongUsernameOrPassword);
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
