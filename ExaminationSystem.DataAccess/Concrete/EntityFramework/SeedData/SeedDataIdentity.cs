using ExaminationSystem.Models.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework.SeedData
{
    public static class SeedDataIdentity
    {
        public static void SeedUserAndRole(ModelBuilder modelBuilder)
        {
            List<AppUser> users = new List<AppUser>
            {
                new AppUser
                {
                    UserName = "Admin",
                    Email = "admin@admin.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEKxVbFFGNeRokGvAN6VFSlqaGjW6762LhAzOL6NAnjP5dc61u9eZPzjNLL/KB9NSxQ==",
                    EmailConfirmed = true,
                }
            };

            List<AppRole> roles = new List<AppRole>
            {
                new AppRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Student",
                    NormalizedName = "STUDENT",
                },
                new AppRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            };

            modelBuilder.Entity<AppUser>().HasData(users);
            modelBuilder.Entity<AppRole>().HasData(roles);
        }
    }
}