
namespace Application.DTO;

public record CreateHolidayPeriodDTO
{
    public Guid HolidayPlanId { get; set; }
    public DateOnly InitDate { get; set; }
    public DateOnly FinalDate { get; set; }

    public CreateHolidayPeriodDTO()
    {
    }
}
