using FluentValidation;

namespace MediumClone.Models.Validators
{
    public class UserVMValidator : AbstractValidator<UserVM>
    {
        public UserVMValidator()
        {
            RuleFor(b => b.FirstName).NotEmpty().WithMessage("First Name cannot be empty").NotNull().WithMessage("First Name cannot be empty").MaximumLength(100);
            RuleFor(b => b.LastName).NotEmpty().WithMessage("Last Name cannot be empty").NotNull().WithMessage("Last Name cannot be empty").MaximumLength(100);          
            
               
        }      
    }
}
