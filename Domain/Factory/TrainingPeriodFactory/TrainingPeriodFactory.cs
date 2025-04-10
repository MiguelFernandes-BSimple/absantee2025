using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory.TrainingPeriodFactory;

public class TrainingPeriodFactory : ITrainingPeriodFactory
{

    public TrainingPeriodFactory()
    {

    }

    public TrainingPeriod Create(IPeriodDate periodDate)
    {

        if (periodDate.IsInitDateSmallerThan(DateOnly.FromDateTime(DateTime.Now)))
            throw new ArgumentException("Period date cannot start in the past.");

        return new TrainingPeriod(periodDate);
    }

    public TrainingPeriod Create(ITrainingPeriodVisitor trainingPeriodVisitor)
    {
        return new TrainingPeriod(trainingPeriodVisitor.Id, trainingPeriodVisitor.PeriodDate);
    }
}