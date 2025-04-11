namespace Infrastructure.Mapper;

using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

public class ProjectManagerMapper : IMapper<ProjectManager, ProjectManagerDataModel>
{
    public ProjectManager ToDomain(ProjectManagerDataModel projectManagerDM)
    {
        ProjectManager projectManager = new ProjectManager(projectManagerDM.UserId, projectManagerDM.PeriodDateTime);

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