using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using GoogleKeepDB.Model;
using Swashbuckle.AspNetCore.Swagger;

namespace GoogleKeepDB
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment ihostingenv)
        {
            Configuration = configuration;
            _hostingenv = ihostingenv;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment _hostingenv { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<dataAcc>();

            //if(_hostingenv.IsEnvironment("Testing"))
            //{
            //    services.AddDbContext<KeepContext>(options =>
            //                       options.UseInMemoryDatabase("InMemoryDB"));
            //}
            //else
            //{
            //    services.AddDbContext<KeepContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("KeepContext"), dbOptions => dbOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)));
            //}


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env /*KeepContext context*/)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // swagger is used for testing apis intuitively
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();

            // drop and recreate db each time docker is executed
            //if (env.IsDevelopment())
            //{
            //    context.Database.Migrate();
            //}
                
        }
    }
}
