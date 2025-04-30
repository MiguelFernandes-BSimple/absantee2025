
namespace Application.DTO;

public record CreateHolidayPlanDTO
{
    public Guid CollaboratorId { get; set; }
    public List<HolidayPeriodDTO> HolidayPeriods { get; set; }

    public CreateHolidayPlanDTO()
    {
    }
}
