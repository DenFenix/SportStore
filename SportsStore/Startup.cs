using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(
               Configuration["Data:SportStoreProuct:ConnectionString"]));
            services.AddMvc();
            services.AddTransient<IProductRepository, EFProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List" }
                    );
                routes.MapRoute(
                    name: null,
                    template: "{controller=Product}/{action=List}/Page{productPage:int}");
                routes.MapRoute(
                    name:null,
                    template:"{controller=Product}/{action=List}/{category}"
                    );
                routes.MapRoute(
                    name: null,
                    template: "{controller=Product}/{action=List}/{id?}"
                    );
            });
           /* app.UseMvc(routes => {
                routes.MapRoute(
                    name:"pagination",
                    template:"Product/Page{productPage}",
                    defaults:new {Controller = "Product", action = "List" });
                routes.MapRoute(
                    "default",
                    "{controller=Product}/{action=List}/{id?}");
            });*/
            SeedData.EnsurePopulated(app);
        }
    }
}
