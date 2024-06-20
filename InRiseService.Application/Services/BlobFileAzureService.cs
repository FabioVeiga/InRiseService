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
            _blobServiceClient = new (_setting.ConnectionString);
            _containerClient = _blobServiceClient.GetBlobContainerClient(_setting.ContainerName);
        }

        public async Task UploadFileAsync(Stream fileStream, string fileName, string pathkey)
        {
            try
            {
                var blobClient = _containerClient.GetBlobClient($"{pathkey}/{fileName}");
                await blobClient.UploadAsync(fileStream, overwrite: true);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}