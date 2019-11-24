using ExaminationSystem.Models.Base;

namespace ExaminationSystem.Models.Entities
{
    public class ExamParameter : BaseEntityWithId
    {
        public int NumberOfQuestions { get; set; } = 20;
        public int LengthOfExam { get; set; } = 30;
    }
}