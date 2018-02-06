using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using DMS.Models;


namespace DMS
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940


        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            DatabaseConnection.ConnectionString = Configuration["Data:DMS:ConnectionString"];
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseBrowserLink();
            app.UseMvc(routes => {
                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");

            });

            // Gavin Testing 09-Jan-2018
            //app.UseMvc(routes => {
            //    routes.MapRoute(
            //    name: "AddDocument",
            //    template: "{controller=Document}/{action=AddDocument}/{id?}");

            //});

            //app.UseMvc(routes => {
            //    routes.MapRoute(
            //    name: "EditDocument",
            //    template: "{controller=Document}/{action=EditDocument}/{id?}");

            //});

            // commented out cos I'm not sure whether I still need the app.Run call. Gavin  08-Jan-2018
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
