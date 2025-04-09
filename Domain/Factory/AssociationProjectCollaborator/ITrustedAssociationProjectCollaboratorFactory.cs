using Domain.Interfaces;
using Domain.Models;

public interface ITrustedAssociationProjectCollaboratorFactory
{
    AssociationProjectCollaborator Create(long Id, long collaboratorId, long projectId, IPeriodDate periodDate);

}