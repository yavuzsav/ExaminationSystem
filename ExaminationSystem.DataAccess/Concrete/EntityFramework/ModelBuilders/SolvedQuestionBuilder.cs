using System;
using ExaminationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework.ModelBuilders
{
    public class SolvedQuestionBuilder
    {
        public SolvedQuestionBuilder(EntityTypeBuilder<SolvedQuestion> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.QuestionId).IsRequired();
            builder.Property(x => x.Date).HasDefaultValue(DateTime.Now);
        }
    }
}