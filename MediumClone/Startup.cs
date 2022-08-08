using FluentValidation.AspNetCore;
using MediumClone.Models.Authentication;
using MediumClone.Models.Context;
using MediumClone.Repositories.Abstract;
using MediumClone.Repositories.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediumClone
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddDbContext<AppDbContext>(a => a.UseSqlServer(Configuration["ConnectionStrings:ConStr"]));
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {                
                options.SignIn.RequireConfirmedEmail = true ;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();//identity role þuan standart olarak ayaða kalkacak.Eðer deðiþtirmek istiyorsam kalýtým verdiðim sýnýfý yazmam gerekiyor.
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddSession();
            //services.AddTransient<IEmailSender, EmailSender>();
            services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.Name = "Mycookie";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(120);
                option.SlidingExpiration = true;
                option.LoginPath = "/LogIn/Login";
                option.AccessDeniedPath = "/Home/AccessDenied";

            });
            
            services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
           


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();//Identity için bunu ekledik
            app.UseSession();            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
