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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RequestHelperSample.Data.Context;
using RequestHelperSample.Data.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace RequestHelperSample.API
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
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IGradeRepository, GradeRepository>();

            services.AddSwaggerGen(opt =>
            {
                opt.DescribeAllEnumsAsStrings();
                opt.SwaggerDoc("v1", new Info
                {
                    Title = "Sample",
                    Version = "v1",
                    Description = "The User Microservice HTTP API. This is a Data-Driven/CRUD microservice",
                    TermsOfService = "Terms Of Service"
                });
            });

            var connection = @"Server=localhost\SQLEXPRESS;Database=RequestHelperSample;Trusted_Connection=True;";
            services.AddDbContext<DatabaseContext>
                (options => options.UseSqlServer(connection));

            var dbContext = services.BuildServiceProvider()
                       .GetService<DatabaseContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            app.UseSwagger()
                  .UseSwaggerUI(c =>
                  {
                      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample.API V1");
                  });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
