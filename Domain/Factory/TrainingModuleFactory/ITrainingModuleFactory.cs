using Domain.Interfaces;
using Domain.Models;

namespace Domain.Factory;

public interface ITrainingModuleFactory {
    Task<TrainingModule> Create(long subjectId, List<PeriodDateTime> periods);
    //TrainingModule Create(ITrainingModuleVisitor visitor);
}
