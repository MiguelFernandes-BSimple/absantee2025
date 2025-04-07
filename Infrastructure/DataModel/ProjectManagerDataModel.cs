using Domain.Models;

namespace Infrastructure.DataModel;

public class ProjectManagerDataModel
{
    public long Id { get; set; }
    public UserDataModel User { get; set; }
    public PeriodDateTimeDataModel PeriodDateTime { get; set; }

    public ProjectManagerDataModel() { }
}