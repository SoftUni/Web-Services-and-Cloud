namespace BugTracker.Data
{
    using BugTracker.Data.Migrations;
    using System.Data.Entity;

    public class BugTrackerDbContext : DbContext
    {
        public BugTrackerDbContext()
            : base("BugTracker")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<BugTrackerDbContext, MigrationsConfiguration>());
        }

        public virtual IDbSet<Bug> Bugs { get; set; }
    }
}
