namespace SocialNetwork.Services.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public UserViewModel Author { get; set; }
    }
}