using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class CollaboratorMapper
{
    public Collaborator ToDomain(CollaboratorDataModel collaboratorDataModel)
    {
        var periodDateTime = new PeriodDateTime(collaboratorDataModel.PeriodDateTime._initDate, collaboratorDataModel.PeriodDateTime._finalDate);
        var CollaboratorDomain = new Collaborator(collaboratorDataModel.User, periodDateTime);
        CollaboratorDomain.SetId(collaboratorDataModel.Id);
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
