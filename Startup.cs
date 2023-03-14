using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BLUEWAY.Data;
using BLUEWAY.Interfaces;
using BLUEWAY.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BLUEWAY
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
            services.AddSingleton<SqlCnConfigMain>();
            services.AddTransient<IadSolicitarViaje, adSolicitarAsignacionService>();
			services.AddTransient<ILogin, LoginService>();

			services.AddDistributedMemoryCache();

			services.AddSession(options =>
			{
				options.Cookie.Name = ".mBlueway.Session";
				options.IdleTimeout = TimeSpan.FromHours(8); //El alias de la sesión dura 8 horas desde que se accede por última vez, el tiempo vuelve a contar de 0 cada vez que se consulte el alias de una sesion en especifico
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
				options.Cookie.SameSite = SameSiteMode.Lax;
				options.Cookie.SecurePolicy = CookieSecurePolicy.None; //colocar always para que siempre sea por https
			});


			services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
			app.UseSession();

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=Login}/{action=Login}/{id?}");
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                    //pattern: "{controller=Mapa}/{action=mapakml}/{id?}");
            });
        }
    }
}
