using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPC.Application.Common.Models
{
    public class OperationListResponse<T> : OperationResponse<IReadOnlyCollection<T>>
    {
        public MetaListResult Meta { get; set; }

        public OperationListResponse(IReadOnlyCollection<T> payload, int totalCount, int page, int size)
        {
            Payload = payload;
            Meta = new MetaListResult
            {
                Page = page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)size),
                TotalCount = totalCount
            };
        }

        public static async Task<OperationListResponse<T>> CreateAsync(IQueryable<T> query, int page, int size)
        {
            var count = await query.CountAsync();
            var payload = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new OperationListResponse<T>(payload, count, page, size);
        }
    }
}
