using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sneaker_Bar.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Design;

namespace Sneaker_Bar
{
    public class Startup
    {

        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            /*  services.AddDbContext<PurchaseContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=Sneakers; Persist Security Info=False; MultipleActiveResultSets=True; Trusted_Connection=True;"));
              services.AddDbContext<SneakersContext>(options=>options.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=Sneakers; Persist Security Info=False; MultipleActiveResultSets=True; Trusted_Connection=True;"));
              services.AddDbContext<UserContext>(options=>options.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=Sneakers; Persist Security Info=False; MultipleActiveResultSets=True; Trusted_Connection=True;"));
              services.AddDbContext<CommentContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=Sneakers; Persist Security Info=False; MultipleActiveResultSets=True; Trusted_Connection=True;"));
              services.AddDbContext<ArticleContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=Sneakers; Persist Security Info=False; MultipleActiveResultSets=True; Trusted_Connection=True;"));
            */
            services.AddDbContext<PurchaseContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("Connection")));
            services.AddDbContext<SneakersContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("Connection")));
            services.AddDbContext<UserContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("Connection")));
            services.AddDbContext<CommentContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("Connection")));
            services.AddDbContext<ArticleContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("Connection")));

             services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<UserContext>();

            services.AddControllersWithViews(mvcOtions =>
            {
                mvcOtions.EnableEndpointRouting = false;
            });
        
            services.AddRazorPages();
         
            services.AddScoped<SneakersRepository>();
            services.AddScoped<PurchaseRepository>();
            services.AddScoped<CommentRepository>();
            services.AddScoped<ArticleRepository>();


            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseBrowserLink();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(routes =>
            {



                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
               
                routes.MapRoute(name: "detail", template: "Sneakers/SneakersDetail/{id?}");

            });
        }
    }
}
