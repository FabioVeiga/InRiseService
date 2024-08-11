using Azure.Storage.Blobs;
using InRiseService.Application.DTOs.ApiSettingDto;
using InRiseService.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace InRiseService.Application.Services
{
    public class BlobFileAzureService : IBlobFileAzureService
    {
        private readonly AzureBlobStorageSetting _setting;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;
        
        public BlobFileAzureService(IOptions<AzureBlobStorageSetting> options)
        {
            _setting = options.Value;
            _setting.ConnectionString = Environment.GetEnvironmentVariable("BlobStorageConection");
            _blobServiceClient = new (_setting.ConnectionString);
            _containerClient = _blobServiceClient.GetBlobContainerClient(_setting.ContainerName);
        }

        public async Task<bool> DeleteFileAsync(string pathimage)
        {
            try
            {
                var blobClient = _containerClient.GetBlobClient(pathimage);
                var response = await blobClient.DeleteIfExistsAsync();
                return response.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Uri?> UploadFileAsync(Stream fileStream, string pathkey, string fileName)
        {
            try
            {
                var blobClient = _containerClient.GetBlobClient($"{pathkey}/{fileName}");
                var result = await blobClient.UploadAsync(fileStream, overwrite: true);
                if(result is not null)
                {
                    return blobClient.Uri;
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}