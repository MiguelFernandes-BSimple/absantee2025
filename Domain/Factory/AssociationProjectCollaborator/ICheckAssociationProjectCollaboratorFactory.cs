using Domain.Interfaces;
using Domain.Models;

namespace Domain.Factory;

public interface ICheckAssociationProjectCollaboratorFactory
{
    public AssociationProjectCollaborator Create(IPeriodDate periodDate, long collaboratorId, long projectId);
}