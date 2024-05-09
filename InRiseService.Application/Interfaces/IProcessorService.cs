using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.ProcessorDto;
using InRiseService.Domain.Processors;

namespace InRiseService.Application.Interfaces
{
    public interface IProcessorService
    {
        Task<Processor> InsertAsync(Processor processor);
        Task UpdateAsync(Processor processor);
        Task DeleteAsync(Processor processor);
        Task<Pagination<Processor>> GetByFilterAsync(ProcessorDtoFilterRequest filter);
    }
}