using InRiseService.Application.DTOs.ApiSettingDto;
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
            services.AddHttpClient();
            services.AddScoped<IUserProfileService,UserProfileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserAutenticationService, UserAutenticationService>();
            services.AddScoped<IUserAddressService, UserAddressService>();
            services.AddScoped<IZipCodeBaseService,ZipCodeBaseService>();
            services.AddScoped<IAddressService,AddressService>();
            services.AddScoped<ITypeCodeValidationService,TypeCodeValidationService>();
            services.AddScoped<IValidationCodeService,ValidationCodeService>();
            services.AddScoped<ISendGridService,SendGridService>();
            services.AddScoped<ICategoryService,CategoryService>();
        }

        public static void RegisterConfigurationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SendGridSetting>(configuration.GetSection("SendGridSetting"));
            services.Configure<AppSetting>(configuration.GetSection("AppSettings"));
            services.Configure<ZipCodeBaseSettings>(configuration.GetSection("ZipCodeBaseSettings"));

        }
    }
}