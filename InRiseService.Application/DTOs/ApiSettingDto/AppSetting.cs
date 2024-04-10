namespace InRiseService.Application.DTOs.ApiSettingDto
{
    public class AppSetting
    {
        public string Secret { get; set; } = null!;
        public double AcessTokenTime { get; set; }
    }
}