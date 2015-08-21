namespace SocialNetwork.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EditPostBindingModel
    {
        [Required]
        [MinLength(5)]
        public string Content { get; set; }
    }
}