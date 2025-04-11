using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Factory;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class CollaboratorMapper : IMapper<Collaborator, CollaboratorDataModel>
{
    private ICollaboratorFactory _checkCollaboratorFactory;

    public CollaboratorMapper(ICollaboratorFactory checkCollaboratorFactory)
    {
        _checkCollaboratorFactory = checkCollaboratorFactory;
    }

    public Collaborator ToDomain(CollaboratorDataModel collaboratorDataModel)
    {
        var CollaboratorDomain = _checkCollaboratorFactory.Create(collaboratorDataModel);
        return CollaboratorDomain;
    }
    public CollaboratorDataModel ToDataModel(Collaborator collaborator)
    {
        return new CollaboratorDataModel(collaborator);
    }
    public IEnumerable<Collaborator> ToDomain(IEnumerable<CollaboratorDataModel> collaboratorDataModels)
    {
        return collaboratorDataModels.Select(ToDomain);
    }
    public IEnumerable<CollaboratorDataModel> ToDataModel(IEnumerable<Collaborator> collaborators)
    {
        return collaborators.Select(ToDataModel);
    }
}
