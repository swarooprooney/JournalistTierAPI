using JournalistTierAPI.Coordinators;
using JournalistTierAPI.Data;
using JournalistTierAPI.Helpers;
using JournalistTierAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JournalistTierAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services
                          .AddDbContext<DataContext>(opt =>
                              opt
                                  .UseSqlServer(configuration
                                      .GetConnectionString("JournalistTierConnection")));
            services.AddControllers();
            services.AddScoped<ITopicRepo, TopicRepo>();
            services.AddScoped<IJournalistRepo, JournalistRepo>();
            services.AddScoped<IMediaRepo, MediaRepo>();
            services.AddScoped<IRatingCalculatorCoordinator, RatingCalculatorCoordinator>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPhotoService, PhotoService>();
            return services;
        }
    }
}