namespace Body4U.Application.Features.Administration.Commands
{
    using FluentValidation;

    using static Body4U.Domain.Models.ModelConstants.User;

    public class EditUserRolesCommandValidator : AbstractValidator<EditUserRolesCommand>
    {
        public EditUserRolesCommandValidator()
            => this.RuleFor(x => x.Email)
                .Matches(EmailRegex)
                .NotEmpty();
    }
}
