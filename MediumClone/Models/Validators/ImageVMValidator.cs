using FluentValidation;

namespace MediumClone.Models.Validators
{
    public class ImageVMValidator:AbstractValidator<ImageVM>
    {
        public ImageVMValidator()
        {
            RuleFor(a => a.ImageFile).NotEmpty().WithMessage("File not null").NotNull().WithMessage("File not empty");
        }
    }
}
