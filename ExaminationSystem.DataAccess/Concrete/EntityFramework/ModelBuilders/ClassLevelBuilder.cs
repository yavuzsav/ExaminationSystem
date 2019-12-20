using ExaminationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework.ModelBuilders
{
    public class ClassLevelBuilder
    {
        public ClassLevelBuilder(EntityTypeBuilder<ClassLevel> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.ClassLevelName).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.Description);
        }
    }
}