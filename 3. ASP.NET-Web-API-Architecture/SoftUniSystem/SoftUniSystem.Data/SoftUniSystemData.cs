namespace SoftUniSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using SoftUniSystem.Data.Repositories;
    using SoftUniSystem.Models;

    public class SoftUniSystemData : ISoftUniSystemData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories; 

        public SoftUniSystemData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }


        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var repository = Activator.CreateInstance(typeof(GenericRepository<T>), this.context);
                this.repositories.Add(typeOfRepository, repository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        } 
    }
}
