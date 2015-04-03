using BattleShips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Data.Repositories
{
    using System.Data.Entity;

    public class GamesRepository : GenericRepository<Game>
    {
        public GamesRepository(DbContext context) 
            : base(context)
        {
        }

        public IQueryable<Game> GetGamesByState(GameState state)
        {
            return this.Set.Where(x => x.State == state);
        }
    }
}
