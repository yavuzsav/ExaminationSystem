using ExaminationSystem.Framework.Entities;

namespace ExaminationSystem.Models.Dtos.User
{
    public class UserForRegisterDto : IDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}