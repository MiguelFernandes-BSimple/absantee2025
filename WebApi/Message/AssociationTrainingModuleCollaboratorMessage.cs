using Domain.Models;
namespace WebApi;
//dto
public record AssociationTrainingModuleCollaboratorMessage(Guid Id, Guid TrainingModuleId, Guid CollaboratorId);