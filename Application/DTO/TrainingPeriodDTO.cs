using Domain.Models;

namespace Application.DTO;

public record TrainingPeriodDTO
{
    public PeriodDate PeriodDate { get; set; }

    public TrainingPeriodDTO()
    {
    }

}
