using FluentValidation;

namespace MediumClone.Models.Validators
{
    public class NewArticleVMValidator:AbstractValidator<NewArticleVM>
    {
        public NewArticleVMValidator()
        {
            RuleFor(a => a.Title).NotNull().WithMessage("Titles cannot be null").NotEmpty().WithMessage("Title cannot be empty");
            RuleFor(b=>b.Content).NotNull().WithMessage("Content cannot null").NotEmpty().WithMessage("Content cannot empty").MinimumLength(100).MaximumLength(10000);
            RuleFor(b => b.CategoryIds).NotEmpty().WithMessage("Categories cannot be empty").NotNull().WithMessage("Categories cannot be null");
        }
    }
}
