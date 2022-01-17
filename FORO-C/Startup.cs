using FORO_C.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using FORO_C.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FORO_C
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private bool _dbInMemory = false;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_dbInMemory)
            {
                //InMemory
                services.AddDbContext<ForoContext>(options => options.UseInMemoryDatabase("ForoDB"));
            }
            else
            {
                //SQLServer
                services.AddDbContext<ForoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ForoCS")));
            }

            services.AddIdentity<Usuario, Rol>().AddEntityFrameworkStores<ForoContext>();

            services.Configure<IdentityOptions>(
                opciones =>
                {
                    opciones.Password.RequiredLength = 4;
                }
                );


            services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
                opciones =>
                {
                    opciones.LoginPath = "/Registraciones/IniciarSesion";
                    opciones.AccessDeniedPath = "";
                });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ForoContext contexto)
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

            if (!_dbInMemory)
                contexto.Database.Migrate();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            }
            );
        }
    }
}
