using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory.TrainingPeriodFactory;

public class TrainingPeriodFactory : ITrainingPeriodFactory
{
    public TrainingPeriodFactory()
    {
    }

    public TrainingPeriod Create(DateOnly initDate, DateOnly finalDate)
    {
        PeriodDate periodDate = new PeriodDate(initDate, finalDate);
        return new TrainingPeriod(periodDate);
    }

    public TrainingPeriod Create(ITrainingPeriodVisitor trainingPeriodVisitor)
    {
        return new TrainingPeriod(trainingPeriodVisitor.Id, trainingPeriodVisitor.PeriodDate);
    }
}