using Domain.Models;

namespace Application.DTO.TrainingModule;

public record TrainingModuleDTO
{
    public Guid Id { get; set; }
    public Guid TrainingSubjectId { get; set; }
    public List<PeriodDateTime> Periods { get; set; }

    public TrainingModuleDTO()
    {

    }
}