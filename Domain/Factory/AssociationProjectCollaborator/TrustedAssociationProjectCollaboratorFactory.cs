using Domain.Interfaces;
using Domain.Models;

namespace Domain.Factory;

public class TrustedAssociationProjectCollaboratorFactory : ITrustedAssociationProjectCollaboratorFactory
{
    public TrustedAssociationProjectCollaboratorFactory() { }

    public AssociationProjectCollaborator Create(long Id, long collaboratorId, long projectId, IPeriodDate periodDate)
    {
        return new AssociationProjectCollaborator(Id, collaboratorId, projectId, periodDate);
    }
}
