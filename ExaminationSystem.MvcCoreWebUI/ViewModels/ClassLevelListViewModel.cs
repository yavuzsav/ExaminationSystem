using ExaminationSystem.Models.Entities;
using ExaminationSystem.MvcCoreWebUI.ViewModels.Bases;
using System.Collections.Generic;

namespace ExaminationSystem.MvcCoreWebUI.ViewModels
{
    public class ClassLevelListViewModel : BaseListViewModel
    {
        public List<ClassLevel> ClassLevels { get; set; }
    }
}