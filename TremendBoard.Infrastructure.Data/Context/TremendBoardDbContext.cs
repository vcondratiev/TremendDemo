using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Identity;

namespace TremendBoard.Infrastructure.Data.Context
{
    public class TremendBoardDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public TremendBoardDbContext(DbContextOptions<TremendBoardDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("Users");
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.ToTable("Roles");
            });

            modelBuilder.Entity<ApplicationUserToken>(b =>
            {
                b.ToTable("UserTokens");
            });

            modelBuilder.Entity<ApplicationUserLogin>(b =>
            {
                b.ToTable("UserLogins");
            });

            modelBuilder.Entity<ApplicationUserClaim>(b =>
            {
                b.ToTable("UserClaims");
            });

            modelBuilder.Entity<ApplicationRoleClaim>(b =>
            {
                b.ToTable("RoleClaims");
            });

            modelBuilder.Entity<ApplicationUserRole>(b =>
            {
                b.ToTable("UserRoles");
            });

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Tasks)
                .WithOne(x => x.Project)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.UserRoles)
                .WithOne(x => x.Project)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectTask>()
                .Property(s => s.Duration)
                .HasConversion(new TimeSpanToTicksConverter());
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }

    }
}
