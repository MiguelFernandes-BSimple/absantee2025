using Domain.Models;

namespace Application.DTO;

public record TrainingPeriodDTO
{
    public DateOnly InitDate { get; set; }
    public DateOnly FinalDate { get; set; }

    public TrainingPeriodDTO()
    {
    }

}
