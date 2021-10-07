namespace Body4U.Application.Features.Identity.Commands.VerifyEmail
{
    using FluentValidation;

    public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailCommandValidator()
        {
            this.RuleFor(x => x.UserId)
                .NotEmpty();

            this.RuleFor(x => x.Token)
                .NotEmpty();
        }
    }
}
