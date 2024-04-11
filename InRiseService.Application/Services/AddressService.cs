using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.Addressed;
using InRiseService.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<AddressService> _logger;

        public AddressService(ApplicationContext context,  ILogger<AddressService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Address?> GetByPostalCode(string postalCode)
        {
            try
            {
                return await _context.Addresses.FirstOrDefaultAsync(x => x.PostalCode == postalCode);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(AddressService)}::{nameof(GetByPostalCode)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<Address> InsertAsync(Address address)
        {
            try
            {
                address.PostalCode = StringHelper.NormalizePostalCode(address.PostalCode);
                address.InsertIn = DateTime.Now;
                address.Active = true;
                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();
                return address;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(AddressService)}::{nameof(GetByPostalCode)}] - Exception: {ex}");
                throw;
            }
        }
    }
}