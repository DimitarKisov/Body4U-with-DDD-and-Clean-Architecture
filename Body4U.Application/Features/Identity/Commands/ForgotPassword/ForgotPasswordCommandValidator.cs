namespace Body4U.Application.Features.Identity.Commands.ForgotPassword
{
    using FluentValidation;

    using static Body4U.Domain.Models.ModelContants.User;

    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
            => this.RuleFor(x => x.Email)
                .Matches(EmailRegex)
                .NotEmpty();
    }
}
