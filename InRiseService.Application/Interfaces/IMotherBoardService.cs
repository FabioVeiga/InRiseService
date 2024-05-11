using InRiseService.Application.DTOs.MotherBoardDto;
using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Domain.MotherBoards;

namespace InRiseService.Application.Interfaces
{
    public interface IMotherBoardService
    {
        Task<MotherBoard> InsertAsync(MotherBoard motherBoard);
        Task UpdateAsync(MotherBoard motherBoard);
        Task DeleteAsync(MotherBoard motherBoard);
        Task<MotherBoard?> GetByIdAsync(int id);
        Task<Pagination<MotherBoard>> GetByFilterAsync(MotherBoardDtoFilterRequest request);

    }
}