using ExaminationSystem.DataAccess.Concrete.EntityFramework.Contexts;
using ExaminationSystem.Models.Entities;
using ExaminationSystem.Models.Enums;
using ExaminationSystem.Models.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework.SeedData
{
    public static class SeedData
    {
        public static async Task Seed(ExaminationSystemContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (!context.Roles.Any())
            {
                await roleManager.CreateAsync(new AppRole { Name = "Student" });
                await roleManager.CreateAsync(new AppRole { Name = "Admin" });
            }

            var bob = new AppUser
            {
                Id = "a",
                UserName = "bob",
                Email = "bob@test.com",
            };

            var jane = new AppUser
            {
                Id = "b",
                UserName = "jane",
                Email = "jane@test.com"
            };

            var tom = new AppUser
            {
                Id = "c",
                UserName = "tom",
                Email = "tom@test.com"
            };

            if (!userManager.Users.Any())
            {
                await userManager.CreateAsync(bob, "Pa$$w0rd");
                await userManager.AddToRoleAsync(bob, "Admin");

                await userManager.CreateAsync(jane, "Pa$$w0rd");
                await userManager.AddToRoleAsync(jane, "Student");

                await userManager.CreateAsync(tom, "Pa$$w0rd");
                await userManager.AddToRoleAsync(tom, "Student");
            }


            //create classlevel
            List<ClassLevel> classLevels = new List<ClassLevel>
            {
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
                    ClassLevel = classLevels[i],
                    CreatedUser = bob,
                });

                categories.Add(new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    CategoryName = "Coğrafya",
                    ClassLevelId = classLevels[i].Id,
                    CreatedUser = bob
                });

                categories.Add(new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    CategoryName = "Kimya",
                    ClassLevelId = classLevels[i].Id,
                    CreatedUser = jane
                });

                categories.Add(new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    CategoryName = "Fizik",
                    ClassLevelId = classLevels[i].Id,
                    CreatedUser = jane
                });
            }

            List<Question> questions = new List<Question>();

            foreach (var category in categories)
            {
                for (int i = 0; i < 50; i++)
                {
                    questions.Add(new Question
                    {
                        Id = Guid.NewGuid().ToString(),
                        //CategoryId = categories[FakeData.NumberData.GetNumber(categories.Count)].Id,
                        Category = category,
                        QuestionContent = FakeData.TextData.GetSentence(),
                        A = FakeData.TextData.GetSentence(),
                        B = FakeData.TextData.GetSentence(),
                        C = FakeData.TextData.GetSentence(),
                        D = FakeData.TextData.GetSentence(),
                        CorrectAnswer = FakeData.EnumData.GetElement<CorrectAnswer>(),
                        CreatedUser = category.CreatedUser
                    });
                }
            }

            context.ClassLevels.AddRange(classLevels);
            context.Categories.AddRange(categories);
            context.Questions.AddRange(questions);
            context.SaveChanges();
        }
    }
}