
namespace InRiseService.Application.DTOs.ApiSettingDto
{
    public class SendGridSetting
    {
        public string ApiKey { get; set; } = null!;
        public string FromAddress { get; set; } = null!;
    }
}