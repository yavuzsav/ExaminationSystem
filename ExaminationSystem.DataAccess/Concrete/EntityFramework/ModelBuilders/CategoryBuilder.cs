using System;
using ExaminationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework.ModelBuilders
{
    public class CategoryBuilder
    {
        public CategoryBuilder(EntityTypeBuilder<Category> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.CategoryName).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.OnCreated).IsRequired().HasDefaultValue(DateTime.Now);
            entityBuilder.Property(x => x.CreatedUserName).IsRequired();
        }
    }
}