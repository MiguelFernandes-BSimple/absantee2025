using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface ITrainingModuleFactory
{
    public Task<TrainingModule> Create(long trainingSubjectId, List<PeriodDateTime> periods);
    public TrainingModule Create(ITrainingModuleVisitor trainingModuleVisitor);
}