using ExaminationSystem.Models.Dtos.User;
using ExaminationSystem.Models.Entities;
using ExaminationSystem.MvcCoreWebUI.ViewModels.Bases;
using System.Collections.Generic;

namespace ExaminationSystem.MvcCoreWebUI.ViewModels.Notes
{
    public class NoteListViewModel : BaseListViewModel
    {
        public List<Note> Notes { get; set; }
        public List<UserDto> Users { get; set; }
    }
}