using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public interface ITrainingModuleFactory
    {
        Task<TrainingModule> Create(Guid traingSubjectId, List<PeriodDateTime> periods);
        TrainingModule Create(ITrainingModuleVisitor trainingModuleVisitor);
    }
}
