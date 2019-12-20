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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            new CategoryBuilder(modelBuilder.Entity<Category>());
            new ClassLevelBuilder(modelBuilder.Entity<ClassLevel>());
            new QuestionBuilder(modelBuilder.Entity<Question>());
            new NoteBuilder(modelBuilder.Entity<Note>());
            new ExamPatameterBuilder(modelBuilder.Entity<ExamParameter>());
            new SolvedQuestionBuilder(modelBuilder.Entity<SolvedQuestion>());

            //modelBuilder.Entity<ClassLevel>().HasMany(x => x.Categories).WithOne().HasForeignKey(x => x.ClassLevelId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Category>().HasOne(x => x.ClassLevel).WithMany(x => x.Categories)
                .HasForeignKey(x => x.ClassLevelId).OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Category>().HasMany(x => x.Questions).WithOne().HasForeignKey(x => x.CategoryId)
            //    .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Question>().HasOne(x => x.Category).WithMany(x => x.Questions)
                .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Category>().HasMany(x => x.Notes).WithOne().HasForeignKey(x => x.CategoryId)
            //    .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Note>().HasOne(x => x.Category).WithMany(x => x.Notes).HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

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