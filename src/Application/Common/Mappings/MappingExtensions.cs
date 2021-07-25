using HPC.Application.Common.Models;
using System.Linq;
using System.Threading.Tasks;

namespace HPC.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<OperationListResponse<TDestination>> CreateOperationListResponseAsync<TDestination>(this IQueryable<TDestination> query, int page, int size)
            => OperationListResponse<TDestination>.CreateAsync(query, page, size);

        public static Task<OperationResponse<TDestination>> CreateOperationResponseAsync<TDestination>(this IQueryable<TDestination> query)
            => OperationResponse<TDestination>.CreateAsync(query);
    }
}
