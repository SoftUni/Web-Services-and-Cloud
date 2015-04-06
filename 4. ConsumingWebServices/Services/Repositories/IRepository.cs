namespace Services.Repositories
{
    using System.Collections.Generic;

    interface IRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        T Add(T item);
        void Delete(T item);
        void Update(T item);
    }
}
