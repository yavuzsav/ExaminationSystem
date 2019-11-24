using ExaminationSystem.Models.Entities;
using FluentValidation;

namespace ExaminationSystem.Business.ValidationRules.FluentValidation
{
    public class ClassLevelValidator : AbstractValidator<ClassLevel>
    {
        public ClassLevelValidator()
        {
            RuleFor(x => x.ClassLevelName).NotEmpty();
        }
    }
}