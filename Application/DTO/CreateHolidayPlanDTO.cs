
namespace Application.DTO;

public record CreateHolidayPlanDTO
{
    public Guid CollaboratorId { get; set; }
    public List<CreateHolidayPeriodDTO> HolidayPeriods { get; set; }

    public CreateHolidayPlanDTO()
    {
    }
}
