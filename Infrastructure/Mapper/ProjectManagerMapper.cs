namespace Infrastructure.Mapper;

using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

public class ProjectManagerMapper
{
    private UserMapper _userMapper;
    private PeriodDateTimeMapper _periodDateTimeMapper;

    public ProjectManagerMapper(UserMapper userMapper, PeriodDateTimeMapper periodDateTimeMapper)
    {
        _userMapper = userMapper;
        _periodDateTimeMapper = periodDateTimeMapper;
    }

    public ProjectManager ToDomain(ProjectManagerDataModel projectManagerDM)
    {
        IUser user = _userMapper.ToDomain(projectManagerDM.User);
        IPeriodDateTime periodDateTime = _periodDateTimeMapper.ToDomain(projectManagerDM.PeriodDateTime);

        ProjectManager projectManager = new ProjectManager(user, periodDateTime);

        projectManager.SetId(projectManagerDM.Id);

        return projectManager;
    }

    public IEnumerable<ProjectManager> ToDomain(IEnumerable<ProjectManagerDataModel> projectManagersDM)
    {
        return projectManagersDM.Select(ToDomain);
    }

    public ProjectManagerDataModel ToDataModel(ProjectManager projectManager)
    {
        return new ProjectManagerDataModel(projectManager);
    }

    public IEnumerable<ProjectManagerDataModel> ToDataModel(IEnumerable<ProjectManager> projectManagers)
    {
        return projectManagers.Select(ToDataModel);
    }
}