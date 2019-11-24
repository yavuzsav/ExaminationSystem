using ExaminationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework.ModelBuilders
{
    public class ExamPatameterBuilder
    {
        public ExamPatameterBuilder(EntityTypeBuilder<ExamParameter> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NumberOfQuestions).IsRequired().HasDefaultValue(20);
            builder.Property(x => x.LengthOfExam).IsRequired().HasDefaultValue(30);
        }
    }
}