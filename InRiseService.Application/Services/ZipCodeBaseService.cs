using System.Text.Json;
using InRiseService.Application.DTOs.ApiSettingDto;
using InRiseService.Application.DTOs.ZipCodeBaseDto;
using InRiseService.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace InRiseService.Application.Services
{
    public class ZipCodeBaseService : IZipCodeBaseService
    {
        private readonly ILogger<ZipCodeBaseService> _logger;
        private readonly HttpClient _httpClient;
        private readonly ZipCodeBaseSettingsDto _zipCodeBaseSettingsDto;

        public ZipCodeBaseService(ILogger<ZipCodeBaseService> logger, HttpClient httpClient, IOptions<ZipCodeBaseSettingsDto> options)
        {
            _logger = logger;
            _httpClient = httpClient;
            _zipCodeBaseSettingsDto = options.Value;
        }
        public async Task<ZipCodeBaseDtoResponse?> GetAddressByZipCode(string zipcode)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_zipCodeBaseSettingsDto.CountryDefault}search?apikey={_zipCodeBaseSettingsDto.ApiKey}&codes={zipcode}&country={_zipCodeBaseSettingsDto.CountryDefault}");
                if (response.IsSuccessStatusCode){
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var queryResult = JsonSerializer.Deserialize<dynamic>(jsonString);
                    if (queryResult is not null)
                    {
                        JsonDocument jsonDocument = JsonDocument.Parse(jsonString);
                        JsonElement root = jsonDocument.RootElement;
                        JsonElement results = root.GetProperty("results");
                        JsonElement codeArray = results.GetProperty(zipcode)[0];

                        var returnZipCodebaseDto = new ZipCodeBaseDtoResponse()
                        {
                            PostalCode = codeArray.GetProperty("postal_code").ToString(),
                            City = codeArray.GetProperty("city").ToString(),
                            State = codeArray.GetProperty("state").ToString(),
                            CityEn = codeArray.GetProperty("city_en").ToString(),
                            CountryCode = codeArray.GetProperty("country_code").ToString(),
                            ProviceCode = codeArray.GetProperty("province_code").ToString(),
                            Province = codeArray.GetProperty("province").ToString(),
                            StateCode = codeArray.GetProperty("state_code").ToString(),
                            StateEn = codeArray.GetProperty("state").ToString()
                        };
                        return returnZipCodebaseDto;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(ZipCodeBaseService)}::{nameof(GetAddressByZipCode)}] - Exception: {ex}");
                throw;
            }
        }
    }
}