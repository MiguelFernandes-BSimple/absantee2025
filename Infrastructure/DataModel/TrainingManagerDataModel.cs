using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.DataModel;

[Table("TrainingManager")]
public class TrainingManagerDataModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }

    public TrainingManagerDataModel(TrainingManager trainingManager)
    {
        Id = trainingManager.GetId();
        UserId = trainingManager.GetUserId();
        PeriodDateTime = (PeriodDateTime)trainingManager.GetPeriodDateTime();
    }
}