using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface ITrainingModuleFactory
{
    public TrainingModule Create(long subjectId, List<PeriodDateTime> periods);
    public TrainingModule Create(ITrainingModuleVisitor trainingModuleVisitor);

}