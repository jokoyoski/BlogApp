using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlogApp
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {


            var builder = new ConfigurationBuilder();


        
            if (env.IsDevelopment())
            {
                // Adds JSON settings first
                builder.SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            }
            else
            {

            }




            // Adds environment variables
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            this.env = env;

            
        }

        public IConfigurationRoot Configuration { get; }
        public IWebHostEnvironment env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton(Configuration);
            var conn = Configuration.GetSection("DB_CONNECTION_STRING").Value;

            services.AddDbContext<ApiDbContext>(
                e =>
                {
                   
                    e.UseSqlServer(conn);
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
