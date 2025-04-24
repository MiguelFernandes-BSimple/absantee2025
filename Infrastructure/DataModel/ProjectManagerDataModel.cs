using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.DataModel;

[Table("ProjectManager")]
public class ProjectManagerDataModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }

    public ProjectManagerDataModel(ProjectManager projectManager)
    {
        Id = projectManager.Id;
        UserId = projectManager.UserId;
        PeriodDateTime = projectManager.PeriodDateTime;
    }
}