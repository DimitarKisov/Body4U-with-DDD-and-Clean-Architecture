namespace Body4U.Application.Features.Articles.Commands.Create
{
    using FluentValidation;

    using static Body4U.Domain.Models.ModelConstants.Article;

    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator()
        {
            this.RuleFor(x => x.Title)
                .MinimumLength(MinTitleLength)
                .MaximumLength(MaxTitleLength)
                .NotEmpty();

            this.RuleFor(x => x.Image)
                .NotEmpty();

            this.RuleFor(x => x.Content)
                .MinimumLength(MinContentLength)
                .NotEmpty();
        }
    }
}
