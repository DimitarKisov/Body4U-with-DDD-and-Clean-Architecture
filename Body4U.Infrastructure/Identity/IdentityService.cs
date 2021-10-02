namespace Body4U.Infrastructure.Identity
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity;
    using Body4U.Application.Features.Identity.Commands.CreateUser;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using System.IO;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Account;
    using static Body4U.Application.Common.GlobalConstants.System;

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
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
