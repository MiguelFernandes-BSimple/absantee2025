using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class AssociationTrainingModuleCollaboratorMapper : IMapper<AssociationTrainingModuleCollaborator, AssociationTrainingModuleCollaboratorDataModel>
{
    private IAssociationTrainingModuleCollaboratorFactory _factory;

    public AssociationTrainingModuleCollaboratorMapper(IAssociationTrainingModuleCollaboratorFactory factory)
    {
        _factory = factory;
    }

    public AssociationTrainingModuleCollaboratorDataModel ToDataModel(AssociationTrainingModuleCollaborator domainEntity)
    {
        return new AssociationTrainingModuleCollaboratorDataModel(domainEntity);
    }

    public IEnumerable<AssociationTrainingModuleCollaboratorDataModel> ToDataModel(IEnumerable<AssociationTrainingModuleCollaborator> dataModels)
    {
        return dataModels.Select(ToDataModel);
    }

    public AssociationTrainingModuleCollaborator ToDomain(AssociationTrainingModuleCollaboratorDataModel dataModel)
    {
        var atcDomain = _factory.Create(dataModel);

        return atcDomain;
    }

    public IEnumerable<AssociationTrainingModuleCollaborator> ToDomain(IEnumerable<AssociationTrainingModuleCollaboratorDataModel> dataModels)
    {
        return dataModels.Select(ToDomain);
    }
}