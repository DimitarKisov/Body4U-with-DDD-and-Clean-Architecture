namespace Body4U.Application.Features.Identity.Commands.EditUser
{
    using FluentValidation;

    using static Body4U.Domain.Models.ModelContants.User;

    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
        {
            this.RuleFor(x => x.PhoneNumber)
                .Matches(PhoneNumberRegex)
                .NotEmpty();

            this.RuleFor(x => x.FirstName)
                .NotEmpty();

            this.RuleFor(x => x.LastName)
                .NotEmpty();

            this.RuleFor(x => x.Age)
                .InclusiveBetween(MinAge, MaxAge)
                .NotEmpty();

            this.RuleFor(x => x.PhoneNumber)
                .Matches(PhoneNumberRegex)
                .NotEmpty();

            this.RuleFor(x => x.Gender)
                .NotEmpty();
        }
    }
}
