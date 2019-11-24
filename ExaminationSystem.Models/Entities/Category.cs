using ExaminationSystem.Models.Base;
using System.Collections.Generic;

namespace ExaminationSystem.Models.Entities
{
    public class Category : BaseEntityWithProperties
    {
        public string CategoryName { get; set; }
        public string ClassLevelId { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ClassLevel ClassLevel { get; set; }
    }
}