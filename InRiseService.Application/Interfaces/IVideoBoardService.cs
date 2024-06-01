using InRiseService.Application.DTOs.PaginationDto;
using InRiseService.Application.DTOs.VideoBoardDto;
using InRiseService.Domain.VideoBoards;

namespace InRiseService.Application.Interfaces
{
    public interface IVideoBoardService
    {
        Task<VideoBoard> InsertAsync(VideoBoard videoBoard);
        Task UpdateAsync(VideoBoard videoBoard);
        Task DeleteAsync(VideoBoard videoBoard);
        Task<VideoBoard?> GetByIdAsync(int id);
        Task<Pagination<VideoBoard>> GetByFilterAsync(VideoBoardFilterDto filter);
    }
}