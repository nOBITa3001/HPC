using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPC.Application.Common.Models
{
    public class OperationResponse<T>
    {
        public T Payload { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public static async Task<OperationResponse<T>> CreateAsync(IQueryable<T> query) =>
            new OperationResponse<T> { Payload = await query.FirstOrDefaultAsync() };
    }
}
