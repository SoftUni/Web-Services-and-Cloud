namespace SoftUniSystem.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using SoftUniSystem.Data;

    public class UsersController : BaseApiConroller
    {
        public UsersController(ISoftUniSystemData data) : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult GetUsersCount()
        {
            var count = this.Data.Users.All().Count();
            return this.Ok(count);
        }
    }
}