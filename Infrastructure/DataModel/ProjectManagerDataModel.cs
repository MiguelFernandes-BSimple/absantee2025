using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;

namespace Infrastructure.DataModel;

[Table("ProjectManager")]
public class ProjectManagerDataModel
{
    public long Id { get; set; }
    public UserDataModel User { get; set; }
    public PeriodDateTimeDataModel PeriodDateTime { get; set; }

    public ProjectManagerDataModel() { }

    public ProjectManagerDataModel(ProjectManager projectManager)
    {
        Id = projectManager.GetId();
        User = new UserDataModel(projectManager.GetUser());
        PeriodDateTime = new PeriodDateTimeDataModel(projectManager.GetPeriodDateTime());
    }
}