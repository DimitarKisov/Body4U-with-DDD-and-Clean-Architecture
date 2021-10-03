namespace Body4U.Application.Features.Identity.Commands.LoginUser
{
    using FluentValidation;

    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            this.RuleFor(x => x.Email)
                .NotEmpty();

            this.RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
