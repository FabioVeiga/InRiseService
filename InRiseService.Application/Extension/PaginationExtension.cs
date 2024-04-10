using InRiseService.Application.DTOs.PaginationDto;
using Microsoft.EntityFrameworkCore;

namespace InRiseService.Application.Extentions
{
    public static class PaginationHelper
    {
        public static async Task<Pagination<T>> PaginationAsync<T>(this IQueryable<T> queryable, int paginaIndex, int pageSize)
        {
            var totalCount = await queryable.CountAsync();
            var items = await queryable.Skip((paginaIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new Pagination<T>(paginaIndex, pageSize, totalCount, items);
        }

        public static Pagination<T> Pagination<T>(this ICollection<T> list, int pageIndex, int pageSize)
        {
            var totalCount = list.Count;
            var items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new Pagination<T>(pageIndex, pageSize, totalCount, items);
        }
    }
}