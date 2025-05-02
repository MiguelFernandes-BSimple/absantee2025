using Domain.Models;

namespace Application.DTO;

public record HolidayPeriodDTO
{
    public PeriodDate PeriodDate { get; set; }

    public HolidayPeriodDTO()
    {
    }
}
