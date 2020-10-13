using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Demo.Contracts;
using Demo.Repos;
using Demo.Services;
using DemoService.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddGrpc();
            services.AddDbContext<DemoContext>(options =>{
                options.UseNpgsql(Configuration.GetConnectionString("Database"));
                options.UseSnakeCaseNamingConvention();
            });
            // Swagger
            services.AddSwaggerGen();

            // Repo
            services.AddSingleton<IPersonRepo, PersonRepo>();
            
            // DB Access Repo
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();

            services.AddScoped<IDepartementRepository, DepartementRepository>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
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
                endpoints.MapGrpcService<DepartementService>();
                endpoints.MapGrpcService<EmployeeService>();
                endpoints.MapControllers();
            });

            //Swagger Middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo Web API");
            });
        }
    }
}
