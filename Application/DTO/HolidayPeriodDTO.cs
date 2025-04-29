using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO
{
    public record HolidayPeriodDTO
    {
        public Guid Id { get; }
        public PeriodDate PeriodDate { get; }
    }
}
