using Domain.Models;

namespace Application.DTO;

public record HolidayPlanDTO
{
    public Guid Id { get; set; }
    public Guid CollaboratorId { get; set; }
    public List<HolidayPeriodDTO> HolidayPeriods { get; set; }

    public HolidayPlanDTO()
    {
    }
}
