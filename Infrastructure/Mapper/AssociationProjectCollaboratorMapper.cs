using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;
public class AssociationProjectCollaboratorMapper : IMapper<AssociationProjectCollaborator, AssociationProjectCollaboratorDataModel>
{
    private IAssociationProjectCollaboratorFactory _associationProjectCollaboratorFactory;

    public AssociationProjectCollaboratorMapper(IAssociationProjectCollaboratorFactory associationProjectCollaboratorFactory)
    {
        _associationProjectCollaboratorFactory = associationProjectCollaboratorFactory;
    }

    public AssociationProjectCollaborator ToDomain(AssociationProjectCollaboratorDataModel apcModel)
    {
        var apcDomain = _associationProjectCollaboratorFactory.Create(apcModel);
        return apcDomain;
    }

    public IEnumerable<AssociationProjectCollaborator> ToDomain(IEnumerable<AssociationProjectCollaboratorDataModel> apcModels)
    {
        return apcModels.Select(ToDomain);
    }

    public AssociationProjectCollaboratorDataModel ToDataModel(AssociationProjectCollaborator apc)
    {
        return new AssociationProjectCollaboratorDataModel(apc);
    }

    public IEnumerable<AssociationProjectCollaboratorDataModel> ToDataModel(IEnumerable<AssociationProjectCollaborator> apcs)
    {
        return apcs.Select(ToDataModel);
    }

}