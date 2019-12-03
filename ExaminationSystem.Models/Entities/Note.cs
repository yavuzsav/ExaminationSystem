using ExaminationSystem.Models.Base;
using System;
using ExaminationSystem.Models.IdentityEntities;

namespace ExaminationSystem.Models.Entities
{
    public class Note : BaseEntityWithId
    {
        public string UserId { get; set; }
        public string CategoryId { get; set; }
        public int Correct { get; set; }
        public int Wrong { get; set; }
        public int Empty { get; set; }
        public DateTime Date { get; set; }

        public virtual Category Category { get; set; }
    }
}