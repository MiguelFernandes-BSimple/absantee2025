namespace Application.DTO.AssociationTrainingModuleCollaborator;

public record AssociationTrainingModuleCollaboratorDTO
{
    public Guid Id { get; }
    public Guid TrainingModuleId { get; }
    public Guid CollaboratorId { get; }
}