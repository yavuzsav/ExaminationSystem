using System;
using ExaminationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework.ModelBuilders
{
    public class NoteBuilder
    {
        public NoteBuilder(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Correct).IsRequired();
            builder.Property(x => x.Wrong).IsRequired();
            builder.Property(x => x.Empty).IsRequired();
            builder.Property(x => x.Date).IsRequired().HasDefaultValue(DateTime.Now);
        }
    }
}