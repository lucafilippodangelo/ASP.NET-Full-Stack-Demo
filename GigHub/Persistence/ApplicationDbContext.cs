using GigHub.Persistence.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GigHub.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext() : base("CustomConnectionGit", throwIfV1Schema: false)
        { 
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //LDP1_004
            /*
            //LD BEFORE, with this code I have a "REVERSE RELATIONSHIP" problem
            //modelBuilder.Entity<Attendance>()
            //    .HasRequired(a => a.Gig)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //LDP1_004
            //LD AFTER
            modelBuilder.Entity<Attendance>()
            .HasRequired(a => a.Gig)
            .WithMany(g => g.Attendances)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followees)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);

            //LD BEFORE, with this code I have a "REVERSE RELATIONSHIP" problem
            // this happen because we added 
            //modelBuilder.Entity<UserNotification>()
            //    .HasRequired(n => n.User) //LD each "UserNotification" has ONE and only ONE "User"
            //    .WithMany()//LD THE REVERSAL, each "User" can have MANY "UserNotification"
            //    .WillCascadeOnDelete(false); 
            //LD AFTER
            modelBuilder.Entity<UserNotification>()
            .HasRequired(n => n.User)
            .WithMany(u => u.UserNotifications)
            .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
            */

            //LD WE ARE MOVING ANYTHING TO DEDICATED CLASSES
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new AttendanceConfiguration());
            modelBuilder.Configurations.Add(new FollowingConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new GigConfiguration());
            modelBuilder.Configurations.Add(new NotificationConfiguration());
            modelBuilder.Configurations.Add(new UserNotificationConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}




