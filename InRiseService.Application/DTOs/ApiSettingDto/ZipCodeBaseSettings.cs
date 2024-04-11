
namespace InRiseService.Application.DTOs.ApiSettingDto
{
    public class ZipCodeBaseSettings
    {
        public string ApiKey { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string CountryDefault { get; set; } = null!;
    }
}