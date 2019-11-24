using ExaminationSystem.Framework.Entities;

namespace ExaminationSystem.Models.Base
{
    public class BaseEntityWithId : IEntity
    {
        public string Id { get; set; }
    }
}