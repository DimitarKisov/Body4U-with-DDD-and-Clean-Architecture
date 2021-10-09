namespace Body4U.Application.Features.Identity.Commands.ResetPassword
{
    using FluentValidation;

    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            this.RuleFor(x => x.NewPassword)
                .NotEmpty();

            this.RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty();

            this.RuleFor(x => x.ConfirmNewPassword)
                .Equal(x => x.NewPassword)
                .WithMessage("Passwords doesn't match.");
        }
    }
}
