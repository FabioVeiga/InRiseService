namespace InRiseService.Application.Interfaces
{
    public interface IBlobFileAzureService
    {
        Task<Uri?> UploadFileAsync(Stream fileStream, string pathkey, string fileName);
        Task<bool> DeleteFileAsync(string pathimage);
    }
}