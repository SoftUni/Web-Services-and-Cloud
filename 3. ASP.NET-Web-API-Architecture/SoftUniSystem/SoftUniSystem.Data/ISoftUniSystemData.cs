namespace SoftUniSystem.Data
{
    using SoftUniSystem.Data.Repositories;
    using SoftUniSystem.Models;

    public interface ISoftUniSystemData
    {
        IRepository<ApplicationUser> Users { get; }
 
        // TODO: Add repositories

        int SaveChanges();
    }
}
