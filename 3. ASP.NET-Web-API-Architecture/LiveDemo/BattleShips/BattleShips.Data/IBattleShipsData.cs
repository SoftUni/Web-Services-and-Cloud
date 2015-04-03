using System;
namespace BattleShips.Data
{
    using BattleShips.Data.Repositories;
    using BattleShips.Models;

    public interface IBattleShipsData
    {
        IRepository<Game> Games { get; }

        IRepository<Ship> Ships { get; }

        int SaveChanges();
    }
}