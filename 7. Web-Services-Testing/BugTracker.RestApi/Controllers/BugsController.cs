namespace BugTracker.RestApi.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using BugTracker.Data;
    using BugTracker.Repositories;

    public class BugsController : ApiController
    {
        private IRepository<Bug> repo;

        public BugsController() : this(new BugsRepository(new BugTrackerDbContext()))
        {
        }

        public BugsController(IRepository<Bug> repository)
        {
            this.repo = repository;
        }

        public IQueryable<Bug> GetAll()
        {
            var bugs = this.repo.All().OrderBy(b => b.Id);
            return bugs;
        }

        public IHttpActionResult PostBug(Bug bug)
        {
            if (string.IsNullOrEmpty(bug.Text))
            {
                return this.BadRequest("Text cannot be null");
            }

            bug.Status = BugStatus.New;
            bug.DateCreated = DateTime.Now;
            this.repo.Add(bug);
            this.repo.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bug.Id }, bug);
        }
    }
}
