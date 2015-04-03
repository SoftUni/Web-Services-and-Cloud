namespace SoftUniSystem.Data
{
    using SoftUniSystem.Data.Repositories;
    using SoftUniSystem.Models;

    public interface ISoftUniSystemData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Game> Games { get; }

        IRepository<GameResult> Results { get; }

        int SaveChanges();
    }
}
