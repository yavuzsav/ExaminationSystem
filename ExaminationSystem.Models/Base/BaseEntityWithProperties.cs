using ExaminationSystem.Framework.Entities;
using ExaminationSystem.Models.IdentityEntities;
using System;

namespace ExaminationSystem.Models.Base
{
    public class BaseEntityWithProperties : IEntity
    {
        public string Id { get; set; }
        public DateTime OnCreated { get; set; } = DateTime.Now;
        public DateTime? OnModified { get; set; }
        public virtual AppUser CreatedUser { get; set; }
        public virtual AppUser ModifiedUser { get; set; }

    }
}