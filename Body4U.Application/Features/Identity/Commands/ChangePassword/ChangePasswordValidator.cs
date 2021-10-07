namespace Body4U.Application.Features.Identity.Commands.ChangePassword
{
    using FluentValidation;

    using static Body4U.Domain.Models.ModelContants.User;

    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            this.RuleFor(x => x.OldPassword)
                .NotEmpty();

            this.RuleFor(x => x.NewPassword)
                .MinimumLength(MinPasswordLength)
                .MaximumLength(MaxPasswordLength)
                .NotEmpty();

            this.RuleFor(x => x.ConfirmNewPassword)
                .Matches(x => x.NewPassword);
        }
    }
}
