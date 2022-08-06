using Entities;
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
            builder.Entity<Article>().HasMany(a => a.Categories).WithMany(b => b.Articles);
            builder.Entity<AppUser>().HasMany(a=>a.Categories).WithMany(b=>b.AppUsers);            
            base.OnModelCreating(builder);
        }
    }
}
