namespace Body4U.Infrastructure.Identity.Services
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Administration.Commands;
    using Body4U.Application.Features.Identity;
    using Body4U.Application.Features.Identity.Commands.ChangePassword;
    using Body4U.Application.Features.Identity.Commands.CreateUser;
    using Body4U.Application.Features.Identity.Commands.EditUser;
    using Body4U.Application.Features.Identity.Commands.ForgotPassword;
    using Body4U.Application.Features.Identity.Commands.LoginUser;
    using Body4U.Application.Features.Identity.Commands.ResetPassword;
    using Body4U.Application.Features.Identity.Commands.VerifyEmail;
    using Body4U.Domain.Common;
    using Body4U.Infrastructure.Identity.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Serilog;
    using System;
    using System.Collections.Generic;
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
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IJwtTokenGeneratorService jwtTokenGeneratorService;
        private readonly IEmailSender emailSender;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGeneratorService jwtTokenGeneratorService,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
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

                var allowedGenderValues = Enumeration.GetAll<Gender>().Select(x => x.Value);

                if (!allowedGenderValues.Any(x => x == request.Gender))
                {
                    return Result<CreateUserOutputModel>.Failure(WrongGender);
                }

                var gender = Enumeration.FromValue<Gender>(request.Gender);

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
                    return Result<CreateUserOutputModel>.SuccessWith(new CreateUserOutputModel(user.Email, user.Id, token));
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
                    var tokenResult = await this.jwtTokenGeneratorService.GenerateToken(user);

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

                var gender = Enumeration.FromValue<Gender>(request.Gender);

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

                var tokenDecoded = HttpUtility.UrlDecode(request.Token);
                var result = await this.userManager.ConfirmEmailAsync(user, tokenDecoded);
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

        public async Task<Result<ForgotPasswordOutputModel>> ForgotPassword(ForgotPasswordCommand request)
        {
            try
            {
                var user = await this.userManager.FindByEmailAsync(request.Email);
                if (user != null && await this.userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    return Result<ForgotPasswordOutputModel>.SuccessWith(new ForgotPasswordOutputModel(user.Email, user.Id, token));
                }

                return Result<ForgotPasswordOutputModel>.SuccessWith(new ForgotPasswordOutputModel(default!, default!, default!));
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(IdentityService)}.{nameof(this.ForgotPassword)}", ex);
                return Result<ForgotPasswordOutputModel>.Failure(string.Format(Wrong, nameof(this.ForgotPassword)));
            }
        }

        public async Task<Result> ResetPassword(string userId, string token, ResetPasswordCommand request)
        {
            try
            {
                var user = await this.userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Result.Failure(string.Format(WrongId, userId));
                }

                var tokenDecoded = HttpUtility.UrlDecode(token);
                var result = await this.userManager.ResetPasswordAsync(user, token, request.NewPassword);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(x => x.Description);
                    return Result.Failure(errors);
                }

                return Result.Success;
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(IdentityService)}.{nameof(this.ResetPassword)}", ex);
                return Result.Failure(string.Format(Wrong, nameof(this.ResetPassword)));
            }
        }

        public async Task<Result<EditUserRolesOutputModel>> EditUserRoles(EditUserRolesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await this.userManager.FindByEmailAsync(request.Email);
                var userRolesIds = new List<string>();

                //Взимаме всички имена на роли за дадения потребител
                var userRoleNames = await userManager.GetRolesAsync(user);
                //Взимаме тези роли, които отговарят за съответния потребител
                var roles = await roleManager.Roles
                    .Where(x => userRoleNames
                        .Contains(x.Name))
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken);

                //И ги добавяме към списъка за роли от ИД-та на потребителя
                roles.ForEach(x => userRolesIds.Add(x));
                
                var rolesForAdd = request.RolesIds.Except(userRolesIds);
                var rolesForRemove = userRolesIds.Except(request.RolesIds);

                var usersIdsForCreate = new List<string>();
                var usersIdsForDelete = new List<string>();

                var errors = new List<string>();

                foreach (var roleId in rolesForAdd)
                {
                    var roleName = (await this.roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken))?.Name;

                    IdentityResult identityResult;

                    if (roleName != null)
                    {
                        if (!await this.userManager.IsInRoleAsync(user, roleName))
                        {
                            identityResult = await this.userManager.AddToRoleAsync(user, roleName);

                            if (identityResult.Succeeded && roleName == TrainerRoleName)
                            {
                                usersIdsForCreate.Add(user.Id);
                            }
                            else
                            {
                                errors.AddRange(identityResult.Errors.Select(x => x.Description));
                            }
                        }
                    }
                }

                foreach (var roleId in rolesForRemove)
                {
                    var roleName = (await this.roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken))?.Name;

                    IdentityResult identityResult;

                    if (roleName != null)
                    {
                        if (await this.userManager.IsInRoleAsync(user, roleName))
                        {
                            identityResult = await this.userManager.RemoveFromRoleAsync(user, roleName);

                            if (identityResult.Succeeded && roleName == TrainerRoleName)
                            {
                                usersIdsForDelete.Add(user.Id);
                            }
                            else
                            {
                                errors.AddRange(identityResult.Errors.Select(x => x.Description));
                            }
                        }
                    }
                }

                return errors.Count() == 0
                    ? Result<EditUserRolesOutputModel>.SuccessWith(new EditUserRolesOutputModel(usersIdsForCreate, usersIdsForDelete))
                    : Result<EditUserRolesOutputModel>.Failure(errors);
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(IdentityService)}.{nameof(this.EditUserRoles)}", ex);
                return Result<EditUserRolesOutputModel>.Failure(string.Format(Wrong, nameof(this.EditUserRoles)));
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
