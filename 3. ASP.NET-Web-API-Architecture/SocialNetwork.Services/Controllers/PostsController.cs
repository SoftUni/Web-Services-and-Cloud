namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Models;
    using SocialNetwork.Models;

    [Authorize]
    public class PostsController : BaseApiController
    {
        // GET api/posts
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult ExtractPosts()
        {
            Thread.Sleep(5000);

            var data = this.Data.Posts
                .OrderBy(p => p.PostedOn)
                .Select(PostViewModel.Create);

            return this.Ok(data);
        }

        // POST api/posts/{id}
        [HttpPost]
        public IHttpActionResult AddPost(
            [FromBody]AddPostBindingModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Model cannot be null (no data in request)");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }
            
            var wallOwner = this.Data.Users
                .FirstOrDefault(u => u.UserName == model.WallOwnerUsername);

            if (wallOwner == null)
            {
                return this.BadRequest(string.Format(
                    "User {0} does not exist",
                    model.WallOwnerUsername));
            }

            string loggedUserId = this.User.Identity
                .GetUserId();

            var post = new Post()
            {
                AuthorId = loggedUserId,
                WallOwner = wallOwner,
                Content = model.Content,
                PostedOn = DateTime.Now,
            };

            this.Data.Posts.Add(post);
            this.Data.SaveChanges();

            var data = this.Data.Posts
                .Where(p => p.Id == post.Id)
                .Select(PostViewModel.Create)
                .FirstOrDefault();

            return this.Ok(data);
        }

        // PUT api/posts/{id}
        [HttpPut]
        public IHttpActionResult EditPost(
            int id,
            [FromBody]EditPostBindingModel model)
        {
            var post = this.Data.Posts.Find(id);
            if (post == null)
            {
                return this.NotFound();
            }

            var loggedUserId = this.User.Identity.GetUserId();
            if (loggedUserId != post.AuthorId)
            {
                return this.Unauthorized();
            }

            if (model == null)
            {
                return this.BadRequest("Model is empty");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }
            
            post.Content = model.Content;
            this.Data.SaveChanges();

            var data = this.Data.Posts
                .Where(p => p.Id == post.Id)
                .Select(PostViewModel.Create)
                .FirstOrDefault();

            return this.Ok(data);
        }

        // DELETE api/posts/{id}
        [HttpDelete]
        public IHttpActionResult DeletePost(int id)
        {
            var post = this.Data.Posts.Find(id);
            if (post == null)
            {
                return this.NotFound();
            }

            var loggedUserId = this.User.Identity.GetUserId();

            if (loggedUserId != post.AuthorId && 
                loggedUserId != post.WallOwnerId && 
                !this.User.IsInRole("Admin"))
            {
                return this.Unauthorized();
            }
            
            this.Data.Posts.Remove(post);
            this.Data.SaveChanges();

            return this.Ok();
        }
    }
}