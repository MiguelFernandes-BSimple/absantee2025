namespace Application.DTO.AssociationTrainingModuleCollaborator;

public record AssociationTrainingModuleCollaboratorDTO
{
    public Guid Id { get; set; }
    public Guid TrainingModuleId { get; set; }
    public Guid CollaboratorId { get; set; }

    public AssociationTrainingModuleCollaboratorDTO()
    {

    }
}