using System;
using ExaminationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework.ModelBuilders
{
    public class QuestionBuilder
    {
        public QuestionBuilder(EntityTypeBuilder<Question> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.CategoryId).IsRequired();
            entityBuilder.Property(x => x.QuestionContent).IsRequired();
            entityBuilder.Property(x => x.A).IsRequired();
            entityBuilder.Property(x => x.B).IsRequired();
            entityBuilder.Property(x => x.C).IsRequired();
            entityBuilder.Property(x => x.D).IsRequired();
            entityBuilder.Property(x => x.CorrectAnswer).IsRequired();

            entityBuilder.Property(x => x.CreatedUserName).IsRequired();
            entityBuilder.Property(x => x.OnCreated).IsRequired().HasDefaultValue(DateTime.Now);
        }
    }
}