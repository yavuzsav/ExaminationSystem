using System.Collections.Generic;
using ExaminationSystem.Models.Base;

namespace ExaminationSystem.Models.Entities
{
    public class ClassLevel : BaseEntityWithId
    {
        public string ClassLevelName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}