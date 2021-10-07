namespace Body4U.Application.Features.Identity.Commands.SendEmailConfirmation
{
    using FluentValidation;

    using static Body4U.Domain.Models.ModelContants.User;

    public class SendEmailConfirmationCommandValidator : AbstractValidator<SendEmailConfirmationCommand>
    {
        public SendEmailConfirmationCommandValidator()
        {
            this.RuleFor(x => x.Email)
                .Matches(EmailRegex)
                .NotEmpty();

            this.RuleFor(x => x.ConfirmationLink)
                .NotEmpty();
        }
    }
}
