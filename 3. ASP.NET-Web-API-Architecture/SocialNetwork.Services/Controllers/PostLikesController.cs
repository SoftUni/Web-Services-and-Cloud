namespace SocialNetwork.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using SocialNetwork.Models;

    [Authorize]
    public class PostLikesController : BaseApiController
    {
        // POST api/posts/{postId}/likes
        [Route("api/posts/{postId}/likes")]
        [HttpPost]
        public IHttpActionResult LikePost(int postId)
        {
            var post = this.Data.Posts.Find(postId);
            if (post == null)
            {
                return this.NotFound();
            }

            var loggedUserId = this.User.Identity.GetUserId();

            var isAlreadyLiked = post.Likes
                .Any(pl => pl.UserId == loggedUserId);
            if (isAlreadyLiked)
            {
                return this.BadRequest("You have already liked this post");
            }

            if (post.AuthorId == loggedUserId)
            {
                return this.BadRequest("Cannot like own posts");
            }

            post.Likes.Add(new PostLike()
            {
                UserId = loggedUserId
            });

            this.Data.SaveChanges();

            return this.Ok();
        }
    }
}