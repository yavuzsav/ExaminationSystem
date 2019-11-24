using ExaminationSystem.Models.Entities;
using ExaminationSystem.MvcCoreWebUI.ViewModels.Bases;
using System.Collections.Generic;

namespace ExaminationSystem.MvcCoreWebUI.ViewModels
{
    public class CategoryListViewModel : BaseListViewModel
    {
        public List<Category> Categories { get; internal set; }
    }
}