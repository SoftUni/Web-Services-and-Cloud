using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Data
{
    using BattleShips.Data.Repositories;
    using BattleShips.Models;
    using System.Data.Entity;

    public class BattleShipsData : IBattleShipsData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;


        public BattleShipsData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Game> Games
        {
            get
            {
                return this.GetRepository<Game>();
            }
        }

        public IRepository<Ship> Ships
        {
            get
            {
                return this.GetRepository<Ship>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!this.repositories.ContainsKey(type))
            {
                //var typeOfRepository = typeof(GenericRepository<T>);
                //if (type.IsAssignableFrom(typeof(Game)))
                //{
                //    typeOfRepository = typeof(GamesRepository);
                //}

                var repository = Activator.CreateInstance(typeof(GenericRepository<T>), this.context);
                this.repositories.Add(type, repository);
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}