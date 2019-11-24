using ExaminationSystem.Models.Base;
using ExaminationSystem.Models.Enums;

namespace ExaminationSystem.Models.Entities
{
    public class Question : BaseEntityWithProperties
    {
        public string QuestionContent { get; set; }
        public string CategoryId { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public CorrectAnswer CorrectAnswer { get; set; }

        public virtual Category Category { get; set; }
    }
}