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
    private UserMapper _userMapper;
    private PeriodDateTimeMapper _periodDateTimeMapper;
    public CollaboratorMapper(UserMapper userMapper, PeriodDateTimeMapper periodDateTimeMapper)
    {
        _userMapper = userMapper;
        _periodDateTimeMapper = periodDateTimeMapper;
    }
    public Collaborator ToDomain(CollaboratorDataModel collaboratorDataModel)
    {
        IUser user = _userMapper.ToDomain(collaboratorDataModel.User);
        IPeriodDateTime periodDateTime = _periodDateTimeMapper.ToDomain(collaboratorDataModel.PeriodDateTime);
        var CollaboratorDomain = new Collaborator(user, periodDateTime);
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
