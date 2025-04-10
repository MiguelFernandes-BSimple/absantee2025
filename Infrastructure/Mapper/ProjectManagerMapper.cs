namespace Infrastructure.Mapper;

using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

public class ProjectManagerMapper
{
    private PeriodDateTimeMapper _periodDateTimeMapper;

    public ProjectManagerMapper(PeriodDateTimeMapper periodDateTimeMapper)
    {
        _periodDateTimeMapper = periodDateTimeMapper;
    }

    public ProjectManager ToDomain(ProjectManagerDataModel projectManagerDM)
    {
        IPeriodDateTime periodDateTime = _periodDateTimeMapper.ToDomain(projectManagerDM.PeriodDateTime);

        ProjectManager projectManager = new ProjectManager(projectManagerDM.UserId, periodDateTime);

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