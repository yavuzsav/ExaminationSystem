using ExaminationSystem.Models.Entities;
using ExaminationSystem.MvcCoreWebUI.ViewModels.Bases;
using System.Collections.Generic;

namespace ExaminationSystem.MvcCoreWebUI.ViewModels
{
    public class QuestionListViewModel : BaseListViewModel
    {
        public List<Question> Questions { get; set; }
    }
}