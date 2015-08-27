namespace BugTracker.RestApi.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BugTracker.Repositories;

    public class RepositoryMock<T> : IRepository<T>
    {
        public IList<T> Entities { get; set; }

        public RepositoryMock()
        {
            this.Entities = new List<T>();
        }

        public T Add(T entity)
        {
            this.Entities.Add(entity);
            return entity;
        }

        public T Find(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> All()
        {
            return this.Entities.AsQueryable();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            this.IsSaveCalled = true;
        }

        public bool IsSaveCalled { get; set; }
    }
}
