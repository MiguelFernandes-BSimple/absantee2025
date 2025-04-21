using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory.TrainingPeriodFactory;

public class TrainingModuleFactory : ITrainingModuleFactory
{
    public TrainingModuleFactory()
    {
    }

    public TrainingModule Create(long subjectId, List<PeriodDateTime> periods)
    {
        return new TrainingModule(subjectId, periods);
    }

    public TrainingModule Create(ITrainingModuleVisitor trainingModuleVisitor)
    {
        return new TrainingModule(trainingModuleVisitor.Id, trainingModuleVisitor.Periods);
    }
}