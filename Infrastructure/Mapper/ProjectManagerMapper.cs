namespace Infrastructure.Mapper;

using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

public class ProjectManagerMapper
{
    public ProjectManagerMapper() { }

    public ProjectManager ToDomain(ProjectManagerDataModel projectManagerDM)
    {
        IUser user =
            new User(
                projectManagerDM.User.Names,
                projectManagerDM.User.Surnames,
                projectManagerDM.User.Email,
                projectManagerDM.User.PeriodDateTime._finalDate);

        IPeriodDateTime periodDateTime =
            new PeriodDateTime(
                projectManagerDM.PeriodDateTime._initDate,
                projectManagerDM.PeriodDateTime._finalDate);

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