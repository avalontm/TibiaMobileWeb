using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PluginSQL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TibiaMobileWeb.Helpers;

namespace TibiaMobileWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            //services.AddControllersWithViews();
        }

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

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            //Iniciando
            MYSQL.Init(Configuration["ConnectionStrings:DefaultConnection"]);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");

            if (MYSQL.CheckStatus())
            {
                Console.WriteLine("[MYSQL] OK");
            }
            else
            {
                Console.WriteLine("[MYSQL] BAD");
            }

            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            PushNotification.KEY = appSettings.FMCToken;

            Console.WriteLine("[FMCToken] " + PushNotification.KEY);
            Console.WriteLine("[MYSQL] " + MYSQL.connectionString);
        }
    }
}
