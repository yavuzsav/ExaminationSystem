using ExaminationSystem.DataAccess.Concrete.EntityFramework.ModelBuilders;
using ExaminationSystem.Framework.Entities.Concrete;
using ExaminationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework.Contexts
{
    public class ExaminationSystemContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-E6T8364\SQLEXPRESS;Database=ExaminationSystem;Integrated Security=true");
        }

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            new CategoryBuilder(modelBuilder.Entity<Category>());
            new QuestionBuilder(modelBuilder.Entity<Question>());
            new NoteBuilder(modelBuilder.Entity<Note>());
            new ExamPatameterBuilder(modelBuilder.Entity<ExamParameter>());
            new SolvedQuestionBuilder(modelBuilder.Entity<SolvedQuestion>());

            //todo data seed

            SeedData.SeedData.Seed(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ExamParameter> ExamParameters { get; set; }
        public DbSet<SolvedQuestion> SolvedQuestions { get; set; }
        public DbSet<ClassLevel> ClassLevels { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}