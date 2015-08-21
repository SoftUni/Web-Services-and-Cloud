namespace SocialNetwork.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Description;
    using System.Web.OData;
    using Models;

    [Authorize]
    public class UsersController : BaseApiController
    {
        [Route("api/users/{username}/wall")]
        [EnableQuery]
        [ResponseType(typeof(IQueryable<AddPostBindingModel>))]
        public IHttpActionResult GetUserWall(string username)
        {
            var user = this.Data.Users
                .FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return this.BadRequest();
            }

            var userWall = this.Data.Posts
                .Where(p => p.WallOwnerId == user.Id)
                .Select(PostViewModel.Create);

            return this.Ok(userWall);
        }

        // GET api/users/search?name=..&minAge=..&maxAge=..
        [ActionName("search")]
        [HttpGet]
        public IHttpActionResult SearchUser(
            [FromUri]UserSearchBindingModel model)
        {
            var usersSearchResult = this.Data.Users.AsQueryable();

            if (model.Name != null)
            {
                usersSearchResult = usersSearchResult
                    .Where(u => u.UserName.Contains(model.Name));
            }

            if (model.MinAge.HasValue)
            {
                usersSearchResult = usersSearchResult
                    .Where(u => u.Age >= model.MinAge.Value);
            }

            if (model.MaxAge.HasValue)
            {
                usersSearchResult = usersSearchResult
                    .Where(u => u.Age <= model.MaxAge.Value);
            }

            var finalResult = usersSearchResult
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.UserName,
                    u.Age
                });

            return this.Ok(finalResult);
        }
    }
}