namespace InRiseService.Application.DTOs.ApiSettingDto
{
    public class AzureBlobStorageSetting
    {
        public string ConnectionString { get; set; } = default!;
        public string ContainerName { get; set; } = default!;
    }
}