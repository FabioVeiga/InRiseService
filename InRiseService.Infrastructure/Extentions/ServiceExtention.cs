using InRiseService.Application.DTOs.AppSettings;
using InRiseService.Application.Interfaces;
using InRiseService.Application.Services;
using InRiseService.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InRiseService.Infrastructure.Extentions
{
    public static class ServiceExtention
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mappings));
            services.AddScoped<IUserProfileService,UserProfileService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}