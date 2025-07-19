using FluentValidation;

namespace book_api.Interface.Input
{
    public class BookInputValidator : AbstractValidator<BookInput>
    {
        public BookInputValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must be less than 100 characters.");

            RuleFor(x => x.PublishDate)
                .NotEmpty().WithMessage("Publish date is required.");

            RuleFor(x => x.Authors)
                .NotNull().WithMessage("Authors are required.")
                .Must(a => a != null && a.Count > 0).WithMessage("At least one author is required.");

            RuleForEach(x => x.Authors).SetValidator(new AuthorInputValidator());
        }
    }

    public class AuthorInputValidator : AbstractValidator<AuthorInput>
    {
        public AuthorInputValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must be less than 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must be less than 50 characters.");
        }
    }
}