using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EvoPC.DataAccess.Interfaces;
using EvoPC.DataAccess.Repository;
using EvoPC.Models;
using EvoPC.Models.Entities;
using EvoPC.Models.Interfaces;
using EvoPC.Services;
using System;
using Microsoft.AspNetCore.Http;

namespace EvoPC.WEB
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
            services.AddDbContext<EvoPCContext>(option => option.UseSqlServer(Configuration.GetConnectionString("AppDbConn")));

            services.AddControllersWithViews();

            services.AddSingleton(MapperConfig.GetMapper());

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region Services

            services.AddScoped<ISocketTypeService, SocketTypeService>();
            services.AddScoped<IProcesorServices, ProcesorService>();

            #endregion Services

            #region Repository

            services.AddScoped<IRepository<SocketType, int>, Repository<SocketType, int>>();
            services.AddScoped<IRepository<Procesor, int>, Repository<Procesor, int>>();

            #endregion Repository
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
