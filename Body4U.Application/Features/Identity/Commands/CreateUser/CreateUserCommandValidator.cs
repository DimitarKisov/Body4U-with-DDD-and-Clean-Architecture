namespace Body4U.Application.Features.Identity.Commands.CreateUser
{
    using FluentValidation;

    using static Body4U.Domain.Models.ModelContants.User;

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            this.RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();

            this.RuleFor(x => x.PhoneNumber)
                .Matches(PhoneRegex)
                .NotEmpty();

            this.RuleFor(x => x.FirstName)
                .MinimumLength(MinFirstNameLength)
                .MaximumLength(MaxFirstNameLength)
                .NotEmpty();

            this.RuleFor(x => x.LastName)
                .MinimumLength(MinLastNameLength)
                .MaximumLength(MaxLastNameLength)
                .NotEmpty();

            this.RuleFor(x => x.Age)
                .InclusiveBetween(MinAge, MaxAge)
                .NotEmpty();
        }
    }
}
