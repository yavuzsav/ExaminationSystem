using ExaminationSystem.Models.Entities;
using FluentValidation;

namespace ExaminationSystem.Business.ValidationRules.FluentValidation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty();
        }
    }
}