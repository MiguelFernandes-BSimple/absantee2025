using Domain.Models;

namespace Application.DTO;

public record CreateTrainingPeriodDTO
{
    public DateOnly InitDate { get; set; }
    public DateOnly FinalDate { get; set; }

    public CreateTrainingPeriodDTO()
    {
    }

}
