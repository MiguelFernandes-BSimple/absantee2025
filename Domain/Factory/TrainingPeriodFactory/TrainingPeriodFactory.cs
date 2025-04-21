using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory.TrainingPeriodFactory;

public class TrainingPeriodFactory : ITrainingPeriodFactory
{
    public TrainingPeriodFactory()
    {
    }

    public TrainingPeriod Create(PeriodDate periodDate)
    {
        return new TrainingPeriod(periodDate);
    }

    public TrainingPeriod Create(ITrainingPeriodVisitor trainingPeriodVisitor)
    {
        return new TrainingPeriod(trainingPeriodVisitor.Id, trainingPeriodVisitor.PeriodDate);
    }
}