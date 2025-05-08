using Application.DTO.AssociationTrainingModuleCollaborator;

namespace WebApi.IntegrationTests.Helpers;

public static class AssociationTrainingModuleCollaboratorHelper
{
    public static CreateAssociationTrainingModuleCollaboratorDTO GenerateCreateAssociationTrainingModuleCollaboratorDTO(Guid collabId)
    {
        return new CreateAssociationTrainingModuleCollaboratorDTO
        {
            CollaboratorId = collabId
        };
    }
}