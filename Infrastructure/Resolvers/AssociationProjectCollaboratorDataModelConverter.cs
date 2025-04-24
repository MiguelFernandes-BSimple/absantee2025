using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class AssociationProjectCollaboratorDataModelConverter : ITypeConverter<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>
{
    private readonly IAssociationProjectCollaboratorFactory _factory;

    public AssociationProjectCollaboratorDataModelConverter(IAssociationProjectCollaboratorFactory factory)
    {
        _factory = factory;
    }

    public AssociationProjectCollaborator Convert(AssociationProjectCollaboratorDataModel source, AssociationProjectCollaborator destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}
