namespace Body4U.Infrastructure.Identity.Services
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity;
    using Body4U.Application.Features.Identity.Commands.ChangePassword;
    using Body4U.Application.Features.Identity.Commands.CreateUser;
    using Body4U.Application.Features.Identity.Commands.EditUser;
    using Body4U.Application.Features.Identity.Commands.LoginUser;
    using Body4U.Application.Features.Identity.Commands.VerifyEmail;
    using Body4U.Infrastructure.Identity.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Serilog;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;

    using static Body4U.Application.Common.GlobalConstants.Account;
    using static Body4U.Application.Common.GlobalConstants.System;

    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IJwtTokenGeneratorService jwtTokenGeneratorService;
        private readonly IEmailSender emailSender;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtTokenGeneratorService jwtTokenGeneratorService,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtTokenGeneratorService = jwtTokenGeneratorService;
            this.emailSender = emailSender;
        }

        public async Task<Result<CreateUserOutputModel>> Register(CreateUserCommand request)
        {
            try
            {
                var imageResult = ImageConverter(request.ProfilePicture);
                if (!imageResult.Succeeded)
                {
                    return Result<CreateUserOutputModel>.Failure(imageResult.Errors);
                }

                var allowedGenderValues = Domain.Common.Enumeration.GetAll<Gender>().Select(x => x.Value);

                if (!allowedGenderValues.Any(x => x == request.Gender))
                {
                    return Result<CreateUserOutputModel>.Failure(WrongGender);
                }

                var gender = Domain.Common.Enumeration.FromValue<Gender>(request.Gender);

                var user = new ApplicationUser(
                    request.Email,
                    request.PhoneNumber,
                    request.FirstName,
                    request.LastName,
                    request.Age,
                    imageResult.Data,
                    gender);

                var result = await this.userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    return Result<CreateUserOutputModel>.SuccessWith(new CreateUserOutputModel(user.Email, user.Id, token, default!));
                }

                return Result<CreateUserOutputModel>.Failure(result.Errors.Select(x => x.Description));
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(IdentityService)}.{nameof(this.Register)}", ex);
                return Result<CreateUserOutputModel>.Failure(string.Format(Wrong, nameof(this.Register)));
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

                var emailConfirmed = await this.userManager.IsEmailConfirmedAsync(user);
                if (!emailConfirmed)
                {
                    return Result<LoginOutputModel>.Failure(EmailNotConfirmed);
                }

                var userLocked = await this.userManager.IsLockedOutAsync(user);
                if (userLocked)
                {
                    return Result<LoginOutputModel>.Failure(Locked);
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

                return Result<LoginOutputModel>.Failure(WrongUsernameOrPassword);
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(IdentityService)}.{nameof(this.Login)}", ex);
                return Result<LoginOutputModel>.Failure(string.Format(Wrong, nameof(this.Login)));
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
                return Result<LoginOutputModel>.Failure(string.Format(Wrong, nameof(this.ChangePassword)));
            }
        }

        public async Task<Result<IUser>> EditMyProfile(EditUserCommand request, IUser user, CancellationToken cancellationToken)
        {
            try
            {
                var castedUser = user as ApplicationUser;

                if (request.ProfilePicture != null &&
                    request.ProfilePicture.ContentType != "image/jpeg" &&
                    request.ProfilePicture.ContentType != "image/png" &&
                    request.ProfilePicture.ContentType != "image/jpg")
                {
                    return Result<IUser>.Failure(WrongImageFormat);
                }

                var gender = Domain.Common.Enumeration.FromValue<Gender>(request.Gender);

                castedUser!
                    .UpdatePhoneNumber(request.PhoneNumber)
                    .UpdateFirstName(request.FirstName)
                    .UpdateLastName(request.LastName)
                    .UpdateAge(request.Age)
                    .UpdateGender(gender);

                if (request.ProfilePicture != null)
                {
                    if (request.ProfilePicture.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await request.ProfilePicture.CopyToAsync(stream, cancellationToken);

                            if (castedUser.ProfilePicture != stream.ToArray())
                            {
                                castedUser.UpdateProfilePicture(stream.ToArray());
                            }
                        }
                    }
                }

                return Result<IUser>.SuccessWith(castedUser);

                //TODO: Когато се добавят и треньори го довърши? Може и да не се ъпдейтва оттука.
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(IdentityService)}.{nameof(this.EditMyProfile)}", ex);
                return Result<IUser>.Failure(string.Format(Wrong, nameof(this.EditMyProfile)));
            }
        }

        public async Task<Result> VerifyEmail(VerifyEmailCommand request)
        {
            try
            {
                var user = await this.userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    return Result.Failure(string.Format(WrongId, request.UserId));
                }

                //TODO: Не знам дали е ще е нужно, когато се деплойне. Ако фронт енда обработва по някакъв начин токена, няма да е нужно. Ако няма да го обработва ще е нужно. Ако си се изпраща "обработени" няма да е нужно

                //ПРИМЕР : CfDJ8BTAUaeVErZMmgBhJwO+tnhguBAxVOU//1bFlbAOUJXKFnAr59nId7rMkezJy5eCVsf8fmXqwjwem5GWJdEK3tHsYugJ0xNPPw0gFqODDqYoggShAfnbInZ7lcFf+s9zIWqpQA0106JebAkfd36SrYLH+ynv5FSjNnHWcSDkthmnTPpthMp/wTI91RflC6/0Tr4pcRdUSmvZSYzRowNRLCZ3/jXCQm+Ds5k8G2QS8YNchV+NqCqjwv7tC/8JJgOU6w== Така е, когато се генерира, обаче когато се изпрати към дадения човек става така : CfDJ8BTAUaeVErZMmgBhJwO%2BtnhguBAxVOU%2F%2F1bFlbAOUJXKFnAr59nId7rMkezJy5eCVsf8fmXqwjwem5GWJdEK3tHsYugJ0xNPPw0gFqODDqYoggShAfnbInZ7lcFf%2Bs9zIWqpQA0106JebAkfd36SrYLH%2Bynv5FSjNnHWcSDkthmnTPpthMp%2FwTI91RflC6%2F0Tr4pcRdUSmvZSYzRowNRLCZ3%2FjXCQm%2BDs5k8G2QS8YNchV%2BNqCqjwv7tC%2F8JJgOU6w%3D%3D

                var tokenDecoded = HttpUtility.UrlDecode(request.Token);

                var result = await userManager.ConfirmEmailAsync(user, tokenDecoded);
                if (result.Succeeded)
                {
                    return Result.Success;
                }

                return Result.Failure(result.Errors.Select(x => x.Description));
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(IdentityService)}.{nameof(this.VerifyEmail)}", ex);
                return Result.Failure(string.Format(Wrong, nameof(this.VerifyEmail)));
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
