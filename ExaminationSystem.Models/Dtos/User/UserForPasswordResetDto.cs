namespace ExaminationSystem.Models.Dtos.User
{
    public class UserForPasswordResetDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}