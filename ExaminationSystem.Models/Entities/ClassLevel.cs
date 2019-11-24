using ExaminationSystem.Models.Base;

namespace ExaminationSystem.Models.Entities
{
    public class ClassLevel : BaseEntityWithId
    {
        public string ClassLevelName { get; set; }
        public string Description { get; set; }
    }
}