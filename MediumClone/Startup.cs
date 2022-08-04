using MediumClone.Models.Authentication;
using MediumClone.Models.Context;
using MediumClone.Repositories.Abstract;
using MediumClone.Repositories.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
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
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(a => a.UseSqlServer(Configuration["ConnectionStrings:ConStr"]));
            services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<AppDbContext>();//identity role �uan standart olarak aya�a kalkacak.E�er de�i�tirmek istiyorsam kal�t�m verdi�im s�n�f� yazmam gerekiyor.
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();     services.AddSession();      
            services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.Name = "CloneMediumUser";//cookie ad� bu
                option.ExpireTimeSpan = TimeSpan.FromHours(1);//1 saat i�lem olmazsa session sonlan�r
                option.SlidingExpiration = true;//i�lem ya�t���nda s�reyi s�f�rlar
                option.LoginPath = "/LogIn/LogIn";
            });

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
            app.UseAuthentication();//Identity i�in bunu ekledik
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
