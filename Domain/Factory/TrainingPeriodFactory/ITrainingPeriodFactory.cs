using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory.TrainingPeriodFactory;

public interface ITrainingPeriodFactory
{
    public TrainingPeriod Create(IPeriodDate periodDate);
    public TrainingPeriod Create(ITrainingPeriodVisitor trainingPeriodVisitor);
}