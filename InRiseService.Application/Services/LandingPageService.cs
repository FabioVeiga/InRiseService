using InRiseService.Application.Interfaces;
using InRiseService.Data.Context;
using InRiseService.Domain.LandingPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InRiseService.Application.Services
{
    public class LandingPageService : ILandingPageService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<LandingPageService> _logger;

        public LandingPageService(ApplicationContext context,  ILogger<LandingPageService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IList<LandingPage>> GetAll()
        {
            try
            {
                return await _context.LandingPages.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(LandingPageService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task<LandingPage> InsertAsync(LandingPage model)
        {
            try
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(LandingPageService)}::{nameof(InsertAsync)}] - Exception: {ex}");
                throw;
            }
        }

        public async Task UpdateAsync(LandingPage model)
        {
            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(LandingPageService)}::{nameof(UpdateAsync)}] - Exception: {ex}");
                throw;
            }
        }
    }
}