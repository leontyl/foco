using App.Contracts;
using App.Data;
using App.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace App
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
            services.AddAutoMapper(GetType().Assembly);

            // Registering DbContext
            services.AddDbContext<FoCoDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default"), b =>
                {
                    b.MigrationsAssembly("App");
                }));

            services.AddLogging();

            services.AddControllers();

            // Api versioning.
            // if you test API via swagger please add "1" into "version" field.
            services.AddApiVersioning(versioningOptions =>
            {
                versioningOptions.AssumeDefaultVersionWhenUnspecified = true;
                versioningOptions.DefaultApiVersion = new ApiVersion(1, 0);
                versioningOptions.ReportApiVersions = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "App", Version = "v1" });
            });

            services.AddScoped<IRepository<Site>, SiteRepository>();
            services.AddScoped<IRepository<Queue>, QueueRepository>();
            services.AddScoped<ICheckInService, ICheckInService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
