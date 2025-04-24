using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class AssociationTrainingModuleCollaboratorDataModelConverter : ITypeConverter<AssociationTrainingModuleCollaboratorDataModel, AssociationTrainingModuleCollaborator>
{
    private readonly IAssociationTrainingModuleCollaboratorFactory _factory;

    public AssociationTrainingModuleCollaboratorDataModelConverter(IAssociationTrainingModuleCollaboratorFactory factory)
    {
        _factory = factory;
    }

    public AssociationTrainingModuleCollaborator Convert(AssociationTrainingModuleCollaboratorDataModel source, AssociationTrainingModuleCollaborator destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}
