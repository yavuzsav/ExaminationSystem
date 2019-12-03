using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationSystem.Models.Dtos.User
{
    public class UserWithIdDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}