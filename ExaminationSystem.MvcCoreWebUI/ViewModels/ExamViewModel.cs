using System.Collections.Generic;
using ExaminationSystem.Models.Entities;

namespace ExaminationSystem.MvcCoreWebUI.ViewModels
{
    public class ExamViewModel
    {
        public List<Question> Questions { get; set; }

        public List<string> UserAnswers { get; set; }
    }
}