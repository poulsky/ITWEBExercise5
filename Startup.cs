using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITWEBExercise5.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ITWEBExercise5.Data.Repository;

namespace ITWEBExercise5
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
            services.AddTransient<IComponentRepository, ComponentRepository>();
            services.AddTransient<IComponentTypeRepository, ComponentTypeRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddDbContext<EmbeddedStockContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(
                        "Development")));
            //services.AddDbContext<EmbeddedStockContext>(options => options.UseSqlite("Data Source=TestES.db"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
