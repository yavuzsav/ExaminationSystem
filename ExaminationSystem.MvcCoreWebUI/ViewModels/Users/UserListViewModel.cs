using ExaminationSystem.Models.Dtos.User;
using ExaminationSystem.MvcCoreWebUI.ViewModels.Bases;
using System.Collections.Generic;

namespace ExaminationSystem.MvcCoreWebUI.ViewModels.Users
{
    public class UserListViewModel : BaseListViewModel
    {
        public List<UserDto> Users { get; set; }
        public List<string> UsersRoles { get; set; }
    }
}