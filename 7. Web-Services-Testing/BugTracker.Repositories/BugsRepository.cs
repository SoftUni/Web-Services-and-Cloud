namespace BugTracker.Repositories
{
    using System.Data.Entity;
    using System.Linq;

    using BugTracker.Data;

    public class BugsRepository : IRepository<Bug>
    {
        private readonly DbContext dbContext;

        public BugsRepository(DbContext context)
        {
            this.dbContext = context;
        }

        public Bug Add(Bug entity)
        {
            this.dbContext.Set<Bug>().Add(entity);
            return entity;
        }

        public IQueryable<Bug> All()
        {
            return this.dbContext.Set<Bug>();
        }

        public void Delete(Bug entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        public void Update(Bug entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public Bug Find(int id)
        {
            return this.dbContext.Set<Bug>().Find(id);
        }

        private void ChangeState(Bug bug, EntityState state)
        {
            var entry = this.dbContext.Entry(bug);
            if (entry.State == EntityState.Detached)
            {
                this.dbContext.Set<Bug>().Attach(bug);
            }

            entry.State = state;
        }
    }
}
