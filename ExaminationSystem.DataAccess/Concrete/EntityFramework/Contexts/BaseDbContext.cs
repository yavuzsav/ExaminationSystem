using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework.Contexts
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext()
        {
        }

        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }
    }
}