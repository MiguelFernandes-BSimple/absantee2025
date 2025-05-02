
namespace Application.DTO;

public record CreateHolidayPeriodDTO
{
    public DateOnly InitDate { get; set; }
    public DateOnly FinalDate { get; set; }

}
