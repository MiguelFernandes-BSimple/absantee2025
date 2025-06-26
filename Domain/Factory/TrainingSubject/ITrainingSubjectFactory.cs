using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface ITrainingSubjectFactory
{
    Task<TrainingSubject> Create(Guid id, string subject, string description);
    TrainingSubject Create(ITrainingSubjectVisitor trainingSubjectVisitor);
}

