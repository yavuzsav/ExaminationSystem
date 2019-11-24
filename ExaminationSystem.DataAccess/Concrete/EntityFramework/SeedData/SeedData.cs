using ExaminationSystem.Models.Entities;
using ExaminationSystem.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework.SeedData
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            List<ClassLevel> classLevels = new List<ClassLevel>
            {
                new ClassLevel { Id = Guid.NewGuid().ToString(), ClassLevelName = "1.Sınıf", Description = "1" },
                new ClassLevel { Id = Guid.NewGuid().ToString(), ClassLevelName = "2.Sınıf", Description = "2" },
                new ClassLevel { Id = Guid.NewGuid().ToString(), ClassLevelName = "3.Sınıf", Description = "3" },
                new ClassLevel { Id = Guid.NewGuid().ToString(), ClassLevelName = "4.Sınıf", Description = "4" },
                new ClassLevel { Id = Guid.NewGuid().ToString(), ClassLevelName = "5.Sınıf", Description = "5" },
                new ClassLevel { Id = Guid.NewGuid().ToString(), ClassLevelName = "6.Sınıf", Description = "6" },
                new ClassLevel { Id = Guid.NewGuid().ToString(), ClassLevelName = "7.Sınıf", Description = "7" },
                new ClassLevel { Id = Guid.NewGuid().ToString(), ClassLevelName = "8.Sınıf", Description = "8" },
                new ClassLevel { Id = Guid.NewGuid().ToString(), ClassLevelName = "9.Sınıf", Description = "9" },
                new ClassLevel { Id = Guid.NewGuid().ToString(), ClassLevelName = "10.Sınıf", Description = "10" },
                new ClassLevel { Id = Guid.NewGuid().ToString(), ClassLevelName = "11.Sınıf", Description = "11" },
                new ClassLevel { Id = Guid.NewGuid().ToString(), ClassLevelName = "12.Sınıf", Description = "12" }
            };

            List<Category> categories = new List<Category>();

            for (int i = 0; i < classLevels.Count; i++)
            {
                categories.Add(new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    CategoryName = "Edebiyat",
                    ClassLevelId = classLevels[i].Id,
                    CreatedUserName = "DataSeed",
                });

                categories.Add(new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    CategoryName = "Coğrafya",
                    ClassLevelId = classLevels[i].Id,
                    CreatedUserName = "DataSeed",
                });

                categories.Add(new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    CategoryName = "Kimya",
                    ClassLevelId = classLevels[i].Id,
                    CreatedUserName = "DataSeed",
                });

                categories.Add(new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    CategoryName = "Fizik",
                    ClassLevelId = classLevels[i].Id,
                    CreatedUserName = "DataSeed",
                });
            }

            List<Question> questions = new List<Question>();

            for (int i = 0; i < 700; i++)
            {
                questions.Add(new Question
                {
                    Id = Guid.NewGuid().ToString(),
                    CategoryId = categories[FakeData.NumberData.GetNumber(categories.Count)].Id,
                    QuestionContent = FakeData.TextData.GetSentence(),
                    A = FakeData.TextData.GetSentence(),
                    B = FakeData.TextData.GetSentence(),
                    C = FakeData.TextData.GetSentence(),
                    D = FakeData.TextData.GetSentence(),
                    CorrectAnswer = FakeData.EnumData.GetElement<CorrectAnswer>(),
                    CreatedUserName = "DataSeed",
                });
            }

            modelBuilder.Entity<ClassLevel>().HasData(classLevels);
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Question>().HasData(questions);
        }
    }
}