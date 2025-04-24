using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class CollaboratorDataModelConverter : ITypeConverter<CollaboratorDataModel, Collaborator>
{
    private readonly ICollaboratorFactory _factory;

    public CollaboratorDataModelConverter(ICollaboratorFactory factory)
    {
        _factory = factory;
    }

    public Collaborator Convert(CollaboratorDataModel source, Collaborator destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}