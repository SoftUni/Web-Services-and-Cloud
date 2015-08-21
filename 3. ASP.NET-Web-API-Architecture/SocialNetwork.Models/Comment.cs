namespace SocialNetwork.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
        
        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
