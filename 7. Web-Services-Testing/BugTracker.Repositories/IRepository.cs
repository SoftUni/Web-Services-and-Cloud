namespace BugTracker.Repositories
{
    using System.Linq;

    public interface IRepository<T>
    {
        T Add(T entity);

        T Find(int id);

        IQueryable<T> All();

        void Delete(T entity);

        void Update(T entity);

        void SaveChanges();
    }
}
