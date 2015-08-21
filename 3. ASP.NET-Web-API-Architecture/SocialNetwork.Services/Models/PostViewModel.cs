namespace SocialNetwork.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using SocialNetwork.Models;

    public class PostViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public UserViewModel Author { get; set; }

        public UserViewModel WallOwner { get; set; }

        public DateTime PostedOn { get; set; }

        public int LikesCount { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public static Expression<Func<Post, PostViewModel>> Create
        {
            get
            {
                return p => new PostViewModel
                {
                    Id = p.Id,
                    Content = p.Content,
                    Author = new UserViewModel()
                    {
                        Username = p.Author.UserName
                    },
                    WallOwner = new UserViewModel()
                    {
                        Username = p.WallOwner.UserName
                    },
                    PostedOn = p.PostedOn,
                    LikesCount = p.Likes.Count(),
                    Comments = p.Comments
                        .OrderBy(c => c.PostedOn)
                        .Take(3)
                        .Select(c => new CommentViewModel()
                        {
                            Id = c.Id,
                            Content = c.Content,
                            Author = new UserViewModel()
                            {
                                Username = c.Author.UserName
                            }
                        })
                };
            }
        }
    }
}