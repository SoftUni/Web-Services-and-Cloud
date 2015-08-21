namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Models;
    using SocialNetwork.Models;

    [Authorize]
    public class CommentsController : BaseApiController
    {
        // POST api/posts/{postId}/comments
        [HttpPost]
        [Route("api/posts/{postId}/comments")]
        public IHttpActionResult AddCommentToPost(
            int postId,
            AddCommentBindingModel model)
        {
            var post = this.Data.Posts.Find(postId);
            if (post == null)
            {
                return this.NotFound();
            }

            if (model == null)
            {
                return this.BadRequest("Empty model is not allowed");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var userId = this.User.Identity.GetUserId();

            var comment = new Comment()
            {
                Content = model.Content,
                PostedOn = DateTime.Now,
                AuthorId = userId
            };

            post.Comments.Add(comment);
            this.Data.SaveChanges();

            return this.Ok();
        }
    }
}