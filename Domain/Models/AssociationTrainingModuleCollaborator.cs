using Domain.Interfaces;

namespace Domain.Models;

public class AssociationTrainingModuleCollaborator : IAssociationTrainingModuleCollaborator
{
    public long _id { get; set; }
    public long _collaboratorId { get; set; }
    public long _trainingModuleId { get; set; }

    public AssociationTrainingModuleCollaborator(long collab, long module) {
        _collaboratorId = collab;
        _trainingModuleId = module;
    }

    public AssociationTrainingModuleCollaborator(long id, long collab, long module) {
        _id = id;
        _collaboratorId = collab;
        _trainingModuleId = module;
    }

    public long GetId()
    {
        return _id;
    }

    public long GetCollaboratorId()
    {
        return _collaboratorId;
    }

    public long GetTrainingModuleId()
    {
        return _trainingModuleId;
    }
}
