using Domain.Models;

namespace Application.DTO.AssociationTrainingModuleCollaborator;

public record AssociationTrainingModuleCollaboratorDTO
{
    public Guid Id { get; set; }
    public Guid TrainingModuleId { get; set; }
    public string? TrainingSubject { get; set; }
    public List<PeriodDateTime>? Periods { get; set; }
    public Guid CollaboratorId { get; set; }
    public string? CollaboratorEmail { get; set; }
    public AssociationTrainingModuleCollaboratorDTO()
    {

    }
}