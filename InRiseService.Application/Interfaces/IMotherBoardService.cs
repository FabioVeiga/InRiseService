using InRiseService.Domain.MotherBoards;

namespace InRiseService.Application.Interfaces
{
    public interface IMotherBoardService
    {
        Task<MotherBoard> InsertAsync(MotherBoard motherBoard);
        Task UpdateAsync(MotherBoard motherBoard);
        Task DeleteAsync(MotherBoard motherBoard);
        Task<MotherBoard?> GetByIdAsync(int id);

    }
}