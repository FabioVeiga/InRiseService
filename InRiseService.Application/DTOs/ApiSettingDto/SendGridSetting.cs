
namespace InRiseService.Application.DTOs.ApiSettingDto
{
    public class SendGridSetting
    {
        public string ApiKey { get; set; } = null!;
        public string FromEmail { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}