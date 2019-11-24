using ExaminationSystem.Models.Entities;
using FluentValidation;

namespace ExaminationSystem.Business.ValidationRules.FluentValidation
{
    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.QuestionContent).NotEmpty().MinimumLength(1);
            RuleFor(x => x.A).NotEmpty().MinimumLength(1);
            RuleFor(x => x.B).NotEmpty().MinimumLength(1);
            RuleFor(x => x.C).NotEmpty().MinimumLength(1);
            RuleFor(x => x.D).NotEmpty().MinimumLength(1);
            RuleFor(x => x.CorrectAnswer).NotEmpty();
            RuleFor(x => x.CreatedUserName).NotEmpty();
            RuleFor(x => x.OnCreated).NotEmpty();
        }
    }
}