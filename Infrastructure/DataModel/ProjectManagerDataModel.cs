using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.DataModel;

[Table("ProjectManager")]
public class ProjectManagerDataModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }

    public ProjectManagerDataModel(ProjectManager projectManager)
    {
        Id = projectManager.GetId();
        UserId = projectManager.GetUserId();
        PeriodDateTime =(PeriodDateTime)projectManager.GetPeriodDateTime();
    }
}