using ExaminationSystem.DataAccess.Concrete.EntityFramework.ModelBuilders;
using ExaminationSystem.Framework.Entities.Concrete;
using ExaminationSystem.Models.Entities;
using ExaminationSystem.Models.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework.Contexts
{
    public class ExaminationSystemContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ExaminationSystemContext(DbContextOptions options) : base(options)
        {
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

            modelBuilder.Entity<Category>().HasOne(x => x.ClassLevel).WithMany(x => x.Categories)
                .HasForeignKey(x => x.ClassLevelId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>().HasOne(x => x.Category).WithMany(x => x.Questions)
                .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Note>().HasOne(x => x.Category).WithMany(x => x.Notes).HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
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