using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;
public class AssociationTrainingModuleCollaboratorMapper : IMapper<AssociationTrainingModuleCollaborator, AssociationTrainingModuleCollaboratorDataModel>
{
    private IAssociationTrainingModuleCollaboratorFactory _associationTrainingModuleCollaboratorFactory;

    public AssociationTrainingModuleCollaboratorMapper(IAssociationTrainingModuleCollaboratorFactory associationTrainingModuleCollaboratorFactory)
    {
        _associationTrainingModuleCollaboratorFactory = associationTrainingModuleCollaboratorFactory;
    }

    public AssociationTrainingModuleCollaborator ToDomain(AssociationTrainingModuleCollaboratorDataModel atmcModel)
    {
        var atmcDomain = _associationTrainingModuleCollaboratorFactory.Create(atmcModel);
        return atmcDomain;
    }

    public IEnumerable<AssociationTrainingModuleCollaborator> ToDomain(IEnumerable<AssociationTrainingModuleCollaboratorDataModel> atmcModels)
    {
        return atmcModels.Select(ToDomain);
    }

    public AssociationTrainingModuleCollaboratorDataModel ToDataModel(AssociationTrainingModuleCollaborator atmc)
    {
        return new AssociationTrainingModuleCollaboratorDataModel(atmc);
    }

    public IEnumerable<AssociationTrainingModuleCollaboratorDataModel> ToDataModel(IEnumerable<AssociationTrainingModuleCollaborator> atmcs)
    {
        return atmcs.Select(ToDataModel);
    }

}
