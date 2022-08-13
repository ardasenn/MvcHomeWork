using Entities;
using MediumClone.Entities;
using MediumClone.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediumClone.Models.Context
{
    public class AppDbContext :IdentityDbContext<AppUser,IdentityRole,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> context) :base(context)
        {

        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }        
        protected override void OnModelCreating(ModelBuilder builder)
        {
           builder.Entity<ProfileImage>().HasOne<AppUser>(a=>a.User).WithOne(b=>b.ProfileImage).HasForeignKey<ProfileImage>(s=>s.UserId);
            builder.Entity<Article>().HasMany(a => a.Categories).WithMany(b => b.Articles);
            builder.Entity<AppUser>().HasMany(a=>a.Categories).WithMany(b=>b.AppUsers);
            builder.Entity<ProfileImage>().Ignore(a => a.ImageFile);
            builder.Entity<ProfileImage>().HasKey(a => a.ImageId);
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User" });
            builder.Entity<AppUser>().HasData(new AppUser { Email = "ardasen.96@gmail.com", NormalizedEmail= "ARDASEN.96@GMAIL.COM", UserName = "Developer", NormalizedUserName="DEVELOPER", PasswordHash = "AQAAAAEAACcQAAAAEExFHJ5JkVIsbrxfy7NtylSVvQ6V9wo3Zx+aHC1d0SgeVshpGdsNSMJTcohXsL3iGQ==", FirstName = "Developer", LastName = "Best", EmailConfirmed = true });           

            base.OnModelCreating(builder);
        }
    }
}
