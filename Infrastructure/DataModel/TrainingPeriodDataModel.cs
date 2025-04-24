using Domain.Models;
using Domain.Visitor;
namespace Infrastructure.DataModel;

public class TrainingPeriodDataModel : ITrainingPeriodVisitor
{
    public Guid Id { get; set; }
    public PeriodDate PeriodDate { get; set; }

    public TrainingPeriodDataModel()
    {
    }

    public TrainingPeriodDataModel(TrainingPeriod trainingPeriod)
    {
        Id = trainingPeriod.Id;
        PeriodDate = trainingPeriod.PeriodDate;
    }
}
