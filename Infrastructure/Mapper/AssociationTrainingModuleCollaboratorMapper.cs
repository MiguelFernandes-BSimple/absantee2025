using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class AssociationTrainingModuleCollaboratorMapper : IMapper<AssociationTrainingModuleCollaborator, AssociationTrainingModuleCollaboratorDataModel>
{
    private readonly IAssociationTrainingModuleCollaboratorFactory _AssociationTrainingModuleCollaboratorfactory;

    public AssociationTrainingModuleCollaboratorMapper(IAssociationTrainingModuleCollaboratorFactory AssociationTrainingModuleCollaboratorfactory)
    {
        _AssociationTrainingModuleCollaboratorfactory = AssociationTrainingModuleCollaboratorfactory;
    }

    public AssociationTrainingModuleCollaboratorDataModel ToDataModel(AssociationTrainingModuleCollaborator AssociationTrainingModuleCollaborator)
    {
        return new AssociationTrainingModuleCollaboratorDataModel(AssociationTrainingModuleCollaborator);
    }

    public IEnumerable<AssociationTrainingModuleCollaboratorDataModel> ToDataModel(IEnumerable<AssociationTrainingModuleCollaborator> dataModels)
    {
        return dataModels.Select(ToDataModel);
    }

    public AssociationTrainingModuleCollaborator ToDomain(AssociationTrainingModuleCollaboratorDataModel dataModel)
    {
        var atcDomain = _AssociationTrainingModuleCollaboratorfactory.Create(dataModel);
        return atcDomain;
    }

    public IEnumerable<AssociationTrainingModuleCollaborator> ToDomain(IEnumerable<AssociationTrainingModuleCollaboratorDataModel> dataModels)
    {
        return dataModels.Select(ToDomain);
    }
}
