namespace SocialNetwork.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;

    public class SocialNetworkContext : IdentityDbContext<ApplicationUser>
    {
        public SocialNetworkContext()
            : base("SocialNetworkContext")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<SocialNetworkContext, Configuration>());
        }

        public virtual IDbSet<Post> Posts { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<PostLike> PostLikes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.WallPosts)
                .WithRequired(p => p.WallOwner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.OwnPosts)
                .WithRequired(p => p.Author)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public static SocialNetworkContext Create()
        {
            return new SocialNetworkContext();
        }
    }
}