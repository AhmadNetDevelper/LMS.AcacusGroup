using FluentValidation;

namespace LMS.Application.Features.BookFeatures.CreateBook
{
    public class CreateBookValidator : AbstractValidator<CreateBookRequest>
    {
        public CreateBookValidator() 
        {
            //RuleFor(x => x.n).NotEmpty().MaximumLength(50).EmailAddress();
            //RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
        }
    }
}
