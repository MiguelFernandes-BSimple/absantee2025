using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class AssociationTrainingModuleCollaboratorMapper : IMapper<AssociationTrainingModuleCollaborator, AssociationTrainingModuleCollaboratorDataModel> {
    public AssociationTrainingModuleCollaboratorDataModel ToDataModel(AssociationTrainingModuleCollaborator amc)
    {
        return new AssociationTrainingModuleCollaboratorDataModel(amc);
    }

    public IEnumerable<AssociationTrainingModuleCollaboratorDataModel> ToDataModel(IEnumerable<AssociationTrainingModuleCollaborator> amc)
    {
        return amc.Select(ToDataModel);
    }

    public AssociationTrainingModuleCollaborator ToDomain(AssociationTrainingModuleCollaboratorDataModel amcdm)
    {
        return new AssociationTrainingModuleCollaborator(amcdm.Id, amcdm.CollaboratorId, amcdm.TrainingModuleId);
    }

    public IEnumerable<AssociationTrainingModuleCollaborator> ToDomain(IEnumerable<AssociationTrainingModuleCollaboratorDataModel> amcdm)
    {
        return amcdm.Select(ToDomain);
    }
}
