using HPC.Application.Common.Interfaces;
using System;

namespace HPC.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
