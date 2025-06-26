using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory.TrainingPeriodFactory;

public interface ITrainingPeriodFactory
{
    public TrainingPeriod Create(DateOnly initDate, DateOnly finalDate);
    public TrainingPeriod Create(ITrainingPeriodVisitor trainingPeriodVisitor);
}