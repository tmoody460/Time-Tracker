using Microsoft.EntityFrameworkCore;
using TimeTracker.Accessors.EntityFramework;

namespace TimeTracker.Accessors
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}