using ExaminationSystem.Framework.Entities;
using System;

namespace ExaminationSystem.Models.Base
{
    public class BaseEntityWithProperties : IEntity
    {
        public string Id { get; set; }
        public DateTime OnCreated { get; set; } = DateTime.Now;
        public DateTime? OnModified { get; set; } = DateTime.Now;
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }
    }
}