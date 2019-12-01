using ExaminationSystem.Framework.Entities;

namespace ExaminationSystem.Models.Dtos.User
{
    public class UserForLoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}