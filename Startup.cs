using System;
using JournalistTierAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using JournalistTierAPI.Coordinators;

namespace JournalistTierAPI
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
            services
              .AddDbContext<DataContext>(opt =>
                  opt
                      .UseSqlServer(Configuration
                          .GetConnectionString("JournalistTierConnection")));
            services.AddControllers();
            services.AddScoped<ITopicRepo, TopicRepo>();
            services.AddScoped<IJournalistRepo, JournalistRepo>();
            services.AddScoped<IMediaRepo, MediaRepo>();
            services.AddScoped<IRatingCalculatorCoordinator, RatingCalculatorCoordinator>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen();
            services.AddCors(options =>
          {
              options.AddDefaultPolicy(policy => policy.AllowAnyHeader()
                  .AllowAnyMethod().WithOrigins("https://localhost:4200"));
          });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Journalist Tier API");
        c.RoutePrefix = string.Empty;
    });
            //app.UseHttpsRedirection();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
