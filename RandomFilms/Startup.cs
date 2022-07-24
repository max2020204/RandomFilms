using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using AspNetCore.SEOHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RandomFilms.Data;
using RandomFilms.Data.Repositories.Impliment.EF;
using RandomFilms.Data.Repositories.Interfaces;
using RandomFilms.Models;

namespace RandomFilms
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
            services.AddTransient<IFilmRepository, EFFilm>();
            services.AddTransient<IGenereRepository, EFGenre>();
            services.AddTransient<IFilmGenreRepository, EFFilmGenre>();
            services.AddTransient<ICountryRepository, EFCountry>();
            services.AddTransient<ICountryFilmRepository, EFFilmCountry>();
            services.AddTransient<DataManager>();
            services.AddDbContext<AppDbContext>(
                  options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddControllersWithViews();
            services.AddResponseCompression(options => options.EnableForHttps = true);
            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //env.EnvironmentName = "Development";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseDeveloperExceptionPage();
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            app.UseResponseCompression();
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                 {
                     ctx.Context.Response.Headers.Add("Cache-Control", "public,max-age=2678400");
                 }
            });
            app.UseRobotsTxt(env.ContentRootPath);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "Film",
                    pattern: "Film/{action=Index}/{id?}",
                    defaults: new { Controller = "Film", action = "Index" });
                endpoints.MapControllerRoute(
                   name: "Admin",
                   pattern: "Admin/{controller=Movie}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                   name: "Admin",
                   pattern: "Admin/{controller=Genre}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                   name: "Admin",
                   pattern: "Admin/{controller=Users}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                   name: "Admin",
                   pattern: "Admin/{controller=Roles}/{action=Index}/{id?}");
            });
        }
    }
}
