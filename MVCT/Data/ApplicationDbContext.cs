using MVCT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using MVCT.Models.Attandance;
using MVCT.Models.UserReport;

namespace MVCT.Data
{

    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        public DbSet<Attandances> Attandances { set; get; }

        public DbSet<UserReports> UserReports { set; get; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Attandances>().HasOne(p => p.User).WithMany(u => u.Attandances).HasForeignKey(a => a.UserId);
            builder.Entity<UserReports>().HasOne(p => p.User).WithMany(u => u.UserReports).HasForeignKey(a => a.UserId);

            //builder.Entity<AppUser>().HasMany(p => p.Attandances).WithOne(u => u.User).HasForeignKey(a => a.UserId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }
    }
}
