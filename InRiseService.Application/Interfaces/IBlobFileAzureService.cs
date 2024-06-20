namespace InRiseService.Application.Interfaces
{
    public interface IBlobFileAzureService
    {
        Task UploadFileAsync(Stream fileStream, string fileName, string pathkey);
    }
}