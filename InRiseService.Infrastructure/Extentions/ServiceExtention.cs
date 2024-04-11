using InRiseService.Application.Interfaces;
using InRiseService.Application.Services;
using InRiseService.Infrastructure.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace InRiseService.Infrastructure.Extentions
{
    public static class ServiceExtention
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mappings));
            services.AddHttpClient();
            services.AddScoped<IUserProfileService,UserProfileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserAutenticationService, UserAutenticationService>();
            services.AddScoped<IUserAddressService, UserAddressService>();
            services.AddScoped<IZipCodeBaseService,ZipCodeBaseService>();
        }
    }
}