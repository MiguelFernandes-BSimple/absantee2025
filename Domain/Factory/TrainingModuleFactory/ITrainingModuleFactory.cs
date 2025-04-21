using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public interface ITrainingModuleFactory
{
    TrainingModule Create(long subjectId, List<PeriodDateTime> periodsList);
    TrainingModule Create(ITrainingModuleVisitor visitor);
}