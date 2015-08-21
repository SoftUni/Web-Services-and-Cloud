namespace SocialNetwork.Services.Models
{
    public class UserSearchBindingModel
    {
        public string Name { get; set; }

        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }
    }
}