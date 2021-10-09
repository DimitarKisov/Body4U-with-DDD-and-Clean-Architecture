namespace Body4U.Application.Features.Identity.Commands.SendEmail
{
    using FluentValidation;

    using static Body4U.Domain.Models.ModelContants.User;

    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailCommandValidator()
        {
            this.RuleFor(x => x.Email)
                .Matches(EmailRegex)
                .NotEmpty();

            this.RuleFor(x => x.ConfirmationLink)
                .NotEmpty();
        }
    }
}
