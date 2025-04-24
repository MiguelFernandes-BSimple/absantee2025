using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface ITrainingSubjectFactory
{
    Task<TrainingSubject> Create(string subject, string description);
    TrainingSubject Create(ITrainingSubjectVisitor trainingSubjectVisitor);
}

