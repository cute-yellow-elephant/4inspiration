using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain;

namespace SqlRepository
{
    public class ProjectDBContext: DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<PictureSet> PictureSets { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ProjectDBContext() : base("name=ProjectDBContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users).WithMany(u => u.Roles)
                .Map(t => t.MapLeftKey("RoleID")
                    .MapRightKey("UserID")
                    .ToTable("RolesUsers"));

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.Pictures).WithMany(p => p.Tags)
                .Map(t => t.MapLeftKey("TagID")
                    .MapRightKey("PictureID")
                    .ToTable("TagsPictures"));

            modelBuilder.Entity<Profile>()
                .HasMany(p => p.Followers).WithMany(p => p.Following)
                .Map(t => t.MapLeftKey("FollowingID")
                    .MapRightKey("FollowerID")
                    .ToTable("FollowersFollowing"));

            modelBuilder.Entity<Profile>()
                .HasMany(p => p.HeartedPics).WithMany(p => p.PeopleWhoLiked)
                .Map(t => t.MapLeftKey("WhoLikedID")
                    .MapRightKey("HeartedPicID")
                    .ToTable("HeartsPeople"));
        } 
    }
}
